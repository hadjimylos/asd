using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pmo.Services.Lists;
using pmo.Services.PowerPoint;
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

       struct Project_Data {

            public Project _p { get; set; }
            public Project_User _u { get; set; }
            public List<Schedule> _schedules { get; set; }
            public ProductInfrigmentPatentability _pip { get; set; }
            public List<Risk> _risk { get; set; }
            public InvestmentPlan _ip { get; set; }
            public BusinessCase _bc { get; set; }
            public PostLaunchReview _plr { get; set; }
            public string _gateDescript { get; set; }

            public Project_Data(Project p, Project_User u, List<Schedule> schedules , ProductInfrigmentPatentability pip  ,List<Risk> risk , InvestmentPlan ip , BusinessCase bc ,PostLaunchReview plr ,string gateDescript) {
                _p = p;
                _u = u;
                _schedules = schedules;
                _pip = pip;
                _risk = risk;
                _ip = ip;
                _bc = bc;
                _plr = plr;
                _gateDescript = gateDescript;
            }
        }

        [Route("download")]
        public IActionResult Download() {
   
            var p = _context.Projects
                .Include(i => i.ProjectDetails)
                .First(w => w.Id == _projectId);
             
            var u = _context.Project_User
                .Where(x => x.ProjectId == _projectId && x.JobDescriptionKey == "program-manager")
                .Include(i => i.User)
                .First();
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
            string GateDescription = _stageNumber == 5 ? "PLR" : _stageNumber.ToString();
            Project_Data project_data = new Project_Data(p, u, schedules, pip, risk, ip, bc, plr, GateDescription);
            var dataObj = JsonConvert.SerializeObject(project_data, Formatting.Indented, new JsonSerializerSettings { ReferenceLoopHandling=ReferenceLoopHandling.Ignore});
            return new JsonResult(dataObj);
            
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