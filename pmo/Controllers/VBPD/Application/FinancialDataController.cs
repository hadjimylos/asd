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
    [Route("vbpd-projects/{projectid}/stages/{stageNumber}/business-case/{businessCaseId}/FinancialData")]
    public class FinancialDataController : BaseStageComponentController    
    {
        private readonly string viewPath = "~/Views/VBPD/Application/FinancialData";
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
            return View();
        }
        [HttpPost]
        [Route("")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Detail(int projectId, int stageNumber, int businessCaseId, FinancialDataViewModel model)
        {
            return View();
        }

        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int projectId, int stageNumber, int businessCaseId)
        {
            return View();
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int projectId, int stageNumber, int businessCaseId, FinancialDataViewModel model)
        {
            return View();
        }
    }
}