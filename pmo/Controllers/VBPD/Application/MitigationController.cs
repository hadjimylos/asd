namespace pmo.Controllers {
    using System.Collections.Generic;
    using System.Linq;
    using AutoMapper;
    using dbModels;
    using forms;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using pmo.Services.Lists;
    using ViewModels.Helpers;

    [Route("projects/{projectId}/stages/{stageNumber}/risk/{riskId}/mitigations")]
    public class MitigationController : BaseStageComponentController {
        private readonly IListService _listService;
        private readonly string path = "~/Views/VBPD/Application/Mitigation";
        private readonly int _riskId;

        public MitigationController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            this._riskId = int.Parse(ViewModels.Helpers.Helpers.GetRouteValue(httpContextAccessor.HttpContext.Request, "riskId"));
        }

        [Route("create")]
        public IActionResult Create() {
            var model = new MitigationForm() { RiskId = this._riskId };
            model.UsersDropDown = new SelectList(_context.Users.ToList(), "Id", "DisplayName");
            return View($"{path}/Create.cshtml", model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("create")]
        public IActionResult Create(MitigationForm form) {
            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                form.UsersDropDown = new SelectList(_context.Users.ToList(), "Id", "DisplayName",form.ResponsibilityUserId);
                return View($"{path}/Create.cshtml", form);
            }

            var newForm = _mapper.Map<Mitigation>(form);
            _context.Mitigations.Add(newForm);
            _context.SaveChanges();
            return RedirectToAction("Index", "Risk", new { projectId = _projectId, stageNumber = _stageNumber });
        }

        [Route("{id}")]
        public IActionResult Edit(int id) {
            var dbMitigation = _context.Mitigations.Include(i=>i.ResponsibilityUser).First(f => f.Id == id);
            var model = _mapper.Map<MitigationForm>(dbMitigation);
            model.UsersDropDown = new SelectList(_context.Users.ToList(), "Id", "DisplayName", dbMitigation.ResponsibilityUserId);
            return View($"{path}/Edit.cshtml", model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("{id}")]
        public IActionResult Edit(MitigationForm form, int id) {
            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                form.UsersDropDown = new SelectList(_context.Users.ToList(), "Id", "DisplayName", form.ResponsibilityUserId);
                return View($"{path}/Edit.cshtml", form);
            }

            var notTracked = _mapper.Map<Mitigation>(form);
            var tracked = _context.Mitigations.Find(id);
            tracked.PrepForUpdate(notTracked);
            
            _context.Mitigations.Update(notTracked);
            _context.SaveChanges();
            return RedirectToAction("Index", "Risk", new { projectId = _projectId, stageNumber = _stageNumber });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("{id}/delete")]
        public IActionResult Delete(int id) {
            _context.Mitigations.Remove(_context.Mitigations.Find(id));
            _context.SaveChanges();
            return RedirectToAction("Index", "Risk", new { projectId = _projectId, stageNumber = _stageNumber });
        }
    }
}