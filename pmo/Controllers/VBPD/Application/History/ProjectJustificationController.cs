using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pmo.Services.Lists;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using ViewModels.Helpers;

namespace pmo.Controllers
{
    [Route("vbpd-projects/{projectid}/stages/{stageNumber}/project-justification")]
    public class ProjectJustificationController : BaseStageComponentController
    {
        private readonly string viewPath = "~/Views/VBPD/Application/ProjectJustification";

        public ProjectJustificationController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
            //  _listService = listService;
            //   _httpContextAccessor = httpContextAccessor;
        }

        [Route("{version}")]
        public IActionResult Detail(int version)
        {
            var model = GetViewModel(_stageId, version);
            return View($"{viewPath}/Detail.cshtml", model);
        }

        [Route("create-version")]
        public IActionResult CreateVersion(int projectId, int stageNumber)
        {
            var currentVersion = _context.ProjectJustifications
                 .AsNoTracking()
                 .Include(s => s.Stage)
                 .Where(n => n.StageId == _stageId)
                 .Max(m => m.Version);

            var model = new CreateVersionViewModel
            {
                BackPath = $"/vbpd-projects/{projectId}/stages/{stageNumber}/project-justification/{currentVersion}",
                PostPath = $"/vbpd-projects/{projectId}/stages/{stageNumber}/project-justification/create-version",
                ComponentName = "Project Justification",
                CurrentVersion = currentVersion,
            };
            return View($"{viewPath}/CreateVersion.cshtml", model);
        }

        [HttpPost]
        [Route("create-version")]
        [AutoValidateAntiforgeryToken]
        public IActionResult PostCreateVerison(int projectId, int stageNumber)
        {
            // get latest transaction of latest version
            var latestRecord = _context.ProjectJustifications
                .Include(s => s.Stage)
                .Where(n => n.StageId == _stageId)
                .OrderByDescending(o => o.CreateDate)
                .FirstOrDefault();

            if (latestRecord == null)
            {//check to see if first record.If so redirect to edit only.
                RedirectToAction("Edit");
            }
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    // set variables for create
                    latestRecord.Id = 0;
                    latestRecord.Version = ++latestRecord.Version;
                    _context.Add(latestRecord);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback();
                    throw e;
                }

