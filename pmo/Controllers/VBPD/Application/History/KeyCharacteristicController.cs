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

namespace pmo.Controllers.Application.History
{
    [Route("vbpd-projects/{projectid}/stages/{stageNumber}/key-characteristic")]
    public class KeyCharacteristicController : BaseProjectDetailController
    {
        private readonly string viewPath = "~/Views/VBPD/Application/KeyCharacteristic";
        private readonly IListService _listService;

        public KeyCharacteristicController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
            //  _listService = listService;
            //   _httpContextAccessor = httpContextAccessor;
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
            var currentVersion = _context.KeyCharacteristics
                 .AsNoTracking()
                 .Include(s => s.Stage)
                 .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
                 .Max(m => m.Version);

            var model = new CreateVersionViewModel
            {
                BackPath = $"/vbpd-projects/{projectId}/stage/{stageNumber}/key-characteristic/{currentVersion}",
                PostPath = $"/vbpd-projects/{projectId}/stage/{stageNumber}/key-characteristic/create-version",
                ComponentName = "Key Characteristic",
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
            var latestRecord = _context.KeyCharacteristics
                .AsNoTracking()
                .Include(s => s.Stage)
                .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
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
                    // set variables for create
                    latestRecord.Id = 0;
                    latestRecord.StageId = latestRecord.Stage.Id;
                    latestRecord.Stage = null;//remove relation before saving 
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

                return RedirectToAction("Edit", new { stageNumber, projectId });
            }
        }


        [Route("edit")]
        public IActionResult Edit(int projectId, int stageNumber)
        {
            // always populate latest version in edit if not just an empty form
            var currentVersion = _context.KeyCharacteristics
                 .AsNoTracking()
                 .Include(s => s.Stage)
                 .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
                 .OrderByDescending(c => c.CreateDate)
                 .FirstOrDefault();
            var currentStage = _context.Stages.Where(n => n.StageNumber == stageNumber && n.ProjectId == projectId).First();

            if (currentVersion == null)
            {
                var vm = new KeyCharacteristicViewModel()
                {
                    StageId = currentStage.Id,
                    Versions = new List<KeyCharacteristicViewModel>(),
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

            var model = GetViewModel(currentStage.Id, currentVersion.Version);
            model.Versions = GetVersionHistory(currentStage.Id);
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
        public IActionResult Edit(KeyCharacteristicViewModel vm, int projectId, int stageNumber)
        {
            int currentVersion = 0;
            var latestKeyCharacteristics = _context.KeyCharacteristics
               .Include(s => s.Stage)
               .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
               .OrderByDescending(o => o.CreateDate)
               .FirstOrDefault();
            var stage = _context.Stages.Where(n => n.StageNumber == stageNumber && n.ProjectId == projectId).First();
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = stage;
                vm.Versions = GetVersionHistory(stage.Id);
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
            keyCharacteristic.StageId = _context.Stages.Where(p => p.ProjectId == projectId && p.StageNumber == stageNumber).First().Id;
            if (latestKeyCharacteristics == null)  //first version
            {
                keyCharacteristic.StageId = stage.Id;
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
                if (isUpdate)//if same user then update
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
                {//if not then new record 
                    _context.KeyCharacteristics.Add(keyCharacteristic);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Detail", new { projectId, stageNumber, version =currentVersion });
        }

        private List<KeyCharacteristicViewModel> GetVersionHistory(int stageId)
        {
            var grouped = _context.KeyCharacteristics.Include(k=>k.RequirementSource)
                .Where(w => w.StageId == stageId)
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
        private KeyCharacteristicViewModel GetViewModel(int stageId, int version)
        {
            var model = _context.KeyCharacteristics.Where(
                s => s.StageId == stageId && s.Version == version
            ).OrderByDescending(o => o.CreateDate)
            .Include(i => i.RequirementSource)
            .Include(s => s.Stage)
            .FirstOrDefault();

            var vm = _mapper.Map<KeyCharacteristicViewModel>(model);
            vm.RequirementSourceText = model.RequirementSource.Name;
            return vm;
        }
    }
}
