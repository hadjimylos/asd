using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace pmo.Controllers.VBPD.Application.History
{
    [Route("vbpd-projects/{projectid}/stages/{stageNumber}/qualification-testing")]
    public class QualificationTestingController : BaseStageComponentController {
        private readonly string viewPath = "~/Views/VBPD/Application/QualificationTesting";
        public QualificationTestingController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
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
            //get current version before create a new one
            var currentVersion = _context.QualificationTestings
                .AsNoTracking()
                .Include(s => s.Stage)
                .Where(n => n.StageId == _stageId)
                .Max(m => m.Version);

            var model = new CreateVersionViewModel
            {
                BackPath = $"/vbpd-projects/{projectId}/stages/{stageNumber}/qualification-testing/{currentVersion}",
                PostPath = $"/vbpd-projects/{projectId}/stages/{stageNumber}/qualification-testing/create-version",
                ComponentName = "Customer Design Approval",
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
            var latestRecord = _context.QualificationTestings
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
                var previousId = latestRecord.Id;
                try
                {
                    // set variables for create
                    latestRecord.Id = 0;
                    latestRecord.Version = ++latestRecord.Version;
                    _context.Add(latestRecord);
                    _context.SaveChanges();
                    //var latestDocuments = _context.CustomerDesignApprovalUploadedDocumentations.Where(x => x.CustomerDesignApprovalId == previousId).ToList();
                    //foreach (var latestDocument in latestDocuments)
                    //{
                    //    latestDocument.Id = 0;
                    //    latestDocument.CustomerDesignApprovalId = latestRecord.Id;
                    //    _context.CustomerDesignApprovalUploadedDocumentations.Add(latestDocument);
                    //    _context.SaveChanges();
                    //}
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
        public IActionResult Edit(int stageNumber, int projectId)
        {
            // always populate latest version in edit
            //Tha skasei ama einai 0
            var currentVersion = _context.QualificationTestings
                 .AsNoTracking()
                 .Include(s => s.Stage)
                 .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
                 .OrderByDescending(c => c.CreateDate)
                 .FirstOrDefault();

            var currentStage = _context.Stages.Where(n => n.StageNumber == stageNumber && n.ProjectId == projectId).First();
            if (currentVersion == null)
            {
                var vm = new QualificationTestingViewModel()
                {
                    StageId = currentStage.Id,
                    Versions = new List<QualificationTestingViewModel>(),
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
        public IActionResult Edit(QualificationTestingViewModel vm)
        {
            int currentVersion = 0;
            var stage = _context.Stages.Where(s => s.Id == _stageId).First();
            var latestQualificationTesting = _context.QualificationTestings
                  .Include(s => s.Stage)
                  .Where(n => n.StageId == _stageId)
                  .OrderByDescending(o => o.CreateDate)
                  .FirstOrDefault();
            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = stage;
                vm.Versions = GetVersionHistory(stage.Id);
                vm.Version = latestQualificationTesting == null ? 0 : latestQualificationTesting.Version;
                return View($"{viewPath}/Edit.cshtml", vm);
            }

            var qualificationTesting = _mapper.Map<QualificationTesting>(vm);
            qualificationTesting.StageId = stage.Id;
            if (latestQualificationTesting == null)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        currentVersion = 1;
                        qualificationTesting.Version = 1;
                        _context.QualificationTestings.Add(qualificationTesting);
                        _context.SaveChanges();
                        transaction.Commit();
                        //return View($"{UploadViewPath}/UploadFiles.cshtml", new UploadDocumentsViewModel
                        //{
                        //    ComponentId = customerDesignApproval.Id,
                        //    ComponentName = "Customer Design Approval",
                        //    CurrentVersion = customerDesignApproval.Version,
                        //    StageNumber = stage.StageNumber,
                        //    ProjectId = stage.ProjectId,
                        //    Files = new List<IFormFile>(),
                        //    Type = "CustomerDesignApprovalUploadedDocumentation",
                        //    ControllerName = "CustomerDesignApproval",
                        //    Path = "customer-design-approval"
                        //});
                    }
                    catch (Exception e)
                    {
                        transaction.Rollback();
                        throw e;
                    }
                }
            }
            else //There is already a previous version
            {
                currentVersion = latestQualificationTesting.Version;
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestQualificationTesting.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate) //if same user update record
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {

                            latestQualificationTesting.IsQualificationRequired = qualificationTesting.IsQualificationRequired;
                            latestQualificationTesting.AttachmentUrl = qualificationTesting.AttachmentUrl;
                            latestQualificationTesting.Description = qualificationTesting.Description;
                            //DO Upload Documentation as well
                            _context.QualificationTestings.Update(latestQualificationTesting);
                            _context.SaveChanges();

                            transaction.Commit();
                            //return View($"{UploadViewPath}/UploadFiles.cshtml", new UploadDocumentsViewModel
                            //{
                            //    ComponentId = latestCustomerDesignApproval.Id,
                            //    ComponentName = "Customer Design Approval",
                            //    CurrentVersion = latestCustomerDesignApproval.Version,
                            //    StageNumber = stage.StageNumber,
                            //    ProjectId = stage.ProjectId,
                            //    Files = new List<IFormFile>(),
                            //    Type = "CustomerDesignApprovalUploadedDocumentation",
                            //    ControllerName = "CustomerDesignApproval",
                            //    Path = "customer-design-approval"
                            //});
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            throw e;
                        }
                    }
                }
                else//if not same user then add a new record to DB (transactions functionality)
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            qualificationTesting.Version = currentVersion;
                            _context.QualificationTestings.Add(qualificationTesting);
                            _context.SaveChanges();
                            transaction.Commit();
                            //return View($"{UploadViewPath}/UploadFiles.cshtml", new UploadDocumentsViewModel
                            //{
                            //    ComponentId = customerDesignApproval.Id,
                            //    ComponentName = "Customer Design Approval",
                            //    CurrentVersion = customerDesignApproval.Version,
                            //    StageNumber = stage.StageNumber,
                            //    ProjectId = stage.ProjectId,
                            //    Files = new List<IFormFile>(),
                            //    Type = "CustomerDesignApprovalUploadedDocumentation",
                            //    ControllerName = "CustomerDesignApproval",
                            //    Path = "customer-design-approval"

                            //});
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

        private QualificationTestingViewModel GetViewModel(int stageId, int version)
        {
            //Missing UploadedDocuments Include(what would be of upload funcionality?)
            var model = _context.QualificationTestings.Where(
                s => s.StageId == stageId && s.Version == version
            ).OrderByDescending(o => o.CreateDate)
            .Include(s => s.Stage)
            .FirstOrDefault();

            var vm = _mapper.Map<QualificationTestingViewModel>(model);
            return vm;
        }
        private List<QualificationTestingViewModel> GetVersionHistory(int stageId)
        {
            var grouped = _context.QualificationTestings
                .Where(w => w.StageId == stageId)
                .ToList()
                .GroupBy(g => g.Version)
                .ToList();

            if (grouped.Count == 0)
            {
                return new List<QualificationTestingViewModel>();
            }

            List<QualificationTesting> versions = new List<QualificationTesting>();
            grouped.ForEach(group => versions.Add(group.OrderByDescending(o => o.CreateDate).First()));

            return _mapper.Map<List<QualificationTestingViewModel>>(versions);
        }
    }
}