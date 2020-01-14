using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace pmo.Controllers {
    [Route("admin/stage-config")]
    public class AdminStageConfigController : BaseController {

        public AdminStageConfigController(EfContext context, IMapper mapper) : base(context, mapper) {

        }

        [Route("")]
        public IActionResult Create() {
            List<StageConfigViewModel> stageconfigViewModel = new List<StageConfigViewModel>();
            return View(stageconfigViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("")]
        public IActionResult Create(StageConfigViewModel stageConfigViewModel) {
            if (!ModelState.IsValid) {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                var all_errors = string.Join("/n", errors);
                ModelState.AddModelError("error_partial_view", all_errors);
                return View(stageConfigViewModel);
            }

            var domainModel = _mapper.Map<StageConfigViewModel>(stageConfigViewModel);
            TryValidateModel(domainModel);
            _context.Add(domainModel);
            _context.SaveChanges();

            return RedirectToAction(
                actionName: "Index",
                controllerName: "Admin"
            );
        }

        [Route("{id}")]
        public IActionResult Edit(int id) {
            var item = _context.StageConfigs.Find(id);

            if (item != null) {
                return View(item);
            }

            return NotFound();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("{id}")]
        public IActionResult Edit(StageConfigViewModel stageConfigViewModel) {
            if (!ModelState.IsValid) {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                var all_errors = string.Join("/n", errors);
                ModelState.AddModelError("error_partial_view", all_errors);
                return View(stageConfigViewModel);
            }
            var domainModel = _mapper.Map<StageConfigViewModel>(stageConfigViewModel);
            TryValidateModel(domainModel);
            _context.Update(domainModel);
            _context.SaveChanges();

            return RedirectToAction(
                actionName: "Index",
                controllerName: "Admin"
            );
        }
    }
}