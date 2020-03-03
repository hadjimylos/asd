using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using pmo.Services.Lists;
using pmo.Services.PowerPoint;
using ViewModels;
using ViewModels.Helpers;

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

        
        [Route("download")]
        public IActionResult Download()
        {
            ViewBag.ProjectId = _projectId;
            ViewBag.stageNumber = _stageNumber;
            //ViewBag.currentGate = _currentGate;

            var p = _context.Projects.Where(w => w.Id == _projectId).OrderByDescending(x => x.CreateDate).IncludeAll().FirstOrDefault();
            var u = _context.Project_User.Where(x => x.ProjectId == _projectId && x.JobDescriptionKey == "program-manager").First();
            List<Schedule> schedules = schedules = _context.Schedules
                .Include(x => x.Stage)
                .Where(n => n.Stage.Id == _stageId && n.Stage.ProjectId == _projectId)
                .Include(x => x.ScheduleType).AsNoTracking().ToList(); 
            ProductInfrigmentPatentability pip = _context.ProductInfrigmentPatentabilities.IncludeAll().GetLatestVersion(_projectId);
            List<Risk> risk = _context.Risks.Include(x => x.Stage)
               .Include(x => x.RiskImpact)
               .Include(x => x.RiskType)
               .Where(x => x.StageId == _stageId && x.Stage.ProjectId == _projectId).ToList();
            InvestmentPlan ip = _context.InvestmentPlans.AsNoTracking().GetLatestVersion(_projectId);
            BusinessCase bc = _context.BusinessCases.IncludeAll().GetLatestVersion(_projectId); 
            PostLaunchReview plr = _context.PostLaunchReviews.AsNoTracking().GetLatestVersion(_projectId);
            string GateDescription = _stageNumber == 5?"PLR": _stageNumber.ToString();
            
            string FilePath = "";   

            FilePath = _powerpointService.CreatePowerPointGateReview(GateDescription, 
                p.ProjectDetails.OrderByDescending(x=>x.CreateDate).FirstOrDefault(), 
                u, schedules, pip, risk, ip, bc, plr);

            if (FilePath!="")
            {
                byte[] FileData = GetFile(FilePath);
                System.IO.File.Delete(FilePath);
                var content = new System.IO.MemoryStream(FileData);
                var contentType = "APPLICATION/octet-stream";
                var fileName = $"{p.Name} - Gate {GateDescription} Review.pptx";
                return File(content, contentType, fileName);
            }
            return BadRequest();
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