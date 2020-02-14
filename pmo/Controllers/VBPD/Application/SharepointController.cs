using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pmo.Services.SharePoint;
using System.Collections.Generic;

namespace pmo.Controllers
{
    [Route("vbpd-projects/{projectId}/stages/{stageNumber}/files")]
    public class SharepointController : BaseProjectDetailController {
        private readonly string documentLibraryToSharepointPath = $"{Config.AppSettings["Sharepoint:documentLibraryToSharepointPath"]}";
        private readonly ISharePointService _SharePointService;
        private readonly string path = "~/Views/VBPD/Application/Files";

        public SharepointController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor,ISharePointService SharePointService) : base(context, mapper, httpContextAccessor) {
            _SharePointService = SharePointService;
        }

        [Route("edit")]
        public IActionResult Edit(int projectId, int stageNumber) {
            return View($"{path}/Edit.cshtml", (projectId, stageNumber));
        }

        [Route("edit")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Edit(int projectId, int stageNumber, List<IFormFile> files, string descrip, string newTag, int tagId) {
            return RedirectToAction("Edit", new { projectId, stageNumber });
        }
    }
}