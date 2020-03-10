using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace pmo.Controllers {
    public class BaseController : Controller {
        protected readonly EfContext _context;
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public BaseController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        
    }
}