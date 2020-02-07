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
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Helpers;

namespace pmo.Controllers.Application.History
{
    [Route("vbpd/{projectid}/stage/{stageId}/key-characteristic")]
    public class KeyCharacteristicController : BaseController
    {
        private readonly string viewPath = "~/Views/Application/KeyCharacteristic";
        private readonly IListService _listService;

        public KeyCharacteristicController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
            //  _listService = listService;
            //   _httpContextAccessor = httpContextAccessor;
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
            // always populate latest version in edit if not just an empty form
            var currentVersion = _context.KeyCharacteristics
                 .AsNoTracking()
                 .Where(w => w.StageId == stageId)
                 .OrderByDescending(c => c.CreateDate)
                 .FirstOrDefault();
            if (currentVersion == null)
            {
                var vm = new KeyCharacteristicViewModel()
                {
                    StageId = stageId,
                    Versions = new List<KeyCharacteristicViewModel>(),
                    Stage = _context.Stages.Where(s => s.Id == stageId).FirstOrDefault(),
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

            var model = GetViewModel(stageId, currentVersion.Version);
            model.Versions = GetVersionHistory(stageId);
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
        public IActionResult Edit(KeyCharacteristicViewModel vm, int stageId)
        {

            var latestKeyCharacteristics = _context.KeyCharacteristics.AsNoTracking().Where(
                  w => w.StageId == stageId
              ).OrderByDescending(o => o.CreateDate)
              .FirstOrDefault();

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = _context.Stages.Where(s => s.Id == stageId).FirstOrDefault();
                vm.Versions = GetVersionHistory(stageId);
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
            keyCharacteristic.StageId = stageId;
            //first version
            if (latestKeyCharacteristics == null)
            {
                keyCharacteristic.Version = 1;
                _context.KeyCharacteristics.Add(keyCharacteristic);
                _context.SaveChanges();
            }
            //There is already a previous version
            else
            {
                keyCharacteristic.Version = latestKeyCharacteristics.Version;
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestKeyCharacteristics.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate)
                {
                    keyCharacteristic.Id = latestKeyCharacteristics.Id;
                    _context.KeyCharacteristics.Update(keyCharacteristic);
                    _context.SaveChanges();
                }
                else
                {
                    _context.KeyCharacteristics.Add(keyCharacteristic);
                    _context.SaveChanges();
                }

            }
            return RedirectToAction("Detail", new { stageId, version = keyCharacteristic.Version });
        }

        [Route("create-version")]
        public IActionResult CreateVersion(int stageId, int projectId)
        {
            var currentVersion = _context.KeyCharacteristics
                .AsNoTracking()
                .Where(
                    w => w.StageId == stageId
                ).Max(m => m.Version);

            var model = new CreateVersionViewModel
            {
                BackPath = $"/vbpd/{projectId}/stage/{stageId}/key-characteristic{currentVersion}",
                PostPath = $"/vbpd/{projectId}/stage/{stageId}/key-characteristic/create-version",
                ComponentName = "Key Characteristic",
                CurrentVersion = currentVersion,
            };

            return View($"{viewPath}/CreateVersion.cshtml", model);
        }

        [HttpPost]
        [Route("create-version")]
        [AutoValidateAntiforgeryToken]
        public IActionResult PostCreateVerison(int projectId, int stageId)
        {
            // get latest transaction of latest version
            var latestRecord = _context.KeyCharacteristics
                .AsNoTracking()
                .Where(w => w.StageId == stageId)
                .OrderByDescending(o => o.CreateDate)
                .FirstOrDefault();
            if (latestRecord == null)
            {
                RedirectToAction("edit");
            }


            // set variables for create
            latestRecord.Id = 0;
            latestRecord.Version = ++latestRecord.Version;
            _context.Add(latestRecord);
            _context.SaveChanges();
            return RedirectToAction("edit", new { stageId });
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
