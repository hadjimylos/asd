using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pmo.Services.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using ViewModels.Helpers;

namespace pmo.Controllers
{
    [Route("vbpd-projects/{id}/team-members")]
    public class TeamMembersController : BaseController
    {
        private readonly IListService _listService;
        private readonly string path = "~/Views/VBPD/Application/TeamMembers";

        public TeamMembersController(EfContext context, IMapper mapper, IListService listService, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
            _listService = listService;
        }

        public IActionResult Edit(int id)
        {
            if (!ProjectExists(id))
                return NotFound();

            ViewBag.ProjectId = id;
            var allTeamMembers = _context.Project_User.Where(w => w.ProjectId == id).ToList();
            var teamMembersViewModel = new TeamMembersViewModel {
                // populate selected list items
                Program_Manager = GetPopulated(allTeamMembers, JobDescripKeys.ProgramManager),
                Product_Manager = GetPopulated(allTeamMembers, JobDescripKeys.ProductManager),
                Lead_Engineer = GetPopulated(allTeamMembers, JobDescripKeys.LeadEngineer),
                Program_Management = GetPopulated(allTeamMembers, JobDescripKeys.ProgramManagement),
                Product_Engineering = GetPopulated(allTeamMembers, JobDescripKeys.ProductEngineering),
                Advanced_Technology = GetPopulated(allTeamMembers, JobDescripKeys.AdvancedTechnology),
                Sales = GetPopulated(allTeamMembers, JobDescripKeys.Sales),
                Industry_Segment = GetPopulated(allTeamMembers, JobDescripKeys.IndustrySegment),
                Operations = GetPopulated(allTeamMembers, JobDescripKeys.Operations),
                Manufacturing_Engineering = GetPopulated(allTeamMembers, JobDescripKeys.ManufacturingEngineering),
                Planning = GetPopulated(allTeamMembers, JobDescripKeys.Planning),
                Sourcing = GetPopulated(allTeamMembers, JobDescripKeys.Sourcing),
                Quality = GetPopulated(allTeamMembers, JobDescripKeys.Quality),
                Laboratory_Testing = GetPopulated(allTeamMembers, JobDescripKeys.LaboratoryTesting),
                Finance = GetPopulated(allTeamMembers, JobDescripKeys.Finance),
            };

            SetDropdowns(teamMembersViewModel);

            return View($"{path}/Edit.cshtml", teamMembersViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        
        public IActionResult Edit(TeamMembersViewModel teamMembersViewModel, int id) {
            if (!ProjectExists(id))
                return NotFound();

            ViewBag.ProjectId = id;

            if (!ModelState.IsValid) {
                SetDropdowns(teamMembersViewModel);

                // append errors
                ViewBag.Errors = ModelState;
                return View($"{path}/Edit.cshtml", teamMembersViewModel);
            }

            using (var transaction = _context.Database.BeginTransaction()) {
                try {
                    _context.Project_User.RemoveRange(_context.Project_User.Where(r => r.ProjectId == id));
                    _context.SaveChanges();

                    PopulateTeamMember(teamMembersViewModel.Program_Manager, JobDescripKeys.ProgramManager, id);
                    PopulateTeamMember(teamMembersViewModel.Product_Manager, JobDescripKeys.ProductManager, id);
                    PopulateTeamMember(teamMembersViewModel.Lead_Engineer, JobDescripKeys.LeadEngineer, id);
                    PopulateTeamMember(teamMembersViewModel.Program_Management, JobDescripKeys.ProgramManagement, id);
                    PopulateTeamMember(teamMembersViewModel.Product_Engineering, JobDescripKeys.ProductEngineering, id);
                    PopulateTeamMember(teamMembersViewModel.Advanced_Technology, JobDescripKeys.AdvancedTechnology, id);
                    PopulateTeamMember(teamMembersViewModel.Sales, JobDescripKeys.Sales, id);
                    PopulateTeamMember(teamMembersViewModel.Industry_Segment, JobDescripKeys.IndustrySegment, id);
                    PopulateTeamMember(teamMembersViewModel.Operations, JobDescripKeys.Operations, id);
                    PopulateTeamMember(teamMembersViewModel.Manufacturing_Engineering, JobDescripKeys.ManufacturingEngineering, id);
                    PopulateTeamMember(teamMembersViewModel.Planning, JobDescripKeys.Planning, id);
                    PopulateTeamMember(teamMembersViewModel.Sourcing, JobDescripKeys.Sourcing, id);
                    PopulateTeamMember(teamMembersViewModel.Quality, JobDescripKeys.Quality, id);
                    PopulateTeamMember(teamMembersViewModel.Laboratory_Testing, JobDescripKeys.LaboratoryTesting, id);
                    PopulateTeamMember(teamMembersViewModel.Finance, JobDescripKeys.Finance, id);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception e) {
                    transaction.Rollback();
                }
            }

            return RedirectToAction(actionName: "Index", controllerName: "VBPD");
        }

        private void SetDropdowns(TeamMembersViewModel model) {
            var selectList = _context.Users.AsNoTracking().Select(s => new SelectListItem {
                Text = s.NetworkUsername,
                Value = s.Id.ToString(),
            }).ToList();

            // initialize all dropdowns with nothing selected
            model.ProgramManagerList = selectList;
            model.ProductManagerList = selectList;
            model.LeadEngineerList = selectList;
            model.ProgramManagementList = selectList;
            model.ProductEngineeringList = selectList;
            model.AdvancedTechnologyList = selectList;
            model.SalesList = selectList;
            model.IndustrySegment = selectList;
            model.OperationsList = selectList;
            model.ManufacturingEngineeringList = selectList;
            model.PlanningList = selectList;
            model.SourcingList = selectList;
            model.QualityList = selectList;
            model.LaboratoryTestingList = selectList;
            model.FinanceList = selectList;
        }

        private List<int> GetPopulated(List<Project_User> users, string jobKey)
        {
            return users.Where(
                w => w.JobDescriptionKey == jobKey
            ).Select(s => s.UserId).ToList();
        }

        private void PopulateTeamMember(List<int> userIds, string jobKey, int projectId) {
            userIds.ForEach(managerId => {
                _context.Project_User.Add(new Project_User {
                    ProjectId = projectId,
                    UserId = managerId,
                    JobDescriptionKey = jobKey,
                });
            });

        }

        private bool ProjectExists(int id)
        {
            return _context.Projects.Find(id) != null;
        }
    }
}