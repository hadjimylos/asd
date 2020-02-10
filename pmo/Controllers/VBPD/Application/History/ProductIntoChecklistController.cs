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
    [Route("vbpd-projects/{projectid}/stage/{stageId}/product-intro-checklist")]
    public class ProductIntoChecklistController : BaseController
    {

        private readonly string viewPath = "~/Views/VBPD/Application/ProductIntroChecklist";

        public ProductIntoChecklistController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
        }

        [Route("create-version")]
        public IActionResult CreateVersion(int stageId, int projectId)
        {
            var currentVersion = _context.ProductIntroChecklists
                .AsNoTracking()
                .Where(
                    w => w.StageId == stageId
                ).Max(m => m.Version);

            var model = new CreateVersionViewModel
            {
                BackPath = $"/vbpd-projects/{projectId}/stage/{stageId}/product-intro-checklist/{currentVersion}",
                PostPath = $"/vbpd-projects/{projectId}/stage/{stageId}/product-intro-checklist/create-version",
                ComponentName = "Product Intro Checklist",
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
            var latestRecord = _context.ProductIntroChecklists
                .AsNoTracking()
                .Where(w => w.StageId == stageId)
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
                return RedirectToAction("Edit", new { stageId });
            }
        }

        [Route("{version}")]
        public IActionResult Detail(int stageId, int version)
        {
            var model = GetViewModel(stageId, version);
            return View($"{viewPath}/Detail.cshtml", model);
        }

        [Route("edit")]
        public IActionResult Edit(int stageId)
        {
            // always populate latest version in edit
            var currentVersion = _context.ProductIntroChecklists
                 .AsNoTracking()
                 .Where(w => w.StageId == stageId)
                 .OrderByDescending(c => c.CreateDate)
                 .FirstOrDefault();

            if (currentVersion == null)
            {
                var vm = new ProductIntroChecklistViewModel()
                {
                    StageId = stageId,
                    Versions = new List<ProductIntroChecklistViewModel>(),
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
        public IActionResult Edit(ProductIntroChecklistViewModel vm, int stageId)
        {
            var latestProductIntoChecklist = _context.ProductIntroChecklists.Where(
                  w => w.StageId == stageId
              ).OrderByDescending(o => o.CreateDate)
              .FirstOrDefault();

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = _context.Stages.Where(s => s.Id == stageId).FirstOrDefault();
                vm.Versions = GetVersionHistory(stageId);
                vm.Version = latestProductIntoChecklist == null ? 0 : latestProductIntoChecklist.Version;
                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var productIntroChecklist = _mapper.Map<ProductIntroChecklist>(vm);
            //first version
            if (latestProductIntoChecklist == null)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        productIntroChecklist.Version = 1;
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
            //There is already a previous version
            else
            {
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestProductIntoChecklist.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate)
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            productIntroChecklist.Version = latestProductIntoChecklist.Version;
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
                            productIntroChecklist.Version = latestProductIntoChecklist.Version;
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
            }

            return RedirectToAction("Detail", new { stageId, version = productIntroChecklist.Version });
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