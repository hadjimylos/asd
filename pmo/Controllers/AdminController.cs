using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using Microsoft.EntityFrameworkCore;

namespace pmo.Controllers {

       [Route("admin")]
       public class AdminController : BaseController {
        public AdminController(EfContext context, IMapper mapper) : base(context, mapper) {
            
        }

        [Route("")]
        public IActionResult Index(){

            var stageConfig = _context.StageConfigs.Include(x=>x.RequiredDesignConcepts).Include(x=>x.RequiredSchedules).ToList();
            var stageConfigViewModel = _mapper.Map<List<StageConfigViewModel>>(stageConfig);

            var gateConfig = _context.GateConfigs.ToList();
            var gateConfigViewModel = _mapper.Map<List<GateConfigViewModel>>(gateConfig);

            var roles = _context.Roles.ToList();
            var roleViewModel = _mapper.Map<List<RoleViewModel>>(roles);

            var tags = _context.Tags.Include(x=>x.TagCategory).ToList();
            var tagViewModel = _mapper.Map<List<TagViewModel>>(tags);

            var users = _context.Users.Include(x => x.Citizenships).ToList();
            var userViewModel = _mapper.Map<List<UserViewModel>>(users);

            AdminViewModel vm = new AdminViewModel { 
                StageConfigViewModel = stageConfigViewModel, 
                GateConfigViewModel = gateConfigViewModel, 
                RolesViewModel= roleViewModel, 
                TagViewModel= tagViewModel,
                UserViewModel = userViewModel
            };       

            return View(vm);
        }

        
    }
}