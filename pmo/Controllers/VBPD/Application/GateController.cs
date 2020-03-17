namespace pmo.Controllers {
    using AutoMapper;
    using dbModels;
    using forms;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;

    [Route("projects/{projectId}/gates")]
    public class GateController : BaseProjectDetailController {
        private readonly string path = "~/Views/VBPD/Application/Gate";
        private Gate _currentGate;

        public GateController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            this._currentGate = _context.Gates.Where(g=>g.ProjectId==_projectId)
                .Include(i => i.StageConfig)
                .OrderByDescending(o => o.CreateDate).FirstOrDefault();
        }

        [Route("move-to-gate")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult MoveToGate(int projectId) {
            // set all stages to complete
            var closingStage = _context.Stages.First(w => w.ProjectId == projectId && !w.IsComplete);
            closingStage.IsComplete = true;
            _context.Entry(closingStage).State = EntityState.Modified;

            // create gate for this closing stage
            var currentStageConfigId = _context.StageConfigs
                .First(
                    f =>
                        f.StageNumber == closingStage.StageNumber
                ).Id;

            // create new open gate
            _context.Gates.Add(new Gate {
                Decision = GateDecisionType.Open,
                ProjectId = projectId,
                StageConfigId = currentStageConfigId,
            });

            _context.SaveChanges();
            return Redirect($"/projects/{_projectId}");
        }

        [Route("go-decision")]
        public IActionResult GoDecision() {
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
                    s.UserId,
                })
                .ToList() :
                _context.GateKeeperLites
                .Where(w => w.GateId == _currentGate.Id)
                .Select(s => new {
                    GateKeeperLabel = s.GateKeeperConfig.Keeper,
                    s.GateKeeperConfigId,
                    s.UserId,
                })
                .ToList();

            var model = _mapper.Map<GateForm>(_currentGate);

            // apply defaults for save else load as is
            if (gateKeepers.Count > 0) {
                gateKeepers.ForEach(
                    f =>
                        model.GateKeepersNew.Add(new GateKeeperForm
                        {
                            Label = f.GateKeeperLabel,
                            GateKeeperConfigId = f.GateKeeperConfigId,
                            UserDropDown = new SelectList(_context.Users.ToList(), "Id", "DisplayName"),
                            UserId =f.UserId
                        }));

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
                            GateKeeperConfigId = f.Id,
                            UserDropDown = new SelectList(_context.Users.ToList(), "Id", "DisplayName")
                        })
                );
            }

            return View($"{path}/MoveToGo.cshtml", model);
        }

        [Route("go-decision")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public  IActionResult GoDecision(GateForm model) {
            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                model.GateKeepersNew.ForEach(f=> 
                    f.UserDropDown = new SelectList(_context.Users.ToList(), "Id", "DisplayName")
                );
                return View($"{path}/MoveToGo.cshtml", model);
            }

            if(!_isLite) {
                // remove all references
                _context.RemoveRange(_context.GateKeepers.Where(w => w.GateId == _currentGate.Id).ToList());

                // insert all new references
                model.GateKeepersNew.ForEach(
                    f =>
                        _context.GateKeepers.Add(new GateKeeper {
                            UserId = f.UserId,
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
                            UserId = f.UserId,
                            GateId = _currentGate.Id,
                            GateKeeperConfigId = f.GateKeeperConfigId,
                        })
                );
            }

            _currentGate.ActualReviewDate = (DateTime)model.ActualReviewDate;
            _currentGate.Comments = model.Comments;
            _currentGate.Decision = GateDecisionType.PendingDecision;

            _context.Entry(_currentGate).State = EntityState.Modified;
            _context.SaveChanges();


            // continue to Go Decision logic:

            // add comment if necessary
            if (!string.IsNullOrEmpty(model.Comments)) {
                _context.GateComments.Add(new GateComment {
                    GateId = _currentGate.Id,
                    Comment = model.Comments,
                    DecisionType = GateDecisionType.Go,
                });
            }

            // alter decision
            _currentGate.Decision = GateDecisionType.Go;

            // create new stage
            _context.Stages.Add(new Stage {
                ProjectId = _projectId,
                StageNumber = _currentGate.StageConfig.StageNumber + 1,
            });

            // add project state history
            this.ChangeProjectState(ProjectState.Go);

            _context.Entry(_currentGate).State = EntityState.Modified;
            _context.SaveChanges();

            return Redirect($"/projects/{_projectId}");
        }

        [Route("on-hold-decision")]
        public IActionResult AddHold() {
            return View($"{path}/OnHoldDecision.cshtml");
        }

        [Route("on-hold-decision")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult AddHold(string comment) {
            if (string.IsNullOrEmpty(comment)) {
                ModelState.AddModelError("comment", "This is a required field.");
                ViewBag.Errors = ModelState;
                return View($"{path}/OnHoldDecision.cshtml");
            }

            // add comment
            _context.GateComments.Add(new GateComment {
                GateId = _currentGate.Id,
                Comment = comment,
                DecisionType = GateDecisionType.OnHold,
            });

            //add on hold on project
            _currentGate.Decision = GateDecisionType.OnHold;
            this.ChangeProjectState(ProjectState.OnHold);

            _context.Entry(_currentGate).State = EntityState.Modified;
            _context.SaveChanges();
            
            return Redirect($"/projects/{_projectId}");
        }

        [Route("remove-hold")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult RemoveHold() {
            // remove hold from project 
            _currentGate.Decision = GateDecisionType.PendingDecision;
            this.ChangeProjectState(ProjectState.Go);

            _context.Entry(_currentGate).State = EntityState.Modified;
            _context.SaveChanges();

            return Redirect($"/projects/{_projectId}");
        }

        [Route("close-decision")]
        public IActionResult AddClose () {
            return View($"{path}/CloseDecision.cshtml");
        }

        [Route("close-decision")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult AddClose(string comment) {
            if (string.IsNullOrEmpty(comment)) {
                ModelState.AddModelError("comment", "This is a required field.");
                ViewBag.Errors = ModelState;
                return View($"{path}/CloseDecision.cshtml");
            }

            // add comment
            _context.GateComments.Add(new GateComment {
                GateId = _currentGate.Id,
                Comment = comment,
                DecisionType = GateDecisionType.Closed,
            });

            _currentGate.Decision = GateDecisionType.Closed;
            this.ChangeProjectState(ProjectState.Closed);

            _context.Entry(_currentGate).State = EntityState.Modified;
            _context.SaveChanges();
            
            return Redirect($"/projects/{_projectId}");
        }

        [Route("remove-close")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Close() {
            // Reopen project  
            _currentGate.Decision = GateDecisionType.PendingDecision;
            this.ChangeProjectState(ProjectState.Go);
                
            _context.Entry(_currentGate).State = EntityState.Modified;
            _context.SaveChanges();
            
            return Redirect($"/projects/{_projectId}");
        }

        [Route("complete")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Complete() {
            //close project
            _currentGate.Decision = GateDecisionType.Complete;
            this.ChangeProjectState(ProjectState.Complete);

            _context.Entry(_currentGate).State = EntityState.Modified;
            _context.SaveChanges();

            return Redirect($"/projects/{_projectId}");
        }

        private void ChangeProjectState(ProjectState projectState) {
            _context.ProjectStateHistories.Add(new ProjectStateHistory {
                ProjectId = _projectId,
                ProjectState = projectState,
            });
        }
    }
}