using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;

namespace pmo.Controllers
{
    public class BaseController : Controller
    {
        protected readonly EfContext _context;
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public BaseController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context.ChangeTracker.QueryTrackingBehavior = Microsoft.EntityFrameworkCore.QueryTrackingBehavior.NoTracking;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            // always remove trailing slash from Url
            var requestPath = HttpContext.Request.Path.Value;
            if (requestPath.Last() == '/' && requestPath.Length > 1)
            {
                context.Result = new RedirectResult(requestPath.Remove(requestPath.Length - 1));
            }
        }
    }
}