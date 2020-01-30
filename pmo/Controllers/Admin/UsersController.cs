using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pmo.Services.Lists;
using pmo.Services.Users;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;
using ViewModels.Helpers;

namespace pmo.Controllers {
    [Route("admin/users")]
    public class UsersController : BaseController {

        private readonly IUserService _userService;
        private readonly IListService _listService;

        public UsersController(EfContext context, IMapper mapper , IUserService userService, IListService listService, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            _userService = userService;
            _listService = listService;

        }
        public IActionResult Index()
        {
            var vm = _mapper.Map<List<UserViewModel>>(_context.Users.Include(i => i.Role).ToList());
            return View(vm);
        }

        [Route("create")]
        public IActionResult Create() {
            UserViewModel userViewModel = new UserViewModel() { isCreate = true};
            userViewModel.RoleList = _listService.Roles();
            userViewModel.CitizenshipsList = _listService.Tags_SelectList(TagCategoryHelper.Citizenships); 

            return View(userViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("create")]
        public IActionResult Create(UserViewModel userViewModel) {
            userViewModel.isCreate = true;

            if (!ModelState.IsValid)
            {
                userViewModel.RoleList = _listService.Roles();
                userViewModel.CitizenshipsList = _listService.Tags_MultiSelectList(TagCategoryHelper.Citizenships);

                ViewBag.Errors = ModelState;
                return View(userViewModel);
            }

            _userService.AddNewUser(userViewModel);

            return RedirectToAction("Index");
        }

        [Route("{id}")]
        public IActionResult Edit(int id) {

            var user = _userService.GetUserById(id);
            
            if (user == null) {
                return NotFound();
            }
            var vm = _mapper.Map<UserViewModel>(user);
            vm.RoleList = _listService.Roles(vm.RoleId);
            var Ready= _context.Tags.Where(x => vm.UserCitizenships.Contains(x.Id)).ToList();
            vm.CitizenshipsList = _listService.Tags_MultiSelectList<Tag>(TagCategoryHelper.Citizenships, Ready);
            vm.isCreate = false;
            return View(vm);

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("{id}")]
        public ActionResult Edit(UserViewModel vm) {

            if (!ModelState.IsValid)
            {
                vm.RoleList = _listService.Roles(vm.RoleId);
                var Ready = _context.Tags.Where(x => vm.UserCitizenships.Contains(x.Id)).ToList();
                vm.CitizenshipsList = _listService.Tags_MultiSelectList<Tag>(TagCategoryHelper.Citizenships, Ready);
                ViewBag.Errors = ModelState;
                return View(vm);
            }
            _userService.UpdateUser(vm);
            return RedirectToAction(actionName: "Index");
        }
    }
}