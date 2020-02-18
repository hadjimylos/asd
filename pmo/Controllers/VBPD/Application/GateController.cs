namespace pmo.Controllers {
    using AutoMapper;
    using dbModels;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using ViewModels;

    [Route("vbpd-projects/{projectId}/gates")]
    public class GateController : BaseProjectDetailController {
        private readonly string path = "~/Views/VBPD/Application/Gate";
        private Gate _currentGate;

        public GateController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            this._currentGate = _context.Gates
                .OrderByDescending(o => o.CreateDate)
                .First(f => f.Decision == GateDecisionType.Open);
        }

        [Route("edit")]
        public IActionResult Edit() {
            var gateKeeperConfigs = _context.GateKeeperConfigs
                .Where(w => w.GateConfigId == _currentGate.GateConfigId)
                .ToList();

            var gateKeepers = _context.GateKeepers
                .Where(w => w.GateId == _currentGate.Id)
                .ToList();

            var model = _mapper.Map<GateViewModel>(_currentGate);

            // apply defaults for save else load as is
            if (gateKeepers.Count > 0) {
                gateKeepers.ForEach(
                    f =>
                        model.GateKeepersNew.Add(new GateKeeperViewModel {
                            Label = f.GateKeeperConfig.Keeper,
                            GateKeeperConfigId = f.GateKeeperConfigId,
                            GateKeeperName = f.GateKeeperName,
                        })
                );
            } else {
                // initialize as empty  
                gateKeeperConfigs.ForEach(
                    f =>
                        model.GateKeepersNew.Add(new GateKeeperViewModel {
                            Label = f.Keeper,
                            GateKeeperConfigId = f.Id
                        })
                );
            }

            return View($"{path}/Edit.cshtml", model);
        }

        [Route("move-to-gate")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult MoveToGate(int projectId) {
            // set all stages to complete
            var closingStage = _context.Stages.First(w => w.ProjectId == projectId && !w.IsComplete);
            closingStage.IsComplete = true;
            var openingGateConfig = _context.GateConfigs
                .First (
                    f =>
                        f.GateNumber == closingStage.StageNumber
                );

            // create new open gate
            _context.Gates.Add(new Gate {
                Decision =  GateDecisionType.Open,
                ProjectId = projectId,
                GateConfigId = openingGateConfig.Id,
            });
            
            _context.SaveChanges();
            return RedirectToAction("Edit");
        }

        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Edit(GateViewModel model) {
            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                return View($"{path}/Edit.cshtml", model);
            }

            // remove all references
            _context.RemoveRange(_context.GateKeepers.Where(w => w.GateId == _currentGate.Id).ToList());

            // insert all new references
            model.GateKeepersNew.ForEach (
                f =>
                    _context.GateKeepers.Add(new GateKeeper {
                        GateKeeperName = f.GateKeeperName,
                        GateId = _currentGate.Id,
                        GateKeeperConfigId = f.GateKeeperConfigId,
                    })
            );

            _currentGate.ActualReviewDate = (DateTime)model.ActualReviewDate;
            _currentGate.Comments = model.Comments;
            _currentGate.Decision = GateDecisionType.PendingDecision;

            _context.SaveChanges();
            return View($"{path}/Edit.cshtml", model);
        }
    }
}