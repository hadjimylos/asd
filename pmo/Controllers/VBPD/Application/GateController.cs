namespace pmo.Controllers {
    using AutoMapper;
    using dbModels;
    using forms;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    [Route("vbpd-projects/{projectId}/gates")]
    public class GateController : BaseProjectDetailController {
        private readonly string path = "~/Views/VBPD/Application/Gate";
        private Gate _currentGate;

        public GateController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            this._currentGate = _context.Gates.Where(g=>g.ProjectId==_projectId)
                .Include(i => i.StageConfig)
                .OrderByDescending(o => o.CreateDate).FirstOrDefault();
        }

        [Route("edit")]
        public IActionResult Edit() { 
            var gateKeeperConfigs = !_isLite ? 
                _context.StageConfigs
                    .Include(i => i.GateKeeperConfigs)
                    .First(f => f.StageNumber == _currentGate.StageConfig.StageNumber)
                    .GateKeeperConfigs.Select(s => new { s.Id, s.Keeper }).ToList() :
                _context.LiteStageConfigs
                    .Include(i => i.GateKeeperConfigs)
                    .First(f => f.StageNumber == _currentGate.StageConfig.StageNumber)
                    .GateKeeperConfigs.Select(s => new { s.Id, s.Keeper }).ToList();

            var gateKeepers = !_isLite ?
                _context.GateKeepers
                .Where(w => w.GateId == _currentGate.Id)
                .Select(s => new {
                    GateKeeperLabel = s.GateKeeperConfig.Keeper,
                    s.GateKeeperConfigId,
                    s.GateKeeperName,
                })
                .ToList() :
                _context.GateKeeperLites
                .Where(w => w.GateId == _currentGate.Id)
                .Select(s => new {
                    GateKeeperLabel = s.GateKeeperConfig.Keeper,
                    s.GateKeeperConfigId,
                    s.GateKeeperName,
                })
                .ToList();

            var model = _mapper.Map<GateForm>(_currentGate);

            // apply defaults for save else load as is
            if (gateKeepers.Count > 0) {
                gateKeepers.ForEach(
                    f =>
                        model.GateKeepersNew.Add(new GateKeeperForm {
                            Label = f.GateKeeperLabel,
                            GateKeeperConfigId = f.GateKeeperConfigId,
                            GateKeeperName = f.GateKeeperName,
                        })
                );

                // append any missing new configs
                var newlyAddedConfigs = gateKeeperConfigs.Where ( 
                    w => !gateKeepers.Select(s => s.GateKeeperConfigId).ToList().Contains(w.Id)
                ).ToList();

                newlyAddedConfigs.ForEach (
                    f =>
                        model.GateKeepersNew.Add(new GateKeeperForm {
                            Label = f.Keeper,
                            GateKeeperConfigId = f.Id
                        })
                    );
            } else {
                // initialize as empty  
                gateKeeperConfigs.ForEach(
                    f =>
                        model.GateKeepersNew.Add(new GateKeeperForm {
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

            // create gate for this closing stage
            var currentStageConfigId = _context.StageConfigs
                .First (
                    f =>
                        f.StageNumber == closingStage.StageNumber
                ).Id;

            // create new open gate
            _context.Gates.Add(new Gate {
                Decision =  GateDecisionType.Open,
                ProjectId = projectId,
                StageConfigId = currentStageConfigId,
            });
            
            _context.SaveChanges();
            return RedirectToAction("Edit");
        }

        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public  IActionResult Edit(GateForm model) {
            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                return View($"{path}/Edit.cshtml", model);
            }

            if(!_isLite) {
                // remove all references
                _context.RemoveRange(_context.GateKeepers.Where(w => w.GateId == _currentGate.Id).ToList());

                // insert all new references
                model.GateKeepersNew.ForEach(
                    f =>
                        _context.GateKeepers.Add(new GateKeeper {
                            GateKeeperName = f.GateKeeperName,
                            GateId = _currentGate.Id,
                            GateKeeperConfigId = f.GateKeeperConfigId,
                        })
                );
            } else {
                // remove all references
                _context.RemoveRange(_context.GateKeeperLites.Where(w => w.GateId == _currentGate.Id).ToList());

                // insert all new references
                model.GateKeepersNew.ForEach(
                    f =>
                        _context.GateKeeperLites.Add(new GateKeeperLite {
                            GateKeeperName = f.GateKeeperName,
                            GateId = _currentGate.Id,
                            GateKeeperConfigId = f.GateKeeperConfigId,
                        })
                );
            }

            _currentGate.ActualReviewDate = (DateTime)model.ActualReviewDate;
            _currentGate.Comments = model.Comments;
            _currentGate.Decision = GateDecisionType.PendingDecision;

             _context.SaveChanges();
            return RedirectToAction("edit");
        }

        [Route("go")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Go() {
            // alter decision
            _currentGate.Decision = GateDecisionType.Go;

            // create new stage
            _context.Stages.Add(new Stage {
                ProjectId = _projectId,
                StageNumber = _currentGate.StageConfig.StageNumber + 1,
            });

            // add project state history
            this.ChangeProjectState(ProjectState.Go);

            _context.SaveChanges();

            return Redirect($"/vbpd-projects/{_projectId}");
        }

        [Route("close")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        //Remove Close && add Close  --> both handle here
        public IActionResult Close() {
            if (_currentGate.Decision == GateDecisionType.Closed) {
                // Reopen project  
                _currentGate.Decision = GateDecisionType.PendingDecision;
                this.ChangeProjectState(ProjectState.Go);
                _context.SaveChanges();
            }
            else {
                //close project
                _currentGate.Decision = GateDecisionType.Closed;
                this.ChangeProjectState(ProjectState.Closed);
                _context.SaveChanges();
            }
            return Redirect($"/vbpd-projects/{_projectId}");
        }

        [Route("on-hold")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        //Remove Hold && add On Hold  --> both handle here
        public IActionResult AddRemoveHold() {
            if (_currentGate.Decision == GateDecisionType.OnHold) {
                // remove hold from project 
                _currentGate.Decision = GateDecisionType.PendingDecision;
                this.ChangeProjectState(ProjectState.Go);
                _context.SaveChanges();
            }
            else  {
                //add on hold on project
                _currentGate.Decision = GateDecisionType.OnHold;
                this.ChangeProjectState(ProjectState.OnHold);
                _context.SaveChanges();
            }
            return Redirect($"/vbpd-projects/{_projectId}");
        }

        private void ChangeProjectState(ProjectState projectState) {
            _context.ProjectStateHistories.Add(new ProjectStateHistory {
                ProjectId = _projectId,
                ProjectState = projectState,
            });
        }
    }
}