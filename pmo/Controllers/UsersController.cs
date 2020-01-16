using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace pmo.Controllers {
    [Route("admin/users")]
    public class UsersController : BaseController {

        public UsersController(EfContext context, IMapper mapper) : base(context, mapper) {

        }

        [Route("")]
        public IActionResult Create() {
            List<UserViewModel> userViewModel = new List<UserViewModel>();
            return View(userViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("")]
        public IActionResult Create(UserViewModel userViewModel) {
            if (!ModelState.IsValid) {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                var all_errors = string.Join("/n", errors);
                ModelState.AddModelError("error_partial_view", all_errors);
                return View(userViewModel);
            }

            var domainModel = _mapper.Map<UserViewModel>(userViewModel);
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
            var item = _context.Users.Include(x=>x.Citizenships).Where(x=>x.Id==id).FirstOrDefault();

            if (item != null) {
                return View(item);
            }

            return NotFound();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("{id}")]
        public IActionResult Edit(UserViewModel userViewModel) {
            if (!ModelState.IsValid) {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                var all_errors = string.Join("/n", errors);
                ModelState.AddModelError("error_partial_view", all_errors);
                return View(userViewModel);
            }
            var domainModel = _mapper.Map<UserViewModel>(userViewModel);
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