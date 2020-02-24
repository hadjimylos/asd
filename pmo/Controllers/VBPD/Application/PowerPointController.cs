using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using pmo.Services.Lists;

namespace pmo.Controllers.VBPD.Application
{
    [Route("vbpd-projects/{projectid}/stages/{stageNumber}/powerpoint")]
    public class PowerPointController : BaseStageComponentController
    {
        private readonly string path = "~/Views/VBPD/Application/PowerPoint";
        private readonly IListService _listService;
        private readonly IWebHostEnvironment _hostingEnvironment;



        public PowerPointController(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IListService listService, IWebHostEnvironment hostingEnvironment) : base(context, mapper, httpContextAccessor)
        {
            _listService = listService;
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
            var FilePath = CreatePowerPoint();
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
        public void InitializePowerPoint() 
        {
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
            objText.Text = $"Review: Gate {GateNumber} {Environment.NewLine}Date: {DateTime.Now.ToString("MM/dd/yyyy")} {Environment.NewLine}Program Manager: {ProgramManager}";
            //objframe.AutoSize = PpAutoSize.ppAutoSizeShapeToFitText;

        }
        public void AddImageToSlide(Microsoft.Office.Interop.PowerPoint._Slide slide, string picturePath, int Left, int Top, int Height = -1, int Width=-1)
        {
            //string pictureFileName = _hostingEnvironment.WebRootPath + @"\PowerPointAssets\logo.png";
            //Microsoft.Office.Interop.PowerPoint.Shape shape = slide.Shapes[2];
            slide.Shapes.AddPicture(picturePath, MsoTriState.msoFalse, MsoTriState.msoTrue, Left, Top, Width, Height);
        }
        
        //In progress
        public void CreateTableSlide(Presentation pptPresentation, Microsoft.Office.Interop.PowerPoint.Slides slides, int SlideId, string Title, List<string> TableTitles)
        {
            Microsoft.Office.Interop.PowerPoint.CustomLayout customLayoutContent = pptPresentation.SlideMaster.CustomLayouts[9];

            Microsoft.Office.Interop.PowerPoint._Slide slide;

            Microsoft.Office.Interop.PowerPoint.TextRange objText;

            Microsoft.Office.Interop.PowerPoint.Shape objShape;

            Microsoft.Office.Interop.PowerPoint.Table objtable;

            //slide.NotesPage.Shapes[2].TextFrame.TextRange.Text = "This demo is created using C# for PMO Project";
            //slides = pptPresentation.Slides;
            slide = slides.AddSlide(SlideId, customLayoutContent);
            //slide2.ApplyTheme(_hostingEnvironment.WebRootPath + @"\PowerPointAssets\ThemeITT\Theme.thmx");
            // Add title

            objText = slide.Shapes[1].TextFrame.TextRange;
            objText.Text = Title;

            objShape = slide.Shapes.AddTable(10, 5);
            objtable = objShape.Table;
            objtable.ApplyStyle("FABFCF23-3B69-468F-B69F-88F6DE6A72F2", true);
            for (int i = 1; i <= objtable.Rows.Count; i++)
            {
                for (int j = 1; j <= objtable.Columns.Count; j++)
                {
                    //objtable.Cell(i, j).Shape.Fill.Solid(.SolidFill.BackColor.RGB = 0xffffff;
                    objtable.Cell(i, j).Shape.TextFrame.TextRange.Font.Size = 12;
                    // objtable.Cell(i, j).Shape.Line.Style.BackColor.RGB = 0xFF3300;
                    objtable.Cell(i, j).Shape.TextFrame.TextRange.ParagraphFormat.Alignment = PpParagraphAlignment.ppAlignCenter;
                    objtable.Cell(i, j).Shape.TextFrame.VerticalAnchor = MsoVerticalAnchor.msoAnchorMiddle;
                    objtable.Cell(i, j).Shape.TextFrame.TextRange.Text = string.Format("[{0},{1}]", i, j);
                }

            }

            /*
             * table.Cell(i, j).Borders[Microsoft.Office.Interop.PowerPoint.PpBorderType.ppBorderLeft].DashStyle = MsoLineDashStyle.msoLineLongDashDot;
             * table.Cell(i, j).Borders[Microsoft.Office.Interop.PowerPoint.PpBorderType.ppBorderLeft].ForeColor.RGB = 0xff00ff;
             * table.Cell(i, j).Borders[Microsoft.Office.Interop.PowerPoint.PpBorderType.ppBorderLeft].Weight = 1.0f;
             */

        }

        string CreatePowerPoint()
        {
            
            Microsoft.Office.Interop.PowerPoint.Application pptApplication = new Microsoft.Office.Interop.PowerPoint.Application();
            // Create the Presentation File
            Presentation pptPresentation = pptApplication.Presentations.Add(MsoTriState.msoTrue);
            pptPresentation.PageSetup.SlideSize = Microsoft.Office.Interop.PowerPoint.PpSlideSizeType.ppSlideSizeOnScreen;
            pptPresentation.ApplyTheme(_hostingEnvironment.WebRootPath + @"\PowerPointAssets\ThemeITT\Theme.thmx");
            try
            {

                Microsoft.Office.Interop.PowerPoint.Slides slides;
                slides = pptPresentation.Slides;
                CreateTitleSlide(pptPresentation, slides, 1, "TESTID#54834", "Test Name for Test and Test and Test for Test", 2, "Georgia Nombre de Oliveira Kalyva");
                CreateTableSlide(pptPresentation, slides, 2, "Business Cases");

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
    }
}