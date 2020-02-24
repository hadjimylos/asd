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

namespace pmo.Controllers {
    [Route("vbpd-projects/{projectId}/stages/{stageNumber}/schedules")]
    public class SchedulesController : BaseStageComponentController {
        private readonly IListService _listService;
        private readonly string path = "~/Views/VBPD/Application/Schedules";

        public SchedulesController(EfContext context, IMapper mapper, IListService listService, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            _listService = listService;
        }

        [Route("detail")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Detail(int projectId, int stageNumber) {
            List<ScheduleForm> viewModel = new List<ScheduleForm>();
            //TODO: Fix possible error
            var model = _context.Schedules.Include(x => x.Stage).Include(x => x.ScheduleType).Where(x => x.StageId == _stageId).ToList();
            var stage = _context.Stages.Find(_stageId);
            var settings = _context.StageConfig_RequiredSchedules.Include(x => x.StageConfig).Include(x => x.RequiredSchedule).Where(x => x.StageConfig.StageNumber == stage.StageNumber).ToList();

            if (model.Count > 0 && settings.Count == model.Count) {
                viewModel = _mapper.Map<List<ScheduleForm>>(model);
            }
            else if (model.Count > 0 && settings.Count != model.Count) {
                viewModel = _mapper.Map<List<ScheduleForm>>(model);

                List<int> SettingsList = settings.OrderBy(x => x.RequiredScheduleTagId).Select(x => x.RequiredScheduleTagId).ToList();
                List<int> ScheduleList = viewModel.OrderBy(x => x.TagId).Select(x => x.TagId).ToList();
                if (!SettingsList.SequenceEqual(ScheduleList)) {
                    List<int> AddedTags = new List<int>();
                    if (SettingsList.Count > ScheduleList.Count) {
                        AddedTags = SettingsList.Except(ScheduleList).ToList();
                    }
                    else {
                        AddedTags = ScheduleList.Except(SettingsList).ToList();
                    }
                    foreach (var tag in AddedTags) {
                        viewModel.Add(new ScheduleForm() { Id = 0, Date = DateTime.Now, StageId = stage.Id, Stage = stage, TagId = tag, ScheduleType = settings.Where(x => x.RequiredScheduleTagId == tag).First().RequiredSchedule });
                    }
                }

            }
            else {
                foreach (var tag in settings) {
                    viewModel.Add(new ScheduleForm() { Id = 0, Date = DateTime.Now, StageId = stage.Id, Stage = stage, TagId = tag.RequiredScheduleTagId, ScheduleType = tag.RequiredSchedule });
                }
            }
            return View($"{path}/Detail.cshtml", viewModel);
        }

        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int projectId, int stageNumber) {
            var stage = _context.Stages.Where(n => n.StageNumber == stageNumber && n.ProjectId == projectId).First();
            List<ScheduleForm> viewModel = new List<ScheduleForm>();
            var model = _context.Schedules
                .Include(x => x.Stage)
                .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
                .Include(x => x.ScheduleType).AsNoTracking().ToList();
            var settings = _context.StageConfig_RequiredSchedules.Include(x => x.StageConfig).Include(x => x.RequiredSchedule).Where(x => x.StageConfig.StageNumber == stageNumber).ToList();

            if (model.Count > 0 && settings.Count == model.Count) {
                viewModel = _mapper.Map<List<ScheduleForm>>(model);
            }
            else if (model.Count > 0 && settings.Count != model.Count) {
                viewModel = _mapper.Map<List<ScheduleForm>>(model);

                List<int> SettingsList = settings.OrderBy(x => x.RequiredScheduleTagId).Select(x => x.RequiredScheduleTagId).ToList();
                List<int> ScheduleList = viewModel.OrderBy(x => x.TagId).Select(x => x.TagId).ToList();
                if (!SettingsList.SequenceEqual(ScheduleList)) {
                    List<int> AddedTags = new List<int>();
                    if (SettingsList.Count > ScheduleList.Count) {
                        AddedTags = SettingsList.Except(ScheduleList).ToList();
                    }
                    else {
                        AddedTags = ScheduleList.Except(SettingsList).ToList();
                    }
                    foreach (var tag in AddedTags) {
                        viewModel.Add(new ScheduleForm() { Id = 0, Date = DateTime.Now, StageId = stage.Id, Stage = stage, TagId = tag, ScheduleType = settings.Where(x => x.RequiredScheduleTagId == tag).First().RequiredSchedule });
                    }
                }

            }
            else {
                foreach (var tag in settings) {
                    viewModel.Add(new ScheduleForm() { Id = 0, Date = DateTime.Now, StageId = stage.Id, Stage = stage, TagId = tag.RequiredScheduleTagId, ScheduleType = tag.RequiredSchedule });
                }
            }
            return View($"{path}/Edit.cshtml", viewModel);
        }

        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Edit(int projectId, int stageNumber, List<ScheduleForm> viewModel) {
            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                return View(viewModel);
            }

            var model = _context.Schedules.Include(x => x.Stage).Where(x => x.Stage.StageNumber == stageNumber && x.Stage.ProjectId == projectId).ToList();

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

            return RedirectToAction("detail", new { projectId, stageNumber });
        }
    }
}