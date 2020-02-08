using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ViewModels;

namespace pmo.Controllers.Admin
{
    [Route("vbpd-admin/tags")]
    public class Tags : BaseController
    {
        private readonly string path = "~/Views/VBPD/Config/Tags";

        public Tags(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor) {

        }
        [Route("categories")]
        public IActionResult Index()
        {
            var vm = _mapper.Map<List<TagCategoryViewModel>>(_context.TagCategories.ToList());
            return View($"{path}/Index.cshtml", vm);
        }

        [Route("categories/create")]
        public IActionResult Create()
        {
            var tagcategoryViewModel = new TagCategoryViewModel()
            {
                isCreate = true,
            };
            return View($"{path}/Create.cshtml", tagcategoryViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("categories/create")]
        public IActionResult Create(TagCategoryViewModel tagcategoryViewModel)
        {
            var transFormFriendlyName = tagcategoryViewModel.FriendlyName.Trim().ToLower().Replace(" ", "-");
            var exists = _context.TagCategories.Select(fn => fn.Key.Contains(tagcategoryViewModel.Key)).Count();
            if (exists > 0)
            {
                exists++;
                tagcategoryViewModel.Key = string.Concat(exists, transFormFriendlyName);
            }
            tagcategoryViewModel.Key = transFormFriendlyName;
            tagcategoryViewModel.isCreate = true;

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                return View($"{path}/Create.cshtml", tagcategoryViewModel);
            }

            var domainModel = _mapper.Map<TagCategory>(tagcategoryViewModel);
            _context.TagCategories.Add(domainModel);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [Route("categories/{id}")]
        public IActionResult Edit(int id)
        {
            var tagcategories = _context.TagCategories.Include(t => t.Tags).Where(i => i.Id == id).FirstOrDefault();
            if (tagcategories == null)
                return NotFound();

            var vm = _mapper.Map<TagCategoryViewModel>(tagcategories);
            vm.isCreate = false;
            return View($"{path}/Edit.cshtml", vm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("categories/{id}")]
        public IActionResult Edit(TagCategoryViewModel model)
        {
            // populate key as is from DB
            model.Key = _context.TagCategories
                .AsNoTracking()
                .First(
                    tagcategory => tagcategory.Id == model.Id
                ).Key;

            model.isCreate = false;

            if (!ModelState.IsValid)
            {
                var tagcategories = _context.TagCategories.Include(t => t.Tags).Where(i => i.Id == model.Id).FirstOrDefault();
                model.Tags = tagcategories.Tags;
                ViewBag.Errors = ModelState;
                return View($"{path}/Edit.cshtml", model);
            }

            var domainModel = _mapper.Map<TagCategory>(model);
            _context.Update(domainModel);
            _context.SaveChanges();

            return RedirectToAction(actionName: "Index");
        }

        [Route("")]
        public IActionResult Index_Tags()
        
        {
            var vm = _mapper.Map<List<TagViewModel>>(_context.Tags.Include(cat => cat.TagCategory).OrderBy(x => x.TagCategory.FriendlyName).ToList());
            return View($"{path}/Index_Tags.cshtml", vm);
        }

        [Route("create")]
        public IActionResult Create_Tags()
        {
            var tagsViewModel = new TagViewModel()
            {
                isCreate = true,
            };
            tagsViewModel.TagCategorySelectList = new SelectList(_context.TagCategories.ToList(), "Id", "FriendlyName");
            return View($"{path}/Create_Tags.cshtml", tagsViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("create")]
        public IActionResult Create_Tags(TagViewModel tagviewModel)
        {
            if (!ModelState.IsValid)
            {
                tagviewModel.TagCategorySelectList = new SelectList(_context.TagCategories.ToList(), "Id", "FriendlyName");

                ViewBag.Errors = ModelState;
                return View($"{path}/Create_Tags.cshtml", tagviewModel);
            }

            var domainModel = _mapper.Map<Tag>(tagviewModel);
            _context.Tags.Add(domainModel);
            _context.SaveChanges();

            return RedirectToAction("Index_Tags");
        }

        [Route("{id}")]
        public IActionResult Edit_Tags(int id)
        {
            var tag = _context.Tags.Include(t => t.TagCategory).Where(t => t.Id == id).FirstOrDefault();
            if (tag == null)
                return NotFound();

            var vm = _mapper.Map<TagViewModel>(tag);
            vm.TagCategorySelectList = new SelectList(_context.TagCategories.ToList(), "Id", "FriendlyName", vm.TagCategoryId);
            vm.isCreate = false;
            return View($"{path}/Edit_Tags.cshtml", vm);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("{id}")]
        public IActionResult Edit_Tags(TagViewModel model)
        {
            model.isCreate = false;
            if (!ModelState.IsValid)
            {
                model.TagCategorySelectList = new SelectList(_context.TagCategories.ToList(), "Id", "FriendlyName", model.TagCategoryId);

                ViewBag.Errors = ModelState;
                return View($"{path}/Edit_Tags.cshtml", model);
            }

            var domainModel = _mapper.Map<Tag>(model);
            _context.Tags.Update(domainModel);
            _context.SaveChanges();

            return RedirectToAction("Index_Tags");
        }
    }
}
