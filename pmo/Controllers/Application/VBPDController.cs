using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pmo.Services.Projects;
using System.Collections.Generic;
using ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using dbModels;
using pmo.Services.Lists;
using ViewModels.Helpers;

namespace pmo.Controllers.Application
{
    [Route("")]
    public class VBPDController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly IListService _listService;

        public VBPDController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IProjectService projectService , IListService listService) : base(context, mapper, httpContextAccessor)
        { 
            _projectService = projectService;
            _listService = listService;
        }

        public IActionResult Index()
        {
            var projects = _projectService.GetAllVBPDProjectDetailList();
            var vm = _mapper.Map<List<VBPDViewModel>>(projects);
            return View("~/Views/Application/VBPD/Index.cshtml", vm);
        }

        [Route("create")]
        public IActionResult Create()
        {
            var project = new VBPDViewModel();
            project.ProjectCategoryTagDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProjectCategory);
            project.ProductLineDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProductLine);
            project.ProjectClassificationDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProjectClassification);
            project.DesignAuthorityDropDown = _listService.Tags_SelectList(TagCategoryHelper.DesignAuthority);
            project.CustomerDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.Customer);
            return View("~/Views/Application/VBPD/Create.cshtml", project);
        }

        [HttpPost]
        [Route("create")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(VBPDViewModel model)
        {
            if (!ModelState.IsValid)
            {

                ViewBag.Errors = ModelState;
                model.ProjectCategoryTagDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProjectCategory, model.ProjectCategoryTagId.ToString());
                model.ProductLineDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProductLine, model.ProjectCategoryTagId.ToString());
                model.ProjectClassificationDropDown = _listService.Tags_SelectList(TagCategoryHelper.ProjectClassification, model.ProjectCategoryTagId.ToString());
                model.DesignAuthorityDropDown = _listService.Tags_SelectList(TagCategoryHelper.DesignAuthority, model.ProjectCategoryTagId.ToString());
                model.CustomerDropDown = _listService.Tags_MultiSelectList(TagCategoryHelper.Customer,model.Customers);
                return View("~/Views/Application/VBPD/Create.cshtml", model);
            }
           var created =  _projectService.AddNewVBPDProject(model);
           // if(created)
                 return RedirectToAction("Index");
            //IF FALSE???
        }
    }
}