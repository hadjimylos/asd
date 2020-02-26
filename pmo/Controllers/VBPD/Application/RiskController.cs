using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pmo.Services.Lists;
using ViewModels.Helpers;

namespace pmo.Controllers.Application {
    [Route("vbpd-projects/{projectId}/stages/{stageNumber}/risk")]
    public class RiskController : BaseStageComponentController {
        private readonly IListService _listService;
        private readonly string path = "~/Views/VBPD/Application/Risk";

        public RiskController(EfContext context, IMapper mapper, IListService listService, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            _listService = listService;
        }

        [Route("")]
        public IActionResult Index(int projectId, int stageNumber) {
            ViewBag.ProjectId = projectId;
            ViewBag.StageNumber = stageNumber;
            var vm = _mapper.Map<List<forms.RiskForm>>(
                _context.Risks
                .Include(x => x.Stage)
                .Include(x => x.RiskImpact)
                .Include(x => x.RiskType)
                .Where(x => x.StageId == _stageId).ToList());
            return View($"{path}/Index.cshtml", vm);
        }


        [Route("create")]
        public IActionResult Create(int projectId, int stageNumber) {
            ViewBag.ProjectId = projectId;
            ViewBag.StageNumber = stageNumber;
            var riskViewModel = new forms.RiskForm() {
                isCreate = true,
                StageId = _stageId,
                RiskTypeList = _listService.Tags_SelectList(TagCategoryHelper.RiskType),
                RiskImpactList = _listService.Tags_SelectList(TagCategoryHelper.RiskImpact),

            };
            return View($"{path}/Create.cshtml", riskViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("create")]
        public IActionResult Create(int projectId, int stageNumber, forms.RiskForm riskViewModel) {
            ViewBag.ProjectId = projectId;
            ViewBag.stageNumber = stageNumber;

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
        public IActionResult Edit(int projectId, int stageNumber, int id) {
            ViewBag.ProjectId = projectId;
            ViewBag.StageNumber = stageNumber;
            var risk = _context.Risks.Include(x => x.Stage)
                .Include(x => x.RiskImpact)
                .Include(x => x.RiskType)
                .Where(x => x.Id == id).FirstOrDefault();

            if (risk == null)
                return NotFound();

            var vm = _mapper.Map<forms.RiskForm>(risk);
            vm.isCreate = false;
            vm.RiskTypeList = _listService.Tags_SelectList(TagCategoryHelper.RiskType);
            vm.RiskImpactList = _listService.Tags_SelectList(TagCategoryHelper.RiskImpact);
            return View($"{path}/Edit.cshtml", vm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("{id}")]
        public IActionResult Edit(int projectId, int stageNumber, forms.RiskForm model) {
            ViewBag.ProjectId = projectId;
            ViewBag.stageNumber = stageNumber;
            model.isCreate = false;

            if (!ModelState.IsValid) {
                model.RiskTypeList = _listService.Tags_SelectList(TagCategoryHelper.RiskType);
                model.RiskImpactList = _listService.Tags_SelectList(TagCategoryHelper.RiskImpact);
                ViewBag.Errors = ModelState;
                return View($"{path}/Edit.cshtml", model);
            }
            var domainModel = _mapper.Map<Risk>(model);

            var riskRecord = _context.Risks.First(r => r.Id == domainModel.Id);
            riskRecord.Name = domainModel.Name;
            riskRecord.RiskPropability = domainModel.RiskPropability;
            riskRecord.RiskImpactTagId = domainModel.RiskImpactTagId;
            riskRecord.RiskTypeTagId = domainModel.RiskTypeTagId;
            riskRecord.StageId = _stageId;

            _context.Risks.Update(riskRecord);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}