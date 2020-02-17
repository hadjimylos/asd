using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

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
            var model = GetViewModel(_stageId, version);
            return View($"{viewPath}/Detail.cshtml", model);
        }

        [Route("create-version")]
        public IActionResult CreateVersion(int projectId, int stageNumber)
        {
            var currentVersion = _context.ProductIntroChecklists
                .AsNoTracking()
                .Include(s => s.Stage)
                .Where(n => n.StageId == _stageId)
                .Max(m => m.Version);

            var model = new CreateVersionViewModel
            {
                BackPath = $"/vbpd-projects/{projectId}/stages/{stageNumber}/product-intro-checklist/{currentVersion}",
                PostPath = $"/vbpd-projects/{projectId}/stages/{stageNumber}/product-intro-checklist/create-version",
                ComponentName = "Product Intro Checklist",
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
            var latestRecord = _context.ProductIntroChecklists
                .Include(s => s.Stage)
                .Where(n => n.StageId == _stageId)
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
                return RedirectToAction("Edit", new { projectId, stageNumber });
            }
        }

        [Route("edit")]
        public IActionResult Edit()
        {
            // always populate latest version in edit
            var currentVersion = _context.ProductIntroChecklists
                 .AsNoTracking()
                 .Include(s => s.Stage)
                 .Where(n => n.StageId == _stageId)
                 .OrderByDescending(c => c.CreateDate)
                 .FirstOrDefault();
            var currentStage = _context.Stages.First(s=>s.Id == _stageId);

            if (currentVersion == null)
            {
                var vm = new ProductIntroChecklistViewModel()
                {
                    StageId = currentStage.Id,
                    Versions = new List<ProductIntroChecklistViewModel>(),
                    Stage = currentStage
                };

                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var model = GetViewModel(_stageId, currentVersion.Version);
            model.Versions = GetVersionHistory(currentStage.Id);
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(ProductIntroChecklistViewModel vm, int projectId, int stageNumber)
        {
            int currentVersion = 0;
            var latestProductIntoChecklist = _context.ProductIntroChecklists
               .Include(s => s.Stage)
               .Where(n => n.StageId == _stageId)
               .OrderByDescending(o => o.CreateDate)
               .FirstOrDefault();
            var stage = _context.Stages.First(s=>s.Id == _stageId);
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = _context.Stages.Where(s => s.Id == stage.Id).FirstOrDefault();
                vm.Versions = GetVersionHistory(stage.Id);
                vm.Version = latestProductIntoChecklist == null ? 0 : latestProductIntoChecklist.Version;
                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var productIntroChecklist = _mapper.Map<ProductIntroChecklist>(vm);
            productIntroChecklist.StageId = _stageId;
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
                if (isUpdate)//same user  trying to edit 
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
                            productIntroChecklist.Version = currentVersion;
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

            return RedirectToAction("Detail", new { projectId, stageNumber, version = currentVersion });
        }

        private ProductIntroChecklistViewModel GetViewModel(int stageId, int version)
        {
            var model = _context.ProductIntroChecklists.Where(
                s => s.StageId == stageId && s.Version == version
            ).OrderByDescending(o => o.CreateDate)
            .Include(s => s.Stage)
            .FirstOrDefault();

            var vm = _mapper.Map<ProductIntroChecklistViewModel>(model);

            return vm;
        }

        private List<ProductIntroChecklistViewModel> GetVersionHistory(int stageId)
        {
            var grouped = _context.ProductIntroChecklists
                .Where(w => w.StageId == stageId)
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