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
    [Route("projects/{projectId}/stages/{stageNumber}/gate-files")]
    public class GateFileController : BaseStageComponentController {
        private readonly string documentLibraryToSharepointPath = $"{Config.AppSettings["Sharepoint:documentLibraryToSharepointPath"]}";
        private readonly ISharePointService _SharePointService;
        private readonly string path = "~/Views/VBPD/Application/GateFiles";

        public GateFileController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, ISharePointService SharePointService) : base(context, mapper, httpContextAccessor) {
            _SharePointService = SharePointService;
        }

        [Route("edit")]
        public IActionResult Edit() {
            List<FileForm> model = new List<FileForm>();
            model.Add(new FileForm {
                TagDescription = "Upload any pertinent documentation"
            });

            // list of uploaded files
            ViewBag.Uploaded = _context.GateFiles
                .IncludeAll()
                .Where(
                    w =>
                        w.GateNumber == _stageNumber &&
                        w.Gate.ProjectId == _projectId
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
                var gate = _context.Gates.Where(x => x.ProjectId == _projectId).First();
                _context.GateFiles.Add(new GateFile {
                    Url = savePath,
                    Description = f.Description,
                    GateNumber = _stageNumber,
                    Gate = gate
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
            _context.Remove(_context.GateFiles.Find(fileid));
            _context.SaveChanges();
            return RedirectToAction("Edit", new { projectId = _projectId, stageNumber = _stageNumber });
        }

        [Route("download")]
        public IActionResult Download(int id)
        {
            string filename = _SharePointService.GetFileNameFromUrl(_context.GateFiles.Find(id).Url);
            var content = _SharePointService.Download(filename);
            var contentType = "APPLICATION/octet-stream";
            return File(content, contentType, filename);
        }
    }
}