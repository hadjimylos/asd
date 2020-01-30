using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using pmo.Services.SharePoint;

namespace pmo.Controllers
{
    [Route("/SharePointUpload")]
    public class SharepointController : BaseController
    {
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
        public async Task<IActionResult> Upload(IFormFile file)
        {

            string Id = await _SharePointService.GetUserPrincipalId("georgia.kalyva@itt.com");

            await _SharePointService.Upload(file)
                .ContinueWith(x=> _SharePointService.BreakFileRoleInheritance(file.FileName))
                .ContinueWith(y => _SharePointService.RemoveFilePermissions(file.FileName, Id))
                .ContinueWith(z => _SharePointService.AddFilePermissions(file.FileName, Id));
            //
            
            //.ContinueWith(z=>_SharePointService.AddFilePermissions(file.FileName));
            return View();
        }
        
        
        string fileName = "ContentEditorHTML.txt";
        [Route("delete")]
        [AutoValidateAntiforgeryToken]
        [HttpGet]
        public IActionResult Delete()
        {
            _SharePointService.Delete(fileName);
            return View();
        }

    }
}