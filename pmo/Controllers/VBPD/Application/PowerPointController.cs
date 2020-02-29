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
        private Gate _currentGate;
        public PowerPointController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IListService listService, IPowerPointService powerpointService, IWebHostEnvironment hostingEnvironment) : base(context, mapper, httpContextAccessor)
        {
            _listService = listService;
            _powerpointService = powerpointService;
            _hostingEnvironment = hostingEnvironment;
            this._currentGate = _context.Gates.Include(s => s.GateConfig).Where(g => g.ProjectId == _projectId)
               .OrderByDescending(o => o.CreateDate).FirstOrDefault();
        }

        
        [Route("download")]
        public IActionResult Download()
        {
            ViewBag.ProjectId = _projectId;
            ViewBag.stageNumber = _stageNumber;
            ViewBag.currentGate = _currentGate;

            var p = _context.Projects.Where(w => w.Id == _projectId).OrderByDescending(x => x.CreateDate).IncludeAll().FirstOrDefault();
            var u = _context.Project_User.Where(x => x.ProjectId == _projectId && x.JobDescriptionKey == "program-manager").First();
            List<Schedule> schedules = null;
            ProductInfrigmentPatentability pip = null; 
            Risk risk = null; 
            InvestmentPlan ip = null;
            BusinessCase bc = null;
            PostLaunchReview plr = null;
            string GateDescription = "";

            string FilePath = "";
            //Gate 2 Slides requirement: Title Page, Business Case.           
            if (_currentGate.GateConfig.GateNumber == 2)
            {
                GateDescription = "2";
                bc = _context.BusinessCases.IncludeAll().GetLatestVersion(_projectId);
            }
            //Gate 3 Slides requirement: Title Page, Schedule, Product Infringement & Patentability (if exists), 
            //Risk Analysis, Investment Plan (if exists), Business Case.
            else if (_currentGate.GateConfig.GateNumber == 3)
            {
                GateDescription = "3";

                schedules = _context.Schedules
                .Include(x => x.Stage)
                .Where(n => n.Stage.Id == _stageId && n.Stage.ProjectId == _projectId)
                .Include(x => x.ScheduleType).AsNoTracking().ToList();

                pip = _context.ProductInfrigmentPatentabilities.IncludeAll().GetLatestVersion(_projectId);

                risk = _context.Risks.Include(x => x.Stage)
               .Include(x => x.RiskImpact)
               .Include(x => x.RiskType)
               .Where(x => x.StageId == _stageId && x.Stage.ProjectId==_projectId).FirstOrDefault();

                ip = _context.InvestmentPlans.AsNoTracking().GetLatestVersion(_projectId);

                bc = _context.BusinessCases.IncludeAll().GetLatestVersion(_projectId);

            }
            //Gate 4 Slides requirement:  Title Page, Risk Analysis, Business Case.
            else if (_currentGate.GateConfig.GateNumber == 4)
            {
                GateDescription = "4";

                risk = _context.Risks.Include(x => x.Stage)
               .Include(x => x.RiskImpact)
               .Include(x => x.RiskType)
               .Where(x => x.StageId == _stageId && x.Stage.ProjectId == _projectId).FirstOrDefault();

                bc = _context.BusinessCases.IncludeAll().GetLatestVersion(_projectId);
            }
            //Gate PLR Slides requirement: Title Page, Financial Review, Post Launch Review.
            else if (_currentGate.GateConfig.GateNumber == 5)
            {
                GateDescription = "PLR";

                bc = _context.BusinessCases.IncludeAll().GetLatestVersion(_projectId);

                plr = _context.PostLaunchReviews.AsNoTracking().GetLatestVersion(_projectId);
            }
            FilePath = _powerpointService.CreatePowerPointGateReview(GateDescription, 
                p.ProjectDetails.OrderByDescending(x=>x.CreateDate).FirstOrDefault(), 
                u, schedules, pip, risk, ip, bc, plr);

            if (FilePath!="")
            {
                byte[] FileData = GetFile(FilePath);
                System.IO.File.Delete(FilePath);
                var content = new System.IO.MemoryStream(FileData);
                var contentType = "APPLICATION/octet-stream";
                var fileName = "something.pptx";
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