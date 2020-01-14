using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace pmo.Controllers
{
    public class BaseController : Controller
    {
        protected readonly EfContext _context;
        protected readonly IMapper _mapper;
        public BaseController(EfContext context,  IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public BaseController()
        {
        }
    }
}