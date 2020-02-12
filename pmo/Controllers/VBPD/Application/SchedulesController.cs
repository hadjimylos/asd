using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pmo.Services.Lists;
using ViewModels;

namespace pmo.Controllers.Application
{
    [Route("vbpd-projects/{projectId}/stages/{StageNumber}/schedules")]
    public class SchedulesController : BaseController
    {
        private readonly IListService _listService;

        public SchedulesController(EfContext context, IMapper mapper, IListService listService, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
            _listService = listService;
        }   

        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int projectId, int stageNumber)
        {
            var stage = _context.Stages.Where(n => n.StageNumber == stageNumber && n.ProjectId == projectId).First();
            ViewBag.ProjectId = projectId;
            ViewBag.StageNumber = stageNumber;
            List<SchedulesViewModel> viewModel = new List<SchedulesViewModel>();
            var model = _context.Schedules
                .Include(x => x.Stage)
                .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
                .Include(x => x.ScheduleType).ToList();
            var settings = _context.StageConfig_RequiredSchedules.Include(x => x.StageConfig).Include(x => x.RequiredSchedule).Where(x => x.StageConfig.StageNumber == stageNumber).ToList();

            if (model.Count > 0 && settings.Count == model.Count)
            {
                viewModel = _mapper.Map<List<SchedulesViewModel>>(model);
            }
            else if (model.Count > 0 && settings.Count != model.Count)
            {
                viewModel = _mapper.Map<List<SchedulesViewModel>>(model);

                List<int> SettingsList = settings.OrderBy(x=>x.RequiredScheduleTagId).Select(x=>x.RequiredScheduleTagId).ToList();
                List<int> ScheduleList = viewModel.OrderBy(x => x.TagId).Select(x => x.TagId).ToList();
                if (!SettingsList.SequenceEqual(ScheduleList))
                {
                    List<int> AddedTags = new List<int>();
                    if (SettingsList.Count > ScheduleList.Count)
                    {
                       AddedTags = SettingsList.Except(ScheduleList).ToList();
                    }
                    else 
                    {
                        AddedTags = ScheduleList.Except(SettingsList).ToList();
                    }
                    foreach (var tag in AddedTags)
                    {
                        viewModel.Add(new SchedulesViewModel() {Id=0, Date=DateTime.Now, StageId = stage.Id, Stage = stage, TagId = tag, ScheduleType = settings.Where(x=>x.RequiredScheduleTagId==tag).First().RequiredSchedule });
                    }
                }
               
            }
            else
            {                
                foreach (var tag in settings)
                {
                    viewModel.Add(new SchedulesViewModel() { Id = 0, Date = DateTime.Now, StageId = stage.Id, Stage = stage, TagId = tag.RequiredScheduleTagId, ScheduleType = tag.RequiredSchedule });
                }
            }
            return View(viewModel);
        }

        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Edit(int projectId, int stageNumber, List<SchedulesViewModel> viewModel)
        {
         
            ViewBag.ProjectId = projectId;
            ViewBag.StageNumber = stageNumber;

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                return View(viewModel);
            }

            var model = _context.Schedules.AsNoTracking().Include(x => x.Stage).Include(x => x.ScheduleType).ToList();
            if (model.Count > 0)
            {
                var modelToUpdate = _mapper.Map<List<Schedule>>(viewModel);
                _context.Schedules.UpdateRange(modelToUpdate.Where(x=>x.Id!=0));
                _context.Schedules.AddRange(modelToUpdate.Where(x => x.Id == 0));

            }
            else
            {
               var modelToInsert = _mapper.Map<List<Schedule>>(viewModel);
               _context.Schedules.AddRange(modelToInsert);
            }
            _context.SaveChanges();
            return RedirectToAction("edit", new { projectId = projectId , stageId = stageNumber });
        }
    }
}