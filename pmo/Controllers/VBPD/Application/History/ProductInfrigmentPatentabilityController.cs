using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace pmo.Controllers.VBPD.Application.History
{
    [Route("vbpd-projects/{projectid}/stages/{stageNumber}/product-infrigment-patentability")]
    public class ProductInfrigmentPatentabilityController : BaseProjectDetailController
    {
        private readonly string viewPath = "~/Views/VBPD/Application/ProductInfrigmentPatentability";
        private readonly string UploadViewPath = "~/Views/VBPD";

        public ProductInfrigmentPatentabilityController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
        }

        [Route("{version}")]
        public IActionResult Detail(int projectId, int stageNumber, int version)
        {
            var stageId = _context.Stages.Where(n => n.StageNumber == stageNumber && n.ProjectId == projectId).First().Id;
            var model = GetViewModel(stageId, version);
            return View($"{viewPath}/Detail.cshtml", model);
        }

        [Route("create-version")]
        public IActionResult CreateVersion(int projectId, int stageNumber)
        {
            //get current version before create a new one
            var currentVersion = _context.ProductInfrigmentPatentabilities
                .AsNoTracking()
                .Include(s => s.Stage)
                .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
                .Max(m => m.Version);

            var model = new CreateVersionViewModel
            {
                BackPath = $"/vbpd-projects/{projectId}/stages/{stageNumber}/product-infrigment-patentability/{currentVersion}",
                PostPath = $"/vbpd-projects/{projectId}/stages/{stageNumber}/product-infrigment-patentability/create-version",
                ComponentName = "Customer Design Approval",
                CurrentVersion = currentVersion,
            };

            return View($"{viewPath}/CreateVersion.cshtml", model);
        }

        [HttpPost]
        [Route("create-version")]
        [AutoValidateAntiforgeryToken]
        public IActionResult PostCreateVerison(int projectId, int stageNumber)
        {
            var latestRecord = _context.ProductInfrigmentPatentabilities
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
                var previousId = latestRecord.Id;
                try
                {
                    // set variables for create
                    latestRecord.Id = 0;
                    latestRecord.Version = ++latestRecord.Version;
                    _context.Add(latestRecord);
                    _context.SaveChanges();
                    var latestDocuments = _context.ProductInfrigmentPatentabilityUploadedDocumentations.Where(x => x.ProductInfrigmentPatentabilityId == previousId).ToList();
                    foreach (var latestDocument in latestDocuments)
                    {
                        latestDocument.Id = 0;
                        latestDocument.ProductInfrigmentPatentabilityId = latestRecord.Id;
                        _context.ProductInfrigmentPatentabilityUploadedDocumentations.Add(latestDocument);
                        _context.SaveChanges();
                    }
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }

                return RedirectToAction("Edit", new { stageNumber, projectId });
            }
        }

        [Route("edit")]
        public IActionResult Edit(int stageNumber, int projectId)
        {
            // always populate latest version in edit
            //Tha skasei ama einai 0
            var currentVersion = _context.ProductInfrigmentPatentabilities
                 .AsNoTracking()
                 .Include(s => s.Stage)
                 .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
                 .OrderByDescending(c => c.CreateDate)
                 .FirstOrDefault();

            var currentStage = _context.Stages.Where(n => n.StageNumber == stageNumber && n.ProjectId == projectId).First();
            if (currentVersion == null)
            {
                var vm = new ProductInfrigmentPatentabilityViewModel()
                {
                    StageId = currentStage.Id,
                    Versions = new List<ProductInfrigmentPatentabilityViewModel>(),
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
        public IActionResult Edit(ProductInfrigmentPatentabilityViewModel vm, int projectId, int stageNumber)
        {
            int currentVersion = 0;
            var stage = _context.Stages.Where(n => n.StageNumber == stageNumber && n.ProjectId == projectId).First();
            var latestProductInfrigmentPatentability = _context.ProductInfrigmentPatentabilities
               .Include(s => s.Stage)
               .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
               .OrderByDescending(o => o.CreateDate)
               .FirstOrDefault();
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = stage;
                vm.Versions = GetVersionHistory(stage.Id);
                vm.Version = latestProductInfrigmentPatentability == null ? 0 : latestProductInfrigmentPatentability.Version;
                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var productInfrigmentPatentability = _mapper.Map<ProductInfrigmentPatentability>(vm);
            productInfrigmentPatentability.StageId = stage.Id;
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
                        return View($"{UploadViewPath}/UploadFiles.cshtml", new UploadDocumentsViewModel
                        {
                            ComponentId = productInfrigmentPatentability.Id,
                            ComponentName = "Product Infrigment Patentability",
                            CurrentVersion = productInfrigmentPatentability.Version,
                            StageNumber = stage.StageNumber,
                            ProjectId = stage.ProjectId,
                            Files = new List<IFormFile>(),
                            Type = "ProductInfrigmentPatentabilityUploadedDocumentation",
                            ControllerName = "ProductInfrigmentPatentability",
                            Path = "product-infrigment-patentability"

                        });
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
                if (isUpdate)//same user  trying to edit 
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
                            //TODO Upload Documentation as well
                            _context.ProductInfrigmentPatentabilities.Update(latestProductInfrigmentPatentability);
                            _context.SaveChanges();
                            transaction.Commit();
                            return View($"{UploadViewPath}/UploadFiles.cshtml", new UploadDocumentsViewModel
                            {
                                ComponentId = productInfrigmentPatentability.Id,
                                ComponentName = "Product Infrigment Patentability",
                                CurrentVersion = productInfrigmentPatentability.Version,
                                StageNumber = stage.StageNumber,
                                ProjectId = stage.ProjectId,
                                Files = new List<IFormFile>(),
                                Type = "ProductInfrigmentPatentabilityUploadedDocumentation",
                                ControllerName = "ProductInfrigmentPatentability",
                                Path = "product-infrigment-patentability"

                            });
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
                            productInfrigmentPatentability.Version = currentVersion;
                            _context.ProductInfrigmentPatentabilities.Add(productInfrigmentPatentability);
                            _context.SaveChanges();
                            transaction.Commit();
                        
                            return View($"{UploadViewPath}/UploadFiles.cshtml", new UploadDocumentsViewModel
                            {
                                ComponentId = productInfrigmentPatentability.Id,
                                ComponentName = "Product Infrigment Patentability",
                                CurrentVersion = productInfrigmentPatentability.Version,
                                StageNumber = stage.StageNumber,
                                ProjectId = stage.ProjectId,
                                Files = new List<IFormFile>(),
                                Type = "ProductInfrigmentPatentabilityUploadedDocumentation",
                                ControllerName = "ProductInfrigmentPatentability",
                                Path="product-infrigment-patentability"

                            });
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();

                            throw e;
                        }
                    }
                }
            }
        }

        private ProductInfrigmentPatentabilityViewModel GetViewModel(int stageId, int version)
        {
            var model = _context.ProductInfrigmentPatentabilities.Where(
                s => s.StageId == stageId && s.Version == version
            ).OrderByDescending(o => o.CreateDate)
            .Include(i => i.ProductInfrigmentPatentabilityImportantDocumentation)
            .Include(s => s.Stage)
            .FirstOrDefault();

            model.ProductInfrigmentPatentabilityImportantDocumentation = _context.ProductInfrigmentPatentabilityUploadedDocumentations.Where(x => x.ProductInfrigmentPatentabilityId == model.Id).ToList();
            var vm = _mapper.Map<ProductInfrigmentPatentabilityViewModel>(model);
            return vm;
        }
        private List<ProductInfrigmentPatentabilityViewModel> GetVersionHistory(int stageId)
        {
            var grouped = _context.ProductInfrigmentPatentabilities
                .Where(w => w.StageId == stageId)
                .ToList()
                .GroupBy(g => g.Version)
                .ToList();

            if (grouped.Count == 0)
            {
                return new List<ProductInfrigmentPatentabilityViewModel>();
            }

            List<ProductInfrigmentPatentability> versions = new List<ProductInfrigmentPatentability>();
            grouped.ForEach(group => versions.Add(group.OrderByDescending(o => o.CreateDate).First()));

            return _mapper.Map<List<ProductInfrigmentPatentabilityViewModel>>(versions);
        }

    }
}