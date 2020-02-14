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
    public class SharepointController : BaseController {
        static string documentLibraryToSharepointPath = $"{Config.AppSettings["Sharepoint:documentLibraryToSharepointPath"]}";
        private readonly ISharePointService _SharePointService;
        public SharepointController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor,ISharePointService SharePointService) : base(context, mapper, httpContextAccessor) {
            _SharePointService = SharePointService;
        }
    }
}