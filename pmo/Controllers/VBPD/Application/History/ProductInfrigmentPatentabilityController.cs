using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModels.Helpers;


namespace pmo.Controllers {
    [Route("vbpd-projects/{projectid}/stages/{stageNumber}/product-infrigment-patentability")]
    public class ProductInfrigmentPatentabilityController : BaseStageComponentController {
        private readonly string viewPath = "~/Views/VBPD/Application/ProductInfrigmentPatentability";

        public ProductInfrigmentPatentabilityController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
        }

        [Route("{version}")]
        public IActionResult Detail( int version)
        {
            var model = GetViewModel(version);
            return View($"{viewPath}/Detail.cshtml", model);
        }

        [Route("edit")]
        public IActionResult Edit()
        {
            var currentVersion = _context.ProductInfrigmentPatentabilities
                 .AsNoTracking().GetLatestVersion(_projectId);
            var currentStage = _context.Stages.First(s=>s.Id==_stageId);
            ViewBag.CurrentStageNumber = currentStage.StageNumber;

            if (currentVersion == null)
            {
                var vm = new forms.ProductInfrigmentPatentabilityForm()
                {
                    StageId = currentStage.Id,
                    Versions = GetVersionHistory(),
                    Stage = currentStage
                };
                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var model = GetViewModel(currentVersion.Version);
            model.Versions = GetVersionHistory();
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(forms.ProductInfrigmentPatentabilityForm vm)
        {
            int currentVersion = 0;
            var currentStage = _context.Stages.First(s=>s.Id   == _stageId);
            var latestProductInfrigmentPatentability = _context.ProductInfrigmentPatentabilities
                 .GetLatestVersion(_projectId);
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = currentStage;
                vm.Versions = GetVersionHistory();
                vm.Version = latestProductInfrigmentPatentability == null ? 0 : latestProductInfrigmentPatentability.Version;
                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var productInfrigmentPatentability = _mapper.Map<ProductInfrigmentPatentability>(vm);
            productInfrigmentPatentability.StageId = currentStage.Id;
            if (latestProductInfrigmentPatentability == null)
            {

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        currentVersion = 1;
                        productInfrigmentPatentability.Version = 1;
                        _context.ProductInfrigmentPatentabilities.Add(productInfrigmentPatentability);
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
            else //There is already a previous version
            {
                currentVersion = latestProductInfrigmentPatentability.Version;
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestProductInfrigmentPatentability.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate && currentStage.StageNumber == latestProductInfrigmentPatentability.Stage.StageNumber)//if same user and sameStage then update
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                           latestProductInfrigmentPatentability.ContainsFeaturesRequiringIPProtection = productInfrigmentPatentability.ContainsFeaturesRequiringIPProtection;
                           latestProductInfrigmentPatentability.ContainsInfingmentIssues = productInfrigmentPatentability.ContainsInfingmentIssues;
                           latestProductInfrigmentPatentability.InventionDisclosureSubmitted = productInfrigmentPatentability.InventionDisclosureSubmitted;
                           latestProductInfrigmentPatentability.Issue = productInfrigmentPatentability.Issue;
                           latestProductInfrigmentPatentability.MitigationStrategy = productInfrigmentPatentability.MitigationStrategy;
                           latestProductInfrigmentPatentability.PatentNumber = productInfrigmentPatentability.PatentNumber;
                           latestProductInfrigmentPatentability.ProductFirstTimeOfferedForSale = productInfrigmentPatentability.ProductFirstTimeOfferedForSale;
                            _context.ProductInfrigmentPatentabilities.Update(latestProductInfrigmentPatentability);
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
                            productInfrigmentPatentability.Version = currentVersion+1;
                            _context.ProductInfrigmentPatentabilities.Add(productInfrigmentPatentability);
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

        private forms.ProductInfrigmentPatentabilityForm GetViewModel(int version)
        {
            var model = _context.ProductInfrigmentPatentabilities.Where(s => s.Version == version).GetLatestVersion(_projectId);
            var vm = _mapper.Map<forms.ProductInfrigmentPatentabilityForm>(model);
            return vm;
        }
        private List<forms.ProductInfrigmentPatentabilityForm> GetVersionHistory()
        {
            var grouped = _context.ProductInfrigmentPatentabilities
                .Include(s => s.Stage)
                .Where(i => i.Stage.ProjectId == _projectId)
                .ToList()
                .GroupBy(g => g.Version)
                .ToList();

            if (grouped.Count == 0)
            {
                return new List<forms.ProductInfrigmentPatentabilityForm>();
            }

            List<ProductInfrigmentPatentability> versions = new List<ProductInfrigmentPatentability>();
            grouped.ForEach(group => versions.Add(group.OrderByDescending(o => o.CreateDate).First()));

            return _mapper.Map<List<forms.ProductInfrigmentPatentabilityForm>>(versions);
        }

        private ProductIntroChecklist GetDBModel(int version)
        {
            return _context.ProductIntroChecklists.Where(s => s.Version == version).GetLatestVersion(_projectId);
        }

    }
}