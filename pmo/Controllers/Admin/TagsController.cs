using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace pmo.Controllers {
    [Route("admin/tags")]
    public class TagController : BaseController {

        public TagController(EfContext context, IMapper mapper) : base(context, mapper) {

        }

        [Route("")]
        public IActionResult Create() {
            List<TagViewModel> tagViewModel = new List<TagViewModel>();
            return View(tagViewModel);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("")]
        public IActionResult Create(TagViewModel tagViewModel) {
            if (!ModelState.IsValid) {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                var all_errors = string.Join("/n", errors);
                ModelState.AddModelError("error_partial_view", all_errors);
                return View(tagViewModel);
            }
            
            //TODO: if TagCategory does not exist Insert or create Helper Method

            var domainModel = _mapper.Map<TagViewModel>(tagViewModel);
            TryValidateModel(domainModel);
            _context.Add(domainModel);
            _context.SaveChanges();

            return RedirectToAction(
                actionName: "Index",
                controllerName: "Admin"
            );
        }

        [Route("{id}")]
        public IActionResult Edit(int id) {
            var item = _context.StageConfigs.Find(id);

            if (item != null) {
                return View(item);
            }

            return NotFound();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Route("{id}")]
        public IActionResult Edit(TagViewModel tagViewModel) {
            if (!ModelState.IsValid) {
                var errors = ModelState.Select(x => x.Value.Errors)
                          .Where(y => y.Count > 0)
                          .ToList();
                var all_errors = string.Join("/n", errors);
                ModelState.AddModelError("error_partial_view", all_errors);
                return View(tagViewModel);
            }

            //TODO: if TagCategory does not exist in database Insert or create Helper Method

            var domainModel = _mapper.Map<TagViewModel>(tagViewModel);
            TryValidateModel(domainModel);
            _context.Update(domainModel);
            _context.SaveChanges();

            return RedirectToAction(
                actionName: "Index",
                controllerName: "Admin"
            );
        }

    }
}