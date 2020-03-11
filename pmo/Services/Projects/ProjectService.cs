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
using dbModels.Report;

namespace pmo.Services.Projects
{
    public class ProjectService : BaseController, IProjectService
    {
        private readonly IUserService _userService;
        public ProjectService(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserService userService) : base(context, mapper, httpContextAccessor)
        {
            _userService = userService;
        }

        public void AddNewVBPDProject(forms.VBPDForm model)
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
                    _context.ProjectStateHistories.Add(new ProjectStateHistory
                    {
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
                        JobDescriptionKey = ViewModels.Helpers.JobDescripKeys.ProgramManager,
                        UserId = _userService.GetCurrentUserId()
                    };
                    _context.Project_User.Add(teamMember);
                    var stage = new Stage() //create new stage for new project
                    {
                        ProjectId = project.Id,
                        StageNumber = 1
                    };

                    _context.ProjectStateHistories.Add(new ProjectStateHistory
                    {
                        ProjectId = project.Id,
                        ProjectState = ProjectState.Go,
                    });

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