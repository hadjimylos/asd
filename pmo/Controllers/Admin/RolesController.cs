using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace pmo.Controllers {
    [Route("admin/roles")]
    public class RolesController : BaseController {

        public RolesController(EfContext context, IMapper mapper) : base(context, mapper) {

        }

        public IActionResult Index()
        {
            var vm = _mapper.Map<List<RoleViewModel>>(_context.Roles.ToList());
            return View(vm);
        }


        [Route("create")]
        public IActionResult Create()
        {
            var RoleViewModel = new RoleViewModel()
            {
           
                isCreate = true,
            };
            return View(RoleViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("create")]
        public IActionResult Create(RoleViewModel roleViewModel)
        {
            var transformFriendlyName = roleViewModel.FriendlyName.Trim().ToLower().Replace(" ", "-");
            var exists = _context.Roles.Select(fn => fn.Key.Contains(roleViewModel.Key)).Count();
            if (exists > 0)
            {
                exists++;
                roleViewModel.Key = string.Concat(exists, transformFriendlyName);
            }
            roleViewModel.Key = transformFriendlyName;
            roleViewModel.isCreate = true;

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                return View(roleViewModel);
            }

            var domainModel = _mapper.Map<Role>(roleViewModel);
            _context.Roles.Add(domainModel);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Route("{id}")]
        public IActionResult Edit(int id)
        {
            var role = _context.Roles.Find(id);

            if (role == null)
                return NotFound();

            var vm = _mapper.Map<RoleViewModel>(role);
            vm.isCreate = false;
            return View(vm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("{id}")]
        public IActionResult Edit(RoleViewModel model)
        {
            model.Key = _context.Roles
                .AsNoTracking()
                .First(
                    config => config.Id == model.Id
                ).Key;

            model.isCreate = false;

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                return View(model);
            }

            var domainModel = _mapper.Map<Role>(model);
            _context.Update(domainModel);
            _context.SaveChanges();

            return RedirectToAction(actionName: "Index");
        }
    
    }
}