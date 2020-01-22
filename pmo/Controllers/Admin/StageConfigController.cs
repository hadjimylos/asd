using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace pmo.Controllers {
    [Route("admin/stage-config")]
    public class StageConfigController : BaseController {

        public StageConfigController(EfContext context, IMapper mapper) : base(context, mapper) {

        }

        public IActionResult Index() {
            var vm = _mapper.Map<List<StageConfigViewModel>>(_context.StageConfigs.ToList());
            return View(vm);
        }

        [Route("create")]
        public IActionResult Create() {
            int currentStage = _context.StageConfigs.Count() + 1;
            var stageconfigViewModel = new StageConfigViewModel() { 
                StageNumber = currentStage,
                isCreate = true,
            };
            return View(stageconfigViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("create")]
        public IActionResult Create(StageConfigViewModel stageConfigViewModel) {
            stageConfigViewModel.isCreate = true;
            int currentStage = _context.StageConfigs.AsNoTracking().Count() + 1;
            stageConfigViewModel.StageNumber = currentStage;

            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                return View(stageConfigViewModel);
            }

            var domainModel = _mapper.Map<StageConfig>(stageConfigViewModel);
            _context.StageConfigs.Add(domainModel);
            _context.SaveChanges();

            return RedirectToAction ("Edit", new { id = domainModel.Id });
        }

        [Route("{id}")]
        public IActionResult Edit(int id) {
            var config = _context.StageConfigs.Find(id);

            if (config == null)
                return NotFound();

            var vm = _mapper.Map<StageConfigViewModel>(config);
            vm.isCreate = false;
            return View(vm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("{id}")]
        public IActionResult Edit(StageConfigViewModel model) {
            // populate stage number as is from DB
            model.StageNumber = _context.StageConfigs
                .AsNoTracking()
                .First (
                    config => config.Id == model.Id
                ).StageNumber;

            model.isCreate = false;

            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                return View(model);
            }

            var domainModel = _mapper.Map<StageConfig>(model);
            _context.Update(domainModel);
            _context.SaveChanges();

            return RedirectToAction(actionName: "Index");
        }
    }
}