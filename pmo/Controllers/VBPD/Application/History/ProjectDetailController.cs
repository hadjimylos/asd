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
using ViewModels;
using ViewModels.Helpers;

namespace pmo.Controllers {
    [Route("vbpd-projects/{projectId}/project-detail")]
    public class ProjectDetailController : BaseProjectDetailController {
        private readonly string viewPath = "~/Views/VBPD/Application/ProjectDetail";
        private readonly IListService _listService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProjectDetailController(EfContext context, IMapper mapper, IListService listService, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            _listService = listService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Route("{version}")]
        public IActionResult Detail(int projectId, int version) {
            var model = GetViewModel(projectId, version);
            return View($"{viewPath}/Detail.cshtml", model);
        }

        [Route("edit")]
        public IActionResult Edit(int projectId) {
            // always populate latest version in edit
            var currentVersion = _context.ProjectDetails
                .AsNoTracking()
                .Where(
                    w => w.ProjectId == projectId
                ).Max(m => m.Version);

            var model = GetViewModel(projectId, currentVersion);
            model.Versions = GetVersionHistory(projectId);

            return View($"{viewPath}/Edit.cshtml", model);
        }

        [Route("create-version")]
        public IActionResult CreateVerison(int projectId) {
            var currentVersion = _context.ProjectDetails
                .AsNoTracking()
                .Where (
                    w => w.ProjectId == projectId
                ).Max(m => m.Version);

            var model = new CreateVersionViewModel {
                BackPath = $"/vbpd-projects/{projectId}",
                PostPath = $"/vbpd-projects/{projectId}/project-detail/create-version",
                ComponentName = "Project Detail",
                CurrentVersion = currentVersion,
            };

            return View($"{viewPath}/CreateVersion.cshtml", model);
        }

