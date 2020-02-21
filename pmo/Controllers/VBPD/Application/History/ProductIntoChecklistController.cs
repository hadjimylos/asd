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


namespace pmo.Controllers.VBPD.Application.History
{
    [Route("vbpd-projects/{projectid}/stages/{stageNumber}/product-intro-checklist")]
    public class ProductIntoChecklistController : BaseStageComponentController {

        private readonly string viewPath = "~/Views/VBPD/Application/ProductIntroChecklist";

        public ProductIntoChecklistController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
        }

        [Route("{version}")]
        public IActionResult Detail(int version)
        {
            var model = GetViewModel(version);
            return View($"{viewPath}/Detail.cshtml", model);
        }

        
        [Route("edit")]
        public IActionResult Edit()
        {
            ViewBag.StageNumber = _stageNumber;
            ViewBag.ProjectId = _projectId;
            var latestVersion = _context.ProductIntroChecklists
                 .AsNoTracking().GetLatestVersion(_projectId);
            var currentStage = _context.Stages.First(s=>s.Id == _stageId);

            if (latestVersion == null)
            {
                var vm = new ProductIntroChecklistViewModel()
                {
                    StageId = currentStage.Id,
                    Versions = GetVersionHistory(),
                    Stage = currentStage
                };

                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var model = GetViewModel( latestVersion.Version);
            model.Versions = GetVersionHistory();
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(ProductIntroChecklistViewModel vm)
        {
            int currentVersion = 0;
            var latestProductIntoChecklist = _context.ProductIntroChecklists.GetLatestVersion(_projectId);
            var currentStage = _context.Stages.First(s=>s.Id == _stageId);
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = _context.Stages.Where(s => s.Id == currentStage.Id).FirstOrDefault();
                vm.Versions = GetVersionHistory();
                vm.Version = latestProductIntoChecklist == null ? 0 : latestProductIntoChecklist.Version;
                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var productIntroChecklist = _mapper.Map<ProductIntroChecklist>(vm);
            productIntroChecklist.StageId = currentStage.Id;
            if (latestProductIntoChecklist == null)//first version
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        productIntroChecklist.Version = 1;
                        currentVersion = 1;
                        _context.ProductIntroChecklists.Add(productIntroChecklist);
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
                currentVersion = latestProductIntoChecklist.Version;
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestProductIntoChecklist.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate && currentStage.StageNumber == latestProductIntoChecklist.Stage.StageNumber)//if same user and sameStage then update
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            latestProductIntoChecklist.ApprovedBy = productIntroChecklist.ApprovedBy;
                            latestProductIntoChecklist.ApprovedByDate = productIntroChecklist.ApprovedByDate;
                            latestProductIntoChecklist.Filename = productIntroChecklist.Filename;
                            latestProductIntoChecklist.IsMarketingRequired = productIntroChecklist.IsMarketingRequired;
                            _context.ProductIntroChecklists.Update(latestProductIntoChecklist);
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
                            productIntroChecklist.Version = currentVersion+=1;
                            _context.ProductIntroChecklists.Add(productIntroChecklist);
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

        private ProductIntroChecklistViewModel GetViewModel(int version)
        {
            var model = _context.ProductIntroChecklists.Where(s => s.Version == version).GetLatestVersion(_projectId);
            var vm = _mapper.Map<ProductIntroChecklistViewModel>(model);
            return vm;
        }

        private List<ProductIntroChecklistViewModel> GetVersionHistory()
        {
            var grouped = _context.ProductIntroChecklists
                .Include(s => s.Stage)
                .Where(i => i.Stage.ProjectId == _projectId)
                .ToList()
                .GroupBy(g => g.Version)
                .ToList();

            if (grouped.Count == 0) { return new List<ProductIntroChecklistViewModel>(); }

            List<ProductIntroChecklist> versions = new List<ProductIntroChecklist>();
            grouped.ForEach(group => versions.Add(group.OrderByDescending(o => o.CreateDate).First()));
            return _mapper.Map<List<ProductIntroChecklistViewModel>>(versions);
        }
    }
}