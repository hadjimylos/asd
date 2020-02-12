using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace pmo.Controllers.Application.History
{
    [Route("vbpd-projects/{projectid}/stage/{stageNumber}/customer-design-approval")]
    public class CustomerDesignApprovalController : BaseProjectDetailController
    {
        private readonly string viewPath = "~/Views/VBPD/Application/CustomerDesignApproval";

        public CustomerDesignApprovalController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)

        {

        }

        [Route("{version}")]
        public IActionResult Detail(int projectId, int stageNumber, int version)
        {
            var stageId = _context.Stages.Where(n => n.StageNumber == stageNumber && n.ProjectId == projectId).First().Id;
            var model = GetViewModel(stageId, version);
            return View($"{viewPath}/Detail.cshtml", model);
        }

        [Route("create-version")]
        public IActionResult CreateVersion(int projectId, int stageNumber)
        {
            //get current version before create a new one
            var currentVersion = _context.CustomerDesignApprovals
                .AsNoTracking()
                .Include(s => s.Stage)
                .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
                .Max(m => m.Version);

            var model = new CreateVersionViewModel
            {
                BackPath = $"/vbpd-projects/{projectId}/stage/{stageNumber}/customer-design-approval/{currentVersion}",
                PostPath = $"/vbpd-projects/{projectId}/stage/{stageNumber}/customer-design-approval/create-version",
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
            var latestRecord = _context.CustomerDesignApprovals
                .AsNoTracking()
                .Include(s => s.Stage)
                .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
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
                    latestRecord.StageId = latestRecord.Stage.Id;
                    latestRecord.Stage = null;//remove relation before saving 
                    latestRecord.Version = ++latestRecord.Version;
                    _context.Add(latestRecord);
                    _context.SaveChanges();
                    var latestDocuments = _context.CustomerDesignApprovalUploadedDocumentations.Where(x => x.CustomerDesignApprovalId == previousId).ToList();
                    foreach (var latestDocument in latestDocuments)
                    {
                        latestDocument.Id = 0;
                        latestDocument.CustomerDesignApprovalId = latestRecord.Id;
                        _context.CustomerDesignApprovalUploadedDocumentations.Add(latestDocument);
                        _context.SaveChanges();
                    }
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
            var currentVersion = _context.CustomerDesignApprovals
                 .AsNoTracking()
                 .Include(s => s.Stage)
                 .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
                 .OrderByDescending(c => c.CreateDate)
                 .FirstOrDefault();

            var currentStage = _context.Stages.Where(n => n.StageNumber == stageNumber && n.ProjectId == projectId).First();
            if (currentVersion == null)
            {
                var vm = new CustomerDesignApprovalViewModel()
                {
                    StageId = currentStage.Id,
                    Versions = new List<CustomerDesignApprovalViewModel>(),
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
        public IActionResult Edit(CustomerDesignApprovalViewModel vm, int projectId, int stageNumber)
        {
            //get Stage Entity
            var stage = _context.Stages.Where(n => n.StageNumber == stageNumber && n.ProjectId == projectId).First();
            //get latest transaction of latest version
            var latestCustomerDesignApproval = _context.CustomerDesignApprovals.AsNoTracking()
                  .Include(s => s.Stage)
                  .Where(n => n.Stage.StageNumber == stageNumber && n.Stage.ProjectId == projectId)
                  .OrderByDescending(o => o.CreateDate)
                  .FirstOrDefault();

            if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = stage;
                vm.Versions = GetVersionHistory(stage.Id);
                vm.Version = latestCustomerDesignApproval == null ? 0 : latestCustomerDesignApproval.Version;
                return View($"{viewPath}/Edit.cshtml", vm);
            }

            var customerDesignApproval = _mapper.Map<CustomerDesignApproval>(vm);
            if (latestCustomerDesignApproval == null)
            {

                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        customerDesignApproval.Version = 1;
                        customerDesignApproval.StageId = stage.Id;
                        _context.CustomerDesignApprovals.Add(customerDesignApproval);
                        _context.SaveChanges();
                        transaction.Commit();
                        return View($"{viewPath}/UploadFiles.cshtml", new UploadDocumentsViewModel
                        {
                            ComponentId = customerDesignApproval.Id,
                            ComponentName = "Customer Design Approval",
                            CurrentVersion = customerDesignApproval.Version,
                            StageId = stage.Id,
                            ProjectId = stage.ProjectId,
                            Files = new List<IFormFile>(),
                            Type = "CustomerDesignApprovalUploadedDocumentation",
                            ControllerName = "CustomerDesignApproval",
                            Path= "customer-design-approval"

                        });
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
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestCustomerDesignApproval.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate) //if same user update record
                {
                    customerDesignApproval.Version = latestCustomerDesignApproval.Version;
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            customerDesignApproval.Id = latestCustomerDesignApproval.Id;
                            customerDesignApproval.StageId = stage.Id;
                            customerDesignApproval.CreateDate = latestCustomerDesignApproval.CreateDate;
                            //TODO Upload Documentation as well
                            _context.CustomerDesignApprovals.Update(customerDesignApproval);
                            _context.SaveChanges();

                            transaction.Commit();
                            return View($"{viewPath}/UploadFiles.cshtml", new UploadDocumentsViewModel
                            {
                                ComponentId = customerDesignApproval.Id,
                                ComponentName = "Customer Design Approval",
                                CurrentVersion = customerDesignApproval.Version,
                                StageId = stage.Id,
                                ProjectId = stage.ProjectId,
                                Files = new List<IFormFile>(),
                                Type = "CustomerDesignApprovalUploadedDocumentation",
                                ControllerName = "CustomerDesignApproval",
                                 Path = "customer-design-approval"
                            });
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


                            _context.CustomerDesignApprovals.Add(customerDesignApproval);
                            _context.SaveChanges();
                            transaction.Commit();
                            return View($"{viewPath}/UploadFiles.cshtml", new UploadDocumentsViewModel
                            {
                                ComponentId = customerDesignApproval.Id,
                                ComponentName = "Customer Design Approval",
                                CurrentVersion = customerDesignApproval.Version,
                                StageId = stage.Id,
                                ProjectId = stage.ProjectId,
                                Files = new List<IFormFile>(),
                                Type = "CustomerDesignApprovalUploadedDocumentation",
                                ControllerName = "CustomerDesignApproval",
                                Path = "customer-design-approval"

                            });
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();

                            throw e;
                        }
                    }
                }
            }
        }
        private CustomerDesignApprovalViewModel GetViewModel(int stageId, int version)
        {
            var model = _context.CustomerDesignApprovals.Where(
                s => s.StageId == stageId && s.Version == version
            ).OrderByDescending(o => o.CreateDate)
            .Include(i => i.ImportantDocumentation)
            .Include(s => s.Stage)
            .FirstOrDefault();

            model.ImportantDocumentation = _context.CustomerDesignApprovalUploadedDocumentations.Where(x => x.CustomerDesignApprovalId == model.Id).ToList();
            var vm = _mapper.Map<CustomerDesignApprovalViewModel>(model);
            return vm;
        }
        private List<CustomerDesignApprovalViewModel> GetVersionHistory(int stageId)
        {
            var grouped = _context.CustomerDesignApprovals
                .Where(w => w.StageId == stageId)
                .ToList()
                .GroupBy(g => g.Version)
                .ToList();

            if (grouped.Count == 0)
            {
                return new List<CustomerDesignApprovalViewModel>();
            }

            List<CustomerDesignApproval> versions = new List<CustomerDesignApproval>();
            grouped.ForEach(group => versions.Add(group.OrderByDescending(o => o.CreateDate).First()));

            return _mapper.Map<List<CustomerDesignApprovalViewModel>>(versions);
        }

        private void SetDropdowns(CustomerDesignApprovalViewModel model)
        {
            var teamMembers = _context.Project_User.Where(p => p.ProjectId == model.Stage.ProjectId).Include(u => u.User).Select(s => new SelectListItem()
            {
                Text = s.User.NetworkUsername,
                Value = s.User.Id.ToString(),
            }).Distinct().ToList();
        }
    }
}