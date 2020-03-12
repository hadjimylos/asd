using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels.Helpers;


namespace pmo.Controllers.Application.History
{
    [Route("projects/{projectid}/stages/{stageNumber}/customer-design-approval")]
    public class CustomerDesignApprovalController : BaseStageComponentController {
        private readonly string viewPath = "~/Views/VBPD/Application/CustomerDesignApproval";


        public CustomerDesignApprovalController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)

        {

        }

        [Route("{version}")]
        public IActionResult Detail(int version)
        {
            var model = GetDBModel(version);
            return View($"{viewPath}/Detail.cshtml", model);
        }

       
        [Route("edit")]
        public IActionResult Edit()
        {          
            var latestSavedVersion = _context.CustomerDesignApprovals.AsNoTracking().GetLatestVersion(_projectId);
            var currentStage = _context.Stages.First(s => s.Id == _stageId);
            ViewBag.CurrentStageNumber = currentStage.StageNumber;
            if (latestSavedVersion == null)
            {
                var vm = new forms.CustomerDesignApprovalForm()
                {
                    StageId = currentStage.Id,
                    Versions = GetVersionHistory(),
                    Stage = currentStage
                };

                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var model = GetViewModel(latestSavedVersion.Version);
            model.Versions = GetVersionHistory();
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(forms.CustomerDesignApprovalForm vm)
        {
            int currentVersion = 0;
            var currentStage = _context.Stages.Where(s=>s.Id==_stageId).First();
            var latestCustomerDesignApproval = _context.CustomerDesignApprovals.GetLatestVersion(_projectId);
            if (!ModelState.IsValid && vm.IsRequired)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = currentStage;
                vm.Versions = GetVersionHistory();
                vm.Version = latestCustomerDesignApproval == null ? 0 : latestCustomerDesignApproval.Version;
                return View($"{viewPath}/Edit.cshtml", vm);
            }
          
            var customerDesignApproval = _mapper.Map<CustomerDesignApproval>(vm);
            customerDesignApproval.StageId = currentStage.Id;

            customerDesignApproval.RemoveUnnecessaryValues(f => f.IsRequired);
            
            if (latestCustomerDesignApproval == null)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        currentVersion = 1;
                        customerDesignApproval.Version = 1;
                        _context.CustomerDesignApprovals.Add(customerDesignApproval);
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
            else //There is already a previous version
            {
                currentVersion = latestCustomerDesignApproval.Version;
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestCustomerDesignApproval.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate && currentStage.StageNumber == latestCustomerDesignApproval.Stage.StageNumber)//if same user and sameStage then update
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            latestCustomerDesignApproval.ApprovedBy = customerDesignApproval.ApprovedBy;
                            latestCustomerDesignApproval.ApprovedDate = customerDesignApproval.ApprovedDate;
                            latestCustomerDesignApproval.DateSentForApprove = customerDesignApproval.DateSentForApprove;
                            latestCustomerDesignApproval.SentForApprovalBy = customerDesignApproval.SentForApprovalBy;
                            //TODO Upload Documentation as well
                            _context.CustomerDesignApprovals.Update(latestCustomerDesignApproval);
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
                else//if not same user then add a new record to DB (transactions functionality)
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            customerDesignApproval.Version = currentVersion+=1;
                            _context.CustomerDesignApprovals.Add(customerDesignApproval);
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

            return this._editAction;
        }

        private forms.CustomerDesignApprovalForm GetViewModel(int version)
        {
            var model = _context.CustomerDesignApprovals
                 .Where(s => s.Version == version)
                 .GetLatestVersion(_projectId);

            var vm = _mapper.Map<forms.CustomerDesignApprovalForm>(model);
            return vm;
        }

        private CustomerDesignApproval GetDBModel(int version)
        {
            return _context.CustomerDesignApprovals.Where(s => s.Version == version).GetLatestVersion(_projectId);
        }
        private List<forms.CustomerDesignApprovalForm> GetVersionHistory()
        {
            var grouped = _context.CustomerDesignApprovals
                .Include(s => s.Stage)
                .Where(i => i.Stage.ProjectId == _projectId)
                .ToList()
                .GroupBy(g => g.Version)
                .ToList();

            if (grouped.Count == 0)
            {
                return new List<forms.CustomerDesignApprovalForm>();
            }

            List<CustomerDesignApproval> versions = new List<CustomerDesignApproval>();
            grouped.ForEach(group => versions.Add(group.OrderByDescending(o => o.CreateDate).First()));

            return _mapper.Map<List<forms.CustomerDesignApprovalForm>>(versions);
        }

        private void SetDropdowns(forms.CustomerDesignApprovalForm model)
        {
            var teamMembers = _context.Project_User.Where(p => p.ProjectId == model.Stage.ProjectId).Include(u => u.User).Select(s => new SelectListItem()
            {
                Text = s.User.NetworkUsername,
                Value = s.User.Id.ToString(),
            }).Distinct().ToList();
        }
    }
}