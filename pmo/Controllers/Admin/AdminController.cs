using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace pmo.Controllers {

       [Route("admin")]
       public class AdminController : BaseController {
        public AdminController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            
        }

        [Route("")]
        public IActionResult Index(){
            return View();
        }

        
    }
}