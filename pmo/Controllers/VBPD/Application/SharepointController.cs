using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using pmo.Services.SharePoint;
using System.Collections.Generic;
using System.Linq;
using ViewModels.Helpers;

namespace pmo.Controllers
{
    [Route("vbpd-projects/{projectId}/stages/{stageNumber}/files")]
    public class SharepointController : BaseStageComponentController {
        private readonly string documentLibraryToSharepointPath = $"{Config.AppSettings["Sharepoint:documentLibraryToSharepointPath"]}";
        private readonly ISharePointService _SharePointService;
        private readonly string path = "~/Views/VBPD/Application/Files";

        public SharepointController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor,ISharePointService SharePointService) : base(context, mapper, httpContextAccessor) {
            _SharePointService = SharePointService;
        }

        [Route("edit")]
        public IActionResult Edit(int projectId, int stageNumber) {
            var stageConfigId = _context.StageConfigs.First(f => f.StageNumber == stageNumber).Id;
            var filter = _context.StageFileConfigs.Where(w => w.StageConfigId == stageConfigId).Select(s => s.RequiredFileTagId.ToString());
            var dd = _context.TagCategories.Include(i => i.Tags).First(f => f.Key == TagCategoryHelper.StageFiles).GetListItems();
            dd.Where(w => filter.Contains(w.Value)).ToList().ForEach(f => f.Text += " *");
            ViewBag.Dropdown = dd;
            return View($"{path}/Edit.cshtml", (projectId, stageNumber));
        }

        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Edit(int projectId, int stageNumber, List<IFormFile> files, string descrip, string newTag, int tagId) {
            files.ForEach(f => {
                var asd = _SharePointService.Upload(f);
                var result = JObject.Parse(asd.Result);
                string relative = result["d"]["ServerRelativeUrl"].ToString();
                string name = result["d"]["Name"].ToString();
                string savePath = $"{Config.AppSettings["Sharepoint:SPFarm"]}/{relative}";
                _context.StageFiles.Add(new StageFile {
                    Url = relative,
                    Description = name,
                    StageId = stageNumber
                });
                

            });
            _context.SaveChanges();
            return RedirectToAction("Edit", new { projectId, stageNumber });
        }
    }
}