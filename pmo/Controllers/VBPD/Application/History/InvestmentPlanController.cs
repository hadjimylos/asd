using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using ViewModels.Helpers;


namespace pmo.Controllers.Application.History
{
    [Route("vbpd-projects/{projectid}/stages/{stageNumber}/investment-plan")]
    public class InvestmentPlanController : BaseStageComponentController {
        private readonly string viewPath = "~/Views/VBPD/Application/InvestmentPlan";
        public InvestmentPlanController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
        }

        [Route("{version}")]
        public IActionResult Detail(int version)
        {
            var model = GetViewModel(_stageId, version);
            return View($"{viewPath}/Detail.cshtml", model);
        }


        [Route("edit")]
        public IActionResult Edit(int projectId, int stageNumber)
        {
            ViewBag.StageNumber = _stageNumber;
            ViewBag.ProjectId = _projectId;
            // always populate latest version in edit
            var latestSavedVersion = _context.InvestmentPlans.AsNoTracking().GetLatestVersion(_projectId);
            var currentStage = _context.Stages.First(n => n.Id == _stageId);
            if (latestSavedVersion == null)
            {
                var vm = new InvestmentPlanViewModel()
                {
                    StageId = currentStage.Id,
                    Versions = GetVersionHistory(),
                    Stage = currentStage
                };

                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var model = GetViewModel(latestSavedVersion.Id, latestSavedVersion.Version);
            model.Versions = GetVersionHistory();
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(InvestmentPlanViewModel vm, int projectId, int stageNumber)
        {
            int currentVersion = 0;
            var currentStage = _context.Stages.Where(n => n.Id == _stageId).First();
            var latestInvestmentPlan = _context.InvestmentPlans.AsNoTracking().GetLatestVersion(_projectId);
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = currentStage;
                vm.Versions = GetVersionHistory();
                vm.Version = latestInvestmentPlan == null ? 0 : latestInvestmentPlan.Version;
                return View($"{viewPath}/Edit.cshtml", vm);
            }

            var investmentPlan = _mapper.Map<InvestmentPlan>(vm);
            investmentPlan.StageId = currentStage.Id;
            //first version
            if (latestInvestmentPlan == null)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        currentVersion = 1;
                        investmentPlan.Version = 1;
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
                currentVersion = latestInvestmentPlan.Version;
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestInvestmentPlan.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate && currentStage.StageNumber == latestInvestmentPlan.Stage.StageNumber)//if same user and sameStage then update
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            latestInvestmentPlan.Item = investmentPlan.Item;
                            latestInvestmentPlan.ItemNumber = investmentPlan.ItemNumber;
                            latestInvestmentPlan.PurchasedFrom = investmentPlan.PurchasedFrom;
                            latestInvestmentPlan.ShipToLocation = investmentPlan.ShipToLocation;
                            latestInvestmentPlan.Terms = investmentPlan.Terms;
                            latestInvestmentPlan.Cost = investmentPlan.Cost;
                            //TODO Upload Documentation as well
                            _context.InvestmentPlans.Update(latestInvestmentPlan);
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
                            investmentPlan.Version = currentVersion+=1;
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

            return RedirectToAction("Detail", new { version = currentVersion });
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

        private List<InvestmentPlanViewModel> GetVersionHistory()
        {
            var grouped = _context.InvestmentPlans
                .Include(s => s.Stage)
                .Where(i => i.Stage.ProjectId == _projectId)
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