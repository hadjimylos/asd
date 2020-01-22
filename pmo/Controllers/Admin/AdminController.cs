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
            return View();
        }

        
    }
}