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
using ViewModels.Helpers;

namespace pmo.Controllers
{
    [Route("vbpd-projects/{projectId}/team-members")]
    public class TeamMembersController : BaseProjectDetailController {
        private readonly IListService _listService;
        private readonly string path = "~/Views/VBPD/Application/TeamMembers";

        public TeamMembersController(EfContext context, IMapper mapper, IListService listService, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
            _listService = listService;
        }
        [Route("edit")]
        public IActionResult Edit(int projectId)
        {
            if (!ProjectExists(projectId))
                return NotFound();

            var allTeamMembers = _context.Project_User.Where(w => w.ProjectId == projectId).ToList();
            var teamMembersViewModel = new forms.TeamMembersForm {
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
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(forms.TeamMembersForm teamMembersViewModel, int projectId) {
            if (!ProjectExists(projectId))
                return NotFound();

            if (!ModelState.IsValid) {
                SetDropdowns(teamMembersViewModel);

                // append errors
                ViewBag.Errors = ModelState;
                return View($"{path}/Edit.cshtml", teamMembersViewModel);
            }

            using (var transaction = _context.Database.BeginTransaction()) {
                try {
                    _context.Project_User.RemoveRange(_context.Project_User.Where(r => r.ProjectId == projectId));
                    _context.SaveChanges();

                    if (teamMembersViewModel.Program_Manager != null) PopulateTeamMember(teamMembersViewModel.Program_Manager, JobDescripKeys.ProgramManager, projectId);
                    if (teamMembersViewModel.Product_Manager != null) PopulateTeamMember(teamMembersViewModel.Product_Manager, JobDescripKeys.ProductManager, projectId);
                    if (teamMembersViewModel.Lead_Engineer != null) PopulateTeamMember(teamMembersViewModel.Lead_Engineer, JobDescripKeys.LeadEngineer, projectId);
                    if (teamMembersViewModel.Program_Management != null) PopulateTeamMember(teamMembersViewModel.Program_Management, JobDescripKeys.ProgramManagement, projectId);
                    if (teamMembersViewModel.Product_Engineering != null) PopulateTeamMember(teamMembersViewModel.Product_Engineering, JobDescripKeys.ProductEngineering, projectId);
                    if (teamMembersViewModel.Advanced_Technology != null) PopulateTeamMember(teamMembersViewModel.Advanced_Technology, JobDescripKeys.AdvancedTechnology, projectId);
                    if (teamMembersViewModel.Sales != null) PopulateTeamMember(teamMembersViewModel.Sales, JobDescripKeys.Sales, projectId);
                    if (teamMembersViewModel.Industry_Segment != null) PopulateTeamMember(teamMembersViewModel.Industry_Segment, JobDescripKeys.IndustrySegment, projectId);
                    if (teamMembersViewModel.Operations != null) PopulateTeamMember(teamMembersViewModel.Operations, JobDescripKeys.Operations, projectId);
                    if (teamMembersViewModel.Manufacturing_Engineering != null) PopulateTeamMember(teamMembersViewModel.Manufacturing_Engineering, JobDescripKeys.ManufacturingEngineering, projectId);
                    if (teamMembersViewModel.Planning != null) PopulateTeamMember(teamMembersViewModel.Planning, JobDescripKeys.Planning, projectId);
                    if (teamMembersViewModel.Sourcing != null) PopulateTeamMember(teamMembersViewModel.Sourcing, JobDescripKeys.Sourcing, projectId);
                    if (teamMembersViewModel.Quality != null) PopulateTeamMember(teamMembersViewModel.Quality, JobDescripKeys.Quality, projectId);
                    if (teamMembersViewModel.Laboratory_Testing != null) PopulateTeamMember(teamMembersViewModel.Laboratory_Testing, JobDescripKeys.LaboratoryTesting, projectId);
                    if (teamMembersViewModel.Finance != null) PopulateTeamMember(teamMembersViewModel.Finance, JobDescripKeys.Finance, projectId);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception e) {
                    transaction.Rollback();
                }
            }

            return RedirectToAction("Edit");
        }

        private void SetDropdowns(forms.TeamMembersForm model) {
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

        private bool ProjectExists(int projectId)
        {
            return _context.Projects.Find(projectId) != null;
        }
    }
}