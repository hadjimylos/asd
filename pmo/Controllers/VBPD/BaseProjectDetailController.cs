namespace pmo.Controllers {
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Linq;
    using ViewModels;

    public class BaseProjectDetailController : BaseController {
        private readonly ProjectDetailNav _nav;
        protected readonly int stageId;

        public BaseProjectDetailController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            // set nav component for all of these pages here
            var projectId = int.Parse (
                (string)httpContextAccessor.HttpContext
                .Request
                .RouteValues["projectId"]
            );

            var stageNum = int.Parse(
               (string)httpContextAccessor.HttpContext
               .Request
               .RouteValues["stageNumber"]
           );

            stageId = _context.Stages.First(w => w.StageNumber == stageNum && w.projectId == projectId).Id;
            _nav = new ProjectDetailNav(_context, _mapper, projectId);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            ViewData["ProjectNav"] = _nav;
            base.OnActionExecuting(filterContext);
        }
    }
}