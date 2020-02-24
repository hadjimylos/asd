using AutoMapper;
using dbModels;
using forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            List<FileForm> model = new List<FileForm>();

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


            requiredFiles.ForEach(requiredFile => {
                model.Add(
                    new FileForm {
                        TagDescription = requiredFile.RequiredFile.Name,
                        TagId = requiredFile.RequiredFileTagId,
                    });
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
        public IActionResult Edit(List<FileForm> files) {
            var saveFiles = files.Where(w => w.File != null).ToList();
            var projectName = _context.Projects.Find(_projectId).Name;

            saveFiles.ForEach(f => {
                var upload = _SharePointService.Upload(f.File, projectName);
                var result = JObject.Parse(upload.Result);
                string relative = result["d"]["ServerRelativeUrl"].ToString();
                string name = result["d"]["Name"].ToString();
                string savePath = $"{Config.AppSettings["Sharepoint:SPFarm"]}{relative}";
                _context.StageFiles.Add(new StageFile {
                    Url = savePath,
                    Description = f.Description,
                    StageId = _stageId,
                    FileTagId = f.TagId,
                });
            });

            _context.SaveChanges();
            return RedirectToAction("Edit", new { projectId = _projectId, stageNumber = _stageNumber });
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