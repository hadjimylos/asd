using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViewModels.Helpers;


namespace pmo.Controllers {
    [Route("projects/{projectid}/stages/{stageNumber}/product-infrigment-patentability")]
    public class ProductInfrigmentPatentabilityController : BaseStageComponentController {
        private readonly string path = "~/Views/VBPD/Application/ProductInfrigmentPatentability";
        
        public ProductInfrigmentPatentabilityController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
          
        }

        [Route("")]
        public IActionResult Index() {
            var vm = _context.ProductInfrigmentPatentabilities
                .Include(x => x.Stage)
                .Where(x => x.StageId == _stageId).AsNoTracking()
                .ToList();

            return View($"{path}/Index.cshtml", vm);
        }

        [Route("create")]
        public IActionResult Create() {
            var pipViewModel = new forms.ProductInfrigmentPatentabilityForm() {
                StageId = _stageId
            };
            return View($"{path}/Create.cshtml", pipViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("create")]
        public IActionResult Create(forms.ProductInfrigmentPatentabilityForm pipViewModel) {
            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                return View($"{path}/Create.cshtml", pipViewModel);
            }

            var domainModel = _mapper.Map<ProductInfrigmentPatentability>(pipViewModel);
            domainModel.StageId = _stageId;
            _context.ProductInfrigmentPatentabilities.Add(domainModel);
            _context.SaveChanges();
            return RedirectToAction("Index");
            
        }

        [Route("{id}")]
        public IActionResult Edit(int id) {
            var pip = _context.ProductInfrigmentPatentabilities
                .Include(s => s.Stage)
                .Where(s => s.Id == id)
                .FirstOrDefault();
            if (pip == null)
                return NotFound();

            var vm = _mapper.Map<forms.ProductInfrigmentPatentabilityForm>(pip);
            return View($"{path}/Edit.cshtml", vm);
        }

        [HttpPost]
        [Route("{id}")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(forms.ProductInfrigmentPatentabilityForm vm, int id) {
            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                return View($"{path}/Edit.cshtml", vm);
            }

            var domainModel = _mapper.Map<ProductInfrigmentPatentability>(vm);
            domainModel.StageId = _stageId;
            _context.ProductInfrigmentPatentabilities.Update(domainModel);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }

}