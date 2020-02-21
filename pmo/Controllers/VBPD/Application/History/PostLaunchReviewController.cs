using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using ViewModels.Helpers;


namespace pmo.Controllers
{
    [Route("vbpd-projects/{projectid}/stages/{stageNumber}/post-launch-review")]
    public class PostLaunchReviewController : BaseStageComponentController {
        private readonly string viewPath = "~/Views/VBPD/Application/PostLaunchReview";
        public PostLaunchReviewController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
        }

        [Route("{version}")]
        public IActionResult Detail(int version)
        {
            var model = GetViewModel(_stageId, version);
            return View($"{viewPath}/Detail.cshtml", model);
        }

        [Route("edit")]
        public IActionResult Edit(int projectId, int stageNumber)
        {
            // always populate latest version in edit
            var currentVersion = _context.PostLaunchReviews.AsNoTracking().GetLatestVersion(_projectId);
            var currentStage = _context.Stages.First(n => n.Id == _stageId);
            if (currentVersion == null)
            {
                var vm = new PostLaunchReviewViewModel()
                {
                    StageId = currentStage.Id,
                    Versions = new List<PostLaunchReviewViewModel>(),
                    Stage = currentStage
                };

                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var model = GetViewModel(currentVersion.Id, currentVersion.Version);
            model.Versions = GetVersionHistory(currentStage.Id);
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(PostLaunchReviewViewModel vm, int projectId, int stageNumber)
        {
            int currentVersion = 0;
            var currentStage = _context.Stages.First(s => s.Id == _stageId);
            var latestPLR = _context.PostLaunchReviews.GetLatestVersion(_projectId);
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = currentStage;
                vm.Versions = GetVersionHistory(currentStage.Id);
                vm.Version = latestPLR == null ? 0 : latestPLR.Version;
                return View($"{viewPath}/Edit.cshtml", vm);
            }

            var PLR = _mapper.Map<PostLaunchReview>(vm);
            PLR.StageId = currentStage.Id;
            //first version
            if (latestPLR == null)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        currentVersion = 1;
                        PLR.Version = 1;
                        _context.PostLaunchReviews.Add(PLR);
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
            else
            { //There is already a previous version
                currentVersion = latestPLR.Version;
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestPLR.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate && currentStage.StageNumber == latestPLR.Stage.StageNumber)//if same user and sameStage then update
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            latestPLR.DoneWell = PLR.DoneWell;
                            latestPLR.DonePoorly = PLR.DonePoorly;
                            latestPLR.Bottlenecks = PLR.Bottlenecks;
                            latestPLR.LessonsLearned = PLR.LessonsLearned;
                            latestPLR.ActualVSExpected = PLR.ActualVSExpected;
                            latestPLR.Commercial = PLR.Commercial;
                            //TODO Upload Documentation as well
                            _context.PostLaunchReviews.Update(latestPLR);
                            _context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            throw e;
                        }
                    }
                }
                else// if not same user then add a new record to DB(transactions functionality)
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            PLR.Version = currentVersion+=1;
                            _context.PostLaunchReviews.Add(PLR);
                            _context.SaveChanges();
                            transaction.Commit();
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            throw e;
                        }
                    }
                }
            }

            return RedirectToAction("Detail", new { version = currentVersion });
        }

        private PostLaunchReviewViewModel GetViewModel(int stageId, int version)
        {
            var model = _context.PostLaunchReviews.Where(
                s => s.StageId == stageId && s.Version == version
            ).OrderByDescending(o => o.CreateDate)
            .Include(s => s.Stage)
            .FirstOrDefault();

            var vm = _mapper.Map<PostLaunchReviewViewModel>(model);

            return vm;
        }

        private List<PostLaunchReviewViewModel> GetVersionHistory(int stageId)
        {
            var grouped = _context.PostLaunchReviews
                .Where(w => w.StageId == stageId)
                .ToList()
                .GroupBy(g => g.Version)
                .ToList();

            if (grouped.Count == 0) { return new List<PostLaunchReviewViewModel>(); }
            List<PostLaunchReview> versions = new List<PostLaunchReview>();
            grouped.ForEach(group => versions.Add(group.OrderByDescending(o => o.CreateDate).First()));
            return _mapper.Map<List<PostLaunchReviewViewModel>>(versions);
        }
    }
}