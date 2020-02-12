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
    [Route("vbpd-projects/{projectid}/stage/{stageNumber}/product-intro-checklist")]
    public class ProductIntoChecklistController : BaseProjectDetailController
    {

        private readonly string viewPath = "~/Views/VBPD/Application/ProductIntroChecklist";

        public ProductIntoChecklistController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
        }

        [Route("{version}")]
        public IActionResult Detail(int projectId, int stageNumber, int version)
        {
            var stageId = _context.Stages.Where(s => s.StageNumber == stageNumber && s.ProjectId == projectId).First().Id;
            var model = GetViewModel(stageId, version);
            return View($"{viewPath}/Detail.cshtml", model);
        }

        [Route("create-version")]
        public IActionResult CreateVersion(int projectId, int stageNumber)
        {
            var currentVersion = _context.ProductIntroChecklists
                .AsNoTracking()
                .Include(s => s.Stage)
                .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
                .Max(m => m.Version);

            var model = new CreateVersionViewModel
            {
                BackPath = $"/vbpd-projects/{projectId}/stage/{stageNumber}/product-intro-checklist/{currentVersion}",
                PostPath = $"/vbpd-projects/{projectId}/stage/{stageNumber}/product-intro-checklist/create-version",
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
                    latestRecord.Id = 0;
                    latestRecord.Version = ++latestRecord.Version;
                    latestRecord.StageId = latestRecord.Stage.Id;
                    latestRecord.Stage = null;
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
        public IActionResult Edit(int projectId, int stageNumber)
        {
            // always populate latest version in edit
            var currentVersion = _context.ProductIntroChecklists
                 .AsNoTracking()
                 .Include(s => s.Stage)
                 .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
                 .OrderByDescending(c => c.CreateDate)
                 .FirstOrDefault();
            var currentStage = _context.Stages.Where(n => n.StageNumber == stageNumber && n.ProjectId == projectId).First();

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
            var model = GetViewModel(currentStage.Id, currentVersion.Version);
            model.Versions = GetVersionHistory(currentStage.Id);
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(ProductIntroChecklistViewModel vm, int projectId, int stageNumber)
        {
            var latestProductIntoChecklist = _context.ProductIntroChecklists.AsNoTracking()
               .Include(s => s.Stage)
               .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
               .OrderByDescending(o => o.CreateDate)
               .FirstOrDefault();
            var stage = _context.Stages.Where(n => n.StageNumber == stageNumber && n.ProjectId == projectId).First();
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = _context.Stages.Where(s => s.Id == stage.Id).FirstOrDefault();
                vm.Versions = GetVersionHistory(stage.Id);
                vm.Version = latestProductIntoChecklist == null ? 0 : latestProductIntoChecklist.Version;
                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var productIntroChecklist = _mapper.Map<ProductIntroChecklist>(vm);
            if (latestProductIntoChecklist == null)//first version
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        productIntroChecklist.Version = 1;
                        productIntroChecklist.StageId = stage.Id;
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
            else   //There is already a previous version
            {
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestProductIntoChecklist.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate)//same user trying trying to edit 
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        productIntroChecklist.Version = latestProductIntoChecklist.Version;
                        try
                        {
                           productIntroChecklist.Id = latestProductIntoChecklist.Id;
                           productIntroChecklist.CreateDate = latestProductIntoChecklist.CreateDate;//created date its the same
                           productIntroChecklist.StageId = stage.Id;
                           productIntroChecklist.Stage = null;
                            _context.ProductIntroChecklists.Update(productIntroChecklist);
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
                            productIntroChecklist.Id = 0;
                            productIntroChecklist.StageId = stage.Id;
                            productIntroChecklist.Stage = null;
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

            return RedirectToAction("Detail", new { projectId, stageNumber, version = productIntroChecklist.Version });
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