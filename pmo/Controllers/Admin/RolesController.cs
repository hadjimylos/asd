using AutoMapper;
using dbModels;
using forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace pmo.Controllers {
    [Route("admin/roles")]
    public class RolesController : BaseController {
        private readonly string path = "~/Views/Admin/Roles";

        public RolesController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {

        }

        public IActionResult Index() {
            var vm = _context.Roles.ToList();
            return View($"{path}/Index.cshtml", vm);
        }


        [Route("create")]
        public IActionResult Create() {
            var RoleViewModel = new RoleForm() {

                isCreate = true,
            };
            return View($"{path}/Create.cshtml", RoleViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("create")]
        public IActionResult Create(RoleForm roleViewModel) {
            var transformFriendlyName = roleViewModel.FriendlyName.Trim().ToLower().Replace(" ", "-");
            var exists = _context.Roles.Select(fn => fn.Key.Contains(roleViewModel.Key)).Count();
            if (exists > 0) {
                exists++;
                roleViewModel.Key = string.Concat(exists, transformFriendlyName);
            }
            roleViewModel.Key = transformFriendlyName;
            roleViewModel.isCreate = true;

            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                return View($"{path}/Create.cshtml", roleViewModel);
            }

            var domainModel = _mapper.Map<Role>(roleViewModel);
            _context.Roles.Add(domainModel);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Route("{id}")]
        public IActionResult Edit(int id) {
            var role = _context.Roles.Find(id);

            if (role == null)
                return NotFound();

            var vm = _mapper.Map<RoleForm>(role);
            vm.isCreate = false;
            return View($"{path}/Edit.cshtml", vm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("{id}")]
        public IActionResult Edit(RoleForm model) {
            model.Key = _context.Roles
                .AsNoTracking()
                .First(
                    config => config.Id == model.Id
                ).Key;

            model.isCreate = false;

            if (!ModelState.IsValid) {
                ViewBag.Errors = ModelState;
                return View($"{path}/Edit.cshtml", model);
            }

            var domainModel = _mapper.Map<Role>(model);
            _context.Update(domainModel);
            _context.SaveChanges();

            return RedirectToAction(actionName: "Index");
        }
    }
}