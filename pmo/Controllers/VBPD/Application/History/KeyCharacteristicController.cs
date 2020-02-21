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

namespace pmo.Controllers
{
    [Route("vbpd-projects/{projectid}/stages/{stageNumber}/key-characteristic")]
    public class KeyCharacteristicController : BaseStageComponentController {
        private readonly string viewPath = "~/Views/VBPD/Application/KeyCharacteristic";

        public KeyCharacteristicController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
            //  _listService = listService;
            //   _httpContextAccessor = httpContextAccessor;
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

            // always populate latest version in edit if not just an empty form
            var latestSavedVersion = _context.KeyCharacteristics.AsNoTracking().GetLatestVersion(_projectId);

            var currentStage = _context.Stages.First(n => n.Id == _stageId);
            if (latestSavedVersion == null)
            {
                var vm = new KeyCharacteristicViewModel()
                {
                    StageId = currentStage.Id,
                    Versions = GetVersionHistory(),
                    Stage = currentStage,
                    RequirementSourceDropDown = _context.Tags.Include(C => C.TagCategory)
                        .Where(t => t.TagCategory.Key == TagCategoryHelper.RequirementSource)
                        .ToList().Select(s => new SelectListItem
                        {
                            Value = s.Id.ToString(),
                            Text = s.Name,
                        }).ToList()
                };
                return View($"{viewPath}/Edit.cshtml", vm);
            }

            var model = GetViewModel(latestSavedVersion.Version);
            model.Versions = GetVersionHistory();
            model.RequirementSourceDropDown = _context.Tags.Include(C => C.TagCategory)
                .Where(t => t.TagCategory.Key == TagCategoryHelper.RequirementSource)
                .ToList().Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.Name,
                    Selected = s.Id == model.RequirementSourceId
                }).ToList();
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(KeyCharacteristicViewModel vm)
        {
            int currentVersion = 0;
            var latestKeyCharacteristics = _context.KeyCharacteristics.GetLatestVersion(_projectId);
            var currentStage = _context.Stages.First(s => s.Id == _stageId);
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = currentStage;
                vm.Versions = GetVersionHistory();
                vm.Version = latestKeyCharacteristics == null ? 0 : latestKeyCharacteristics.Version;
                vm.RequirementSourceDropDown = _context.Tags.Include(C => C.TagCategory)
                    .Where(t => t.TagCategory.Key == TagCategoryHelper.RequirementSource)
                    .ToList().Select(s => new SelectListItem
                    {
                        Value = s.Id.ToString(),
                        Text = s.Name,
                        Selected = s.Id == vm.RequirementSourceId
                    }).ToList();
                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var keyCharacteristic = _mapper.Map<KeyCharacteristic>(vm);
            keyCharacteristic.StageId = currentStage.Id;
            if (latestKeyCharacteristics == null)  //first version
            {
                keyCharacteristic.Version = 1;
                currentVersion = 1;
                _context.KeyCharacteristics.Add(keyCharacteristic);
                _context.SaveChanges();
            }
            else //There is already a previous version
            {
                currentVersion = latestKeyCharacteristics.Version;
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestKeyCharacteristics.ModifiedByUser.ToLower() == currentUser.ToLower();

                if (isUpdate && currentStage.StageNumber == latestKeyCharacteristics.Stage.StageNumber)//if same user and sameStage then update
                {
                    latestKeyCharacteristics.ItemNumber = keyCharacteristic.ItemNumber;
                    latestKeyCharacteristics.MeasuredValue = keyCharacteristic.MeasuredValue;
                    latestKeyCharacteristics.Requirement = keyCharacteristic.Requirement;
                    latestKeyCharacteristics.RequirementSourceId = keyCharacteristic.RequirementSourceId;
                    latestKeyCharacteristics.ExpectedOutcomeRisk = keyCharacteristic.ExpectedOutcomeRisk;
                    _context.KeyCharacteristics.Update(latestKeyCharacteristics);
                    _context.SaveChanges();
                }
                else
                {
                    //if not then new record and new version
                    //
                    keyCharacteristic.Version = currentVersion+=1;
                    _context.KeyCharacteristics.Add(keyCharacteristic);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Detail", new {version =currentVersion });
        }

        private List<KeyCharacteristicViewModel> GetVersionHistory()
        {
            var grouped = _context.KeyCharacteristics.Include(k=>k.RequirementSource)
                .Include(s => s.Stage)
                .Where(i => i.Stage.ProjectId == _projectId)
                .ToList()
                .GroupBy(g => g.Version)
                .ToList();

            if (grouped.Count == 0)
            {
                return new List<KeyCharacteristicViewModel>();
            }

            List<KeyCharacteristic> versions = new List<KeyCharacteristic>();
            grouped.ForEach(group => versions.Add(group.OrderByDescending(o => o.CreateDate).First()));
            var vm  = _mapper.Map<List<KeyCharacteristicViewModel>>(versions);
            foreach (var item in vm)
            {
                item.RequirementSourceText = item.RequirementSource.Name;
            }
            return vm;
        }
        private KeyCharacteristicViewModel GetViewModel(int version)
        {
            var model = _context.KeyCharacteristics.Where(s => s.Version == version)
            .Include(i => i.RequirementSource)
            .GetLatestVersion(_projectId);
            var vm = _mapper.Map<KeyCharacteristicViewModel>(model);
            vm.RequirementSourceText = model.RequirementSource.Name;
            return vm;
        }
    }
}
