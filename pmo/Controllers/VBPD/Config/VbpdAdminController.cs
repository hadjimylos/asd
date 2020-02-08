using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace pmo.Controllers {

       [Route("vbpd-admin")]
       public class VbpdAdminController : BaseController {
        public VbpdAdminController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            
        }
        private readonly string path = "~/Views/VBPD/Config/VbpdAdmin";

        public IActionResult Index(){
            return View($"{path}/Index.cshtml");
        }
    }
}