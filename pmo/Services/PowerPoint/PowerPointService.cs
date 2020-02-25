using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using dbModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using pmo.Controllers;
using ViewModels;

namespace pmo.Services.PowerPoint
{
    public class PowerPointService: BaseStageComponentController, IPowerPointService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string _Theme;
        private readonly string _ExportPath;
        private readonly string _whiteSpace = "                                                  ";
        private readonly string _orangeTableStyleGUID = "FABFCF23-3B69-468F-B69F-88F6DE6A72F2";
        public PowerPointService(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment) : base(context, mapper, httpContextAccessor) {
            _hostingEnvironment = hostingEnvironment;
            _Theme = _hostingEnvironment.WebRootPath + @"\PowerPointAssets\ThemeITT\Theme.thmx";
            _ExportPath = _hostingEnvironment.WebRootPath + @"\PowerPointExports\ppt";
        }

        public void CreateTitleSlide(Presentation pptPresentation, Microsoft.Office.Interop.PowerPoint.Slides slides, int SlideId, string ProjectId, string ProjectName, int GateNumber, string ProgramManager)
        {
            Microsoft.Office.Interop.PowerPoint.CustomLayout customLayoutTitle = pptPresentation.SlideMaster.CustomLayouts[4];
            Microsoft.Office.Interop.PowerPoint._Slide slide;

            Microsoft.Office.Interop.PowerPoint.TextRange objText;
            Microsoft.Office.Interop.PowerPoint.TextFrame objframe;


            // Create new Slide

            slide = slides.AddSlide(SlideId, customLayoutTitle);


            // Add title
            objText = slide.Shapes[1].TextFrame.TextRange;
            objText.Text = $"Project ID: {ProjectId} {Environment.NewLine}Project Name: {ProjectName}";
            objText.Font.Name = "Arial";
            objText.Font.Size = 24;

            objframe = slide.Shapes[2].TextFrame;
            objText = objframe.TextRange;
            objText.Text = $"Review: Gate {GateNumber} {Environment.NewLine}Date: {DateTime.Now.ToString("MM/dd/yyyy")} {Environment.NewLine}Program Manager: {ProgramManager.Replace("global\\", "")}";
            //objframe.AutoSize = PpAutoSize.ppAutoSizeShapeToFitText;

        }
        public void AddImageToSlide(Microsoft.Office.Interop.PowerPoint._Slide slide, string picturePath, int Left, int Top, int Height = -1, int Width = -1)
        {
            //string pictureFileName = _hostingEnvironment.WebRootPath + @"\PowerPointAssets\logo.png";
            //Microsoft.Office.Interop.PowerPoint.Shape shape = slide.Shapes[2];
            slide.Shapes.AddPicture(picturePath, MsoTriState.msoFalse, MsoTriState.msoTrue, Left, Top, Width, Height);
        }

        public void AddFooterToSlide(Microsoft.Office.Interop.PowerPoint._Slide slide, string FooterText = "")
        {
            slide.HeadersFooters.SlideNumber.Visible = MsoTriState.msoCTrue;
            slide.HeadersFooters.DateAndTime.Visible = MsoTriState.msoCTrue;
            slide.HeadersFooters.Footer.Visible = MsoTriState.msoCTrue;
            slide.HeadersFooters.Footer.Text = FooterText;

        }

        public string[,] GenerateTableData<T>(T TableData)
        {
            if (TableData.GetType() == typeof(ProductInfrigmentPatentability))
            {
                ProductInfrigmentPatentability pip = TableData as ProductInfrigmentPatentability;
                string[,] Data = new string[8, 2]{{ "Description", "Value"},
                    { "Contains Infingment Issues", pip.ContainsInfingmentIssues?"Yes":"No"},
                    { "Patent Number" , pip.PatentNumber},
                    {       "Issue" , pip.Issue},
                     {       "Mitigation Strategy" , pip.MitigationStrategy},
                      {      "Contains Features Requiring IP Protection" , pip.ContainsFeaturesRequiringIPProtection?"Yes":"No"},
                     {       "Invention Disclosure Submitted" , pip.InventionDisclosureSubmitted?"Yes":"No"},
                      {      "Product First Time Offered For Sale" , pip.ProductFirstTimeOfferedForSale.ToString("MM/dd/yyyy")}
                    };
                
                return Data;
            }
            else if (TableData == null)
            {
                throw new ArgumentNullException("Data is null");
            }
            return null;
        }

