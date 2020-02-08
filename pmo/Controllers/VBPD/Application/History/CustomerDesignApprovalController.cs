using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pmo.Services.Lists;
using ViewModels;

namespace pmo.Controllers.Application.History
{
    [Route("vbpd-projects/{projectid}/stage/{stageId}/customer-design-approval")]
    public class CustomerDesignApprovalController : BaseController {
        private readonly string viewPath = "~/Views/VBPD/Application/CustomerDesignApproval";
        private readonly IListService _listService;
        
        public CustomerDesignApprovalController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(context, mapper, httpContextAccessor)
        {
        }

        [Route("{version}")]
        public IActionResult Detail(int stageId, int version)
        {
            var model = GetViewModel(stageId, version);
            return View($"{viewPath}/Detail.cshtml", model);
        }

        [Route("create-version")]
        public IActionResult CreateVersion(int stageId,int projectId)
        {
            var currentVersion = _context.CustomerDesignApprovals
                .AsNoTracking()
                .Where(
                    w => w.StageId == stageId
                ).Max(m => m.Version);

            var model = new CreateVersionViewModel
            {
                BackPath = $"/vbpd-projects/{projectId}/stage/{stageId}/customer-design-approval/{currentVersion}",
                PostPath = $"/vbpd-projects/{projectId}/stage/{stageId}/customer-design-approval/create-version",
                ComponentName = "Customer Design Approval",
                CurrentVersion = currentVersion,
            };

            return View($"{viewPath}/CreateVersion.cshtml", model);
        }

        [HttpPost]
        [Route("create-version")]
        [AutoValidateAntiforgeryToken]
        public IActionResult PostCreateVerison(int projectId, int stageId)
        {
            // get latest transaction of latest version
            var latestRecord = _context.CustomerDesignApprovals
                .AsNoTracking()
                .Where(w => w.StageId == stageId)
                .OrderByDescending(o => o.CreateDate)
                .FirstOrDefault();

            // NOTE!!!!!!!!: here please check to see if first record. If so redirect to edit only. (not necessary for Project Detail specifically)
            if (latestRecord == null) {
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

                return RedirectToAction("Edit", new { stageId });
            }
        }


        [Route("edit")]
        public IActionResult Edit(int stageId)
        {
            // always populate latest version in edit
            //Tha skasei ama einai 0
            var currentVersion = _context.CustomerDesignApprovals
                 .AsNoTracking()
                 .Where(w => w.StageId == stageId)
                 .OrderByDescending(c => c.CreateDate)
                 .FirstOrDefault();

            
           if (currentVersion == null)
            {
                var vm = new CustomerDesignApprovalViewModel()
                {
                    StageId = stageId,
                    Versions = new List<CustomerDesignApprovalViewModel>(),
                    Stage = _context.Stages.Where(s => s.Id == stageId).FirstOrDefault()
                };
           
                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var model = GetViewModel(stageId, currentVersion.Version);
            model.Versions = GetVersionHistory(stageId);   
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(CustomerDesignApprovalViewModel vm,int stageId)
        {
            var latestCustomerDesignApproval = _context.CustomerDesignApprovals.Where(
                  w => w.StageId == stageId
              ).OrderByDescending(o => o.CreateDate)
              .FirstOrDefault();

            if (!ModelState.IsValid)
            {   ViewBag.Errors = ModelState;
                vm.Stage = _context.Stages.Where(s => s.Id == stageId).FirstOrDefault();
                vm.Versions = GetVersionHistory(stageId);
                vm.Version = latestCustomerDesignApproval == null ?  0 : latestCustomerDesignApproval.Version;
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
                        //TODO Upload Documentation as well
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
            //There is already a previous version
            else
            {
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestCustomerDesignApproval.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate)
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            customerDesignApproval.Version++;
                            //TODO Upload Documentation as well
                            _context.CustomerDesignApprovals.Update(customerDesignApproval);
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
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            customerDesignApproval.Version++;
                            // save to primary table
                            _context.CustomerDesignApprovals.Add(customerDesignApproval);
                            _context.SaveChanges();

                            InsertOneToMany(customerDesignApproval.Id);

                            // set previous versions & transactions to locked
                        }
                        catch (Exception e)
                        {
                            transaction.Rollback();
                            throw e;
                        }
                    }
                }
               

            }


            return RedirectToAction("Detail", new { stageId, version = customerDesignApproval.Version });
        }
        private CustomerDesignApprovalViewModel GetViewModel(int stageId, int version)
        {
            var model = _context.CustomerDesignApprovals.Where(
                s => s.StageId == stageId && s.Version == version
            ).OrderByDescending(o => o.CreateDate)
            .Include(i => i.ImportantDocumentation)
            .Include(s=>s.Stage)
            .FirstOrDefault();



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

            if(grouped.Count == 0)
            {
                return new List<CustomerDesignApprovalViewModel>();
            }
            
            List<CustomerDesignApproval> versions = new List<CustomerDesignApproval>();
            grouped.ForEach(group => versions.Add(group.OrderByDescending(o => o.CreateDate).First()));

            return _mapper.Map<List<CustomerDesignApprovalViewModel>>(versions);
        }
        private void SetDropdowns(CustomerDesignApprovalViewModel model)
        {
            var teamMembers = _context.Project_User.Where(p => p.ProjectId == model.Stage.ProjectId).Include(u => u.User).Select(s => new SelectListItem() {
               Text=s.User.NetworkUsername,
               Value=s.User.Id.ToString(),
            }).Distinct().ToList();
        }
        private void InsertOneToMany(int CustomerDesignApprovalId)
        {
            // TODO: insert one to many
            
            // dead code.
        }
    }
}