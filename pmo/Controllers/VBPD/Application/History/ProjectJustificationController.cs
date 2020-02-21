using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            var model = GetViewModel(version);
            return View($"{viewPath}/Detail.cshtml", model);
        }

        [Route("edit")]
        public IActionResult Edit()
        {
            var currentVersion = _context.ProjectJustifications.AsNoTracking().GetLatestVersion(_projectId);
            var currentStage = _context.Stages.First(n => n.Id == _stageId);
            ViewBag.CurrentStageNumber = currentStage.StageNumber;
            if (currentVersion == null)
            {
                var vm = new ProjectJustificationViewModel()
                {
                    StageId = currentStage.Id,
                    Stage = currentStage,
                    Versions = GetVersionHistory()
            };
                return View($"{viewPath}/Edit.cshtml", vm);
            }

            var model = GetViewModel(currentVersion.Version);
            model.Versions = GetVersionHistory();
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(ProjectJustificationViewModel vm)
        {
            int currentVersion = 0;
            var lastProjectJustification = _context.ProjectJustifications.GetLatestVersion(_projectId);
            var currentStage = _context.Stages.First(s => s.Id == _stageId);
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = currentStage;
                vm.Versions = GetVersionHistory();
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
            projectJust.StageId = currentStage.Id;
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
                if (isUpdate && currentStage.StageNumber == lastProjectJustification.Stage.StageNumber)//if same user and sameStage then update
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
                    projectJust.Version = currentVersion += 1;
                    _context.ProjectJustifications.Add(projectJust);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction("Detail", new { version = currentVersion });
        }

        private List<ProjectJustificationViewModel> GetVersionHistory()
        {
            var grouped = _context.ProjectJustifications.Include(k => k.Product)
                .Include(s => s.Stage)
                .Where(i => i.Stage.ProjectId == _projectId)
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
        private ProjectJustificationViewModel GetViewModel( int version)
        {
            var model = _context.ProjectJustifications.Where(s => s.Version == version).Include(i => i.Product).GetLatestVersion(_projectId);
            var vm = _mapper.Map<ProjectJustificationViewModel>(model);
            return vm;
        }
    }
}
