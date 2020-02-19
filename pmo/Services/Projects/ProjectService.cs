using AutoMapper;
using Microsoft.AspNetCore.Http;
using pmo.Controllers;
using dbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using ViewModels.Helpers;
using pmo.Services.Users;
using System;

namespace pmo.Services.Projects
{
    public class ProjectService : BaseController, IProjectService
    {
        private readonly IUserService _userService;
        public ProjectService(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserService userService) : base(context, mapper, httpContextAccessor)
        {
            _userService =  userService ;
        }
        public List<ProjectDetail> GetAllVBPDProjectDetailList()
        {
            var projects = _context.ProjectDetails.Include(p => p.Project)
                .Include(t => t.ProjectClassification)
                .Include(t => t.ProductLine)
                .Include(t => t.ProjectCategory)
                .Include(t => t.DesignAuthority).ToList();
            //for each project get only latest records from projectDetail table based on CreatedDate. 
            projects = projects.GroupBy(s => s.ProjectId)
            .Select(s => s.OrderByDescending(x => x.CreateDate).FirstOrDefault()).ToList();

            return projects;
        }
        public List<Project> GetAllVBPDOpenProjectDetailList(string option)
        {
            var open_projects = _context.ProjectStateHistories.IncludeAll()
                .Where(p => p.ProjectState == (ProjectState)Enum.Parse(typeof(ProjectState),option)   && p.ModifiedByUser == _httpContextAccessor.HttpContext.User.Identity.Name)
                .Select(p => p.Project).ToList();

            open_projects.ForEach(
                project => _context.ProjectDetails.Where(
                        p => p.ProjectId == project.Id)
                        .Include(p => p.Project)
                        .Include(t => t.ProjectClassification)
                        .Include(t => t.ProductLine)
                        .Include(t => t.ProjectCategory)
                        .Include(t => t.DesignAuthority)
                        .ToList());

            open_projects = open_projects.GroupBy(s => s.Id)
            .Select(s => s.OrderByDescending(x => x.CreateDate).FirstOrDefault()).ToList();




            return open_projects;
        }
        public void AddNewVBPDProject(VBPDViewModel model)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
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

                    model.CustomerIds.ForEach(customerId =>
                    {
                        _context.ProjectDetail_Customers.Add(new ProjectDetail_Customer
                        {
                            CustomersTagId = customerId,
                            ProjectDetailId = projectDetail.Id,
                        });
                    });
                    model.SalesRegionIds.ForEach(endUserCountryTagId =>
                    {
                        _context.ProjectDetail_SalesRegions.Add(new ProjectDetail_SalesRegion
                        {
                            SalesRegionTagId = endUserCountryTagId,
                            ProjectDetailId = projectDetail.Id,
                        });
                    });
                    var teamMember = new Project_User()
                    {
                        ProjectId = project.Id,
                        JobDescriptionKey = ViewModels.Helpers.JobDescripKeys.ProgramManagement,
                        UserId = _userService.GetCurrentUserId()
                    };
                    _context.Project_User.Add(teamMember);
                    var stage = new Stage() //create new stage for new project
                    {
                        ProjectId = project.Id,
                        StageNumber = 1
                    };
                    _context.Stages.Add(stage);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (System.Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}