            public string[,] GenerateTableData<T>(List<T> TableData)
        {
            var TableObjectType = TableData.FirstOrDefault();
            if (TableData.FirstOrDefault().GetType() == typeof(BusinessCaseViewModel))
            {
                
            }
            
            else if (TableData.FirstOrDefault() == null)
            {
                throw new ArgumentNullException("Data is null");
            }
                return new string[1,1];

        }

        //In progress
        public void CreateTableSlide(Presentation pptPresentation, Microsoft.Office.Interop.PowerPoint.Slides slides, int SlideId, string Title, string[,] DataTable, string FooterText = "")
        {
            Microsoft.Office.Interop.PowerPoint.CustomLayout customLayoutContent = pptPresentation.SlideMaster.CustomLayouts[9];
            Microsoft.Office.Interop.PowerPoint._Slide slide;
            Microsoft.Office.Interop.PowerPoint.TextRange objText;
            Microsoft.Office.Interop.PowerPoint.Shape objShape;
            Microsoft.Office.Interop.PowerPoint.Table objtable;

            //slide.NotesPage.Shapes[2].TextFrame.TextRange.Text = "This demo is created using C# for PMO Project";
            //slides = pptPresentation.Slides;
            slide = slides.AddSlide(SlideId, customLayoutContent);
            AddFooterToSlide(slide, FooterText);

            objText = slide.Shapes[1].TextFrame.TextRange;
            objText.Text = Title;

            int Rows = DataTable.GetLength(0);
            int Columns = DataTable.GetLength(1);
            objShape = slide.Shapes.AddTable(Rows, Columns);
            objtable = objShape.Table;
            objtable.ApplyStyle(_orangeTableStyleGUID, true);


            for (int i = 1; i <= Rows; i++)
            {
                for (int j = 1; j <= Columns; j++)
                {
                    //objtable.Cell(i, j).Shape.Fill.Solid(.SolidFill.BackColor.RGB = 0xffffff;
                    objtable.Cell(i, j).Shape.TextFrame.TextRange.Font.Size = 12;
                    // objtable.Cell(i, j).Shape.Line.Style.BackColor.RGB = 0xFF3300;
                    objtable.Cell(i, j).Shape.TextFrame.TextRange.ParagraphFormat.Alignment = PpParagraphAlignment.ppAlignCenter;
                    objtable.Cell(i, j).Shape.TextFrame.VerticalAnchor = MsoVerticalAnchor.msoAnchorMiddle;
                    objtable.Cell(i, j).Shape.TextFrame.TextRange.Text = DataTable[i - 1, j - 1];//string.Format("[{0},{1}]", i, j);
                }

            }

            /*
             * table.Cell(i, j).Borders[Microsoft.Office.Interop.PowerPoint.PpBorderType.ppBorderLeft].DashStyle = MsoLineDashStyle.msoLineLongDashDot;
             * table.Cell(i, j).Borders[Microsoft.Office.Interop.PowerPoint.PpBorderType.ppBorderLeft].ForeColor.RGB = 0xff00ff;
             * table.Cell(i, j).Borders[Microsoft.Office.Interop.PowerPoint.PpBorderType.ppBorderLeft].Weight = 1.0f;
             */

        }

        public string CreatePowerPoint()
        {

            Microsoft.Office.Interop.PowerPoint.Application pptApplication = new Microsoft.Office.Interop.PowerPoint.Application();
            // Create the Presentation File
            Presentation pptPresentation = pptApplication.Presentations.Add(MsoTriState.msoTrue);
            pptPresentation.PageSetup.SlideSize = Microsoft.Office.Interop.PowerPoint.PpSlideSizeType.ppSlideSizeOnScreen;
            pptPresentation.ApplyTheme(_Theme);
            try
            {

                Microsoft.Office.Interop.PowerPoint.Slides slides;
                slides = pptPresentation.Slides;
                CreateTitleSlide(pptPresentation, slides, 1, "TESTID#54834", "Test Name for Test and Test and Test for Test", 2, "Georgia Nombre de Oliveira Kalyva");
                CreateTableSlide(pptPresentation, slides, 2, "Business Cases", new string[2, 10] {{"1","2","3","4","5","6","7","8","9","10" },
                                                                                                {"11","12","13","14","15","16","17","18","19","20" }}, "Gate 2 Review");
                string SaveAs = _hostingEnvironment.WebRootPath + @"\PowerPointExports\ppt" + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".pptx";
                pptPresentation.SaveAs(SaveAs, Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsDefault, MsoTriState.msoTrue);
                return SaveAs;

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                pptPresentation.Close();
                pptApplication.Quit();
            }
        }

