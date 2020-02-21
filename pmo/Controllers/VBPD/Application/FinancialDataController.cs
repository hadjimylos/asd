using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pmo.Services.Lists;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
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
        }


        [Route("")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Detail(int projectId, int stageNumber, int businessCaseId)
        {
            ViewBag.ProjectId = projectId;
            ViewBag.StageNumber = stageNumber;
            ViewBag.BusinessCaseId = businessCaseId;

            var model = _context.FinancialData.AsNoTracking().Include(x => x.BusinessCase).Where(x => x.BusinessCaseId == businessCaseId).ToList();
            if (model.Count == 0)
            {
                return View($"{path}/Detail.cshtml", new List<FinancialDataViewModel>());
            }
            var viewModel = _mapper.Map<List<FinancialDataViewModel>>(model);
            var viewModelWithCalculations = Financials.GenerateFinancialsCalculations(viewModel);
            return View($"{path}/Detail.cshtml", viewModelWithCalculations);
        }
        

        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int projectId, int stageNumber, int businessCaseId)
        {
            ViewBag.ProjectId = projectId;
            ViewBag.StageNumber = stageNumber;
            ViewBag.BusinessCaseId = businessCaseId;

            var model = _context.FinancialData.AsNoTracking().Include(x => x.BusinessCase).Where(x => x.BusinessCaseId == businessCaseId).ToList();
            List<FinancialDataViewModel> viewModel = new List<FinancialDataViewModel>();
            if (model.Count >0)
            {
                viewModel = _mapper.Map<List<FinancialDataViewModel>>(model);
            }
            else
            {
                var bc = _context.BusinessCases.AsNoTracking().Include(x => x.FinancialData).Where(x => x.Id == businessCaseId).FirstOrDefault();
                viewModel = Financials.GetFinancialDataRows(_mapper.Map<BusinessCaseViewModel>(bc));                
            }

            return View($"{path}/Edit.cshtml", viewModel);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(int projectId, int stageNumber, int businessCaseId, List<FinancialDataViewModel> model)
        {
            ViewBag.ProjectId = projectId;
            ViewBag.StageNumber = stageNumber;
            ViewBag.BusinessCaseId = businessCaseId;

            if (!ModelState.IsValid)
            {                
                ViewBag.Errors = ModelState;
                return View($"{path}/Edit.cshtml", model);
            }

           var dbmodel = _context.FinancialData.Include(x => x.BusinessCase).Where(x => x.BusinessCaseId == businessCaseId).ToList();

            if (dbmodel.Count > 0)
            {
                
                foreach (var item in model)
                {

                    dbmodel.Where(x => x.Id == item.Id).First().Quantity = item.Quantity;
                    dbmodel.Where(x => x.Id == item.Id).First().StdCostEstimated = item.StdCostEstimated;
                    dbmodel.Where(x => x.Id == item.Id).First().SalesCostEstimated = item.SalesCostEstimated;
                    dbmodel.Where(x => x.Id == item.Id).First().GPACapital = item.GPACapital;
                    dbmodel.Where(x => x.Id == item.Id).First().GPAExpense = item.GPAExpense;
                    dbmodel.Where(x => x.Id == item.Id).First().QualCosts = item.QualCosts;
                    dbmodel.Where(x => x.Id == item.Id).First().OtherDevelopmentExpenses = item.OtherDevelopmentExpenses;

                }
                var tracker = _context.ChangeTracker.Entries<FinancialData>();

               
                _context.UpdateRange(dbmodel);
                    _context.SaveChanges();
            }
            else
            {
                var dbmodelForInsert = _mapper.Map<List<FinancialData>>(model);

                _context.FinancialData.AddRange(model);
                _context.SaveChanges();

            }

            return RedirectToAction("Detail");

        }
    }
}