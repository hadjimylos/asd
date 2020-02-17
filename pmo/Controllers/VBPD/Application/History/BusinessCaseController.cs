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

namespace pmo.Controllers.VBPD.Application.History
{

  [Route("vbpd-projects/{projectid}/stages/{stageNumber}/business-case")]
    public class BusinessCaseController : BaseStageComponentController {
        private readonly string viewPath = "~/Views/VBPD/Application/BusinessCase";
        private readonly IListService _listService;
        public BusinessCaseController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor , IListService listService) : base(context, mapper, httpContextAccessor)
        {
              _listService = listService;
            //   _httpContextAccessor = httpContextAccessor;
        }

        [Route("{version}")]
        public IActionResult Detail(int version)
        {
            var model = GetViewModel(_stageId, version);
            model.ManufacturingLocationsDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.ManufacturingLocations ,  model.ManufacturingLocationsIds);
            return View($"{viewPath}/Detail.cshtml", model);
        }

        [Route("create-version")]
        public IActionResult CreateVersion(int projectId, int stageNumber)
        {
            var currentVersion = _context.BusinessCases
                 .AsNoTracking()
                 .Include(s => s.Stage)
                 .Where(n => n.StageId == _stageId)
                 .Max(m => m.Version);

            var model = new CreateVersionViewModel
            {
                BackPath = $"/vbpd-projects/{projectId}/stages/{stageNumber}/business-case/{currentVersion}",
                PostPath = $"/vbpd-projects/{projectId}/stages/{stageNumber}/business-case/create-version",
                ComponentName = "Business Case",
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
            var latestRecord = _context.BusinessCases.AsNoTracking()
                .Where(n => n.StageId == _stageId)
                .OrderByDescending(o => o.CreateDate)
                .FirstOrDefault();

            if (latestRecord == null)
            {//check to see if first record.If so redirect to edit only.
                RedirectToAction("Edit");
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var manufIds = _context.BusinessCase_ManufacturingLocations.Where(s => s.BusinessCaseId == latestRecord.Id).Select(s=>s.ManufacturingLocationsTagId).ToList();
                    // set variables for create
                    latestRecord.Id = 0;
                    latestRecord.Version = ++latestRecord.Version;
                    _context.Attach(latestRecord);
                    _context.Entry(latestRecord).State = EntityState.Added;
                    _context.SaveChanges();
                    foreach (var tagId in manufIds)
                    {
                        _context.BusinessCase_ManufacturingLocations.Add(new BusinessCase_ManufacturingLocation()
                        {
                            Id = 0,
                           BusinessCaseId = latestRecord.Id
                           , ManufacturingLocationsTagId = tagId

                        }) ; 
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
        public IActionResult Edit()
        {
            // always populate latest version in edit if not just an empty form
            var currentVersion = _context.BusinessCases
                 .AsNoTracking()
                 .Include(s => s.Stage)
                 .Where(n => n.StageId == _stageId)
                 .OrderByDescending(c => c.CreateDate)
                 .FirstOrDefault();
            var currentStage = _context.Stages.First(n => n.Id == _stageId);


            if (currentVersion == null)
            {
                var vm = new BusinessCaseViewModel()
                {
                    StageId = currentStage.Id,
                    Versions = new List<BusinessCaseViewModel>(),
                    Stage = currentStage, ManufacturingLocationsDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.ManufacturingLocations)
                };
                return View($"{viewPath}/Edit.cshtml", vm);
            }

            var model = GetViewModel(currentStage.Id, currentVersion.Version);
            model.ManufacturingLocationsDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.ManufacturingLocations, model.ManufacturingLocationsIds);
            model.Versions = GetVersionHistory(currentStage.Id);
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(BusinessCaseViewModel vm, int projectId, int stageNumber)
        {
            int currentVersion = 0;
            var latestBusinessCase = _context.BusinessCases
               .Include(s => s.Stage)
               .Where(n => n.StageId == _stageId)
               .OrderByDescending(o => o.CreateDate)
               .FirstOrDefault();
            var stage = _context.Stages.First(s => s.Id == _stageId);
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = stage;
                vm.Versions = GetVersionHistory(stage.Id);
                vm.Version = latestBusinessCase == null ? 0 : latestBusinessCase.Version;
                vm.ManufacturingLocationsDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.ManufacturingLocations, vm.ManufacturingLocationsIds);
                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var businessCase = _mapper.Map<BusinessCase>(vm);
            businessCase.StageId = stage.Id;
            if (latestBusinessCase == null)  //first version
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        businessCase.Version = 1;
                        currentVersion = 1;
                        _context.BusinessCases.Add(businessCase);
                        _context.SaveChanges();

                        vm.ManufacturingLocationsIds.ForEach(id =>
                        {
                            _context.BusinessCase_ManufacturingLocations.Add(new BusinessCase_ManufacturingLocation
                            {
                                ManufacturingLocationsTagId = id,
                                BusinessCaseId = businessCase.Id,
                            });  _context.SaveChanges();
                        });
                      
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

                currentVersion = latestBusinessCase.Version;
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestBusinessCase.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate)//if same user then update
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
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

                            vm.ManufacturingLocationsIds.ForEach(id =>
                            {
                                _context.BusinessCase_ManufacturingLocations.Add(new BusinessCase_ManufacturingLocation
                                {
                                    ManufacturingLocationsTagId = id,
                                    BusinessCaseId = latestBusinessCase.Id,
                                });
                            });
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
                {//if not then new record and 
                    _context.BusinessCases.Add(businessCase);
                    _context.SaveChanges();

                    vm.ManufacturingLocationsIds.ForEach(id =>
                    {
                        _context.BusinessCase_ManufacturingLocations.Add(new BusinessCase_ManufacturingLocation
                        {
                            ManufacturingLocationsTagId = id,
                            BusinessCaseId = businessCase.Id,
                        });
                    });
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Detail", new { projectId, stageNumber, version = currentVersion });
        }

        private List<BusinessCaseViewModel> GetVersionHistory(int stageId)
        {
            var grouped = _context.BusinessCases
                .Where(w => w.StageId == stageId)
                .ToList()
                .GroupBy(g => g.Version)
                .ToList();

            if (grouped.Count == 0)
            {
                return new List<BusinessCaseViewModel>();
            }

            List<BusinessCase> versions = new List<BusinessCase>();
            grouped.ForEach(group => versions.Add(group.OrderByDescending(o => o.CreateDate).First()));
            var vm = _mapper.Map<List<BusinessCaseViewModel>>(versions);
            return vm;
        }
        private BusinessCaseViewModel GetViewModel(int stageId, int version)
        {
            var model = _context.BusinessCases.Where(
                s => s.StageId == stageId && s.Version == version
            ).OrderByDescending(o => o.CreateDate)
            .Include(m=>m.ManufacturingLocations)
            .Include(s => s.Stage)
            .FirstOrDefault();
            var vm = _mapper.Map<BusinessCaseViewModel>(model);

         
            vm.ManufacturingLocationsIds = _context.BusinessCase_ManufacturingLocations
                .Where(m => m.BusinessCaseId == model.Id)
                .Select(s => s.ManufacturingLocationsTagId)
                .ToList();
            return vm;
        }
    }
}