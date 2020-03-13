using AutoMapper;
using dbModels;
using forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using pmo.Services.Lists;
using pmo.Services.SharePoint;
using System.Collections.Generic;
using System.Linq;
using ViewModels;
using ViewModels.Helpers;

namespace pmo.Controllers {
    [Route("projects/{projectId}/stages/{stageNumber}/optional-files")]
    public class OptionalFileController : BaseStageComponentController {
        private readonly string documentLibraryToSharepointPath = $"{Config.AppSettings["Sharepoint:documentLibraryToSharepointPath"]}";
        private readonly ISharePointService _SharePointService;
        private readonly string path = "~/Views/VBPD/Application/OptionalFiles";
        private readonly IListService _listService;      
            
            public OptionalFileController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IListService listService, ISharePointService SharePointService) : base(context, mapper, httpContextAccessor) {
            _SharePointService = SharePointService;
            _listService = listService;
        }

        [Route("")]
        public IActionResult Details()
        {
            var model =  _context.OptionalFiles
                .IncludeAll()
                .Where(
                    w =>
                        w.StageId == _stageId
                ).OrderBy(x=>x.FileTagId).ThenBy(x=>x.CreateDate).ToList();

            return View($"{path}/Details.cshtml", model);
        }

        [Route("edit")]
        public IActionResult Edit() {
            List<OptionalFileForm> model = new List<OptionalFileForm>();
            model.Add(new OptionalFileForm());
            ViewBag.CategoryDropDown = _listService.Tags_SelectList("application-components");
            // list of uploaded files
            ViewBag.Uploaded = _context.OptionalFiles
                .IncludeAll()
                .Where(
                    w =>
                        w.StageId == _stageId
                ).OrderBy(x => x.FileTagId).ThenBy(x => x.CreateDate).ToList();

            return View($"{path}/Edit.cshtml", model);
        }

        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Edit(List<OptionalFileForm> files) {
            ViewBag.CategoryDropDown = _listService.Tags_SelectList("application-components");
            var saveFiles = files.Where(w => w.File != null).ToList();
            var projectName = _context.Projects.Find(_projectId).Name;

            saveFiles.ForEach(f => {
                var upload = _SharePointService.Upload(f.File, projectName);
                var result = JObject.Parse(upload.Result);
                string relative = result["d"]["ServerRelativeUrl"].ToString();
                string name = result["d"]["Name"].ToString();
                string savePath = $"{Config.AppSettings["Sharepoint:SPFarm"]}{relative}";
                _context.OptionalFiles.Add(new OptionalFile {
                    Url = savePath,
                    StageId = _stageId,
                    FileTagId = f.FileTagId,
                    Description = f.Description
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
            _context.Remove(_context.OptionalFiles.Find(fileid));
            _context.SaveChanges();
            return RedirectToAction("Edit", new { projectId = _projectId, stageNumber = _stageNumber });
        }
    }
}