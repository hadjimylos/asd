using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using pmo.Models.Helpers;
using pmo.Services.Lists;
using ViewModels.Helpers;

namespace pmo.Controllers.Application {
    [Route("projects/{projectId}/stages/{stageNumber}/risk")]
    public class RiskController : BaseStageComponentController {
        private readonly IListService _listService;
        private readonly string path = "~/Views/VBPD/Application/Risk";

        public RiskController(EfContext context, IMapper mapper, IListService listService, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            _listService = listService;
        }

        [Route("")]
        public IActionResult Index() {
            var vm = _context.Risks
                .Include(x => x.Stage)
                .Include(x => x.RiskImpact)
                .Include(x => x.RiskType)
                .Include(i => i.Mitigations).ThenInclude(i=>i.ResponsibilityUser)
                .Where(x => x.StageId == _stageId).ToList();

            return View($"{path}/Index.cshtml", vm);
        }

        [Route("detail")]
        public IActionResult Detail() {
            var risk = _context.Risks
                .Where(w => w.StageId == _stageId)
                .IncludeAll()
                .ToList();

            return View($"{path}/Detail.cshtml", risk);

        }

        [Route("create")]
        public IActionResult Create() {
            var riskViewModel = new forms.RiskForm() {
                StageId = _stageId,
                RiskTypeList = _listService.Tags_SelectList(TagCategoryHelper.RiskType),
                RiskImpactList = _listService.Tags_SelectList(TagCategoryHelper.RiskImpact),

            };
            return View($"{path}/Create.cshtml", riskViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("create")]
        public IActionResult Create(forms.RiskForm riskViewModel) {
            if (!ModelState.IsValid) {
                riskViewModel.RiskTypeList = _listService.Tags_SelectList(TagCategoryHelper.RiskType);
                riskViewModel.RiskImpactList = _listService.Tags_SelectList(TagCategoryHelper.RiskImpact);
                ViewBag.Errors = ModelState;
                return View($"{path}/Create.cshtml", riskViewModel);
            }

            var domainModel = _mapper.Map<Risk>(riskViewModel);
            domainModel.StageId = _stageId;
            _context.Risks.Add(domainModel);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [Route("{id}")]
        public IActionResult Edit(int id) {
            var risk = _context.Risks.Include(x => x.Stage)
                .Include(x => x.RiskImpact)
                .Include(x => x.RiskType)
                .Where(x => x.Id == id).FirstOrDefault();

            if (risk == null)
                return NotFound();

            var vm = _mapper.Map<forms.RiskForm>(risk);
            vm.RiskTypeList = _listService.Tags_SelectList(TagCategoryHelper.RiskType);
            vm.RiskImpactList = _listService.Tags_SelectList(TagCategoryHelper.RiskImpact);
            return View($"{path}/Edit.cshtml", vm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("{id}")]
        public IActionResult Edit(forms.RiskForm model, int id) {
            if (!ModelState.IsValid) {
                model.RiskTypeList = _listService.Tags_SelectList(TagCategoryHelper.RiskType);
                model.RiskImpactList = _listService.Tags_SelectList(TagCategoryHelper.RiskImpact);
                ViewBag.Errors = ModelState;
                return View($"{path}/Edit.cshtml", model);
            }

            var notTracked = _mapper.Map<Risk>(model);
            notTracked.StageId = _stageId;
            var tracked = _context.Risks.Find(id);
            tracked.PrepForUpdate(notTracked);

            _context.Risks.Update(tracked);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}