using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pmo.Services.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using ViewModels.Helpers;

namespace pmo.Controllers.VBPD.Application
{
    [Route("vbpd-projects/{projectid}/stages/{stageNumber}/business-case/{businessCaseId}/financial-data")]
    public class FinancialDataController : BaseStageComponentController    
    {
        private readonly string path = "~/Views/VBPD/Application/FinancialData";
        private readonly IListService _listService;
        public FinancialDataController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IListService listService) : base(context, mapper, httpContextAccessor)
        {
            _listService = listService;
            //   _httpContextAccessor = httpContextAccessor;
        }


        [Route("")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Detail(int projectId, int stageNumber, int businessCaseId)
        {
            ViewBag.ProjectId = projectId;
            ViewBag.StageNumber = stageNumber;
            ViewBag.BusinessCaseId = businessCaseId;

            return View($"{path}/Detail.cshtml");
        }
        

        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int projectId, int stageNumber, int businessCaseId)
        {
            ViewBag.ProjectId = projectId;
            ViewBag.StageNumber = stageNumber;
            ViewBag.BusinessCaseId = businessCaseId;
            
            return View($"{path}/Edit.cshtml");

        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int projectId, int stageNumber, int businessCaseId, FinancialDataViewModel model)
        {
            ViewBag.ProjectId = projectId;
            ViewBag.StageNumber = stageNumber;
            ViewBag.BusinessCaseId = businessCaseId;

            return View($"{path}/Detail.cshtml");

        }
    }
}