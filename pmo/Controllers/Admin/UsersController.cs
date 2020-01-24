using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
        public UsersController(EfContext context, IMapper mapper , IUserService userService) : base(context, mapper) {
            _userService = userService;
        }
        public IActionResult Index()
        {
            var vm = _mapper.Map<List<UserViewModel>>(_context.Users.ToList());
            return View(vm);
        }

        [Route("create")]
        public IActionResult Create() {
            UserViewModel userViewModel = new UserViewModel() { isCreate = true};
            userViewModel.RoleList = new SelectList(_context.Roles.ToList(), "Id", "FriendlyName");
            userViewModel.CitizenshipsList = new SelectList(_context.Tags.Include(x=>x.TagCategory).Where(x=>x.TagCategory.Key== TagCategoryHelper.CitizenshipsKey).ToList(), "Id", "Name");

            return View(userViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("create")]
        public IActionResult Create(UserViewModel userViewModel) {
            userViewModel.isCreate = true;
            
            if (!ModelState.IsValid)
            {
                userViewModel.RoleList = new SelectList(_context.Roles.ToList(), "Id", "FriendlyName");
                userViewModel.CitizenshipsList = new MultiSelectList(_context.Tags.Include(x => x.TagCategory).Where(x => x.TagCategory.Key == TagCategoryHelper.CitizenshipsKey).ToList(), "Id", "Name");

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
            vm.RoleList = new SelectList(_context.Roles.ToList(), "Id", "FriendlyName", vm.RoleId);
            var Tags = _context.Tags.Include(x => x.TagCategory).Where(x => x.TagCategory.Key == TagCategoryHelper.CitizenshipsKey).ToList();
            var Ready= Tags.Where(x => vm.UserCitizenships.Contains(x.Id)).ToList();            
            vm.CitizenshipsList = new MultiSelectList(Tags, "Id", "Name", Ready);
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
                vm.RoleList = new SelectList(_context.Roles.ToList(), "Id", "FriendlyName", vm.RoleId);
                var Tags = _context.Tags.Include(x => x.TagCategory).Where(x => x.TagCategory.Key == TagCategoryHelper.CitizenshipsKey).ToList();
                var Ready = Tags.Where(x => vm.UserCitizenships.Contains(x.Id)).ToList();
                vm.CitizenshipsList = new MultiSelectList(Tags, "Id", "Name", Ready);
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