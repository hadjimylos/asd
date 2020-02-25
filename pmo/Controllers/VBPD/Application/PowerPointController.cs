using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using pmo.Services.Lists;
using pmo.Services.PowerPoint;

namespace pmo.Controllers.VBPD.Application
{
    [Route("vbpd-projects/{projectid}/stages/{stageNumber}/powerpoint")]
    public class PowerPointController : BaseStageComponentController
    {
        private readonly string path = "~/Views/VBPD/Application/PowerPoint";
        private readonly IListService _listService;
        private readonly IPowerPointService _powerpointService;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public PowerPointController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IListService listService, IPowerPointService powerpointService, IWebHostEnvironment hostingEnvironment) : base(context, mapper, httpContextAccessor)
        {
            _listService = listService;
            _powerpointService = powerpointService;
            _hostingEnvironment = hostingEnvironment;
        }

        [Route("export")]
        public IActionResult Index()
        {
            return View($"{path}/Index.cshtml");
        }

        [Route("download")]
        public IActionResult Export()
        {
            //_context.ProjectDetails.Include(x=>x.Project).Where(x=>x.Id== 1).FirstOrDefault();
            //_context.Project_User.Include(x => x.Project).Include(x => x.User).Where(x => x.Id == 1).FirstOrDefault();
            //_context.ProductInfrigmentPatentabilities.Find(1);

            var FilePath = _powerpointService.CreatePowerPointGate3(_context.ProjectDetails.Include(x => x.Project).Where(x => x.Id == 1).FirstOrDefault(),
                _context.Project_User.Include(x => x.Project).Include(x => x.User).Where(x => x.Id == 1).FirstOrDefault(),
                _context.ProductInfrigmentPatentabilities.Find(1),
                null, null, null);
            byte[] FileData = GetFile(FilePath);
            System.IO.File.Delete(FilePath);
            var content = new System.IO.MemoryStream(FileData);
            var contentType = "APPLICATION/octet-stream";
            var fileName = "something.pptx";
            return File(content, contentType, fileName);
        }

        byte[] GetFile(string s)
        {
            using (System.IO.FileStream fs = System.IO.File.OpenRead(s))
            {
                byte[] data = new byte[fs.Length];
                int br = fs.Read(data, 0, data.Length);
                if (br != fs.Length)
                    throw new System.IO.IOException(s);
                return data;
            }
        }
       

       
    }
}