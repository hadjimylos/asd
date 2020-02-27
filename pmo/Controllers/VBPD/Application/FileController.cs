using AutoMapper;
using dbModels;
using forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using pmo.Services.SharePoint;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using ViewModels.Helpers;

namespace pmo.Controllers {
    [Route("vbpd-projects/{projectId}/stages/{stageNumber}/files")]
    public class FileController : BaseStageComponentController {
        private readonly string documentLibraryToSharepointPath = $"{Config.AppSettings["Sharepoint:documentLibraryToSharepointPath"]}";
        private readonly ISharePointService _SharePointService;
        private readonly string path = "~/Views/VBPD/Application/Files";

        public FileController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, ISharePointService SharePointService) : base(context, mapper, httpContextAccessor) {
            _SharePointService = SharePointService;
        }

        [Route("edit")]
        public IActionResult Edit() {
            StageFileForm model = new StageFileForm();

            var uploadedFiles = _context.StageFiles.Where(
                w =>
                    w.StageId == _stageId
            ).Select(
                s =>
                    s.FileTagId
            ).ToList();

            var requiredFiles = _context.StageFileConfigs.IncludeAll().Where(
                w =>
                    w.StageConfigId == _stageConfigId &&
                    !uploadedFiles.Contains(w.RequiredFileTagId)
            ).ToList();

            model.FileUploadDropdown = new List<SelectListItem>();
            requiredFiles.ForEach(requiredFile =>
            {
                var option = new SelectListItem
                {
                    Value = requiredFile.RequiredFile.Id.ToString(),
                    Text = requiredFile.RequiredFile.Name,
                    
                };
                model.FileUploadDropdown.Add(option);
            });

            // list of uploaded files
            ViewBag.Uploaded = _context.StageFiles
                .IncludeAll()
                .Where(
                    w =>
                        w.StageId == _stageId
                ).ToList();

            return View($"{path}/Edit.cshtml", model);
        }

        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Edit(StageFileForm files)
        {
             if (!ModelState.IsValid)
            {
                ViewBag.Errors = ModelState;
                  var uploadedFiles = _context.StageFiles.Where(w => w.StageId == _stageId).Select(s => s.FileTagId).ToList();
                  var requiredFiles = _context.StageFileConfigs.IncludeAll().Where(w =>w.StageConfigId == _stageConfigId &&!uploadedFiles.Contains(w.RequiredFileTagId)).ToList();
                  files.FileUploadDropdown = new List<SelectListItem>();
                    requiredFiles.ForEach(requiredFile =>
                    {
                            var option = new SelectListItem
                            {
                                Value = requiredFile.RequiredFile.Id.ToString(),
                                Text = requiredFile.RequiredFile.Name,

                            };
                            files.FileUploadDropdown.Add(option);
                            ViewBag.Uploaded = _context.StageFiles
                           .IncludeAll()
                           .Where(
                               w =>
                                   w.StageId == _stageId
                           ).ToList();
                    });
                    return View($"{path}/Edit.cshtml", files);
            }

            var saveFiles = files.stageFiles.Where(w => w.FileName != null).ToList();
            var projectName = _context.Projects.Find(_projectId).Name;

            saveFiles.ForEach(f =>
            {
                var upload = _SharePointService.Upload(f, projectName);
                var result = JObject.Parse(upload.Result);
                string relative = result["d"]["ServerRelativeUrl"].ToString();
                string name = result["d"]["Name"].ToString();
                string savePath = $"{Config.AppSettings["Sharepoint:SPFarm"]}{relative}";
                _context.StageFiles.Add(new StageFile
                {
                    Url = savePath,
                    Description = files.Description,
                    StageId = _stageId,
                    FileTagId = files.TagId,
                });
            });

            _context.SaveChanges();
            return this._editAction;
        }

        [Route("delete")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Delete(string url, int fileid) {
            _SharePointService.Delete(url);
            _context.Remove(_context.StageFiles.Find(fileid));
            _context.SaveChanges();
            return RedirectToAction("Edit", new { projectId = _projectId, stageNumber = _stageNumber });
        }
    }
}