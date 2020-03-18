using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pmo.Services.Projects;
using System.Collections.Generic;
using ViewModels;
using pmo.Services.Lists;
using ViewModels.Helpers;
using dbModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using pmo.Services.Users;

namespace pmo.Controllers {
    [Route("")]
    public class VBPDController : BaseController {
        private readonly IProjectService _projectService;
        private readonly IListService _listService;
        private readonly IUserService _userService;
        private readonly string path = "~/Views/VBPD/Application/VBPD";


        public VBPDController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IProjectService projectService, IListService listService, IUserService userService) : base(context, mapper, httpContextAccessor) {
            _projectService = projectService;
            _listService = listService;
            _userService = userService;
        }

        public IActionResult Index() {
            var model = GetProjectDetails();
            ViewBag.History = _context.ProjectStateHistories.ToList();

            var projects = _context.Projects
                .Include(i => i.StageHistory)
                .Include(i => i.ProjectStateHistory)
                .Where(
                    w =>
                        w.ProjectStateHistory.OrderByDescending(o => o.CreateDate).First().ProjectState != ProjectState.Complete
                );

            var vbpdLiteProjects = projects.Where (
                    w =>
                        w.ProjectDetails.OrderByDescending(o => o.CreateDate).First().ProjectCategoryTagId == 219 // vbpd lite
                    ).ToList();

            var vbpdProjects = projects.Where(
                    w =>
                        w.ProjectDetails.OrderByDescending(o => o.CreateDate).First().ProjectCategoryTagId != 219 // vbpd
                    ).ToList();

            var vbpdActiveStages = vbpdProjects.Select(s => s.StageHistory.OrderByDescending(o => o.StageNumber).First()).ToList();
            var vbpdLiteActiveStages = vbpdLiteProjects.Select(s => s.StageHistory.OrderByDescending(o => o.StageNumber).First()).ToList();

            ViewBag.VbpdActiveStages = vbpdActiveStages;
            ViewBag.LiteActiveStages = vbpdLiteActiveStages;

            return View($"{path}/Index.cshtml", model);
        }

        [Route("create")]
        public IActionResult Create() {
            var project = new forms.VBPDForm();
            project.ProjectCategoryTagDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProjectCategory);
            project.ProductLineDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProductLine);
            project.ProjectClassificationDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProjectClassification);
            project.DesignAuthorityDropDown = _listService.Tags_SelectList(TagCategoryHelper.DesignAuthority);
            project.CustomerDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.Customer);
            project.SalesRegionsDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.SalesRegion);
            project.ExportApplicationTypeDropDown = _listService.Tags_SelectList(TagCategoryHelper.ExportApplicationType);
            project.ProcessDropDown = _listService.ProjectProcess();


            return View($"{path}/Create.cshtml", project);
        }

        [HttpPost]
        [Route("create")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(forms.VBPDForm model) {
            if (!ModelState.IsValid) {

                ViewBag.Errors = ModelState;
                model.ProjectCategoryTagDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProjectCategory, model.ProjectCategoryTagId.ToString());
                model.ProductLineDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProductLine, model.ProjectCategoryTagId.ToString());
                model.ProjectClassificationDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProjectClassification, model.ProjectCategoryTagId.ToString());
                model.DesignAuthorityDropDown = _listService.Tags_SelectList(TagCategoryHelper.DesignAuthority, model.ProjectCategoryTagId.ToString());
                model.CustomerDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.Customer, model.Customers);
                model.SalesRegionsDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.SalesRegion, model.SalesRegions);
                model.ExportApplicationTypeDropDown = _listService.Tags_SelectList(TagCategoryHelper.ExportApplicationType, model.ExportApplicationTypeTagId.ToString());
                model.ProcessDropDown = _listService.ProjectProcess();

                return View($"{path}/Create.cshtml", model);
            }

            var projectDetail = _mapper.Map<ProjectDetail>(model);
            var project = _mapper.Map<Project>(model);

            // save project
            _context.Projects.Add(project);
            _context.SaveChanges();

            // save project state
            _context.ProjectStateHistories.Add(new ProjectStateHistory {
                ProjectState = ProjectState.Go,
                ProjectId = project.Id,
            });
            _context.SaveChanges();

            projectDetail.ProjectId = project.Id;
            projectDetail.Version = 1;
            _context.ProjectDetails.Add(projectDetail);
            _context.SaveChanges();

            model.CustomerIds.ForEach(customerId => {
                _context.ProjectDetail_Customers.Add(new ProjectDetail_Customer {
                    CustomersTagId = customerId,
                    ProjectDetailId = projectDetail.Id,
                });
            });
            model.SalesRegionIds.ForEach(endUserCountryTagId => {
                _context.ProjectDetail_SalesRegions.Add(new ProjectDetail_SalesRegion {
                    SalesRegionTagId = endUserCountryTagId,
                    ProjectDetailId = projectDetail.Id,
                });
            });
            var teamMember = new Project_User() {
                ProjectId = project.Id,
                JobDescriptionKey = ViewModels.Helpers.JobDescripKeys.ProgramManager,
                UserId = _userService.GetCurrentUserId()
            };
            _context.Project_User.Add(teamMember);
            var stage = new Stage() //create new stage for new project
            {
                ProjectId = project.Id,
                StageNumber = 1
            };

            _context.ProjectStateHistories.Add(new ProjectStateHistory {
                ProjectId = project.Id,
                ProjectState = ProjectState.Go,
            });

            _context.Stages.Add(stage);
            _context.SaveChanges();    

            return Redirect($"/projects/{project.Id}");
        }

        [Route("open")]
        public IActionResult Open() {
            var model = GetProjectDetails(ProjectState.Go);
            return View($"{path}/Index.cshtml", model);
        }

        [Route("closed")]
        public IActionResult Closed() {
            var model = GetProjectDetails(ProjectState.Closed);
            return View($"{path}/Index.cshtml", model);
        }

        [Route("on-hold")]
        public IActionResult OnHold() {
            var model = GetProjectDetails(ProjectState.OnHold);
            return View($"{path}/Index.cshtml", model);
        }

        private IQueryable<ProjectDetail> GetQueryableProjectDetails() {
            var myId = _context.Users.First(f => f.NetworkUsername == _httpContextAccessor.HttpContext.User.Identity.Name).Id;
            return _context.ProjectDetails
                .IncludeAll()
                .Include(i => i.Project)
                    .ThenInclude(i => i.ProjectStateHistory)
                .Include(i => i.Project)
                    .ThenInclude(i => i.StageHistory)
                .Include(i => i.Project)
                        .ThenInclude(i => i.GateHistory);
        }

        private List<ProjectDetail> GetProjectDetails(ProjectState state) {
            return GetQueryableProjectDetails().Where(
                    w =>
                        w.Project.ProjectStateHistory.OrderByDescending(o => o.CreateDate).First().ProjectState == state
                ).DistinctProjectDetail();
        }

        private List<ProjectDetail> GetProjectDetails()
        {
            var query = GetQueryableProjectDetails();
            var test =  query.DistinctProjectDetail();
            return test;
        }

    }
}