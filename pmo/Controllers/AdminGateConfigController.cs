using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace pmo.Controllers {
    [Route("admin/gate-config")]
    public class AdminGateConfigController : BaseController {

        public AdminGateConfigController(EfContext context, IMapper mapper) : base(context, mapper)
        {

        }

        [Route("")]
        public IActionResult Create() {
            List<GateConfigViewModel> gateConfigViewModel = new List<GateConfigViewModel>();
            return View(gateConfigViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("")]
        public IActionResult Create(GateConfigViewModel gateConfigViewModel) {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                                      .Where(y => y.Count > 0)
                                      .ToList();
                var all_errors = string.Join("/n", errors);
                ModelState.AddModelError("error_partial_view", all_errors);
                return View(gateConfigViewModel);
            }
            var domainModel = _mapper.Map<GateConfigViewModel>(gateConfigViewModel);
            TryValidateModel(domainModel);
            _context.Add(domainModel);
            _context.SaveChanges();

            return RedirectToAction("Index","Admin");
        }

        [Route("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _context.GateConfigs.Find(id);

            if (item != null)
            {
                return View(item);
            }

            return NotFound();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("{id}")]
        public IActionResult Edit(GateConfigViewModel gateConfigViewModel)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                var all_errors = string.Join("/n", errors);
                ModelState.AddModelError("error_partial_view", all_errors);
                return View(gateConfigViewModel);
            }
            var domainModel = _mapper.Map<GateConfigViewModel>(gateConfigViewModel);
            TryValidateModel(domainModel);
            _context.Update(domainModel);
            _context.SaveChanges();

            return RedirectToAction("Index","Admin");
        }


    }
}