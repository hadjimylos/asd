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
    [Route("vbpd/{projectid}/stage/{stageId}/investment-plan")]   
    public class InvestmentPlanController : BaseController
    {
        private readonly string viewPath = "~/Views/Application/InvestmentPlan";
        public InvestmentPlanController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
        }

        [Route("{version}")]
        public IActionResult Detail(int stageId, int version)
        {
            var model = GetViewModel(stageId, version);
            return View($"{viewPath}/Detail.cshtml", model);
        }

        [Route("create-version")]
        public IActionResult CreateVersion(int stageId, int projectId)
        {
            var currentVersion = _context.InvestmentPlans
                .AsNoTracking()
                .Where(
                    w => w.StageId == stageId
                ).Max(m => m.Version);

            var model = new CreateVersionViewModel
            {
                BackPath = $"/vbpd/{projectId}/stage/{stageId}/investment-plan/{currentVersion}",
                PostPath = $"/vbpd/{projectId}/stage/{stageId}/investment-plan/create-version",
                ComponentName = "Investment Plan",
                CurrentVersion = currentVersion,
            };

            return View($"{viewPath}/CreateVersion.cshtml", model);
        }

        [HttpPost]
        [Route("create-version")]
        [AutoValidateAntiforgeryToken]
        public IActionResult PostCreateVerison(int stageId)
        {
            // get latest transaction of latest version
            var latestRecord = _context.InvestmentPlans
                .AsNoTracking()
                .Where(w => w.StageId == stageId)
                .OrderByDescending(o => o.CreateDate)
                .FirstOrDefault();

            // NOTE!!!!!!!!: here please check to see if first record. If so redirect to edit only. (not necessary for Project Detail specifically)
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
                    _context.Add(latestRecord);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }
                return RedirectToAction("Edit", new { stageId });
            }
        }

        [Route("edit")]
        public IActionResult Edit(int stageId)
        {
            // always populate latest version in edit
            //Tha skasei ama einai 0
            var currentVersion = _context.InvestmentPlans
                 .AsNoTracking()
                 .Where(w => w.StageId == stageId)
                 .OrderByDescending(c => c.CreateDate)
                 .FirstOrDefault();

            if (currentVersion == null)
            {
                var vm = new InvestmentPlanViewModel()
                {
                    StageId = stageId,
                    Versions = new List<InvestmentPlanViewModel>(),
                    Stage = _context.Stages.Where(s => s.Id == stageId).FirstOrDefault()
                };

                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var model = GetViewModel(stageId, currentVersion.Version);
            model.Versions = GetVersionHistory(stageId);
            return View($"{viewPath}/Edit.cshtml", model);
        }
        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(InvestmentPlanViewModel vm, int stageId)
        {
            var latestInvestmentPlan = _context.InvestmentPlans.Where(
                  w => w.StageId == stageId
              ).OrderByDescending(o => o.CreateDate)
              .FirstOrDefault();

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = _context.Stages.Where(s => s.Id == stageId).FirstOrDefault();
                vm.Versions = GetVersionHistory(stageId);
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
            //There is already a previous version
            else
            {
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestInvestmentPlan.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate)
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            investmentPlan.Version = latestInvestmentPlan.Version;
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
                else
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            investmentPlan.Version = latestInvestmentPlan.Version;
                            // save to primary table
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
            }

            return RedirectToAction("Detail", new { stageId, version = investmentPlan.Version });
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