        public string CreatePowerPointGate2(ProjectDetail project, Project_User user, BusinessCaseViewModel businessCase)
        {
            Microsoft.Office.Interop.PowerPoint.Application pptApplication = new Microsoft.Office.Interop.PowerPoint.Application();
            // Create the Presentation File
            Presentation pptPresentation = pptApplication.Presentations.Add(MsoTriState.msoTrue);
            pptPresentation.PageSetup.SlideSize = Microsoft.Office.Interop.PowerPoint.PpSlideSizeType.ppSlideSizeOnScreen;
            pptPresentation.ApplyTheme(_Theme);

            try
            {

                Microsoft.Office.Interop.PowerPoint.Slides slides;
                slides = pptPresentation.Slides;
                CreateTitleSlide(pptPresentation, slides, 1, project.Project.Id.ToString(), project.ProjectCategory.Name, 2, user.User.NetworkUsername);
                CreateTableSlide(pptPresentation, slides, 2, "Business Cases", new string[2, 10] {{"1","2","3","4","5","6","7","8","9","10" },
                                                                                                {"11","12","13","14","15","16","17","18","19","20" }}, _whiteSpace+"Gate 2 Review");

                string SaveAs = _ExportPath + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".pptx";
                pptPresentation.SaveAs(SaveAs, Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsDefault, MsoTriState.msoTrue);
                return SaveAs;

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                pptPresentation.Close();
                pptApplication.Quit();
            }
        }

        //Slides: Title Page, Schedule, Product Infringement & Patentability (if exists), Risk Analysis, Investment Plan (if exists), Business Case.
        public string CreatePowerPointGate3(ProjectDetail project, Project_User user, ProductInfrigmentPatentability pip, Risk risk, InvestmentPlan ip, BusinessCaseViewModel bc)
        {
            Microsoft.Office.Interop.PowerPoint.Application pptApplication = new Microsoft.Office.Interop.PowerPoint.Application();
            // Create the Presentation File
            Presentation pptPresentation = pptApplication.Presentations.Add(MsoTriState.msoTrue);
            pptPresentation.PageSetup.SlideSize = Microsoft.Office.Interop.PowerPoint.PpSlideSizeType.ppSlideSizeOnScreen;
            pptPresentation.ApplyTheme(_Theme);

            try
            {

                Microsoft.Office.Interop.PowerPoint.Slides slides;
                slides = pptPresentation.Slides;
                CreateTitleSlide(pptPresentation, slides, 1, project.Project.Id.ToString(), project.Project.Name, 2, user.User.NetworkUsername);

                string[,] ProductInfrigmentPatentabilityTable = GenerateTableData<ProductInfrigmentPatentability>(pip);
                CreateTableSlide(pptPresentation, slides, 2, "Product Infringement Patentability", ProductInfrigmentPatentabilityTable, _whiteSpace+"Gate 3 Review");

                string[,] RiskTable = GenerateTableData<Risk>(risk);
                CreateTableSlide(pptPresentation, slides, 3, "Risk Analysis", RiskTable, _whiteSpace + "Gate 3 Review");

                string SaveAs = _ExportPath + DateTime.Now.ToString("yyyyMMdd-HHmm") + ".pptx";
                pptPresentation.SaveAs(SaveAs, Microsoft.Office.Interop.PowerPoint.PpSaveAsFileType.ppSaveAsDefault, MsoTriState.msoTrue);
                return SaveAs;

            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                pptPresentation.Close();
                pptApplication.Quit();
            }
        }


    }
}
