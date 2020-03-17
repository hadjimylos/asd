using AutoMapper;
using dbModels;
using forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace pmo.Controllers {
    [Route("vbpd-admin/stage-config")]
    public class StageConfigController : BaseController {
        private readonly string path = "~/Views/VBPD/Config/StageConfig";

        public StageConfigController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {

        }

        public IActionResult Index() {
            var vm = _context.StageConfigs.Include(s=>s.RequiredSchedules).ToList();
            return View($"{path}/Index.cshtml", vm);
        }

        [Route("create")]
        public IActionResult Create() {
            int currentStage = _context.StageConfigs.Count() + 1;
            var stageconfigViewModel = new StageConfigForm() {
                StageNumber = currentStage,
                isCreate = true,
            };
            
            return View($"{path}/Create.cshtml", stageconfigViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("create")]
        public IActionResult Create(StageConfigForm model) {
            model.isCreate = true;
            int currentStage = _context.StageConfigs.AsNoTracking().Count() + 1;
            model.StageNumber = currentStage;

            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                return View($"{path}/Create.cshtml", model);
            }

            var domainModel = _mapper.Map<StageConfig>(model);
            domainModel.MinProjectJustifications = model.MinProjectJustificationsCheck ? 1:0;
            domainModel.MinBusinessCases = model.MinBusinessCasesCheck ?1:0;
            domainModel.MinProductInfringementPatentabilities = model.MinProductInfringementPatentabilitiesCheck ?1:0;
            domainModel.MinKeyCharacteristics = model.MinKeyCharacteristicsCheck ?1:0;
            domainModel.MinCustomerDesignApprovals = model.MinCustomerDesignApprovalsCheck ?1:0;
            domainModel.MinInvestmentPlans = model.MinInvestmentPlansCheck ?1:0;
            domainModel.MinProductIntroChecklist = model.MinProductIntroChecklistCheck ?1:0;
            domainModel.MinPostLaunchReviews = model.MinPostLaunchReviewsCheck ?1:0;

            _context.StageConfigs.Add(domainModel);
            _context.SaveChanges();

            return RedirectToAction("Edit", new { id = domainModel.Id });
        }

      

        [Route("{id}")]
        public IActionResult Edit(int id) {
            var config = _context.StageConfigs
                .Include(i => i.GateKeeperConfigs).First(f => f.Id == id);

            if (config == null)
                return NotFound();

            var vm = _mapper.Map<StageConfigForm>(config);
            vm.isCreate = false;
            vm.MinProjectJustificationsCheck = config.MinProjectJustifications == 0 ? false : true;
            vm.MinBusinessCasesCheck = config.MinBusinessCases == 0 ? false : true;
            vm.MinProductInfringementPatentabilitiesCheck = config.MinProductInfringementPatentabilities == 0 ? false : true;
            vm.MinKeyCharacteristicsCheck = config.MinKeyCharacteristics == 0 ? false : true;
            vm.MinCustomerDesignApprovalsCheck = config.MinCustomerDesignApprovals == 0 ? false : true;
            vm.MinInvestmentPlansCheck = config.MinInvestmentPlans == 0 ? false : true;
            vm.MinProductIntroChecklistCheck = config.MinProductIntroChecklist == 0 ? false : true;
            vm.MinPostLaunchReviewsCheck = config.MinPostLaunchReviews == 0 ? false : true;

            vm.GateKeepers = config.GateKeeperConfigs
                .Select(s => s.Keeper)
                .ToList();

            SetTagList(vm);

            return View($"{path}/Edit.cshtml", vm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("{id}")]
        public IActionResult Edit(StageConfigForm model) {
            // populate stage number as is from DB
            model.StageNumber = _context.StageConfigs
                .AsNoTracking()
                .First(
                    config => config.Id == model.Id
                ).StageNumber;

            model.isCreate = false;

            if (!ModelState.IsValid) {
                SetTagList(model);
                ViewBag.Errors = ModelState;
                return View($"{path}/Edit.cshtml", model);
            }

            var domainModel = _mapper.Map<StageConfig>(model);
            domainModel.MinProjectJustifications = model.MinProjectJustificationsCheck ? 1 : 0;
            domainModel.MinBusinessCases = model.MinBusinessCasesCheck ? 1 : 0;
            domainModel.MinProductInfringementPatentabilities = model.MinProductInfringementPatentabilitiesCheck ? 1 : 0;
            domainModel.MinKeyCharacteristics = model.MinKeyCharacteristicsCheck ? 1 : 0;
            domainModel.MinCustomerDesignApprovals = model.MinCustomerDesignApprovalsCheck ? 1 : 0;
            domainModel.MinInvestmentPlans = model.MinInvestmentPlansCheck ? 1 : 0;
            domainModel.MinProductIntroChecklist = model.MinProductIntroChecklistCheck ? 1 : 0;
            domainModel.MinPostLaunchReviews = model.MinPostLaunchReviewsCheck ? 1 : 0;

            _context.Entry(domainModel).State = EntityState.Modified;
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
                StageConfigId = _context.StageConfigs
                    .First(f => f.StageNumber == gateNumber)
                    .Id,
            });
            _context.SaveChanges();
            return RedirectToAction(actionName: "Edit", new { id = stageId });
        }

        private void SetTagList(StageConfigForm model) {
            var selected = _context.StageConfig_RequiredSchedules
                .Where(w => w.StageConfigId == model.Id).Select(s => s.RequiredScheduleTagId).ToList();

            model.SelectSchedules = _context.TagCategories
                .Include(i => i.Tags).First(f => f.Key == "required-schedules")
                .GetListItems(selected);

            model.ScheduleIds = selected;
        }
    }
}