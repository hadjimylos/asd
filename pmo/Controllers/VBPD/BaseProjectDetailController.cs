namespace pmo.Controllers {
    using AutoMapper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;
    using ViewModels;
    using ViewModels.Helpers;

    public class BaseProjectDetailController : BaseController {
        private readonly ProjectDetailNav _nav;
        protected readonly int _projectId;


        public BaseProjectDetailController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {
            // set nav component for all of these pages here
            this._projectId = int.Parse(Helpers.GetRouteValue(httpContextAccessor.HttpContext.Request, "projectId"));
            this._nav = new ProjectDetailNav(_context, _mapper, _projectId);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            ViewData["ProjectNav"] = _nav;
            base.OnActionExecuting(filterContext);
        }
    }
}