                return RedirectToAction("Edit", new { stageNumber, projectId });
            }
        }

        [Route("edit")]
        public IActionResult Edit()
        {
            // always populate latest version in edit if not just an empty form
            var currentVersion = _context.ProjectJustifications
                 .AsNoTracking()
                 .Include(s => s.Stage)
                 .Where(n => n.StageId == _stageId)
                 .OrderByDescending(c => c.CreateDate)
                 .FirstOrDefault();
            var currentStage = _context.Stages.First(n => n.Id == _stageId);


            if (currentVersion == null)
            {
                var vm = new ProjectJustificationViewModel()
                {
                    StageId = currentStage.Id,
                    Versions = new List<ProjectJustificationViewModel>(),
                    Stage = currentStage,

                    //toDO producttag id does not exist

                    //ProductDropDown = _context.Tags.Include(C => C.TagCategory) 
                    //    .Where(t => t.TagCategory.Key == TagCategoryHelper.ProductLine)//
                    //    .ToList().Select(s => new SelectListItem
                    //    {
                    //        Value = s.Id.ToString(),
                    //        Text = s.Name,
                    //    }).ToList()
                };
                return View($"{viewPath}/Edit.cshtml", vm);
            }

            var model = GetViewModel(currentStage.Id, currentVersion.Version);
            model.Versions = GetVersionHistory(currentStage.Id);
            //model.RequirementSourceDropDown = _context.Tags.Include(C => C.TagCategory)
            //    .Where(t => t.TagCategory.Key == TagCategoryHelper.RequirementSource)
            //    .ToList().Select(s => new SelectListItem
            //    {
            //        Value = s.Id.ToString(),
            //        Text = s.Name,
            //        Selected = s.Id == model.RequirementSourceId
            //    }).ToList();
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(ProjectJustificationViewModel vm, int projectId, int stageNumber)
        {
            int currentVersion = 0;
            var lastProjectJustification = _context.ProjectJustifications
               .Include(s => s.Stage)
               .Where(n => n.StageId == _stageId)
               .OrderByDescending(o => o.CreateDate)
               .FirstOrDefault();
            var stage = _context.Stages.First(s => s.Id == _stageId);
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = stage;
                vm.Versions = GetVersionHistory(stage.Id);
                vm.Version = lastProjectJustification == null ? 0 : lastProjectJustification.Version;
                //vm.RequirementSourceDropDown = _context.Tags.Include(C => C.TagCategory) //todo 
                //    .Where(t => t.TagCategory.Key == TagCategoryHelper.RequirementSource)
                //    .ToList().Select(s => new SelectListItem
                //    {
                //        Value = s.Id.ToString(),
                //        Text = s.Name,
                //        Selected = s.Id == vm.RequirementSourceId
                //    }).ToList();
                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var projectJust = _mapper.Map<ProjectJustification>(vm);
            projectJust.StageId = stage.Id;
            if (lastProjectJustification == null)  //first version
            {
                projectJust.Version = 1;
                currentVersion = 1;
                _context.ProjectJustifications.Add(projectJust);
                _context.SaveChanges();
            }
            else //There is already a previous version
            {
                currentVersion = lastProjectJustification.Version;
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = lastProjectJustification.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate)//if same user then update
                {
                    lastProjectJustification.AddToInhouseTechnicalAbilities = projectJust.AddToInhouseTechnicalAbilities;
                    lastProjectJustification.AdvantagesWeOffer = projectJust.AdvantagesWeOffer;
                    lastProjectJustification.Application = projectJust.Application;
                    lastProjectJustification.BenchmarkSamples = projectJust.BenchmarkSamples;
                    lastProjectJustification.Competetion = projectJust.Competetion;
                    lastProjectJustification.CustomerMotivation = projectJust.CustomerMotivation;
                    lastProjectJustification.DrawingSpecStandards = projectJust.DrawingSpecStandards;
                    lastProjectJustification.OtherRequirements = projectJust.OtherRequirements;
                    lastProjectJustification.RegulatoryRequirements = projectJust.RegulatoryRequirements;
                    lastProjectJustification.Scope = projectJust.Scope;
                    lastProjectJustification.SingleUseProduct = projectJust.SingleUseProduct;
                    lastProjectJustification.TechnicalFeasibility = projectJust.TechnicalFeasibility;
                    lastProjectJustification.WhyOurOfferPreferred = projectJust.WhyOurOfferPreferred;

                    _context.ProjectJustifications.Update(lastProjectJustification);
                    _context.SaveChanges();
                }
                else
                {//if not then new record 
                    _context.ProjectJustifications.Add(projectJust);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Detail", new { version = currentVersion });
        }

        private List<ProjectJustificationViewModel> GetVersionHistory(int stageId)
        {
            var grouped = _context.ProjectJustifications.Include(k => k.Product)
                .Where(w => w.StageId == stageId)
                .ToList()
                .GroupBy(g => g.Version)
                .ToList();

            if (grouped.Count == 0)
            {
                return new List<ProjectJustificationViewModel>();
            }

            List<ProjectJustification> versions = new List<ProjectJustification>();
            grouped.ForEach(group => versions.Add(group.OrderByDescending(o => o.CreateDate).First()));
            var vm = _mapper.Map<List<ProjectJustificationViewModel>>(versions);
            return vm;
        }
        private ProjectJustificationViewModel GetViewModel(int stageId, int version)
        {
            var model = _context.ProjectJustifications.Where(
                s => s.StageId == stageId && s.Version == version
            ).OrderByDescending(o => o.CreateDate)
            .Include(i => i.Product)
            .Include(s => s.Stage)
            .FirstOrDefault();

            var vm = _mapper.Map<ProjectJustificationViewModel>(model);
            return vm;
        }
    }
}
