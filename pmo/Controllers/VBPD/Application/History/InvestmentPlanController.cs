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
    [Route("projects/{projectid}/stages/{stageNumber}/investment-plan")]
    public class InvestmentPlanController : BaseStageComponentController {
        private readonly string viewPath = "~/Views/VBPD/Application/InvestmentPlan";
        public InvestmentPlanController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
        }

        [Route("{version}")]
        public IActionResult Detail(int version)
        {
            var model = GetDBModel(version);
            return View($"{viewPath}/Detail.cshtml", model);
        }


        [Route("edit")]
        public IActionResult Edit()
        {
            // always populate latest version in edit
            var latestSavedVersion = _context.InvestmentPlans.AsNoTracking().GetLatestVersion(_projectId);
            var currentStage = _context.Stages.First(n => n.Id == _stageId);
            ViewBag.CurrentStageNumber = currentStage.StageNumber;
            if (latestSavedVersion == null)
            {
                var vm = new forms.InvestmentPlanForm()
                {
                    StageId = currentStage.Id,
                    Versions = GetVersionHistory(),
                    Stage = currentStage
                };

                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var model = GetViewModel(latestSavedVersion.Version);
            model.Versions = GetVersionHistory();
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(forms.InvestmentPlanForm vm)
        {
            int currentVersion = 0;
            var currentStage = _context.Stages.Where(n => n.Id == _stageId).First();
            var latestInvestmentPlan = _context.InvestmentPlans.GetLatestVersion(_projectId);
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

            return this._editAction;
        }

        private forms.InvestmentPlanForm GetViewModel(int version)
        {
            var model = _context.InvestmentPlans.Where(s => s.Version == version).GetLatestVersion(_projectId);
            var vm = _mapper.Map<forms.InvestmentPlanForm>(model);

            return vm;
        }

        private List<forms.InvestmentPlanForm> GetVersionHistory()
        {
            var grouped = _context.InvestmentPlans
                .Include(s => s.Stage)
                .Where(i => i.Stage.ProjectId == _projectId)
                .ToList()
                .GroupBy(g => g.Version)
                .ToList();

            if (grouped.Count == 0) { return new List<forms.InvestmentPlanForm>(); }

            List<InvestmentPlan> versions = new List<InvestmentPlan>();
            grouped.ForEach(group => versions.Add(group.OrderByDescending(o => o.CreateDate).First()));
            return _mapper.Map<List<forms.InvestmentPlanForm>>(versions);
        }

        private InvestmentPlan GetDBModel(int version)
        {
            return _context.InvestmentPlans.Where(s => s.Version == version).GetLatestVersion(_projectId);
        }
    }
}