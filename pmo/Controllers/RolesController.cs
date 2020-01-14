using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace pmo.Controllers {
    [Route("admin/roles")]
    public class RolesController : BaseController {

        public RolesController(EfContext context, IMapper mapper) : base(context, mapper) {

        }

        [Route("")]
        public IActionResult Create() {
            List<RoleViewModel> roleViewModel = new List<RoleViewModel>();
            return View(roleViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("")]
        public IActionResult Create(RoleViewModel roleViewModel) {
            if (!ModelState.IsValid) {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                var all_errors = string.Join("/n", errors);
                ModelState.AddModelError("error_partial_view", all_errors);
                return View(roleViewModel);
            }

            var domainModel = _mapper.Map<RoleViewModel>(roleViewModel);
            TryValidateModel(domainModel);
            _context.Add(domainModel);
            _context.SaveChanges();

            return RedirectToAction(
                actionName: "Index",
                controllerName: "Admin"
            );
        }

        [Route("{id}")]
        public IActionResult Edit(int id) {
            var item = _context.Roles.Find(id);

            if (item != null) {
                return View(item);
            }

            return NotFound();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("{id}")]
        public IActionResult Edit(RoleViewModel roleViewModel) {
            if (!ModelState.IsValid) {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                var all_errors = string.Join("/n", errors);
                ModelState.AddModelError("error_partial_view", all_errors);
                return View(roleViewModel);
            }
            var domainModel = _mapper.Map<RoleViewModel>(roleViewModel);
            TryValidateModel(domainModel);
            _context.Update(domainModel);
            _context.SaveChanges();

            return RedirectToAction(
                actionName: "Index",
                controllerName: "Admin"
            );
        }
    }
}