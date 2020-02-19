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

namespace pmo.Controllers {
    [Route("vbpd-projects")]
    public class VBPDController : BaseController {
        private readonly IProjectService _projectService;
        private readonly IListService _listService;
        private readonly string path = "~/Views/VBPD/Application/VBPD";

        public VBPDController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IProjectService projectService, IListService listService) : base(context, mapper, httpContextAccessor) {
            _projectService = projectService;
            _listService = listService;
        }

        public IActionResult Index() {
            var projects = _projectService.GetAllVBPDProjectDetailList();
            var vm = _mapper.Map<List<VBPDViewModel>>(projects);
            return View($"{path}/Index.cshtml", vm);
        }

        [Route("create")]
        public IActionResult Create() {
            var project = new VBPDViewModel();
            project.ProjectCategoryTagDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProjectCategory);
            project.ProductLineDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProductLine);
            project.ProjectClassificationDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProjectClassification);
            project.DesignAuthorityDropDown = _listService.Tags_SelectList(TagCategoryHelper.DesignAuthority);
            project.CustomerDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.Customer);
            project.SalesRegionsDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.SalesRegion);
            project.ExportApplicationTypeDropDown = _listService.Tags_SelectList(TagCategoryHelper.ExportApplicationType);

            return View($"{path}/Create.cshtml", project);
        }

        [HttpPost]
        [Route("create")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(VBPDViewModel model) {
            if (!ModelState.IsValid) {

                ViewBag.Errors = ModelState;
                model.ProjectCategoryTagDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProjectCategory, model.ProjectCategoryTagId.ToString());
                model.ProductLineDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProductLine, model.ProjectCategoryTagId.ToString());
                model.ProjectClassificationDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProjectClassification, model.ProjectCategoryTagId.ToString());
                model.DesignAuthorityDropDown = _listService.Tags_SelectList(TagCategoryHelper.DesignAuthority, model.ProjectCategoryTagId.ToString());
                model.CustomerDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.Customer, model.Customers);
                model.SalesRegionsDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.SalesRegion, model.SalesRegions);
                model.ExportApplicationTypeDropDown = _listService.Tags_SelectList(TagCategoryHelper.ExportApplicationType, model.ExportApplicationTypeTagId.ToString());

                return View($"{path}/Create.cshtml", model);
            }

            _projectService.AddNewVBPDProject(model);
            return RedirectToAction("Index");
        }

        [Route("open")]
        public IActionResult Open()
        {
            var _projectDets = new List<List<ProjectDetail>>();
            var projects = _projectService.GetAllVBPDOpenProjectDetailList("Go");
            foreach (var projectDetail in projects)
            {
                _projectDets.Add(projectDetail.ProjectDetail);   
            }
            var openProjects = _projectDets.SelectMany(x => x).ToList();
            var vm = _mapper.Map<List<VBPDViewModel>>(openProjects);
            return View($"{path}/Index.cshtml", vm);
        }

        [Route("on-hold")]
        public IActionResult Hold()
        {
            var _projectDets = new List<List<ProjectDetail>>();
            var projects = _projectService.GetAllVBPDOpenProjectDetailList("OnHold");
            foreach (var projectDetail in projects)
            {
                _projectDets.Add(projectDetail.ProjectDetail);
            }
            var openProjects = _projectDets.SelectMany(x => x).ToList();
            var vm = _mapper.Map<List<VBPDViewModel>>(openProjects);
            return View($"{path}/Index.cshtml", vm);
        }

        [Route("closed")]
        public IActionResult Closed()
        {
            var _projectDets = new List<List<ProjectDetail>>();
            var projects = _projectService.GetAllVBPDOpenProjectDetailList("Closed");
            foreach (var projectDetail in projects)
            {
                _projectDets.Add(projectDetail.ProjectDetail);
            }
            var openProjects = _projectDets.SelectMany(x => x).ToList();
            var vm = _mapper.Map<List<VBPDViewModel>>(openProjects);
            return View($"{path}/Index.cshtml", vm);
        }
    }
}