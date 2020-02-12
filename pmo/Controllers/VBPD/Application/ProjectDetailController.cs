using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ViewModels;

namespace pmo.Controllers.VBPD.Application {
    [Route("vbpd-projects/{projectId}")]
    public class ProjectDetailController : BaseProjectDetailController {
        private readonly string path = "~/Views/VBPD/Application/VBPD";

        public ProjectDetailController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
        }

        public IActionResult Detail(int projectId) {
            VbpdProjectDetail model = new VbpdProjectDetail(_context, projectId);
            return View($"{path}/Detail.cshtml", model);
        }
    }
}