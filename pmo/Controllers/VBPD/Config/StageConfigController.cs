using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace pmo.Controllers {
    [Route("vbpd-admin/stage-config")]
    public class StageConfigController : BaseController {
        private readonly string path = "~/Views/VBPD/Config/StageConfig";

        public StageConfigController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {

        }

        public IActionResult Index() {
            var vm = _mapper.Map<List<StageConfigViewModel>>(_context.StageConfigs.ToList());
            return View($"{path}/Index.cshtml", vm);
        }

        [Route("create")]
        public IActionResult Create() {
            int currentStage = _context.StageConfigs.Count() + 1;
            var stageconfigViewModel = new StageConfigViewModel() { 
                StageNumber = currentStage,
                isCreate = true,
            };
            return View($"{path}/Create.cshtml", stageconfigViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("create")]
        public IActionResult Create(StageConfigViewModel model) {
            model.isCreate = true;
            int currentStage = _context.StageConfigs.AsNoTracking().Count() + 1;
            model.StageNumber = currentStage;

            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                return View($"{path}/Create.cshtml", model);
            }

            var domainModel = _mapper.Map<StageConfig>(model);
            _context.GateConfigs.Add(new GateConfig { GateNumber = model.StageNumber });
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
            vm.GateKeepers = _context.GateConfigs
                .Include(i => i.GateKeepers)
                .First(w => w.GateNumber == config.StageNumber)
                .GateKeepers
                .Select(s => s.Keeper)
                .ToList();

            SetTagList(vm);

            return View($"{path}/Edit.cshtml", vm);
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
                SetTagList(model);
                ViewBag.Errors = ModelState;
                return View($"{path}/Edit.cshtml", model);
            }

            var domainModel = _mapper.Map<StageConfig>(model);

            _context.Update(domainModel);
            _context.SaveChanges();

            var passedSchedules = model.ScheduleIds
                .Select(s => new StageConfig_RequiredSchedule {
                    StageConfigId = model.Id,
                    RequiredScheduleTagId = s,
                }).OrderBy(o => o.RequiredScheduleTagId)
            .ToList();

            var existing = _context.StageConfig_RequiredSchedules.AsNoTracking().Where(r => r.StageConfigId == model.Id);

            // same without ids
            var existingToCompare = existing
                .OrderBy(o => o.RequiredScheduleTagId)
                .ToList();

            var ids1 = passedSchedules.Select(s => s.RequiredScheduleTagId).OrderBy(o => o).ToList();
            var ids2 = existing.Select(s => s.RequiredScheduleTagId).OrderBy(o => o).ToList();

            if (!Enumerable.SequenceEqual(ids1, ids2)) {
                _context.StageConfig_RequiredSchedules.RemoveRange(existing);
                _context.StageConfig_RequiredSchedules.AddRange(passedSchedules);
                _context.SaveChanges();
            }

            return RedirectToAction(actionName: "Index");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("create/gate-keeper")]
        public IActionResult NewGateKeeper(string keeper, int gateNumber, int stageId) {
            _context.GateKeeperConfigs.Add(new GateKeeperConfig {
                Keeper = keeper,
                GateConfigId = _context.GateConfigs
                    .First(f => f.GateNumber == gateNumber)
                    .Id,
            });
            _context.SaveChanges();
            return RedirectToAction(actionName: "Edit", new { id= stageId });
        }

        private void SetTagList(StageConfigViewModel model) {
            var selected = _context.StageConfig_RequiredSchedules
                .Where(w => w.StageConfigId == model.Id).Select(s => s.RequiredScheduleTagId).ToList();

            model.SelectSchedules = _context.TagCategories
                .Include(i => i.Tags).First(f => f.Key == "required-schedules")
                .GetListItems(selected);

            model.ScheduleIds = selected;
        }
    }
}