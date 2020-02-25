using AutoMapper;
using dbModels;
using forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System.Collections.Generic;
using System.Linq;
using ViewModels.Helpers;

namespace pmo.Controllers.VBPD.Application {
    [Route("vbpd-projects/{projectid}/stages/{stageNumber}/business-case/{businessCaseId}/financial-data")]
    public class FinancialDataController : BaseStageComponentController {
        private readonly string path = "~/Views/VBPD/Application/FinancialData";
        private readonly int _businessCaseId;

        public FinancialDataController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            this._businessCaseId = int.Parse ( 
                ViewModels.Helpers.Helpers.GetRouteValue (httpContextAccessor.HttpContext.Request, "businessCaseId")
            );
        }

        [Route("")]
        public IActionResult Detail() {
            return View($"{path}/Detail.cshtml");
        }

        [Route("edit")]
        public IActionResult Edit() {
            var financials = _context.FinancialData.AsNoTracking()
                .Where (
                    w =>
                        w.BusinessCaseId == _businessCaseId
                ).ToList();

            var model = _mapper.Map<List<FinancialDataForm>>(financials);

            return View($"{path}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("refresh")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Refresh(List<FinancialDataForm> model, int? rowCount) {
            if (rowCount == null || rowCount <= 0) {
                // no rows to add or remove so return as is (no changes)
                return View($"{path}/Edit.cshtml", model);
            }

            List<FinancialDataForm> rows = new List<FinancialDataForm>();

            int start = 0;
            if (model.Count == 0) {
                start = _context.BusinessCases.First (
                    f =>
                        f.Id == _businessCaseId
                ).FinancialStartYear;

                var added = AddRows(model, (int)rowCount, start);
                return View($"{path}/Edit.cshtml", added);
            }

            start = model.Max(m => m.Year) + 1;
            if (model.Count > rowCount) {
                // remove extra rows
                rows = model.Where (
                    w =>
                        w.Year < (start - (model.Count - rowCount))
                    ).ToList();

            } else if (model.Count < rowCount) {
                rows.AddRange(model);
                rows.AddRange(AddRows(rows, (int)rowCount, start));
            } else {
                rows = model;
            }

            return View($"{path}/Edit.cshtml", rows);
        }

        private List<FinancialDataForm> AddRows(List<FinancialDataForm> model, int rowCount, int yearStart) {
            List<FinancialDataForm> rows = new List<FinancialDataForm>();
            // append missing rows
            var recordsToAddCount = rowCount - model.Count;

            for (int yearOffset = 0; yearOffset < recordsToAddCount; yearOffset++) {
                rows.Add(new FinancialDataForm {
                    BusinessCaseId = _businessCaseId,
                    Year = yearOffset + yearStart,
                });
            }

            return rows;
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(List<forms.FinancialDataForm> model) {
            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                return View($"{path}/Edit.cshtml", model);
            }

            // update all based on newly mapped items
            var newFinancials = _mapper.Map<List<FinancialData>>(model);
            var allCurrentRecords = _context.FinancialData.Where(w => w.BusinessCaseId == _businessCaseId);
            newFinancials.UpdateRelated(allCurrentRecords, _context);

            return RedirectToAction("Detail", new {
                projectId = _projectId,
                stageNumber = _stageNumber,
                businessCaseId = _businessCaseId 
            });
        }
    }
}
