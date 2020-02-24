using AutoMapper;
using dbModels;
using forms;
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

namespace pmo.Controllers.VBPD.Application.History {
    [Route("vbpd-projects/{projectid}/stages/{stageNumber}/business-case")]
    public class BusinessCaseController : BaseStageComponentController {
        private readonly string viewPath = "~/Views/VBPD/Application/BusinessCase";
        private readonly IListService _listService;
        public BusinessCaseController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IListService listService) : base(context, mapper, httpContextAccessor) {
            _listService = listService;
        }

        [Route("{version}")]
        public IActionResult Detail(int version) {
            var model = GetViewModel(version);
            model.ManufacturingLocationsDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.ManufacturingLocations, model.ManufacturingLocationsIds);
            return View($"{viewPath}/Detail.cshtml", model);
        }

        [Route("edit")]
        public IActionResult Edit() {

            // always populate latest version in edit if not just an empty form
            var latestSavedVersion = _context.BusinessCases.AsNoTracking().GetLatestVersion(_projectId);
            var currentStage = _context.Stages.First(n => n.Id == _stageId);
            ViewBag.CurrentStageNumber = currentStage.StageNumber;
            if (latestSavedVersion == null) {
                var vm = new BusinessCaseForm() {
                    StageId = currentStage.Id,
                    Versions = GetVersionHistory(),
                    Stage = currentStage,
                    ManufacturingLocationsDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.ManufacturingLocations)
                };
                return View($"{viewPath}/Edit.cshtml", vm);
            }

            var model = GetViewModel(latestSavedVersion.Version);
            model.ManufacturingLocationsDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.ManufacturingLocations, model.ManufacturingLocationsIds);
            model.Versions = GetVersionHistory();
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(BusinessCaseForm vm) {
            int currentVersion = 0;
            var latestBusinessCase = _context.BusinessCases.GetLatestVersion(_projectId);
            var currentStage = _context.Stages.First(s => s.Id == _stageId);
            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                vm.Stage = currentStage;
                vm.Versions = GetVersionHistory();
                vm.Version = latestBusinessCase == null ? 0 : latestBusinessCase.Version;
                vm.ManufacturingLocationsDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.ManufacturingLocations, vm.ManufacturingLocationsIds);
                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var businessCase = _mapper.Map<BusinessCase>(vm);
            businessCase.StageId = currentStage.Id;
            if (latestBusinessCase == null) {  //first version
                using (var transaction = _context.Database.BeginTransaction()) {
                    try {
                        businessCase.Version = 1;
                        currentVersion = 1;
                        _context.BusinessCases.Add(businessCase);
                        _context.SaveChanges();

                        vm.ManufacturingLocationsIds.ForEach(id => {
                            _context.BusinessCase_ManufacturingLocations.Add(new BusinessCase_ManufacturingLocation {
                                ManufacturingLocationsTagId = id,
                                BusinessCaseId = businessCase.Id,
                            }); _context.SaveChanges();
                        });

                        transaction.Commit();
                    }
                    catch (Exception e) {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
            else //There is already a previous version
            {

                currentVersion = latestBusinessCase.Version;
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestBusinessCase.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate && currentStage.StageNumber == latestBusinessCase.Stage.StageNumber)//if same user and sameStage then update
                {
                    using (var transaction = _context.Database.BeginTransaction()) {
                        try {
                            latestBusinessCase.WillCustomerFundQual = businessCase.WillCustomerFundQual;
                            latestBusinessCase.WillCustomerFundTooling = businessCase.WillCustomerFundTooling;
                            latestBusinessCase.ProbabiltyOfWin = businessCase.ProbabiltyOfWin;
                            latestBusinessCase.TargetFirstYearGrossMargin = businessCase.TargetFirstYearGrossMargin;
                            latestBusinessCase.DataStartingDate = businessCase.DataStartingDate;
                            latestBusinessCase.NumberOfYears = businessCase.NumberOfYears;
                            latestBusinessCase.DiscountRate = businessCase.DiscountRate;
                            latestBusinessCase.TaxRate = businessCase.TaxRate;
                            latestBusinessCase.ProjectScope = businessCase.ProjectScope;
                            latestBusinessCase.WorkRequirementAmount = businessCase.WorkRequirementAmount;
                            latestBusinessCase.StrategicAlignment = businessCase.StrategicAlignment;
                            latestBusinessCase.AddToTecnicalAbilities = businessCase.AddToTecnicalAbilities;
                            latestBusinessCase.ProjectCompletion = businessCase.ProjectCompletion;
                            latestBusinessCase.TimeFromProjectCompletionToRevenueGeneration = businessCase.TimeFromProjectCompletionToRevenueGeneration;
                            latestBusinessCase.CustomerMarketAnalysis = businessCase.CustomerMarketAnalysis;
                            latestBusinessCase.Changes = businessCase.Changes;
                            _context.BusinessCases.Update(latestBusinessCase);
                            _context.SaveChanges();

                            var manuIds = _context.BusinessCase_ManufacturingLocations.Where(m => m.BusinessCaseId == latestBusinessCase.Id).ToList();
                            _context.BusinessCase_ManufacturingLocations.RemoveRange(manuIds);
                            _context.SaveChanges();

                            vm.ManufacturingLocationsIds.ForEach(id => {
                                _context.BusinessCase_ManufacturingLocations.Add(new BusinessCase_ManufacturingLocation {
                                    ManufacturingLocationsTagId = id,
                                    BusinessCaseId = latestBusinessCase.Id,
                                });
                            });
                            _context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception e) {
                            transaction.Rollback();
                            throw e;
                        }
                    }
                }
                else {//if not then new record and 
                    businessCase.Version = currentVersion+=1;
                    _context.BusinessCases.Add(businessCase);
                    _context.SaveChanges();

                    vm.ManufacturingLocationsIds.ForEach(id => {
                        _context.BusinessCase_ManufacturingLocations.Add(new BusinessCase_ManufacturingLocation {
                            ManufacturingLocationsTagId = id,
                            BusinessCaseId = businessCase.Id,
                        });
                    });
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Detail", new { version = currentVersion });
        }

        private List<BusinessCaseForm> GetVersionHistory() {
            var grouped = _context.BusinessCases
                .Include(s => s.Stage)
                .Where(i => i.Stage.ProjectId == _projectId)
                .ToList()
                .GroupBy(g => g.Version)
                .ToList();

            if (grouped.Count == 0) {
                return new List<BusinessCaseForm>();
            }

            List<BusinessCase> versions = new List<BusinessCase>();
            grouped.ForEach(group => versions.Add(group.OrderByDescending(o => o.CreateDate).First()));
            var vm = _mapper.Map<List<BusinessCaseForm>>(versions);
            return vm;
        }
        private BusinessCaseForm GetViewModel(int version) {
            var model = _context.BusinessCases.Where(s => s.Version == version)
                .Include(m => m.ManufacturingLocations)
                .GetLatestVersion(_projectId);

            var vm = _mapper.Map<BusinessCaseForm>(model);


            vm.ManufacturingLocationsIds = _context.BusinessCase_ManufacturingLocations
                .Where(m => m.BusinessCaseId == model.Id)
                .Select(s => s.ManufacturingLocationsTagId)
                .ToList();
            return vm;
        }
    }
}