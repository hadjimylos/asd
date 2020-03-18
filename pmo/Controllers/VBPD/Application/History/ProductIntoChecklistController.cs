using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using pmo.Services.SharePoint;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using ViewModels.Helpers;


namespace pmo.Controllers.VBPD.Application.History
{
    [Route("projects/{projectid}/stages/{stageNumber}/product-intro-checklist")]
    public class ProductIntoChecklistController : BaseStageComponentController {

        private readonly string viewPath = "~/Views/VBPD/Application/ProductIntroChecklist";
        private readonly ISharePointService _SharePointService;

        public ProductIntoChecklistController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, ISharePointService SharePointService) : base(context, mapper, httpContextAccessor)
        {
            _SharePointService = SharePointService;
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
            var latestVersion = _context.ProductIntroChecklists
                 .AsNoTracking().GetLatestVersion(_projectId);
            var currentStage = _context.Stages.First(s=>s.Id == _stageId);
            ViewBag.CurrentStageNumber = currentStage.StageNumber;

            if (latestVersion == null)
            {
                var vm = new forms.ProductIntroChecklistForm()
                {
                    StageId = currentStage.Id,
                    Versions = GetVersionHistory(),
                    Stage = currentStage,
                    UsersDropDown = new SelectList(_context.Users.ToList(), "Id", "DisplayName"),
                };

                return View($"{viewPath}/Edit.cshtml", vm);
            }
            var model = GetViewModel( latestVersion.Version);
            model.Versions = GetVersionHistory();
            return View($"{viewPath}/Edit.cshtml", model);
        }

        [HttpPost]
        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Edit(forms.ProductIntroChecklistForm vm)
        {
            int currentVersion = 0;
            var latestProductIntoChecklist = _context.ProductIntroChecklists.GetLatestVersion(_projectId);
            var currentStage = _context.Stages.IncludeAll().First(s=>s.Id == _stageId);
            if (!ModelState.IsValid && vm.IsRequired)
            {
                ViewBag.Errors = ModelState;
                vm.Stage = _context.Stages.Where(s => s.Id == currentStage.Id).FirstOrDefault();
                vm.Versions = GetVersionHistory();
                vm.Version = latestProductIntoChecklist == null ? 0 : latestProductIntoChecklist.Version;
                vm.UsersDropDown = new SelectList(_context.Users.ToList(), "Id", "DisplayName", vm.ApprovedByUserId);
                return View($"{viewPath}/Edit.cshtml", vm);
            }
            
            var productIntroChecklist = _mapper.Map<ProductIntroChecklist>(vm);
            
            string savePath = null;

            //Save Files (only if record is marked as required)
            if (vm.IsRequired) { 
                var upload = _SharePointService.Upload(vm.File, _projectId);
                var result = JObject.Parse(upload.Result);
                string relative = result["d"]["ServerRelativeUrl"].ToString();
                string name = result["d"]["Name"].ToString();
                savePath = $"{Config.AppSettings["Sharepoint:SPFarm"]}{relative}";
                productIntroChecklist.Filename = savePath;
            }

            // Save to the database
            productIntroChecklist.StageId = currentStage.Id;
            productIntroChecklist.RemoveUnnecessaryValues(f => f.IsRequired);

            if (latestProductIntoChecklist == null)//first version
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        
                        productIntroChecklist.Version = 1;
                        currentVersion = 1;
                        _context.ProductIntroChecklists.Add(productIntroChecklist);
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
                currentVersion = latestProductIntoChecklist.Version;
                string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name;
                var isUpdate = latestProductIntoChecklist.ModifiedByUser.ToLower() == currentUser.ToLower();
                if (isUpdate && currentStage.StageNumber == latestProductIntoChecklist.Stage.StageNumber)//if same user and sameStage then update
                {
                    using (var transaction = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            latestProductIntoChecklist.ApprovedByUserId = productIntroChecklist.ApprovedByUserId;
                            latestProductIntoChecklist.ApprovedByDate = productIntroChecklist.ApprovedByDate;                            
                            latestProductIntoChecklist.IsRequired = productIntroChecklist.IsRequired;
                            latestProductIntoChecklist.Filename = savePath;

                            _context.Entry(latestProductIntoChecklist).State = EntityState.Modified;
                            _context.ProductIntroChecklists.Update(latestProductIntoChecklist);
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
                            productIntroChecklist.Version = currentVersion+=1;
                            productIntroChecklist.Filename = savePath;
                            _context.ProductIntroChecklists.Add(productIntroChecklist);
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

        private forms.ProductIntroChecklistForm GetViewModel(int version)
        {
            var model = _context.ProductIntroChecklists.Where(s => s.Version == version).GetLatestVersion(_projectId);
            var vm = _mapper.Map<forms.ProductIntroChecklistForm>(model);
            vm.UsersDropDown = new SelectList(_context.Users.ToList(), "Id", "DisplayName", vm.ApprovedByUserId);
            return vm;
        }

        private List<forms.ProductIntroChecklistForm> GetVersionHistory()
        {
            var grouped = _context.ProductIntroChecklists
                .Include(s => s.Stage)
                .Include(i=>i.ApprovedByUser)
                .Where(i => i.Stage.ProjectId == _projectId)
                .ToList()
                .GroupBy(g => g.Version)
                .ToList();

            if (grouped.Count == 0) { return new List<forms.ProductIntroChecklistForm>(); }

            List<ProductIntroChecklist> versions = new List<ProductIntroChecklist>();
            grouped.ForEach(group => versions.Add(group.OrderByDescending(o => o.CreateDate).First()));
            return _mapper.Map<List<forms.ProductIntroChecklistForm>>(versions);
        }

        private ProductIntroChecklist GetDBModel(int version)
        {
            return _context.ProductIntroChecklists.Include(i=>i.ApprovedByUser).Where(s => s.Version == version).GetLatestVersion(_projectId);
        }

        [Route("download")]
        public IActionResult Download(int id)
        {
            string filename = _SharePointService.GetFileNameFromUrl(_context.ProductIntroChecklists.Find(id).Filename);
            var content = _SharePointService.Download(filename);
            var contentType = "APPLICATION/octet-stream";
            return File(content, contentType, filename);
        }
    }
}