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
    public class PowerPointService : BaseStageComponentController, IPowerPointService
    {
        /// <summary>
        /// Environment Initializations
        /// </summary>
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly string _Theme;
        private readonly string _ExportPath;
        private readonly string _whiteSpace = "                                                  ";
        private readonly string _orangeTableStyleGUID = "FABFCF23-3B69-468F-B69F-88F6DE6A72F2";
        public PowerPointService(EfContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment hostingEnvironment) : base(context, mapper, httpContextAccessor)
        {
            _hostingEnvironment = hostingEnvironment;
            _Theme = _hostingEnvironment.WebRootPath + @"\PowerPointAssets\ThemeITT\Theme.thmx";
            _ExportPath = _hostingEnvironment.WebRootPath + @"\PowerPointExports\ppt";
        }
        /// <summary>
        /// Creates the Title Slide
        /// </summary>
        /// <param name="pptPresentation">Presentation object</param>
        /// <param name="slides">Slides object</param>
        /// <param name="SlideId">Slide increment</param>
        /// <param name="ProjectId">Project ID</param>
        /// <param name="ProjectName">Project Name</param>
        /// <param name="GateNumber">Gate Number</param>
        /// <param name="ProgramManager">Program Manager</param>
        public void CreateTitleSlide(Presentation pptPresentation, Microsoft.Office.Interop.PowerPoint.Slides slides, int SlideId, string ProjectId, string ProjectName, string GateNumber, string ProgramManager)
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
        /// <summary>
        /// Adds Image to specified position on slide
        /// </summary>
        /// <param name="slide">Slide object</param>
        /// <param name="picturePath">Picture Path on Server</param>
        /// <param name="Left">x</param>
        /// <param name="Top">y</param>
        /// <param name="Height">Height</param>
        /// <param name="Width">Width</param>
        public void AddImageToSlide(Microsoft.Office.Interop.PowerPoint._Slide slide, string picturePath, int Left, int Top, int Height = -1, int Width = -1)
        {
            //string pictureFileName = _hostingEnvironment.WebRootPath + @"\PowerPointAssets\logo.png";
            //Microsoft.Office.Interop.PowerPoint.Shape shape = slide.Shapes[2];
            slide.Shapes.AddPicture(picturePath, MsoTriState.msoFalse, MsoTriState.msoTrue, Left, Top, Width, Height);
        }

        /// <summary>
        /// Formats the slide's footer 
        /// </summary>
        /// <param name="slide">Slide increment</param>
        /// <param name="FooterText">Footer Text</param>
        public void AddFooterToSlide(Microsoft.Office.Interop.PowerPoint._Slide slide, string FooterText = "")
        {
            slide.HeadersFooters.SlideNumber.Visible = MsoTriState.msoCTrue;
            slide.HeadersFooters.DateAndTime.Visible = MsoTriState.msoCTrue;
            slide.HeadersFooters.Footer.Visible = MsoTriState.msoCTrue;
            slide.HeadersFooters.Footer.Text = _whiteSpace+FooterText;

        }

        /// <summary>
        /// Generates a 2D Data Table from a given Objec
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="TableData">Table Data</param>
        /// <returns></returns>
        public string[,] GenerateTableData<T>(T TableData, BusinessCaseTableType type = BusinessCaseTableType.None)
        {
            if (TableData.GetType() == typeof(BusinessCase))
            {
                switch (type)
                {
                     case BusinessCaseTableType.Simple:
                        BusinessCase bc = TableData as BusinessCase;
                        string[,] Data = new string[22, 2]{{ "Description", "Value"},
                            { "Will Customer Fund Qual?", bc.WillCustomerFundQual?"Yes":"No"},
                            { "Will Customer Fund Tooling?", bc.WillCustomerFundTooling?"Yes":"No"},
                            { "Probabilty Of Win", bc.ProbabiltyOfWin.ToString()+"%"},
                            { "Target First Year Gross Margin", bc.TargetFirstYearGrossMargin.ToString()},
                            { "Financial Start Year", bc.FinancialStartYear.ToString()},
                            { "Discount Rate", bc.DiscountRate.ToString()},
                            { "TaxRate", bc.TaxRate.ToString()},
                            { "Labor Rate", bc.LaborRate.ToString()},
                            { "Gpa Requirements", bc.GpaRequirements},
                            { "Multiple Fields Generated Table", bc.MultipleFieldsGeneratedTable.ToString()},
                            { "Project Scope", bc.ProjectScope},
                            { "Work Requirement Amount", bc.WorkRequirementAmount.ToString()},
                            { "Strategic Alignment", bc.StrategicAlignment.ToString()},
                            { "Add To Tecnical Abilities", bc.AddToTecnicalAbilities},
                            { "Project Completion", bc.ProjectCompletion.ToString("MM/dd/yyyy")},
                            { "Time From Project Completion To Revenue Generation", bc.TimeFromProjectCompletionToRevenueGeneration.ToString()},
                            { "Customer Market Analysis", bc.CustomerMarketAnalysis},
                            { "Changes", bc.Changes.ToString()},
                            { "NPV", bc.GetNPV().ToString()},
                            { "ROI", bc.GetROI().ToString()},
                            { "Get Payback Period", bc.GetPaybackPeriod().ToString()}
                            };
                        return Data;
                    case BusinessCaseTableType.FinancialsCalculationsCost:
                        List<FinancialData> fd = (TableData as BusinessCase).FinancialData as List<FinancialData>;
                        int Count = fd.Count+2;
                        string[,] Data2 = new string[Count, 8];
                        Data2[0, 0] = "Year";
                        Data2[0, 1] = "Quantity";
                        Data2[0, 2] = "Standard Cost Estimated";
                        Data2[0, 3] = "Sales Price Estimated";
                        Data2[0, 4] = "Cost Extended";
                        Data2[0, 5] = "Revenue Extended";
                        Data2[0, 6] = "Standard Margin Price";
                        Data2[0, 7] = "Standard Margin Price %";

                        for (int i = 1; i <= Count-2; i++)
                        {
                            Data2[i, 0] = fd[i - 1].Year.ToString();
                            Data2[i, 1] = fd[i - 1].Quantity.ToString();
                            Data2[i, 2] = fd[i - 1].StdCostEstimated.ToString();
                            Data2[i, 3] = fd[i - 1].SalesPriceEstimated.ToString();
                            Data2[i, 4] = fd[i - 1].GetCostExtended().ToString();
                            Data2[i, 5] = fd[i - 1].GetRevenueExtended().ToString();
                            Data2[i, 6] = fd[i - 1].GetStandardMarginPrice().ToString();
                            Data2[i, 7] = fd[i - 1].GetStandardMarginPercent().ToString();
                        }

                        Data2[Count - 1, 0] = "Total";
                        Data2[Count - 1, 1] = fd.Sum(x=>x.Quantity).ToString();
                        Data2[Count - 1, 2] = "-";
                        Data2[Count - 1, 3] = "-";
                        Data2[Count - 1, 4] = fd.Sum(x => x.GetCostExtended()).ToString();
                        Data2[Count - 1, 5] = fd.Sum(x => x.GetRevenueExtended()).ToString();
                        Data2[Count - 1, 6] = fd.Sum(x => x.GetStandardMarginPrice()).ToString();
                        Data2[Count - 1, 7] = (fd.Sum(x => x.GetStandardMarginPercent())/(Count-2)).ToString();
                        return Data2;
                    case BusinessCaseTableType.FinancialsCalculationsExpenses:
                        List<FinancialData> fd2 = (TableData as BusinessCase).FinancialData as List<FinancialData>;
                        int Count2 = fd2.Count + 2;
                        string[,] Data3 = new string[Count2, 6];
                        Data3[0, 0] = "Year";
                        Data3[0, 1] = "GPA Capital";
                        Data3[0, 2] = "GPA Expense";
                        Data3[0, 3] = "Qual Costs";
                        Data3[0, 4] = "Other Development Expenses";
                        Data3[0, 5] = "Total Expenses";

                        for (int i = 1; i <= Count2-2; i++)
                        {
                            Data3[i, 0] = fd2[i - 1].Year.ToString();
                            Data3[i, 1] = fd2[i - 1].GPACapital.ToString();
                            Data3[i, 2] = fd2[i - 1].GPAExpense.ToString();
                            Data3[i, 3] = fd2[i - 1].QualCosts.ToString();
                            Data3[i, 4] = fd2[i - 1].OtherDevelopmentExpenses.ToString();
                            Data3[i, 5] = fd2[i - 1].GetTotalExpenses().ToString();
                        }

                        Data3[Count2 - 1, 0] = "Total";
                        Data3[Count2 - 1, 1] = fd2.Sum(x => x.GPACapital).ToString();
                        Data3[Count2 - 1, 2] = fd2.Sum(x => x.GPAExpense).ToString();
                        Data3[Count2 - 1, 3] = fd2.Sum(x => x.QualCosts).ToString();
                        Data3[Count2 - 1, 4] = fd2.Sum(x => x.OtherDevelopmentExpenses).ToString();
                        Data3[Count2 - 1, 5] = fd2.Sum(x => x.GetTotalExpenses()).ToString();
                        return Data3;
                    default:
                        throw new ArgumentNullException("Business Case Table Type is not specified");                        
                }  
            }
            if (TableData.GetType() == typeof(PostLaunchReview))
            {
                PostLaunchReview plr = TableData as PostLaunchReview;
                string[,] Data = new string[7, 2]{{ "Description", "Value"},
                    { "Whas was done well", plr.DoneWell},
                    { "Whas was done poorly", plr.DonePoorly},
                    { "Bottlenecks or obstacles encountered", plr.Bottlenecks},
                    { "Actual VS Expected Results comparison", plr.ActualVSExpected},
                    { "Commercial success or failure", plr.Commercial},
                    { "Lessons learned", plr.LessonsLearned},
                    };

                return Data;
            }
            else if (TableData.GetType() == typeof(InvestmentPlan))
            {
                InvestmentPlan ip = TableData as InvestmentPlan;
                string[,] Data = new string[7, 2]{{ "Description", "Value"},
                    { "Item Number", ip.ItemNumber},
                    { "Item" , ip.Item},
                    { "Purchased From" , ip.PurchasedFrom},
                    { "Ship To location" , ip.ShipToLocation},
                    { "Cost" , ip.Cost},
                    { "Terms" , ip.Terms}
                    };

                return Data;
            }
            else if (TableData.GetType() == typeof(Risk))
            {
                Risk risk = TableData as Risk;
                string[,] Data = new string[5, 2]{{ "Description", "Value"},
                    { "Name", risk.Name},
                    { "Risk Probability" , risk.RiskPropability+"%"},
                    { "Risk Type" , risk.RiskType.Name},
                    { "Risk Impact" , risk.RiskImpact.Name}
                    };

                return Data;
            }
            else if (TableData.GetType() == typeof(ProductInfrigmentPatentability))
            {
                ProductInfrigmentPatentability pip = TableData as ProductInfrigmentPatentability;
                string[,] Data = new string[8, 2]{{ "Description", "Value"},
                    { "Contains Infingment Issues", pip.ContainsInfingmentIssues?"Yes":"No"},
                    { "Patent Number" , pip.PatentNumber},
                    { "Issue" , pip.Issue},
                    { "Mitigation Strategy" , pip.MitigationStrategy},
                    { "Contains Features Requiring IP Protection" , pip.ContainsFeaturesRequiringIPProtection?"Yes":"No"},
                    { "Invention Disclosure Submitted" , pip.InventionDisclosureSubmitted?"Yes":"No"},
                    { "Product First Time Offered For Sale" , pip.ProductFirstTimeOfferedForSale.ToString("MM/dd/yyyy")}
                    };

                return Data;
            }
            else if (TableData == null)
            {
                throw new ArgumentNullException("Data is null");
            }
            throw new ArgumentNullException("Data is null");
        }

        /// <summary>
        /// Generates a 2D Data Table from a given List of Objects
        /// </summary>
        /// <typeparam name="T">Object Type</typeparam>
        /// <param name="TableData"> Table Data</param>
        /// <returns></returns>
        public string[,] GenerateTableData<T>(List<T> TableData, BusinessCaseTableType type = BusinessCaseTableType.None)
        {
            var TableObjectType = TableData.FirstOrDefault();

           if (TableData.FirstOrDefault().GetType() == typeof(Schedule))
            {
                List<Schedule> schedule = TableData as List<Schedule>;
                int Count = schedule.Count+1;
                string[,] Data = new string[Count, 2];
                Data[0, 0] = "Milestone";
                Data[0, 1] = "Date";

                for (int i = 1; i < Count; i++)
                {
                    Data[i, 0] = schedule[i - 1].ScheduleType.Name;
                    Data[i, 1] = schedule[i - 1].Date.ToString("MM/dd/yyyy");
                }

                return Data;
            }
            else if (TableData.FirstOrDefault().GetType() == typeof(Risk))
            {                

                List<Risk> risk = TableData as List<Risk>;
                int Count = risk.Count + 1;
                string[,] Data = new string[Count, 4];
                Data[0, 0] = "Name";
                Data[0, 1] = "Risk Probability";
                Data[0, 2] = "Risk Type";
                Data[0, 3] = "Risk Impact";

                for (int i = 1; i < Count; i++)
                {
                    Data[i, 0] = risk[i - 1].Name;
                    Data[i, 1] = risk[i - 1].RiskPropability.ToString();
                    Data[i, 2] = risk[i - 1].RiskType.Name;
                    Data[i, 3] = risk[i - 1].RiskImpact.Name;

                }
                return Data;
            }
            else  if (TableData.FirstOrDefault() == null)
            {
                throw new ArgumentNullException("Data is null");
            }
            return new string[1, 1];

        }

        /// <summary>
        /// Creates a Table Slide with Title text
        /// </summary>
        /// <param name="pptPresentation">Presentation object</param>
        /// <param name="slides">Slides Object</param>
        /// <param name="SlideId">Slide Increment</param>
        /// <param name="Title">Slide Title</param>
        /// <param name="DataTable">Slide 2D Array</param>
        /// <param name="FooterText">Footer Text</param>
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

        public int CreateTableSlides(Presentation pptPresentation, Microsoft.Office.Interop.PowerPoint.Slides slides, int SlideId, string Title, string[,] DataTable, string FooterText = "")
        {
            int maxRowCount = 11;

            int totalRows = DataTable.GetLength(0); 
            int howManyLists = (int)(Math.Ceiling((totalRows / (decimal)maxRowCount)));

            var results = new List<string[,]>();
            for (int i = 1; i < howManyLists + 1; i++) {
                var newTable = new string[maxRowCount, DataTable.GetLength(1)];
                
                for (int k = 0; k < DataTable.GetLength(1); k++) {
                    newTable[0, k] = DataTable[0, k];
                }

                for (int j = 1; j < maxRowCount; j++) {
                    for (int k = 0; k < DataTable.GetLength(1); k++) {
                        try {
                            newTable[j, k] = DataTable[j * i, k];
                        } catch (IndexOutOfRangeException ex) {
                            var tmpNewTable = new string[j, DataTable.GetLength(1)];
                            for (int a = 0; a < j; a++) {
                                for (int b = 0; b < newTable.GetLength(1); b++) {
                                    tmpNewTable[a, b] = newTable[a, b];
                                }
                            }

                            newTable = tmpNewTable;
                            break;
                        } catch (Exception ex)
                        {
                            throw;
                        }
                    }

                    var currentCount = newTable.GetLength(0);
                    if (currentCount != maxRowCount)
                        break;
                }
                results.Add(newTable);    
            }

            int currentSlide = 0;
            results.ForEach(splitTable => {
                currentSlide++;
                string title = results.Count > 0 ? $"{Title} (Page {currentSlide})" : Title;
                CreateTableSlide(pptPresentation, slides, (SlideId + currentSlide), title, splitTable, FooterText);
            });
            
            return SlideId + currentSlide;
        }

        public string CreatePowerPointTestData()
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
                CreateTitleSlide(pptPresentation, slides, 1, "TESTID#54834", "Test Name for Test and Test and Test for Test", "2", "Georgia Nombre de Oliveira Kalyva");
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

        public string CreatePowerPointGateReview(string Gate, ProjectDetail project, Project_User user, List<Schedule> schedules, 
            ProductInfrigmentPatentability pip, List<Risk> risk, InvestmentPlan ip, BusinessCase bc, PostLaunchReview plr)
        {
            string[,] SchedulesTable = null;
            string[,] ProductInfrigmentPatentabilityTable = null;
            string[,] RiskTable = null;
            string[,] InvestmentPlanTable = null;
            string[,] BusinessCaseTableSimple = null;
            string[,] BusinessCaseTableCost = null;
            string[,] BusinessCaseTableExpenses = null;
            string[,] PLRTable = null;

            try // Try Running all calculations & data tables
            {
                if (schedules != null && schedules.Count > 0)
                {
                    SchedulesTable = GenerateTableData<Schedule>(schedules);
                }
                if (pip != null)
                {
                    ProductInfrigmentPatentabilityTable = GenerateTableData<ProductInfrigmentPatentability>(pip); }

                if (risk != null && risk.Count > 0)
                {
                    RiskTable = GenerateTableData<Risk>(risk);
                }

                if (ip != null)
                {
                    InvestmentPlanTable = GenerateTableData<InvestmentPlan>(ip);
                }
                if (bc != null)
                {
                    BusinessCaseTableSimple = GenerateTableData<BusinessCase>(bc, BusinessCaseTableType.Simple);

                    BusinessCaseTableCost = GenerateTableData<BusinessCase>(bc, BusinessCaseTableType.FinancialsCalculationsCost);

                    BusinessCaseTableExpenses = GenerateTableData<BusinessCase>(bc, BusinessCaseTableType.FinancialsCalculationsExpenses);
                       }
                if (plr != null)
                {
                    PLRTable = GenerateTableData<PostLaunchReview>(plr);
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            Microsoft.Office.Interop.PowerPoint.Application pptApplication = new Microsoft.Office.Interop.PowerPoint.Application();
            // Create the Presentation File
            Presentation pptPresentation = pptApplication.Presentations.Add(MsoTriState.msoTrue);
            pptPresentation.PageSetup.SlideSize = Microsoft.Office.Interop.PowerPoint.PpSlideSizeType.ppSlideSizeOnScreen;
            pptPresentation.ApplyTheme(_Theme);

            try
            {

                int pageIndex = 1;
                Microsoft.Office.Interop.PowerPoint.Slides slides;
                slides = pptPresentation.Slides;
                CreateTitleSlide(pptPresentation, slides, 1, project.Project.Id.ToString(), project.Project.Name,  Gate, user.User.NetworkUsername);
                if (schedules!=null && schedules.Count > 0)
                {
                    pageIndex = CreateTableSlides(pptPresentation, slides, pageIndex, "Schedules", SchedulesTable, $"Gate {Gate} Review");
                }
                if (pip != null)
                {
                    pageIndex = CreateTableSlides(pptPresentation, slides, pageIndex, "Product Infringement Patentability", ProductInfrigmentPatentabilityTable, $"Gate {Gate} Review");
                }

                if (risk != null && risk.Count > 0)
                {
                    pageIndex = CreateTableSlides(pptPresentation, slides, pageIndex, "Risk Analysis", RiskTable, $"Gate {Gate} Review");

                }

                if (ip != null)
                {
                    pageIndex = CreateTableSlides(pptPresentation, slides, pageIndex, "Investment Plan", InvestmentPlanTable, $"Gate {Gate} Review");

                }
                if (bc != null)
                {
                    pageIndex = CreateTableSlides(pptPresentation, slides, pageIndex, "Business Case", BusinessCaseTableSimple, $"Gate {Gate} Review");


                    pageIndex = CreateTableSlides(pptPresentation, slides, pageIndex, "Project GPA", BusinessCaseTableCost, $"Gate {Gate} Review");


                    pageIndex = CreateTableSlides(pptPresentation, slides, pageIndex, "Project Expenses", BusinessCaseTableExpenses, $"Gate {Gate} Review");
                }
                if (plr != null)
                {
                    pageIndex = CreateTableSlides(pptPresentation, slides, pageIndex, "Post Launch Review", PLRTable, $"Gate {Gate} Review"); ;

                }

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



    public enum BusinessCaseTableType 
    {
        None=1,
        Simple=2,
        FinancialsCalculationsCost = 3,
        FinancialsCalculationsExpenses = 4
    }
}
