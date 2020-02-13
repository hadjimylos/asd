using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pmo.Services.SharePoint;
using ViewModels;

namespace pmo.Controllers
{
    [Route("/SharePointUpload")]
    public class SharepointController : BaseController
    {
        private readonly string viewPath = "~/Views/Application/CustomerDesignApproval";
        static string documentLibraryToSharepointPath = $"{Config.AppSettings["Sharepoint:documentLibraryToSharepointPath"]}";
        private readonly ISharePointService _SharePointService;
        public SharepointController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor,ISharePointService SharePointService) : base(context, mapper, httpContextAccessor)
        {
            _SharePointService = SharePointService;
        }

        public IActionResult Upload()
        {            
            return View();
        }

        [Route("upload")]
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files, UploadDocumentsViewModel vm)
        {
            string currentUser = _httpContextAccessor.HttpContext.User.Identity.Name.Replace("GLOBAL\\","");
            string Id = await _SharePointService.GetUserPrincipalId(currentUser + "@itt.com");
            List<IFormFile> FormFiles = files.ToList();
            foreach(var file in FormFiles)
            {
                await _SharePointService.Upload(file)
                .ContinueWith(x => _SharePointService.BreakFileRoleInheritance(file.FileName))
                .ContinueWith(y => _SharePointService.RemoveFilePermissions(file.FileName, Id))
                .ContinueWith(z => _SharePointService.AddFilePermissions(file.FileName, Id));                
            }
            var oldDodcuments = _context.CustomerDesignApprovalUploadedDocumentations.Where(x => x.CustomerDesignApprovalId == vm.ComponentId).ToList();
            _context.RemoveRange(oldDodcuments);
            foreach (var file in FormFiles) {

                var fileName = file.FileName;
                var serverRelativeUrl = documentLibraryToSharepointPath + "/" + fileName;
                var id = vm.ComponentId;
                _SharePointService.InsertOneToMany(vm.Type, id, fileName, serverRelativeUrl);
            }
            return Redirect($"/vbpd-projects/{vm.ProjectId}/stages/{vm.StageId}/{vm.Path}/edit");
        }
        
        [Route("delete")]
        [AutoValidateAntiforgeryToken]
        [HttpGet]
        public IActionResult Delete()
        {
            //Test filename
            _SharePointService.Delete("fileName");
            return View();
        }
    }
}