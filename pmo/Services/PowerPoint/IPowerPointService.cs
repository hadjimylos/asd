using dbModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Office.Interop.PowerPoint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ViewModels;

namespace pmo.Services.PowerPoint
{
    public interface IPowerPointService
    {
        public void CreateTitleSlide(Presentation pptPresentation, Microsoft.Office.Interop.PowerPoint.Slides slides, int SlideId, string ProjectId, string ProjectName, string GateNumber, string ProgramManager);

        public void AddImageToSlide(Microsoft.Office.Interop.PowerPoint._Slide slide, string picturePath, int Left, int Top, int Height = -1, int Width = -1);

        public void AddFooterToSlide(Microsoft.Office.Interop.PowerPoint._Slide slide, string FooterText = "");

        public void CreateTableSlide(Presentation pptPresentation, Microsoft.Office.Interop.PowerPoint.Slides slides, int SlideId, string Title, string[,] DataTable, string FooterText = "");

        public string CreatePowerPointTestData();

        public string CreatePowerPointGateReview(string Gate, ProjectDetail project, Project_User user, List<Schedule> schedules, 
            ProductInfrigmentPatentability pip, List<Risk> risk, InvestmentPlan ip, BusinessCase bc, PostLaunchReview plr);



    }
}