        [HttpPost]
        [Route("create-version")]
        [AutoValidateAntiforgeryToken]
        public IActionResult PostCreateVerison(int projectId) {
            // get latest transaction of latest version
            var latestRecord = _context.ProjectDetails
                .AsNoTracking()
                .Where(w => w.ProjectId == projectId)
                .OrderByDescending(o => o.CreateDate)
                .FirstOrDefault();

            // NOTE!!!!!!!!: here please check to see if first record. If so redirect to edit only. (not necessary for Project Detail specifically)

            using (var transaction = _context.Database.BeginTransaction()) { 
                try {
                    // get many to many
                    var custIds = _context.ProjectDetail_Customers
                        .AsNoTracking()
                        .Where(w => w.ProjectDetailId == latestRecord.Id)
                        .Select(s => s.CustomersTagId)
                        .ToList();

                    var salesResgionIds = _context.ProjectDetail_SalesRegions
                        .AsNoTracking()
                        .Where(w => w.ProjectDetailId == latestRecord.Id)
                        .Select(s => s.SalesRegionTagId)
                        .ToList();

                    // set variables for create
                    latestRecord.Id = 0;
                    latestRecord.Version = ++latestRecord.Version;
                    _context.Add(latestRecord);
                    _context.SaveChanges();

                    // insert all many to many table values
                    custIds.ForEach(customerId => {
                        _context.ProjectDetail_Customers.Add(new ProjectDetail_Customer {
                            CustomersTagId = customerId,
                            ProjectDetailId = latestRecord.Id,
                        });
                    });

                    salesResgionIds.ForEach(salesRegionId => {
                        _context.ProjectDetail_SalesRegions.Add(new ProjectDetail_SalesRegion {
                            SalesRegionTagId = salesRegionId,
                            ProjectDetailId = latestRecord.Id,
                        });
                    });
                    
                    _context.SaveChanges();
                    transaction.Commit();
                } catch(Exception e) {
                    transaction.Rollback();
                    throw e;
                }

                return RedirectToAction("Edit", new { projectId });
            }
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(VBPDProjectDetailViewModel model, int projectId) {
            var latestProjectDetail = _context.ProjectDetails.Where(
                   w => w.ProjectId == projectId
               ).OrderByDescending(o => o.CreateDate)
               .FirstOrDefault();

            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                SetDropdowns(model);
                model.Versions = GetVersionHistory(projectId);
                model.Version = latestProjectDetail.Version;
                return View($"{viewPath}/Edit.cshtml", model);
            }

            // NOTE!!!!!!!!: here please check to see if first record. If so create and continue. (not necessary for Project Detail specifically)

            // transaction logic:
            // check to see if new record or edit latest
            string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
            var isUpdate = latestProjectDetail.ModifiedByUser.ToLower() == currentUser.ToLower();

            var projectDetail = _mapper.Map<ProjectDetail>(model);
            projectDetail.ProjectId = projectId;
            projectDetail.Version = latestProjectDetail.Version;
            if (isUpdate) {
                // get latest record for this version and edit (remains latest and allows you to spam click save)   
                using (var transaction = _context.Database.BeginTransaction()) {
                    try {
                        // save to primary table
                        _context.ProjectDetails.Update(projectDetail);
                        _context.SaveChanges();

                        // clear existing many to many
                        var remove_customers = _context.ProjectDetail_Customers.Where(w => w.ProjectDetailId == latestProjectDetail.Id).ToList();
                        _context.RemoveRange(remove_customers);
                        var remove_salesRegions = _context.ProjectDetail_SalesRegions.Where(w => w.ProjectDetailId == latestProjectDetail.Id).ToList();
                        _context.RemoveRange(remove_salesRegions);
                        _context.SaveChanges();

                        // reinsert many to many
                        InsertManyToMany(model, projectDetail.Id);

                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e){
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
            else {
                using (var transaction = _context.Database.BeginTransaction()) {
                    try {
                        // save to primary table
                        _context.ProjectDetails.Add(projectDetail);
                        _context.SaveChanges();

                        InsertManyToMany(model, projectDetail.Id);

                        // set previous versions & transactions to locked
                    }
                    catch (Exception e) {
                        transaction.Rollback();
                        throw e;
                    }
                }     
            }

            return RedirectToAction("Detail", new { projectId, version = latestProjectDetail.Version });
        }

        private List<VBPDProjectDetailViewModel> GetVersionHistory(int projectId) {
            var grouped = _context.ProjectDetails
                .Where(w => w.ProjectId == projectId)
                .ToList()
                .GroupBy(g => g.Version)
                .ToList();

            // NOTE!!!!!!!!: (if grouped.Count == 0) here please check to see if first record. If so return empty model with initialized variables (e.g. Dropdowns) only.

            List<ProjectDetail> versions = new List<ProjectDetail>();
            grouped.ForEach(group => versions.Add(group.OrderByDescending(o => o.CreateDate).First()));

            return _mapper.Map<List<VBPDProjectDetailViewModel>>(versions);
        }

        private void InsertManyToMany(VBPDProjectDetailViewModel model, int projectDetailId) {
            // insert many to many
            model.CustomerIds.ForEach(customerId => {
                _context.ProjectDetail_Customers.Add(new ProjectDetail_Customer {
                    CustomersTagId = customerId,
                    ProjectDetailId = projectDetailId,
                });
            });
            model.SalesRegionIds.ForEach(salesRegionId => {
                _context.ProjectDetail_SalesRegions.Add(new ProjectDetail_SalesRegion {
                    SalesRegionTagId = salesRegionId,
                    ProjectDetailId = projectDetailId,
                });
            });
            _context.SaveChanges();
        }

        private VBPDProjectDetailViewModel GetViewModel(int projectId, int version) {
            var model = _context.ProjectDetails.Where(
                w => w.ProjectId == projectId && w.Version == version
            ).OrderByDescending(o => o.CreateDate)
            .Include(i => i.Customers)
            .Include(i => i.SalesRegions)
            .First();

            var vm = _mapper.Map<VBPDProjectDetailViewModel>(model);
            SetDropdowns(vm);

            // set many to many selected:
            vm.CustomerIds = model.Customers.Select(s => s.CustomersTagId).ToList();
            vm.SalesRegionIds = model.SalesRegions.Select(s => s.SalesRegionTagId).ToList();

            return vm;
        }

        private void SetDropdowns(VBPDProjectDetailViewModel model) {
            // get all tags
            List<string> tagDropdowns = new List<string>() {
                TagCategoryHelper.ProjectCategory,
                TagCategoryHelper.ProductLine,
                TagCategoryHelper.ProjectClassification,
                TagCategoryHelper.DesignAuthority,
                TagCategoryHelper.ExportApplicationType,
                TagCategoryHelper.Customer,
                TagCategoryHelper.SalesRegion,
            };

            var tagCategories = _context.TagCategories
                .Include(i => i.Tags)
                .Where(w => tagDropdowns.Contains(w.Key))
                .ToList();

            model.ProjectCategoryTagDropDown = GetDropdown(tagCategories, TagCategoryHelper.ProjectCategory);
            model.ProductLineDropDown = GetDropdown(tagCategories, TagCategoryHelper.ProductLine);
            model.ProjectClassificationDropDown = GetDropdown(tagCategories, TagCategoryHelper.ProjectClassification);
            model.DesignAuthorityDropDown = GetDropdown(tagCategories, TagCategoryHelper.DesignAuthority);
            model.ExportApplicationTypeDropDown = GetDropdown(tagCategories, TagCategoryHelper.ExportApplicationType);
            model.CustomerDropDown = GetDropdown(tagCategories, TagCategoryHelper.Customer);
            model.SalesRegionsDropDown = GetDropdown(tagCategories, TagCategoryHelper.SalesRegion);
        }

        private List<SelectListItem> GetDropdown(List<dbModels.TagCategory> categories, string key) => 
            categories.First(f => f.Key == key).GetListItems();
    }
}