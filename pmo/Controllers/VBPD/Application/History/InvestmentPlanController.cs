using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace pmo.Controllers.Application.History
{
    [Route("vbpd-projects/{projectid}/stages/{stageNumber}/investment-plan")]   
    public class InvestmentPlanController : BaseProjectDetailController
    {
        private readonly string viewPath = "~/Views/VBPD/Application/InvestmentPlan";
        public InvestmentPlanController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
        }

        [Route("{version}")]
        public IActionResult Detail(int projectId , int stageNumber, int version)
        {
            var stageId = _context.Stages.Where(s => s.StageNumber == stageNumber && s.ProjectId == projectId).First().Id;
            var model = GetViewModel(stageId, version);
            return View($"{viewPath}/Detail.cshtml", model);
        }

        [Route("create-version")]
        public IActionResult CreateVersion(int projectId, int stageNumber)
        {
            var currentVersion = _context.InvestmentPlans
                .AsNoTracking()
                .Include(s => s.Stage)
                .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
                .Max(m => m.Version);

            var model = new CreateVersionViewModel
            {
                BackPath = $"/vbpd-projects/{projectId}/stage/{stageNumber}/investment-plan/{currentVersion}",
                PostPath = $"/vbpd-projects/{projectId}/stage/{stageNumber}/investment-plan/create-version",
                ComponentName = "Investment Plan",
                CurrentVersion = currentVersion,
            };

            return View($"{viewPath}/CreateVersion.cshtml", model);
        }

        [HttpPost]
        [Route("create-version")]
        [AutoValidateAntiforgeryToken]
        public IActionResult PostCreateVerison(int projectId, int stageNumber)
        {
            // get latest transaction of latest version
            var latestRecord = _context.InvestmentPlans
                .AsNoTracking()
                .Include(s => s.Stage)
                .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
                .OrderByDescending(o => o.CreateDate)
                .FirstOrDefault();

            if (latestRecord == null)
            {
                RedirectToAction("Edit");
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {

                    // set variables for create
                    latestRecord.Id = 0;
                    latestRecord.Version = ++latestRecord.Version;
                    latestRecord.StageId = latestRecord.Stage.Id;
                    latestRecord.Stage = null;//clear relation before saving 
                    _context.Add(latestRecord);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
                return RedirectToAction("Edit", new { projectId  , stageNumber });
            }
        }

        [Route("edit")]
        public IActionResult Edit(int projectId, int stageNumber)
        {
            // always populate latest version in edit
            var currentVersion = _context.InvestmentPlans
                 .AsNoTracking()
                 .Include(s => s.Stage)
                 .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
                 .OrderByDescending(c => c.CreateDate)
                 .FirstOrDefault();
            var currentStage = _context.Stages.Where(n => n.StageNumber == stageNumber && n.ProjectId == projectId).First();
            if (currentVersion == null)
            {
                var vm = new InvestmentPlanViewModel()
                {
                    StageId = currentStage.Id,
                    Versions = new List<InvestmentPlanViewModel>(),
                    Stage = currentStage
                };

                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var model = GetViewModel(currentStage.Id, currentVersion.Version);
            model.Versions = GetVersionHistory(currentStage.Id);
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(InvestmentPlanViewModel vm, int projectId, int stageNumber)
        {
            var latestInvestmentPlan = _context.InvestmentPlans.AsNoTracking()
               .Include(s => s.Stage)
               .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
               .OrderByDescending(o => o.CreateDate)
               .FirstOrDefault();
            var stage = _context.Stages.Where(n => n.StageNumber == stageNumber && n.ProjectId == projectId).First();
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = stage;
                vm.Versions = GetVersionHistory(stage.Id);
                vm.Version = latestInvestmentPlan == null ? 0 : latestInvestmentPlan.Version;
                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var investmentPlan = _mapper.Map<InvestmentPlan>(vm);
            //first version
            if (latestInvestmentPlan == null)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        investmentPlan.Version = 1;
                        investmentPlan.StageId = stage.Id;
                        _context.InvestmentPlans.Add(investmentPlan);
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
            else
            { //There is already a previous version
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestInvestmentPlan.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate)//same user trying trying to edit 
                {
                    investmentPlan.Version = latestInvestmentPlan.Version;
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            investmentPlan.Id = latestInvestmentPlan.Id;
                            investmentPlan.CreateDate = latestInvestmentPlan.CreateDate;//created date its the same
                            investmentPlan.StageId = stage.Id;
                            investmentPlan.Stage = null;
                            //TODO Upload Documentation as well
                            _context.InvestmentPlans.Update(investmentPlan);
                            _context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            throw e;
                        }
                    }
                }
                else// if not same user then add a new record to DB(transactions functionality)
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            investmentPlan.Id = 0;
                            investmentPlan.StageId = stage.Id;
                            investmentPlan.Stage = null;
                            _context.InvestmentPlans.Add(investmentPlan);
                            _context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            throw e;
                        }
                    }
                }
            }

            return RedirectToAction("Detail", new { projectId,  stageNumber, version = investmentPlan.Version });
        }
        private InvestmentPlanViewModel GetViewModel(int stageId, int version)
        {
            var model = _context.InvestmentPlans.Where(
                s => s.StageId == stageId && s.Version == version
            ).OrderByDescending(o => o.CreateDate)
            .Include(s => s.Stage)
            .FirstOrDefault();

            var vm = _mapper.Map<InvestmentPlanViewModel>(model);

            return vm;
        }

        private List<InvestmentPlanViewModel> GetVersionHistory(int stageId)
        {
            var grouped = _context.InvestmentPlans
                .Where(w => w.StageId == stageId)
                .ToList()
                .GroupBy(g => g.Version)
                .ToList();

            if (grouped.Count == 0) { return new List<InvestmentPlanViewModel>(); }
            List<InvestmentPlan> versions = new List<InvestmentPlan>();
            grouped.ForEach(group => versions.Add(group.OrderByDescending(o => o.CreateDate).First()));
            return _mapper.Map<List<InvestmentPlanViewModel>>(versions);
        }
    }
}