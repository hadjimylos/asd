using AutoMapper;
using dbModels;
using forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using pmo.Services.Lists;
using pmo.Services.SharePoint;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using ViewModels.Helpers;

namespace pmo.Controllers
{
    [Route("vbpd-admin/lite-stage-files-config")]

    public class LiteStageFilesConfigController : BaseController
    {
        private readonly string path = "~/Views/VBPD/Config/LiteStageFilesConfig";
        private readonly IListService _listService;
        public LiteStageFilesConfigController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IListService listService) : base(context, mapper, httpContextAccessor)
        {
            _listService = listService;
        }

        [Route("")]
        public IActionResult Details()
        {
            ViewBag.Stages = _context.LiteStageConfigs.Max(x => x.StageNumber);
            var model = _context.LiteStageFileConfigs
                .IncludeAll()
                .OrderBy(x => x.StageConfigId).ThenBy(x => x.RequiredFileTagId).ToList();

            return View($"{path}/Detail.cshtml", model);
        }

        [Route("edit/{stageNumber}")]
        public IActionResult Edit(int stageNumber)
        {
            var Stage = _context.LiteStageConfigs.Where(x=>x.StageNumber==stageNumber).FirstOrDefault();
            LiteStageFileConfig model = new LiteStageFileConfig() { StageConfigId=Stage.Id};
            ViewBag.CurrentStageNumber = stageNumber;
            ViewBag.CategoryDropDown = _listService.Tags_SelectList("stage-files");

            ViewBag.Uploaded = _context.LiteStageFileConfigs
                .IncludeAll()
                .Where(x=>x.StageConfig.StageNumber==stageNumber)
                .OrderBy(x => x.StageConfigId).ThenBy(x => x.RequiredFileTagId).ToList();

            return View($"{path}/Edit.cshtml", model);
        }

        [Route("edit/{stageNumber}")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Edit(int stageNumber, LiteStageFileConfig stageFileConfig)
        {
            ViewBag.CurrentStageNumber = stageNumber;
            ViewBag.CategoryDropDown = _listService.Tags_SelectList("stage-files");

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;                
                return View($"{path}/Edit.cshtml", stageFileConfig);
            }
            _context.LiteStageFileConfigs.Add(stageFileConfig);
            _context.SaveChanges();
            ViewBag.Uploaded = _context.LiteStageFileConfigs
                .IncludeAll()
                .Where(x => x.StageConfig.StageNumber == stageNumber)
                .OrderBy(x => x.StageConfigId).ThenBy(x => x.RequiredFileTagId).ToList();

            return View($"{path}/Edit.cshtml", stageFileConfig);
    }

        [Route("delete")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var stageFileConfig = _context.LiteStageFileConfigs.IncludeAll().Where(x=>x.Id==id).First();
            int StageNumber = stageFileConfig.StageConfig.StageNumber;
            _context.Remove(stageFileConfig);
            _context.SaveChanges();
            return RedirectToAction("Edit",new { stageNumber = StageNumber });
        }
    }
}