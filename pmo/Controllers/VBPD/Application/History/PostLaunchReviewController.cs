using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

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

        [Route("create-version")]
        public IActionResult CreateVersion(int projectId, int stageNumber)
        {
            var currentVersion = _context.PostLaunchReviews
                .AsNoTracking()
                .Include(s => s.Stage)
                .Where(n => n.StageId == _stageId)
                .Max(m => m.Version);

            var model = new CreateVersionViewModel
            {
                BackPath = $"/vbpd-projects/{projectId}/stages/{stageNumber}/post-launch-review/{currentVersion}",
                PostPath = $"/vbpd-projects/{projectId}/stages/{stageNumber}/post-launch-review/create-version",
                ComponentName = "Post Launch Review",
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
            var latestRecord = _context.PostLaunchReviews
                .Include(s => s.Stage)
                .Where(n => n.StageId == _stageId)
                .OrderByDescending(o => o.CreateDate)
                .FirstOrDefault();

            if (latestRecord == null)
            {
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
                return RedirectToAction("Edit", new { projectId, stageNumber });
            }
        }

        [Route("edit")]
        public IActionResult Edit(int projectId, int stageNumber)
        {
            // always populate latest version in edit
            var currentVersion = _context.PostLaunchReviews
                 .AsNoTracking()
                 .Include(s => s.Stage)
                 .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
                 .OrderByDescending(c => c.CreateDate)
                 .FirstOrDefault();
            var currentStage = _context.Stages.Where(n => n.StageNumber == stageNumber && n.ProjectId == projectId).First();
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
            var model = GetViewModel(currentStage.Id, currentVersion.Version);
            model.Versions = GetVersionHistory(currentStage.Id);
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(PostLaunchReviewViewModel vm, int projectId, int stageNumber)
        {
            int currentVersion = 0;
            var stage = _context.Stages.Where(n => n.Id == _stageId).First();
            var latestPLR = _context.PostLaunchReviews
               .Include(s => s.Stage)
               .Where(n => n.StageId == _stageId)
               .OrderByDescending(o => o.CreateDate)
               .FirstOrDefault();
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = stage;
                vm.Versions = GetVersionHistory(stage.Id);
                vm.Version = latestPLR == null ? 0 : latestPLR.Version;
                return View($"{viewPath}/Edit.cshtml", vm);
            }

            var PLR = _mapper.Map<PostLaunchReview>(vm);
            PLR.StageId = stage.Id;
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
                if (isUpdate)//same user trying to edit 
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
                            PLR.Version = currentVersion;
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