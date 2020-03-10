using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dbModels;
using forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pmo.Services.Lists;
using ViewModels.Helpers;

namespace pmo.Controllers {
    [Route("vbpd-projects/{projectId}/stages/{stageNumber}/schedules")]
    public class SchedulesController : BaseStageComponentController {
        private readonly IListService _listService;
        private readonly string path = "~/Views/VBPD/Application/Schedules";

        public SchedulesController(EfContext context, IMapper mapper, IListService listService, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            _listService = listService;
        }

        [Route("detail")]
        public IActionResult Detail() {
            var model = _context.Schedules.IncludeAll()
                .Where(w => w.StageId == _stageId).ToList();

            return View($"{path}/Detail.cshtml", model);
        }

        [Route("edit")]
        public IActionResult Edit() {
            var schedules = _context.Schedules
                .Where(w => w.StageId == _stageId).ToList();
            
            var configs = !_isLite ?
                _context.StageConfig_RequiredSchedules.Where(
                    w =>
                        w.StageConfigId == _context.StageConfigs
                            .First(f => f.StageNumber == _stageNumber).Id
                    )
                .Include(i => i.RequiredSchedule)
                .Select (
                        s => new {
                            name = s.RequiredSchedule,
                            tagid = s.RequiredScheduleTagId
                        })
                    .ToList() :
                _context.LiteRequiredSchedules.Where(
                    w =>
                        w.StageConfigId == _context.LiteStageConfigs
                            .First(f => f.StageNumber == _stageNumber).Id
                    )
                .Include(i => i.RequiredSchedule)
                .Select(
                        s => new {
                            name = s.RequiredSchedule,
                            tagid = s.RequiredScheduleTagId
                        })
                    .ToList();

            var model = _mapper.Map<List<ScheduleForm>>(schedules);
            var missing = configs.Where(w => !schedules.Select(s => s.TagId).Contains(w.tagid)).ToList();
            missing.ForEach(
                f =>
                    model.Add(new ScheduleForm {
                        TagId = f.tagid,
                        ScheduleType = f.name,
                        StageId = _stageId,
                    })
            );

            return View($"{path}/Edit.cshtml", model);
        }

        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Edit(List<ScheduleForm> viewModel) {
            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                return View(viewModel);
            }

            var model = _context.Schedules.Include(x => x.Stage).Where(x => x.Stage.StageNumber == _stageNumber && x.Stage.ProjectId == _projectId).ToList();

            if (model.Count > 0) {
                foreach (var item in viewModel) {
                    if (item.Id != 0) {
                        model.Where(x => x.Id == item.Id).First().Date = item.Date;
                    }
                    else {
                        _context.Schedules.Add(item);
                    }
                }
                _context.Schedules.UpdateRange(model);
                _context.SaveChanges();

            }
            else {
                var modelToInsert = _mapper.Map<List<Schedule>>(viewModel);
                _context.Schedules.AddRange(modelToInsert);
                _context.SaveChanges();
            }

            return this._editAction;
        }
    }
}