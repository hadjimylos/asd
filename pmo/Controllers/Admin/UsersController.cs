using AutoMapper;
using dbModels;
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

        public UsersController(EfContext context, IMapper mapper , IUserService userService, IListService listService) : base(context, mapper) {
            _userService = userService;
            _listService = listService;

        }
        public IActionResult Index()
        {
            var vm = _mapper.Map<List<UserViewModel>>(_context.Users.ToList());
            return View(vm);
        }

        [Route("create")]
        public IActionResult Create() {
            UserViewModel userViewModel = new UserViewModel() { isCreate = true};
            userViewModel.RoleList = _listService.Roles();
            userViewModel.CitizenshipsList = _listService.Citizenships(); 

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
                userViewModel.CitizenshipsList = _listService.CitizenshipsMultiple();

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
            vm.CitizenshipsList = _listService.CitizenshipsMultiple<Tag>(Ready);
            vm.isCreate = false;
            return View(vm);

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("{id}")]
        public async Task<ActionResult> Edit(UserViewModel vm) {

            vm.isCreate = false;
            var user = _context.Users.Include(x => x.Citizenships).Include(x => x.Role).AsNoTracking().Where(x => x.Id == vm.Id).FirstOrDefault();
            vm.NetworkUsername = user.NetworkUsername;
            if (!ModelState.IsValid)
            {
                vm.RoleList = _listService.Roles(vm.RoleId);
                var Ready = _context.Tags.Where(x => vm.UserCitizenships.Contains(x.Id)).ToList();
                vm.CitizenshipsList = _listService.CitizenshipsMultiple<Tag>(Ready);
                ViewBag.Errors = ModelState;
                return View(vm);
            }

            var domainModel = _mapper.Map<User>(vm);
            _context.Users.Update(domainModel);
            await _context.SaveChangesAsync();
            _context.Entry(domainModel).State = EntityState.Detached;

            List<int> Existing = user.Citizenships.Select(x => x.TagId).ToList();
            List<User_CitizenShip> ExistingModel = user.Citizenships;
            var SameElements = Existing.All(vm.UserCitizenships.Contains) ;
            var SameCount = Existing.Count == vm.UserCitizenships.Count;
            if (!(SameElements && SameCount))
            {
                _context.UserCitizenShip.RemoveRange(ExistingModel);

            var citizenships = new List<User_CitizenShip>();
            foreach (var item in vm.UserCitizenships)
            {
                citizenships.Add(new User_CitizenShip() { UserId = domainModel.Id, TagId = item });
            }
            _context.UserCitizenShip.AddRange(citizenships);
            }
            _context.SaveChanges();
            return RedirectToAction(actionName: "Index");
        }
    }
}