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
    [Route("projects/{projectid}/stages/{stageNumber}/key-characteristic")]
    public class KeyCharacteristicController : BaseStageComponentController {
        private readonly string path = "~/Views/VBPD/Application/KeyCharacteristic";

        public KeyCharacteristicController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            //  _listService = listService;
            //   _httpContextAccessor = httpContextAccessor;
        }

        [Route("")]
        public IActionResult Index() {
            var vm = _context.KeyCharacteristics.IncludeAll()
                .Where(x => x.StageId == _stageId)
                .ToList();

            return View($"{path}/Index.cshtml", vm);
        }

        [Route("create")]
        public IActionResult Create() {
            var kc = new forms.KeyCharacteristicForm() {
                StageId = _stageId
            };
            SetDropdowns(kc);
            return View($"{path}/Create.cshtml", kc);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("create")]
        public IActionResult Create(forms.KeyCharacteristicForm kcform) {
            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                SetDropdowns(kcform);
                return View($"{path}/Create.cshtml", kcform);
            }
            var domainModel = _mapper.Map<KeyCharacteristic>(kcform);
            domainModel.StageId = _stageId;
            _context.KeyCharacteristics.Add(domainModel);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }

        [Route("{id}")]
        public IActionResult Edit(int id) {
            var kc = _context.KeyCharacteristics
                .Include(s => s.Stage)
                .Where(s => s.Id == id)
                .FirstOrDefault();
            if (kc == null)
                return NotFound();

            var vm = _mapper.Map<forms.KeyCharacteristicForm>(kc);
            SetDropdowns(vm);
            return View($"{path}/Edit.cshtml", vm);
        }

        [HttpPost]
        [Route("{id}")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(forms.KeyCharacteristicForm vm, int id) {
            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                SetDropdowns(vm);
                return View($"{path}/Edit.cshtml", vm);
            }

            var domainModel = _mapper.Map<KeyCharacteristic>(vm);
            domainModel.StageId = _stageId;
            _context.KeyCharacteristics.Update(domainModel);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        private void SetDropdowns(forms.KeyCharacteristicForm model) {
            // get all tags
            List<string> tagDropdowns = new List<string>() {
                TagCategoryHelper.RequirementSource,
            };

            var tagCategories = _context.TagCategories.Include(i => i.Tags).Where(w => tagDropdowns.Contains(w.Key))
                .ToList();
            model.RequirementSourceDropDown = GetDropdown(tagCategories, TagCategoryHelper.RequirementSource);
        }

        private List<SelectListItem> GetDropdown(List<dbModels.TagCategory> categories, string key) =>
            categories.First(f => f.Key == key).GetListItems();
    }
}
