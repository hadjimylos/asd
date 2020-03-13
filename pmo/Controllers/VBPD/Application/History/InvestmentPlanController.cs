using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using ViewModels.Helpers;


namespace pmo.Controllers.Application.History
{
    [Route("projects/{projectid}/stages/{stageNumber}/investment-plan")]
    public class InvestmentPlanController : BaseStageComponentController {
        private readonly string path = "~/Views/VBPD/Application/InvestmentPlan";
        public InvestmentPlanController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
        }

        [Route("")]
        public IActionResult Index() {
            var vm = _context.InvestmentPlans.IncludeAll()
                .Where(x => x.StageId == _stageId)
                .ToList();

            return View($"{path}/Index.cshtml", vm);
        }

        [Route("create")]
        public IActionResult Create() {
            var ip = new forms.InvestmentPlanForm() {
                StageId = _stageId
            };
            return View($"{path}/Create.cshtml", ip);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("create")]
        public IActionResult Create(forms.InvestmentPlanForm ipform) {
            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                return View($"{path}/Create.cshtml", ipform);
            }
            var domainModel = _mapper.Map<InvestmentPlan>(ipform);
            domainModel.StageId = _stageId;
            _context.InvestmentPlans.Add(domainModel);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }


        [Route("{id}")]
        public IActionResult Edit(int id) {
            var ip = _context.InvestmentPlans
                .Include(s => s.Stage)
                .Where(s => s.Id == id)
                .FirstOrDefault();
            if (ip == null)
                return NotFound();

            var vm = _mapper.Map<forms.InvestmentPlanForm>(ip);
            return View($"{path}/Edit.cshtml", vm);
        }

        [HttpPost]
        [Route("{id}")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(forms.InvestmentPlanForm vm, int id) {
            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                return View($"{path}/Edit.cshtml", vm);
            }

            var domainModel = _mapper.Map<InvestmentPlan>(vm);
            domainModel.StageId = _stageId;
            _context.InvestmentPlans.Update(domainModel);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}