using AutoMapper;
using Microsoft.AspNetCore.Http;
using pmo.Controllers;
using dbModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using ViewModels.Helpers;
using pmo.Services.Users;
using System;
using dbModels.Report;

namespace pmo.Services.Projects
{
    public class ProjectService : BaseController, IProjectService
    {
        private readonly IUserService _userService;
        public ProjectService(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IUserService userService) : base(context, mapper, httpContextAccessor)
        {
            _userService = userService;
        }

        public void AddNewVBPDProject(forms.VBPDForm model)
        {
           
        }
    }
}