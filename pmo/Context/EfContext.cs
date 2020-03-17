using Microsoft.EntityFrameworkCore;
using dbModels;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;
using dbModels.Report;
using AutoMapper;

namespace pmo
{
    public class EfContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public EfContext(DbContextOptions<EfContext> options, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            this._mapper = mapper;
        }

        public EfContext() { }
        public DbSet<GateKeeperConfig> GateKeeperConfigs { get; set; }
        public DbSet<BusinessCase> BusinessCases { get; set; }
        public DbSet<CustomerDesignApproval> CustomerDesignApprovals { get; set; }
        public DbSet<Gate> Gates { get; set; }
        public DbSet<GateKeeper> GateKeepers { get; set; }
        public DbSet<InvestmentPlan> InvestmentPlans { get; set; }
        public DbSet<KeyCharacteristic> KeyCharacteristics { get; set; }
        public DbSet<Mitigation> Mitigations { get; set; }
        public DbSet<ProductInfrigmentPatentability> ProductInfrigmentPatentabilities { get; set; }
        public DbSet<ProductIntroChecklist> ProductIntroChecklists { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectDetail> ProjectDetails { get; set; }
        public DbSet<ProjectJustification> ProjectJustifications { get; set; }
        public DbSet<Risk> Risks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<StageConfig> StageConfigs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagCategory> TagCategories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_CitizenShip> UserCitizenShip { get; set; }
        public DbSet<ProjectDetail_SalesRegion> ProjectDetail_SalesRegions { get; set; }
        public DbSet<ProjectDetail_EndUserCountry> ProjectDetail_EndUserCountries { get; set; }
        public DbSet<ProjectDetail_Customer> ProjectDetail_Customers { get; set; }
        public DbSet<StageConfig_RequiredSchedule> StageConfig_RequiredSchedules { get; set; }
        public DbSet<BusinessCase_ManufacturingLocation> BusinessCase_ManufacturingLocations { get; set; }
        public DbSet<Project_User> Project_User { get; set; }
        public DbSet<ProjectStateHistory> ProjectStateHistories { get; set; }
        public DbSet<StageFile> StageFiles { get; set; }
        public DbSet<StageFileConfig> StageFileConfigs { get; set; }
        public DbSet<FinancialData> FinancialData { get; set; }
        public DbSet<PostLaunchReview> PostLaunchReviews { get; set; }
        public DbSet<GateFile> GateFiles { get; set; }
        public DbSet<LiteStageConfig> LiteStageConfigs { get; set; }
        public DbSet<LiteRequiredSchedule> LiteRequiredSchedules { get; set; }
        public DbSet<LiteStageFileConfig> LiteStageFileConfigs { get; set; }
        public DbSet<LiteGateKeeperConfig> LiteGateKeeperConfigs { get; set; }
        public DbSet<GateKeeperLite> GateKeeperLites { get; set; }
        public DbSet<OptionalFile> OptionalFiles { get; set; }
        public DbSet<Report_Project> Report_Project { get; set; }
        public DbSet<Report_ProjectCustomers> Report_ProjectCustomers { get; set; }
        public DbSet<Report_ProjectEndUserCountries> Report_ProjectEndUserCountries { get; set; }
        public DbSet<Report_ProjectSalesRegion> Report_ProjectSalesRegion { get; set; }
        public DbSet<Report_BusinessCase> Report_BusinessCase { get; set; }
        public DbSet<Report_BusinessCase_ManufacturingLocations> Report_BusinessCase_ManufacturingLocations { get; set; }
        public DbSet<Report_FinancialData> Report_FinancialData { get; set; }
        public DbSet<GateComment> GateComments { get; set; }

        public EfContext(DbContextOptions<EfContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedData(builder);
            SeedDataAfter(builder);

            // set all foreign keys to restrict delete
            var fks = builder.Model.GetEntityTypes().SelectMany(
                s => s.GetForeignKeys()
            ).ToList();
            fks.ForEach(f => f.DeleteBehavior = DeleteBehavior.Restrict);
        }

        public override int SaveChanges()
        {
            //----------------Database Model Add new records----------------//
            var databaseModel_NewRecords = ChangeTracker.Entries<DatabaseModel>().Where(E => E.State == EntityState.Added).ToList();
            databaseModel_NewRecords.ForEach(E =>
            {
                E.Property(x => x.CreateDate).CurrentValue = DateTime.Now;
                E.Property(x => x.CreateDate).IsModified = true;
                if (_httpContextAccessor.HttpContext == null)
                {
                    E.Property(x => x.ModifiedByUser).CurrentValue = "system";
                }
                else
                {
                    E.Property(x => x.ModifiedByUser).CurrentValue = _httpContextAccessor.HttpContext.User.Identity.Name;
                }
                E.Property(x => x.ModifiedByUser).IsModified = true;
            });
            //----------------Database Model Update records----------------//
            var databaseModel_UpdateRecords = ChangeTracker.Entries<DatabaseModel>().Where(E => E.State == EntityState.Modified).ToList();
            databaseModel_UpdateRecords.ForEach(model =>
            {
                model.Property(x => x.ModifiedByUser).CurrentValue = _httpContextAccessor.HttpContext.User.Identity.Name;
                model.Property(x => x.ModifiedByUser).IsModified = true;
            });
            //----------------History Model Add new records----------------//
            var historyModel_NewRecords = ChangeTracker.Entries<HistoryModel>().Where(E => E.State == EntityState.Added).ToList();
            historyModel_NewRecords.ForEach(model =>
            {
                model.Property(prop => prop.LastModified).CurrentValue = DateTime.Now;
                model.Property(prop => prop.LastModified).IsModified = true;
            });
            //----------------History Model Update records----------------//
            var historyModel_UpdateRecords = ChangeTracker.Entries<HistoryModel>().Where(E => E.State == EntityState.Modified).ToList();
            historyModel_UpdateRecords.ForEach(model =>
            {
                model.Property(prop => prop.LastModified).CurrentValue = DateTime.Now;
                model.Property(prop => prop.LastModified).IsModified = true;

                if (_httpContextAccessor.HttpContext == null)
                {
                    model.Property(prop => prop.ModifiedByUser).CurrentValue = "system";
                }
                else
                {
                    model.Property(prop => prop.ModifiedByUser).CurrentValue = _httpContextAccessor.HttpContext.User.Identity.Name;
                }
                model.Property(prop => prop.ModifiedByUser).IsModified = true;
            });

            return base.SaveChanges();
        }

        private void SeedData(ModelBuilder builder)
        {

            #region StageConfig Seed Data
            builder.Entity<StageConfig>().HasData(new List<StageConfig>() {
                new StageConfig(){ Id=1,
                    StageNumber = 1,
                    AllowInsertRiskAssesments = false,
                    MinBusinessCases = 0,
                    MinCustomerDesignApprovals = 0,
                    MinInvestmentPlans=0,
                    MinKeyCharacteristics=0,
                    MinProductInfringementPatentabilities=0,
                    MinProductIntroChecklist = 0,
                    MinProjectJustifications = 1,
                    MinPostLaunchReviews=0,
                    ModifiedByUser = "system"
                    },
                new StageConfig(){ Id=2,
                    StageNumber = 2,
                    AllowInsertRiskAssesments = false,
                    MinBusinessCases = 1,
                    MinCustomerDesignApprovals = 0,
                    MinInvestmentPlans=0,
                    MinKeyCharacteristics=0,
                    MinProductInfringementPatentabilities=0,
                    MinProductIntroChecklist = 0,
                    MinProjectJustifications = 0,
                    MinPostLaunchReviews=0,
                    ModifiedByUser = "system"
                    },
                new StageConfig(){ Id=3,
                    StageNumber = 3,
                    AllowInsertRiskAssesments = true,
                    MinBusinessCases = 1,
                    MinCustomerDesignApprovals = 1,
                    MinInvestmentPlans=1,
                    MinKeyCharacteristics=1,
                    MinProductInfringementPatentabilities=1,
                    MinProductIntroChecklist = 1,
                    MinProjectJustifications = 0,
                    MinPostLaunchReviews=0,
                    ModifiedByUser = "system"
                    },
                new StageConfig(){ Id=4,
                    StageNumber = 4,
                    AllowInsertRiskAssesments = true,
                    MinBusinessCases = 1,
                    MinCustomerDesignApprovals = 1,
                    MinInvestmentPlans=0,
                    MinKeyCharacteristics=0,
                    MinProductInfringementPatentabilities=0,
                    MinProductIntroChecklist = 0,
                    MinProjectJustifications = 0,
                    MinPostLaunchReviews=0,
                    ModifiedByUser = "system"
                    },
                new StageConfig(){ Id=5,
                    StageNumber = 5,
                    AllowInsertRiskAssesments = false,
                    MinBusinessCases = 1,
                    MinCustomerDesignApprovals = 0,
                    MinInvestmentPlans=0,
                    MinKeyCharacteristics=0,
                    MinProductInfringementPatentabilities=0,
                    MinProductIntroChecklist = 0,
                    MinProjectJustifications = 0,
                    MinPostLaunchReviews=1,
                    ModifiedByUser = "system"
                    }
            });
            #endregion
            #region Tag Seed Data
            // Required Schedules
            builder.Entity<TagCategory>().HasData(new List<TagCategory>() {
                new TagCategory { Id = 1, IsFixed = true, FriendlyName = "Required Schedules", Key = "required-schedules" },
                new TagCategory { Id = 2, IsFixed = true, FriendlyName = "Citizenships", Key = "citizenships" },
                new TagCategory { Id = 3, IsFixed = true, FriendlyName = "Sales Region", Key = "sales-region" },
                new TagCategory { Id = 4, IsFixed = true, FriendlyName = "Project Category", Key = "project-category" },
                new TagCategory { Id = 5, IsFixed = true, FriendlyName = "Project Classification", Key = "project-classification" },
                new TagCategory { Id = 6, IsFixed = true, FriendlyName = "Requirement Source", Key = "requirement-source" },
                new TagCategory { Id = 7, IsFixed = true, FriendlyName = "Manufacturing Locations", Key = "manufacturing-locations" },
                new TagCategory { Id = 8, IsFixed = true, FriendlyName = "Customers", Key = "customers" },
                new TagCategory { Id = 9, IsFixed = true, FriendlyName = "Product Line", Key = "product-line" },
                new TagCategory { Id = 10, IsFixed = true, FriendlyName = "Risk Type", Key = "risk-type" },
                new TagCategory { Id = 11, IsFixed = true, FriendlyName = "Risk Probability", Key = "risk-probability" },
                new TagCategory { Id = 12, IsFixed = true, FriendlyName = "Risk Impact", Key = "risk-impact" },
                new TagCategory { Id = 13, IsFixed = true, FriendlyName = "Design Authority", Key = "design-authority" },
                new TagCategory { Id = 14, IsFixed = true, FriendlyName = "Export Application Type", Key = "export-application-type" },
                new TagCategory { Id = 15, IsFixed = false, FriendlyName = "Stage Files", Key = "stage-files" },
                new TagCategory { Id = 16, IsFixed = false, FriendlyName = "Technical Capabilities", Key = "technical-capabilities" },
                new TagCategory { Id = 17, IsFixed = true, FriendlyName = "Application Components", Key = "application-components" },

            });

            builder.Entity<Tag>().HasData(new List<Tag>() {               
                // Schedules
                new Tag { Id = 1,  Name = "Gate 1", TagCategoryId = 1 },
                new Tag { Id = 2,  Name = "Design concept(s)", TagCategoryId = 1 },
                new Tag { Id = 3,  Name = "Gate 2 / Gate A", TagCategoryId = 1 },
                new Tag { Id = 4,  Name = "Draft manufacturing drawings", TagCategoryId = 1 },
                new Tag { Id = 5,  Name = "Design Review", TagCategoryId = 1 },
                new Tag { Id = 6,  Name = "Submit PAR", TagCategoryId = 1 },
                new Tag { Id = 7,  Name = "Release Product Documentation", TagCategoryId = 1 },
                new Tag { Id = 8,  Name = "Create Samples / Prototypes", TagCategoryId = 1 },
                new Tag { Id = 9,  Name = "DVT testing & Test Report", TagCategoryId = 1 },
                new Tag { Id = 10,  Name = "Gate 3", TagCategoryId = 1 },
                new Tag { Id = 11,  Name = "First Article Approval", TagCategoryId = 1 },
                new Tag { Id = 12,  Name = "Qualification Testing & Test Report", TagCategoryId = 1 },
                new Tag { Id = 13,  Name = "Customer Approval / Release", TagCategoryId = 1 },
                new Tag { Id = 14,  Name = "Gate 4 / Gate B", TagCategoryId = 1 },
                new Tag { Id = 15,  Name = "PLR Review", TagCategoryId = 1 },

                // Citizenships
                new Tag { Id = 16,  Name = "Afghanistan", TagCategoryId = 2 },
                new Tag { Id = 17,  Name = "Albania", TagCategoryId = 2 },
                new Tag { Id = 18,  Name = "Algeria", TagCategoryId = 2 },
                new Tag { Id = 19,  Name = "Andorra", TagCategoryId = 2 },
                new Tag { Id = 20,  Name = "Angola", TagCategoryId = 2 },
                new Tag { Id = 21,  Name = "Antigua and Barbuda", TagCategoryId = 2 },
                new Tag { Id = 22,  Name = "Argentina", TagCategoryId = 2 },
                new Tag { Id = 23,  Name = "Armenia", TagCategoryId = 2 },
                new Tag { Id = 24,  Name = "Australia", TagCategoryId = 2 },
                new Tag { Id = 25,  Name = "Austria", TagCategoryId = 2 },
                new Tag { Id = 26,  Name = "Azerbaijan", TagCategoryId = 2 },
                new Tag { Id = 27,  Name = "Bahamas", TagCategoryId = 2 },
                new Tag { Id = 28,  Name = "Bahrain", TagCategoryId = 2 },
                new Tag { Id = 29,  Name = "Bangladesh", TagCategoryId = 2 },
                new Tag { Id = 30,  Name = "Barbados", TagCategoryId = 2 },
                new Tag { Id = 31,  Name = "Belarus", TagCategoryId = 2 },
                new Tag { Id = 32,  Name = "Belgium", TagCategoryId = 2 },
                new Tag { Id = 33,  Name = "Belize", TagCategoryId = 2 },
                new Tag { Id = 34,  Name = "Benin", TagCategoryId = 2 },
                new Tag { Id = 35,  Name = "Bhutan", TagCategoryId = 2 },
                new Tag { Id = 36,  Name = "Bolivia", TagCategoryId = 2 },
                new Tag { Id = 37,  Name = "Bosnia and Herzegovina", TagCategoryId = 2 },
                new Tag { Id = 38,  Name = "Botswana", TagCategoryId = 2 },
                new Tag { Id = 39,  Name = "Brazil", TagCategoryId = 2 },
                new Tag { Id = 40,  Name = "Brunei", TagCategoryId = 2 },
                new Tag { Id = 41,  Name = "Bulgaria", TagCategoryId = 2 },
                new Tag { Id = 42,  Name = "Burkina Faso", TagCategoryId = 2 },
                new Tag { Id = 43,  Name = "Burundi", TagCategoryId = 2 },
                new Tag { Id = 44,  Name = "Cabo Verde", TagCategoryId = 2 },
                new Tag { Id = 45,  Name = "Cambodia", TagCategoryId = 2 },
                new Tag { Id = 46,  Name = "Cameroon", TagCategoryId = 2 },
                new Tag { Id = 47,  Name = "Canada", TagCategoryId = 2 },
                new Tag { Id = 48,  Name = "Central African Republic (CAR)", TagCategoryId = 2 },
                new Tag { Id = 49,  Name = "Chad", TagCategoryId = 2 },
                new Tag { Id = 50,  Name = "Chile", TagCategoryId = 2 },
                new Tag { Id = 51,  Name = "China", TagCategoryId = 2 },
                new Tag { Id = 52,  Name = "Colombia", TagCategoryId = 2 },
                new Tag { Id = 53,  Name = "Comoros", TagCategoryId = 2 },
                new Tag { Id = 54,  Name = "Congo", TagCategoryId = 2 },
                new Tag { Id = 55,  Name = "Democratic Republic of the", TagCategoryId = 2 },
                new Tag { Id = 56,  Name = "Congo", TagCategoryId = 2 },
                new Tag { Id = 57,  Name = "Republic of the", TagCategoryId = 2 },
                new Tag { Id = 58,  Name = "Costa Rica", TagCategoryId = 2 },
                new Tag { Id = 59,  Name = "Cote d'Ivoire", TagCategoryId = 2 },
                new Tag { Id = 60,  Name = "Croatia", TagCategoryId = 2 },
                new Tag { Id = 61,  Name = "Cuba", TagCategoryId = 2 },
                new Tag { Id = 62,  Name = "Cyprus", TagCategoryId = 2 },
                new Tag { Id = 63,  Name = "Czechia", TagCategoryId = 2 },
                new Tag { Id = 64,  Name = "Denmark", TagCategoryId = 2 },
                new Tag { Id = 65,  Name = "Djibouti", TagCategoryId = 2 },
                new Tag { Id = 66,  Name = "Dominica", TagCategoryId = 2 },
                new Tag { Id = 67,  Name = "Dominican Republic", TagCategoryId = 2 },
                new Tag { Id = 68,  Name = "Ecuador", TagCategoryId = 2 },
                new Tag { Id = 69,  Name = "Egypt", TagCategoryId = 2 },
                new Tag { Id = 70,  Name = "El Salvador", TagCategoryId = 2 },
                new Tag { Id = 71,  Name = "Equatorial Guinea", TagCategoryId = 2 },
                new Tag { Id = 72,  Name = "Eritrea", TagCategoryId = 2 },
                new Tag { Id = 73,  Name = "Estonia", TagCategoryId = 2 },
                new Tag { Id = 74,  Name = "Eswatini (formerly Swaziland)", TagCategoryId = 2 },
                new Tag { Id = 75,  Name = "Ethiopia", TagCategoryId = 2 },
                new Tag { Id = 76,  Name = "Fiji", TagCategoryId = 2 },
                new Tag { Id = 77,  Name = "Finland", TagCategoryId = 2 },
                new Tag { Id = 78,  Name = "France", TagCategoryId = 2 },
                new Tag { Id = 79,  Name = "Gabon", TagCategoryId = 2 },
                new Tag { Id = 80,  Name = "Gambia", TagCategoryId = 2 },
                new Tag { Id = 81,  Name = "Georgia", TagCategoryId = 2 },
                new Tag { Id = 82,  Name = "Germany", TagCategoryId = 2 },
                new Tag { Id = 83,  Name = "Ghana", TagCategoryId = 2 },
                new Tag { Id = 84,  Name = "Greece", TagCategoryId = 2 },
                new Tag { Id = 85,  Name = "Grenada", TagCategoryId = 2 },
                new Tag { Id = 86,  Name = "Guatemala", TagCategoryId = 2 },
                new Tag { Id = 87,  Name = "Guinea", TagCategoryId = 2 },
                new Tag { Id = 88,  Name = "Guinea-Bissau", TagCategoryId = 2 },
                new Tag { Id = 89,  Name = "Guyana", TagCategoryId = 2 },
                new Tag { Id = 90,  Name = "Haiti", TagCategoryId = 2 },
                new Tag { Id = 91,  Name = "Honduras", TagCategoryId = 2 },
                new Tag { Id = 92,  Name = "Hungary", TagCategoryId = 2 },
                new Tag { Id = 93,  Name = "Iceland", TagCategoryId = 2 },
                new Tag { Id = 94,  Name = "India", TagCategoryId = 2 },
                new Tag { Id = 95,  Name = "Indonesia", TagCategoryId = 2 },
                new Tag { Id = 96,  Name = "Iran", TagCategoryId = 2 },
                new Tag { Id = 97,  Name = "Iraq", TagCategoryId = 2 },
                new Tag { Id = 98,  Name = "Ireland", TagCategoryId = 2 },
                new Tag { Id = 99,  Name = "Israel", TagCategoryId = 2 },
                new Tag { Id = 100,  Name = "Italy", TagCategoryId = 2 },
                new Tag { Id = 101,  Name = "Jamaica", TagCategoryId = 2 },
                new Tag { Id = 102,  Name = "Japan", TagCategoryId = 2 },
                new Tag { Id = 103,  Name = "Jordan", TagCategoryId = 2 },
                new Tag { Id = 104,  Name = "Kazakhstan", TagCategoryId = 2 },
                new Tag { Id = 105,  Name = "Kenya", TagCategoryId = 2 },
                new Tag { Id = 106,  Name = "Kiribati", TagCategoryId = 2 },
                new Tag { Id = 107,  Name = "Kosovo", TagCategoryId = 2 },
                new Tag { Id = 108,  Name = "Kuwait", TagCategoryId = 2 },
                new Tag { Id = 109,  Name = "Kyrgyzstan", TagCategoryId = 2 },
                new Tag { Id = 110,  Name = "Laos", TagCategoryId = 2 },
                new Tag { Id = 111,  Name = "Latvia", TagCategoryId = 2 },
                new Tag { Id = 112,  Name = "Lebanon", TagCategoryId = 2 },
                new Tag { Id = 113,  Name = "Lesotho", TagCategoryId = 2 },
                new Tag { Id = 114,  Name = "Liberia", TagCategoryId = 2 },
                new Tag { Id = 115,  Name = "Libya", TagCategoryId = 2 },
                new Tag { Id = 116,  Name = "Liechtenstein", TagCategoryId = 2 },
                new Tag { Id = 117,  Name = "Lithuania", TagCategoryId = 2 },
                new Tag { Id = 118,  Name = "Luxembourg", TagCategoryId = 2 },
                new Tag { Id = 119,  Name = "Madagascar", TagCategoryId = 2 },
                new Tag { Id = 120,  Name = "Malawi", TagCategoryId = 2 },
                new Tag { Id = 121,  Name = "Malaysia", TagCategoryId = 2 },
                new Tag { Id = 122,  Name = "Maldives", TagCategoryId = 2 },
                new Tag { Id = 123,  Name = "Mali", TagCategoryId = 2 },
                new Tag { Id = 124,  Name = "Malta", TagCategoryId = 2 },
                new Tag { Id = 125,  Name = "Marshall Islands", TagCategoryId = 2 },
                new Tag { Id = 126,  Name = "Mauritania", TagCategoryId = 2 },
                new Tag { Id = 127,  Name = "Mauritius", TagCategoryId = 2 },
                new Tag { Id = 128,  Name = "Mexico", TagCategoryId = 2 },
                new Tag { Id = 129,  Name = "Micronesia", TagCategoryId = 2 },
                new Tag { Id = 130,  Name = "Moldova", TagCategoryId = 2 },
                new Tag { Id = 131,  Name = "Monaco", TagCategoryId = 2 },
                new Tag { Id = 132,  Name = "Mongolia", TagCategoryId = 2 },
                new Tag { Id = 133,  Name = "Montenegro", TagCategoryId = 2 },
                new Tag { Id = 134,  Name = "Morocco", TagCategoryId = 2 },
                new Tag { Id = 135,  Name = "Mozambique", TagCategoryId = 2 },
                new Tag { Id = 136,  Name = "Myanmar (formerly Burma)", TagCategoryId = 2 },
                new Tag { Id = 137,  Name = "Namibia", TagCategoryId = 2 },
                new Tag { Id = 138,  Name = "Nauru", TagCategoryId = 2 },
                new Tag { Id = 139,  Name = "Nepal", TagCategoryId = 2 },
                new Tag { Id = 140,  Name = "Netherlands", TagCategoryId = 2 },
                new Tag { Id = 141,  Name = "New Zealand", TagCategoryId = 2 },
                new Tag { Id = 142,  Name = "Nicaragua", TagCategoryId = 2 },
                new Tag { Id = 143,  Name = "Niger", TagCategoryId = 2 },
                new Tag { Id = 144,  Name = "Nigeria", TagCategoryId = 2 },
                new Tag { Id = 145,  Name = "North Korea", TagCategoryId = 2 },
                new Tag { Id = 146,  Name = "North Macedonia (formerly Macedonia)", TagCategoryId = 2 },
                new Tag { Id = 147,  Name = "Norway", TagCategoryId = 2 },
                new Tag { Id = 148,  Name = "Oman", TagCategoryId = 2 },
                new Tag { Id = 149,  Name = "Pakistan", TagCategoryId = 2 },
                new Tag { Id = 150,  Name = "Palau", TagCategoryId = 2 },
                new Tag { Id = 151,  Name = "Palestine", TagCategoryId = 2 },
                new Tag { Id = 152,  Name = "Panama", TagCategoryId = 2 },
                new Tag { Id = 153,  Name = "Papua New Guinea", TagCategoryId = 2 },
                new Tag { Id = 154,  Name = "Paraguay", TagCategoryId = 2 },
                new Tag { Id = 155,  Name = "Peru", TagCategoryId = 2 },
                new Tag { Id = 156,  Name = "Philippines", TagCategoryId = 2 },
                new Tag { Id = 157,  Name = "Poland", TagCategoryId = 2 },
                new Tag { Id = 158,  Name = "Portugal", TagCategoryId = 2 },
                new Tag { Id = 159,  Name = "Qatar", TagCategoryId = 2 },
                new Tag { Id = 160,  Name = "Romania", TagCategoryId = 2 },
                new Tag { Id = 161,  Name = "Russia", TagCategoryId = 2 },
                new Tag { Id = 162,  Name = "Rwanda", TagCategoryId = 2 },
                new Tag { Id = 163,  Name = "Saint Kitts and Nevis", TagCategoryId = 2 },
                new Tag { Id = 164,  Name = "Saint Lucia", TagCategoryId = 2 },
                new Tag { Id = 165,  Name = "Saint Vincent and the Grenadines", TagCategoryId = 2 },
                new Tag { Id = 166,  Name = "Samoa", TagCategoryId = 2 },
                new Tag { Id = 167,  Name = "San Marino", TagCategoryId = 2 },
                new Tag { Id = 168,  Name = "Sao Tome and Principe", TagCategoryId = 2 },
                new Tag { Id = 169,  Name = "Saudi Arabia", TagCategoryId = 2 },
                new Tag { Id = 170,  Name = "Senegal", TagCategoryId = 2 },
                new Tag { Id = 171,  Name = "Serbia", TagCategoryId = 2 },
                new Tag { Id = 172,  Name = "Seychelles", TagCategoryId = 2 },
                new Tag { Id = 173,  Name = "Sierra Leone", TagCategoryId = 2 },
                new Tag { Id = 174,  Name = "Singapore", TagCategoryId = 2 },
                new Tag { Id = 175,  Name = "Slovakia", TagCategoryId = 2 },
                new Tag { Id = 176,  Name = "Slovenia", TagCategoryId = 2 },
                new Tag { Id = 177,  Name = "Solomon Islands", TagCategoryId = 2 },
                new Tag { Id = 178,  Name = "Somalia", TagCategoryId = 2 },
                new Tag { Id = 179,  Name = "South Africa", TagCategoryId = 2 },
                new Tag { Id = 180,  Name = "South Korea", TagCategoryId = 2 },
                new Tag { Id = 181,  Name = "South Sudan", TagCategoryId = 2 },
                new Tag { Id = 182,  Name = "Spain", TagCategoryId = 2 },
                new Tag { Id = 183,  Name = "Sri Lanka", TagCategoryId = 2 },
                new Tag { Id = 184,  Name = "Sudan", TagCategoryId = 2 },
                new Tag { Id = 185,  Name = "Suriname", TagCategoryId = 2 },
                new Tag { Id = 186,  Name = "Sweden", TagCategoryId = 2 },
                new Tag { Id = 187,  Name = "Switzerland", TagCategoryId = 2 },
                new Tag { Id = 188,  Name = "Syria", TagCategoryId = 2 },
                new Tag { Id = 189,  Name = "Taiwan", TagCategoryId = 2 },
                new Tag { Id = 190,  Name = "Tajikistan", TagCategoryId = 2 },
                new Tag { Id = 191,  Name = "Tanzania", TagCategoryId = 2 },
                new Tag { Id = 192,  Name = "Thailand", TagCategoryId = 2 },
                new Tag { Id = 193,  Name = "Timor-Leste", TagCategoryId = 2 },
                new Tag { Id = 194,  Name = "Togo", TagCategoryId = 2 },
                new Tag { Id = 195,  Name = "Tonga", TagCategoryId = 2 },
                new Tag { Id = 196,  Name = "Trinidad and Tobago", TagCategoryId = 2 },
                new Tag { Id = 197,  Name = "Tunisia", TagCategoryId = 2 },
                new Tag { Id = 198,  Name = "Turkey", TagCategoryId = 2 },
                new Tag { Id = 199,  Name = "Turkmenistan", TagCategoryId = 2 },
                new Tag { Id = 200,  Name = "Tuvalu", TagCategoryId = 2 },
                new Tag { Id = 201,  Name = "Uganda", TagCategoryId = 2 },
                new Tag { Id = 202,  Name = "Ukraine", TagCategoryId = 2 },
                new Tag { Id = 203,  Name = "United Arab Emirates (UAE)", TagCategoryId = 2 },
                new Tag { Id = 204,  Name = "United Kingdom (UK)", TagCategoryId = 2 },
                new Tag { Id = 205,  Name = "United States of America (USA)", TagCategoryId = 2 },
                new Tag { Id = 206,  Name = "Uruguay", TagCategoryId = 2 },
                new Tag { Id = 207,  Name = "Uzbekistan", TagCategoryId = 2 },
                new Tag { Id = 208,  Name = "Vanuatu", TagCategoryId = 2 },
                new Tag { Id = 209,  Name = "Vatican City (Holy See)", TagCategoryId = 2 },
                new Tag { Id = 210,  Name = "Venezuela", TagCategoryId = 2 },
                new Tag { Id = 211,  Name = "Vietnam", TagCategoryId = 2 },
                new Tag { Id = 212,  Name = "Yemen", TagCategoryId = 2 },
                new Tag { Id = 213,  Name = "Zambia", TagCategoryId = 2 },
                new Tag { Id = 214,  Name = "Zimbabwe", TagCategoryId = 2 },

                 //Sales Regions
                 new Tag { Id = 215,  Name = "Americas", TagCategoryId = 3 },
                 new Tag { Id = 216,  Name = "Asia", TagCategoryId = 3 },
                 new Tag { Id = 217,  Name = "Europe", TagCategoryId = 3 },
                 new Tag { Id = 218,  Name = "Global", TagCategoryId = 3 },
                 //Project Categories
                 new Tag { Id = 219,  Name = "Minor Modification", TagCategoryId = 4 },
                 new Tag { Id = 220,  Name = "Major Modification", TagCategoryId = 4 },
                 new Tag { Id = 221,  Name = "New to World", TagCategoryId = 4 },
                 new Tag { Id = 222,  Name = "New to ITT", TagCategoryId = 4 },

                  //Project Classification
                 new Tag { Id = 223,  Name = "Broad Market ", TagCategoryId = 5 },
                 new Tag { Id = 224,  Name = "Customer Specific (No Marketing Required)", TagCategoryId = 5 },
                 new Tag { Id = 225,  Name = "Customer Specific (With Marketing Support)", TagCategoryId = 5 },
               
                  //Requirement Source
                 new Tag { Id = 226,  Name = "Cust Dwg", TagCategoryId = 6 },
                 new Tag { Id = 227,  Name = "Cust Spec", TagCategoryId = 6 },
                 new Tag { Id = 228,  Name = "Internal Dwg", TagCategoryId = 6 },
                 new Tag { Id = 229,  Name = "Internal Spec", TagCategoryId = 6 },
                 new Tag { Id = 230,  Name = "Industry Standard", TagCategoryId = 6 },
                 new Tag { Id = 231,  Name = "Best In Class Performance", TagCategoryId = 6 },
                 new Tag { Id = 232,  Name = "Other", TagCategoryId = 6 },
                 //Manufacturing Locations
                 new Tag { Id = 233,  Name = "Irvine", TagCategoryId = 7 },
                 new Tag { Id = 234,  Name = "Lainate", TagCategoryId = 7 },
                 new Tag { Id = 235,  Name = "Nogales", TagCategoryId = 7 },
                 new Tag { Id = 236,  Name = "Weinstadt", TagCategoryId = 7 },
                 new Tag { Id = 237,  Name = "Zarma", TagCategoryId = 7 },
                 new Tag { Id = 238,  Name = "Shenzhen", TagCategoryId = 7 },
                 //Customers 
                    new Tag { Id =  239 ,  Name ="ITT - ICS (R&D)", TagCategoryId =     8   },
                    new Tag { Id =  240 ,  Name ="ITT Defense", TagCategoryId =     8   },
                    new Tag { Id =  241 ,  Name ="Boeing", TagCategoryId =     8   },
                    new Tag { Id =  242 ,  Name ="Bombardier Rail", TagCategoryId =     8   },
                    new Tag { Id =  243 ,  Name ="Bombardier Commercial Air", TagCategoryId =     8   },
                    new Tag { Id =  244 ,  Name ="General Dynamics", TagCategoryId =     8   },
                    new Tag { Id =  245 ,  Name ="Raytheon", TagCategoryId =     8   },
                    new Tag { Id =  246 ,  Name ="Northrop", TagCategoryId =     8   },
                    new Tag { Id =  247 ,  Name ="Textron", TagCategoryId =     8   },
                    new Tag { Id =  248 ,  Name ="Lockheed Martin", TagCategoryId =     8   },
                    new Tag { Id =  249 ,  Name ="BAE", TagCategoryId =     8   },
                    new Tag { Id =  250 ,  Name ="Thales", TagCategoryId =     8   },
                    new Tag { Id =  251 ,  Name ="MBDA", TagCategoryId =     8  },
                    new Tag { Id =  252 ,  Name ="Finmeccanica", TagCategoryId =     8   },
                    new Tag { Id =  253 ,  Name ="Alstom", TagCategoryId =     8   },
                    new Tag { Id =  254 ,  Name ="General Atomics", TagCategoryId =     8   },
                    new Tag { Id =  255 ,  Name ="Hamilton Sundstrand", TagCategoryId =     8  },
                    new Tag { Id =  256 ,  Name ="Oshkosh", TagCategoryId =     8   },
                    new Tag { Id =  257 ,  Name ="DRS", TagCategoryId =     8   },
                    new Tag { Id =  258 ,  Name ="GE Medical", TagCategoryId =     8   },
                    new Tag { Id =  259 ,  Name ="Motorola", TagCategoryId =     8   },
                    new Tag { Id =  260 ,  Name ="RIM", TagCategoryId =     8   },
                    new Tag { Id =  261 ,  Name ="Haliburton", TagCategoryId =     8   },
                    new Tag { Id =  262 ,  Name ="Honeywell Aerospace", TagCategoryId =     8   },
                    new Tag { Id =  263 ,  Name ="Rockwell Collins", TagCategoryId =     8   },
                    new Tag { Id =  264 ,  Name ="Siemens", TagCategoryId =     8   },
                    new Tag { Id =  265 ,  Name ="Cobham", TagCategoryId =     8   },
                    new Tag { Id =  266 ,  Name ="Ultra", TagCategoryId =     8   },
                    new Tag { Id =  267 ,  Name ="Esterline Wallop", TagCategoryId =     8   },
                    new Tag { Id =  268 ,  Name ="Racal", TagCategoryId =     8   },
                    new Tag { Id =  269 ,  Name ="Rheinmatel", TagCategoryId =     8   },
                    new Tag { Id =  270 ,  Name ="Diehl", TagCategoryId =     8   },
                    new Tag { Id =  271 ,  Name ="GE Aviation", TagCategoryId =     8   },
                    new Tag { Id =  272 ,  Name ="EADS Defense", TagCategoryId =     8   },
                    new Tag { Id =  273 ,  Name ="Eibit", TagCategoryId =     8   },
                    new Tag { Id =  274 ,  Name ="Sagem", TagCategoryId =     8   },
                    new Tag { Id =  275 ,  Name ="KMW", TagCategoryId =     8   },
                    new Tag { Id =  276 ,  Name ="Liebeherr", TagCategoryId =     8   },
                    new Tag { Id =  277 ,  Name ="Voith", TagCategoryId =     8   },
                    new Tag { Id =  278 ,  Name ="MTU", TagCategoryId =     8   },
                    new Tag { Id =  279 ,  Name ="FAW", TagCategoryId =     8   },
                    new Tag { Id =  280 ,  Name ="Sany", TagCategoryId =     8   },
                    new Tag { Id =  281 ,  Name ="John Deere", TagCategoryId =     8  },
                    new Tag { Id =  282 ,  Name ="Catepillar", TagCategoryId =     8  },
                    new Tag { Id =  283 ,  Name ="Kawaski Rail", TagCategoryId =     8  },
                    new Tag { Id =  284 ,  Name ="Airbus", TagCategoryId =     8  },
                    new Tag { Id =  285 ,  Name ="Schneider", TagCategoryId =     8  },
                    new Tag { Id =  286 ,  Name ="Siemens Industrial", TagCategoryId =     8  },
                    new Tag { Id =  287 ,  Name ="Indramat", TagCategoryId =     8  },
                    new Tag { Id =  288 ,  Name ="Fokker", TagCategoryId =     8  },
                    new Tag { Id =  289 ,  Name ="Komatsu", TagCategoryId =     8  },
                    new Tag { Id =  290 ,  Name ="MAN", TagCategoryId =     8  },
                    new Tag { Id =  291 ,  Name ="Brugg", TagCategoryId =     8  },
                    new Tag { Id =  292 ,  Name ="ZF", TagCategoryId =     8  },
                    new Tag { Id =  293 ,  Name ="Patria", TagCategoryId =     8  },
                    new Tag { Id =  294 ,  Name ="Telex", TagCategoryId =     8  },
                    new Tag { Id =  295 ,  Name ="ITT Water & Wastewater", TagCategoryId =     8   },
                    new Tag { Id =  296 ,  Name ="Mitsubishi Heavy Industry", TagCategoryId =     8  },
                    new Tag { Id =  297 ,  Name ="NEC (UAV market)", TagCategoryId =     8  },
                    new Tag { Id =  298 ,  Name ="Rotem", TagCategoryId =     8  },
                    new Tag { Id =  299 ,  Name ="Hyundai Heavy Vehicle", TagCategoryId =     8  },
                    new Tag { Id =  300 ,  Name ="BYD", TagCategoryId =     8  },
                    new Tag { Id =  301 ,  Name ="Foton", TagCategoryId =     8  },
                    new Tag { Id =  302 ,  Name ="ST aerospace", TagCategoryId =     8  },
                    new Tag { Id =  303 ,  Name ="Bharat Electronics Ltd", TagCategoryId =     8  },
                    new Tag { Id =  304 ,  Name ="Hindustan Aeronautics Ltd", TagCategoryId =     8  },
                    new Tag { Id =  305 ,  Name ="Customer z", TagCategoryId =     8  },
                    new Tag { Id =  306 ,  Name ="John Deere/FPT/Market", TagCategoryId =     8  },
                    new Tag { Id =  307 ,  Name ="GD Mercury 2", TagCategoryId =     8  },
                    new Tag { Id =  308 ,  Name ="ZTE", TagCategoryId =     8  },
                    new Tag { Id =  309 ,  Name ="Alcatel Lucent", TagCategoryId =     8  },
                    new Tag { Id =  310 ,  Name ="Multipulse", TagCategoryId =     8  },
                    new Tag { Id =  311 ,  Name ="ANSALDOBREDA", TagCategoryId =     8   },
                    new Tag { Id =  312 ,  Name ="GE HC", TagCategoryId =     8  },
                    new Tag { Id =  313 ,  Name ="Lucent", TagCategoryId =     8  },
                    new Tag { Id =  314 ,  Name ="AUTS", TagCategoryId =     8  },
                    new Tag { Id =  315 ,  Name ="optional:", TagCategoryId =     8  },
                    new Tag { Id =  316 ,  Name ="STEC", TagCategoryId =     8  },
                    new Tag { Id =  317 ,  Name ="CL Led-PRO", TagCategoryId =     8  },
                    new Tag { Id =  318 ,  Name ="Alcatel Shanghai Bell", TagCategoryId =     8  },
                    new Tag { Id =  319 ,  Name ="Syracuse", TagCategoryId =     8  },
                    new Tag { Id =  320 ,  Name ="Viasat", TagCategoryId =     8  },
                    new Tag { Id =  321 ,  Name ="All", TagCategoryId =     8  },
                    new Tag { Id =  322 ,  Name ="GD", TagCategoryId =     8  },
                    new Tag { Id =  323 ,  Name ="NSN", TagCategoryId =     8   },
                    new Tag { Id =  324 ,  Name ="Fiat Power Train", TagCategoryId =     8  },
                    new Tag { Id =  325 ,  Name ="IVECO", TagCategoryId =     8  },
                    new Tag { Id =  326 ,  Name ="Invensys", TagCategoryId =     8  },
                    new Tag { Id =  327 ,  Name ="SYT", TagCategoryId =     8  },
                    new Tag { Id =  328 ,  Name ="Lucent Qingdao", TagCategoryId =     8  },
                    new Tag { Id =  329 ,  Name ="Gulfstream", TagCategoryId =     8  },
                    new Tag { Id =  330 ,  Name ="Smiths Detection", TagCategoryId =     8  },
                    new Tag { Id =  331 ,  Name ="Smiths Det", TagCategoryId =     8  },
                    new Tag { Id =  332 ,  Name ="Bosch Rexroth", TagCategoryId =     8  },
                    new Tag { Id =  333 ,  Name ="Selex Luton", TagCategoryId =     8  },
                    new Tag { Id =  334 ,  Name ="Lowara", TagCategoryId =     8  },
                    new Tag { Id =  335 ,  Name ="GDEB", TagCategoryId =     8  },
                    new Tag { Id =  336 ,  Name ="BOMBARDIER SW", TagCategoryId =     8  },
                    new Tag { Id =  337 ,  Name ="Harris", TagCategoryId =     8  },
                    new Tag { Id =  338 ,  Name ="GE Healthcare", TagCategoryId =     8  },
                    new Tag { Id =  339 ,  Name ="Philips", TagCategoryId =     8  },
                    new Tag { Id =  340 ,  Name ="ESAOTE", TagCategoryId =     8   },
                    new Tag { Id =  341 ,  Name ="Medison", TagCategoryId =     8   },
                    new Tag { Id =  342 ,  Name ="Hitachi", TagCategoryId =     8  },
                    new Tag { Id =  343 ,  Name ="Mindray", TagCategoryId =     8  },
                    new Tag { Id =  344 ,  Name ="China local", TagCategoryId =     8  },
                    new Tag { Id =  345 ,  Name ="Bathium", TagCategoryId =     8  },
                    new Tag { Id =  346 ,  Name ="DRS-RSTA", TagCategoryId =     8  },
                    new Tag { Id =  347 ,  Name ="Bombardier, Alstom, etc", TagCategoryId =     8  },
                    new Tag { Id =  348 ,  Name ="WIKA", TagCategoryId =     8  },
                    new Tag { Id =  349 ,  Name ="ANSALDO BREDA", TagCategoryId =     8  },
                    new Tag { Id =  350 ,  Name ="Viasystem", TagCategoryId =     8  },
                    new Tag { Id =  351 ,  Name ="Vissysten", TagCategoryId =     8  },
                    new Tag { Id =  352 ,  Name ="Market", TagCategoryId =     8  },
                    new Tag { Id =  353 ,  Name ="Alstom", TagCategoryId =     8  },
                    new Tag { Id =  354 ,  Name ="Bombardier", TagCategoryId =     8  },
                    new Tag { Id =  355 ,  Name ="others", TagCategoryId =     8  },
                    new Tag { Id =  356 ,  Name ="others (railway)", TagCategoryId =     8  },
                    new Tag { Id =  357 ,  Name ="other market", TagCategoryId =     8  },
                    new Tag { Id =  358 ,  Name ="Rail customers", TagCategoryId =     8  },
                    new Tag { Id =  359 ,  Name ="Off Road customers", TagCategoryId =     8  },
                    new Tag { Id =  360 ,  Name ="Industrial customers", TagCategoryId =     8  },
                    new Tag { Id =  361 ,  Name ="Nissan domestic", TagCategoryId =     8  },
                    new Tag { Id =  362 ,  Name ="Mitsubishi", TagCategoryId =     8   },
                    new Tag { Id =  363 ,  Name ="Nissan Export", TagCategoryId =     8  },
                    new Tag { Id =  364 ,  Name ="Nissan", TagCategoryId =     8  },
                    new Tag { Id =  365 ,  Name ="Honda", TagCategoryId =     8  },
                    new Tag { Id =  366 ,  Name ="IEC sales Europe", TagCategoryId =     8  },
                    new Tag { Id =  367 ,  Name ="IEC sales Asia", TagCategoryId =     8  },
                    new Tag { Id =  368 ,  Name ="IEC sales US", TagCategoryId =     8  },
                    new Tag { Id =  369 ,  Name ="Hua Chuang", TagCategoryId =     8  },
                    new Tag { Id =  370 ,  Name ="GD/Rockwell", TagCategoryId =     8  },
                    new Tag { Id =  371 ,  Name ="Catalog Product", TagCategoryId =     8  },
                    new Tag { Id =  372 ,  Name ="-", TagCategoryId =     8  },
                    new Tag { Id =  373 ,  Name ="Tata Leoni, Beijing Benefit, …", TagCategoryId =     8  },
                    new Tag { Id =  374 ,  Name ="Beijing Benefit, …", TagCategoryId =     8  },
                    new Tag { Id =  375 ,  Name ="MTU Friedrichshafen", TagCategoryId =     8  },
                    new Tag { Id =  376 ,  Name ="Bosch Hallein", TagCategoryId =     8  },
                    new Tag { Id =  377 ,  Name ="Thales TME", TagCategoryId =     8  },
                    new Tag { Id =  378 ,  Name ="ATM MI - Italy", TagCategoryId =     8  },
                    new Tag { Id =  379 ,  Name ="Kässbohrer", TagCategoryId =     8  },
                    new Tag { Id =  380 ,  Name ="Bombardier / Honeywell", TagCategoryId =     8  },
                    new Tag { Id =  381 ,  Name ="Honeywell and 2 others", TagCategoryId =     8  },
                    new Tag { Id =  382 ,  Name ="Distry Package", TagCategoryId =     8  },
                    new Tag { Id =  383 ,  Name ="2 projects India", TagCategoryId =     8  },
                    new Tag { Id =  384 ,  Name ="MWM Project", TagCategoryId =     8  },
                    new Tag { Id =  385 ,  Name ="Liebherr Nenzing", TagCategoryId =     8  },
                    new Tag { Id =  386 ,  Name ="Qingdao Si Fang", TagCategoryId =     8  },
                    new Tag { Id =  387 ,  Name ="Scomi", TagCategoryId =     8  },
                    new Tag { Id =  388 ,  Name ="Pektron", TagCategoryId =     8  },
                    new Tag { Id =  389 ,  Name ="Zhengzhou ShiQi", TagCategoryId =     8  },
                    new Tag { Id =  390 ,  Name ="Thales Colombe", TagCategoryId =     8  },
                    new Tag { Id =  391 ,  Name ="Boeing/MB", TagCategoryId =     8  },
                    new Tag { Id =  392 ,  Name ="Sews", TagCategoryId =     8  },
                    new Tag { Id =  393 ,  Name ="AWE", TagCategoryId =     8  },
                    new Tag { Id =  394 ,  Name ="ANSADLOBREDA", TagCategoryId =     8  },
                    new Tag { Id =  395 ,  Name ="ITT Defence", TagCategoryId =     8  },
                    new Tag { Id =  396 ,  Name ="ITT/IS", TagCategoryId =     8  },
                    new Tag { Id =  397 ,  Name ="Rafael", TagCategoryId =     8  },
                    new Tag { Id =  398 ,  Name ="4-way", TagCategoryId =     8  },
                    new Tag { Id =  399 ,  Name ="6-way", TagCategoryId =     8  },
                    new Tag { Id =  400 ,  Name ="7-way", TagCategoryId =     8  },
                    new Tag { Id =  401 ,  Name ="19-way", TagCategoryId =     8  },
                    new Tag { Id =  402 ,  Name ="GA", TagCategoryId =     8  },
                    new Tag { Id =  403 ,  Name ="CAF", TagCategoryId =     8   },
                    new Tag { Id =  404 ,  Name ="STEC-Malaysia", TagCategoryId =     8  },
                    new Tag { Id =  405 ,  Name ="IVECO/ASTRA", TagCategoryId =     8  },
                    new Tag { Id =  406 ,  Name ="KHI", TagCategoryId =     8  },
                    new Tag { Id =  407 ,  Name ="ITT FPS", TagCategoryId =     8  },
                    new Tag { Id =  408 ,  Name ="Novatel", TagCategoryId =     8  },
                    new Tag { Id =  409 ,  Name ="Milectria", TagCategoryId =     8  },
                    new Tag { Id =  410 ,  Name ="Atlantic Tele", TagCategoryId =     8  },
                    new Tag { Id =  411 ,  Name ="ITT", TagCategoryId =     8  },
                    new Tag { Id =  412 ,  Name ="GD Canada", TagCategoryId =     8  },
                    new Tag { Id =  413 ,  Name ="Rockwell", TagCategoryId =     8  },
                    new Tag { Id =  414 ,  Name ="BAE Archer", TagCategoryId =     8  },
                    new Tag { Id =  415 ,  Name ="Customer A", TagCategoryId =     8  },
                    new Tag { Id =  416 ,  Name ="Multipulse Network Rail", TagCategoryId =     8  },
                    new Tag { Id =  417 ,  Name ="Toko-electronics", TagCategoryId =     8  },
                    new Tag { Id =  418 ,  Name ="L3", TagCategoryId =     8  },
                    new Tag { Id =  419 ,  Name ="Loral Space Systems", TagCategoryId =     8  },
                    new Tag { Id =  420 ,  Name ="Carlisle Interconnect Technologies", TagCategoryId =     8  },
                    new Tag { Id =  421 ,  Name ="Protronex", TagCategoryId =     8  },
                    new Tag { Id =  422 ,  Name ="JRC (Toshiba)", TagCategoryId =     8  },
                    new Tag { Id =  423 ,  Name ="SEER TECHNOLOGY", TagCategoryId =     8  },
                    new Tag { Id =  424 ,  Name ="Daimler AG", TagCategoryId =     8  },
                    new Tag { Id =  425 ,  Name ="Proteras", TagCategoryId =     8  },
                    new Tag { Id =  426 ,  Name ="NEC TOSHIBA SPACE SYSTEM", TagCategoryId =     8  },
                    new Tag { Id =  427 ,  Name ="OSA Industries", TagCategoryId =     8  },
                    new Tag { Id =  428 ,  Name ="KHI Japan", TagCategoryId =     8  },
                    new Tag { Id =  429 ,  Name ="TEDER - Israel", TagCategoryId =     8  },
                    new Tag { Id =  430 ,  Name ="Calgary Transit", TagCategoryId =     8  },
                    new Tag { Id =  431 ,  Name ="Gore UK", TagCategoryId =     8  },
                    new Tag { Id =  432 ,  Name ="PEI Genesis", TagCategoryId =     8  },
                    new Tag { Id =  433 ,  Name ="Starling", TagCategoryId =     8  },
                    new Tag { Id =  434 ,  Name ="Pentair", TagCategoryId =     8  },
                    new Tag { Id =  435 ,  Name ="BYD", TagCategoryId =     8  },
                    new Tag { Id =  436 ,  Name ="Danfoss", TagCategoryId =     8  },
                    new Tag { Id =  437 ,  Name ="MOBA", TagCategoryId =     8  },
                    new Tag { Id =  438 ,  Name ="Orbital", TagCategoryId =     8  },
                    new Tag { Id =  439 ,  Name ="ST Electronics", TagCategoryId =     8  },
                    new Tag { Id =  440 ,  Name ="Milper", TagCategoryId =     8  },
                    new Tag { Id =  441 ,  Name ="Amphenol", TagCategoryId =     8  },
                    new Tag { Id =  442 ,  Name ="Lucy Switchgear ltd", TagCategoryId =     8  },
                    new Tag { Id =  443 ,  Name ="INTELEGENT TEXTILES", TagCategoryId =     8  },
                    new Tag { Id =  444 ,  Name ="Suzlon", TagCategoryId =     8  },
                    new Tag { Id =  445 ,  Name ="TBD", TagCategoryId =     8  },
                    new Tag { Id =  446 ,  Name ="Boeing/Fokker Elmo", TagCategoryId =     8   },
                    new Tag { Id =  447 ,  Name ="Boeing/Labinal", TagCategoryId =     8   },
                    new Tag { Id =  448 ,  Name ="ABB", TagCategoryId =     8   },
                    new Tag { Id =  449 ,  Name ="AEROVIRONMENT INC", TagCategoryId =     8   },
                    new Tag { Id =  450 ,  Name ="ALLISON", TagCategoryId =     8   },
                    new Tag { Id =  451 ,  Name ="ALOKA", TagCategoryId =     8   },
                    new Tag { Id =  452 ,  Name ="AMA SPA TRANS", TagCategoryId =     8   },
                    new Tag { Id =  453 ,  Name ="Apple", TagCategoryId =     8   },
                    new Tag { Id =  454 ,  Name ="ARROW", TagCategoryId =     8   },
                    new Tag { Id =  455 ,  Name ="BARCO", TagCategoryId =     8   },
                    new Tag { Id =  456 ,  Name ="BENCHMARK", TagCategoryId =     8   },
                    new Tag { Id =  457 ,  Name ="BOSCH", TagCategoryId =     8   },
                    new Tag { Id =  458 ,  Name ="COBO TRANS", TagCategoryId =     8   },
                    new Tag { Id =  459 ,  Name ="CUMMINS TRUCK/BUS", TagCategoryId =     8   },
                    new Tag { Id =  460 ,  Name ="DAIMLER", TagCategoryId =     8   },
                    new Tag { Id =  461 ,  Name ="DELPHI", TagCategoryId =     8   },
                    new Tag { Id =  462 ,  Name ="EADS/Airbus", TagCategoryId =     8   },
                    new Tag { Id =  463 ,  Name ="Elbit", TagCategoryId =     8   },
                    new Tag { Id =  464 ,  Name ="ELENSYS S.R.L.", TagCategoryId =     8   },
                    new Tag { Id =  465 ,  Name ="Embraer", TagCategoryId =     8   },
                    new Tag { Id =  466 ,  Name ="EMITEC", TagCategoryId =     8   },
                    new Tag { Id =  467 ,  Name ="FAIVELEY RAIL", TagCategoryId =     8   },
                    new Tag { Id =  468 ,  Name ="FANUC", TagCategoryId =     8   },
                    new Tag { Id =  469 ,  Name ="Faradyne Motors (Suzhou)", TagCategoryId =     8   },
                    new Tag { Id =  470 ,  Name ="GE Energy", TagCategoryId =     8   },
                    new Tag { Id =  471 ,  Name ="GE INDUSTRIAL", TagCategoryId =     8   },
                    new Tag { Id =  472 ,  Name ="GE RAIL", TagCategoryId =     8   },
                    new Tag { Id =  473 ,  Name ="Halliburton", TagCategoryId =     8   },
                    new Tag { Id =  474 ,  Name ="ITT F&Motion Control", TagCategoryId =     8   },
                    new Tag { Id =  475 ,  Name ="IVECO TRUCK/BUS", TagCategoryId =     8   },
                    new Tag { Id =  476 ,  Name ="KONGSBERG", TagCategoryId =     8   },
                    new Tag { Id =  477 ,  Name ="Liebherr", TagCategoryId =     8   },
                    new Tag { Id =  478 ,  Name ="MTU", TagCategoryId =     8   },
                    new Tag { Id =  479 ,  Name ="MULTI PHASE METERS AS", TagCategoryId =     8   },
                    new Tag { Id =  480 ,  Name ="NEC", TagCategoryId =     8   },
                    new Tag { Id =  481 ,  Name ="OECO", TagCategoryId =     8   },
                    new Tag { Id =  482 ,  Name ="Orbital", TagCategoryId =     8   },
                    new Tag { Id =  483 ,  Name ="Oshkosh Military", TagCategoryId =     8   },
                    new Tag { Id =  484 ,  Name ="Oshkosh Transport", TagCategoryId =     8   },
                    new Tag { Id =  485 ,  Name ="PHILIPS MEDICAL", TagCategoryId =     8   },
                    new Tag { Id =  486 ,  Name ="PKC", TagCategoryId =     8   },
                    new Tag { Id =  487 ,  Name ="PREMIER CABLES", TagCategoryId =     8   },
                    new Tag { Id =  488 ,  Name ="PRINOTH", TagCategoryId =     8   },
                    new Tag { Id =  489 ,  Name ="Rheinmetall", TagCategoryId =     8   },
                    new Tag { Id =  490 ,  Name ="Robert Bosch", TagCategoryId =     8   },
                    new Tag { Id =  491 ,  Name ="ROCKWELL AUTOMATION", TagCategoryId =     8   },
                    new Tag { Id =  492 ,  Name ="Row 44", TagCategoryId =     8   },
                    new Tag { Id =  493 ,  Name ="RUAG", TagCategoryId =     8   },
                    new Tag { Id =  494 ,  Name ="SAAB", TagCategoryId =     8   },
                    new Tag { Id =  495 ,  Name ="SCHLUMBERGER ENERGY", TagCategoryId =     8   },
                    new Tag { Id =  496 ,  Name ="SECHERON", TagCategoryId =     8   },
                    new Tag { Id =  497 ,  Name ="SEPSA", TagCategoryId =     8   },
                    new Tag { Id =  498 ,  Name ="SEWS CABIND", TagCategoryId =     8   },
                    new Tag { Id =  499 ,  Name ="Sumitomo", TagCategoryId =     8   },
                    new Tag { Id =  500 ,  Name ="Tektronix", TagCategoryId =     8   },
                    new Tag { Id =  501 ,  Name ="TELEINDUSTRIALE SRL", TagCategoryId =     8   },
                    new Tag { Id =  502 ,  Name ="Textron Bell", TagCategoryId =     8   },
                    new Tag { Id =  503 ,  Name ="Textron Defense", TagCategoryId =     8   },
                    new Tag { Id =  504 ,  Name ="TOSHIBA INDUSTRIAL", TagCategoryId =     8   },
                    new Tag { Id =  505 ,  Name ="TOSHIBA MILITARY", TagCategoryId =     8   },
                    new Tag { Id =  506 ,  Name ="VOLVO", TagCategoryId =     8   },
                    new Tag { Id =  507 ,  Name ="AREVA", TagCategoryId =     8  },
                    new Tag { Id =  508 ,  Name ="AVIO SPA", TagCategoryId =     8  },
                    new Tag { Id =  509 ,  Name ="Caterpillar", TagCategoryId =     8  },
                    new Tag { Id =  510 ,  Name ="CNH", TagCategoryId =     8  },
                    new Tag { Id =  511 ,  Name ="Danaher", TagCategoryId =     8  },
                    new Tag { Id =  512 ,  Name ="DUCOMMUN TECHNOLOGIES", TagCategoryId =     8  },
                    new Tag { Id =  513 ,  Name ="EATON AEROSPACE", TagCategoryId =     8  },
                    new Tag { Id =  514 ,  Name ="Ford", TagCategoryId =     8  },
                    new Tag { Id =  515 ,  Name ="Fuji Heavy Industries", TagCategoryId =     8  },
                    new Tag { Id =  516 ,  Name ="GOODRICH", TagCategoryId =     8  },
                    new Tag { Id =  517 ,  Name ="Bobcat", TagCategoryId =     8  },
                    new Tag { Id =  518 ,  Name ="HARRIS ASSEMBLY", TagCategoryId =     8  },
                    new Tag { Id =  519 ,  Name ="Hitachi Medical", TagCategoryId =     8  },
                    new Tag { Id =  520 ,  Name ="Indra Systems Defense", TagCategoryId =     8  },
                    new Tag { Id =  521 ,  Name ="JTRS", TagCategoryId =     8  },
                    new Tag { Id =  522 ,  Name ="Knorr-Bremse", TagCategoryId =     8  },
                    new Tag { Id =  523 ,  Name ="Leviton", TagCategoryId =     8  },
                    new Tag { Id =  524 ,  Name ="LORAL SPACE", TagCategoryId =     8  },
                    new Tag { Id =  525 ,  Name ="MAN Commercial", TagCategoryId =     8  },
                    new Tag { Id =  526 ,  Name ="MAN Military", TagCategoryId =     8  },
                    new Tag { Id =  527 ,  Name ="Moog", TagCategoryId =     8  },
                    new Tag { Id =  528 ,  Name ="PENTAIR  WATER  EMEA", TagCategoryId =     8  },
                    new Tag { Id =  529 ,  Name ="POD POINT", TagCategoryId =     8  },
                    new Tag { Id =  530 ,  Name ="ROCKWELL AUTOMATION", TagCategoryId =     8  },
                    new Tag { Id =  531 ,  Name ="Shiny Success", TagCategoryId =     8  },
                    new Tag { Id =  532 ,  Name ="Singarpore Technologies", TagCategoryId =     8  },
                    new Tag { Id =  533 ,  Name ="Tesla", TagCategoryId =     8  },
                    new Tag { Id =  534 ,  Name ="WEBASTO", TagCategoryId =     8  },
                    new Tag { Id =  535 ,  Name ="Various", TagCategoryId =     8  },
                    new Tag { Id =  536 ,  Name ="AITech", TagCategoryId =     8  },
                    new Tag { Id =  537 ,  Name ="AMK", TagCategoryId =     8  },
                    new Tag { Id =  538 ,  Name ="Emitec", TagCategoryId =     8  },
                    new Tag { Id =  539 ,  Name ="Furukawa Electric ( NISSAN)", TagCategoryId =     8  },
                    new Tag { Id =  540 ,  Name ="BRC Compressors", TagCategoryId =     8  },
                    new Tag { Id =  541 ,  Name ="Cummins", TagCategoryId =     8  },
                    new Tag { Id =  542 ,  Name ="KEIHIN", TagCategoryId =     8  },
                    new Tag { Id =  543 ,  Name ="Universal Avionics", TagCategoryId =     8  },
                    new Tag { Id =  544 ,  Name ="Nexans Cabling Solutions", TagCategoryId =     8  },
                    new Tag { Id =  545 ,  Name ="Wabtec", TagCategoryId =     8  },
                    new Tag { Id =  546 ,  Name ="ENI", TagCategoryId =     8  },
                    new Tag { Id =  547 ,  Name ="Total", TagCategoryId =     8  },
                    new Tag { Id =  548 ,  Name ="Cameron Romania", TagCategoryId =     8  },
                    new Tag { Id =  549 ,  Name ="J+B Aviation Services", TagCategoryId =     8  },
                    new Tag { Id =  550 ,  Name ="AWE Aldermaston", TagCategoryId =     8  },
                    new Tag { Id =  551 ,  Name ="Tyco Thermal Controls", TagCategoryId =     8  },
                    new Tag { Id =  552 ,  Name ="Saudi Aramco", TagCategoryId =     8  },
                    new Tag { Id =  553 ,  Name ="THSI", TagCategoryId =     8  },
                    new Tag { Id =  554 ,  Name ="Dunkermotoren", TagCategoryId =     8  },
                    new Tag { Id =  555 ,  Name ="Teldat", TagCategoryId =     8  },
                    new Tag { Id =  556 ,  Name ="Panasonic", TagCategoryId =     8  },
                    new Tag { Id =  557 ,  Name ="Westen Digital", TagCategoryId =     8  },
                    new Tag { Id =  558 ,  Name ="Cummins Allison", TagCategoryId =     8  },
                    new Tag { Id =  559 ,  Name ="Liebherr Germany", TagCategoryId =     8  },
                    new Tag { Id =  560 ,  Name ="Dspace", TagCategoryId =     8  },
                    new Tag { Id =  561 ,  Name ="TVO Finland", TagCategoryId =     8  },
                    new Tag { Id =  562 ,  Name ="Peugeot Citreon - France", TagCategoryId =     8  },
                    new Tag { Id =  563 ,  Name ="Agco - Sisu Power", TagCategoryId =     8  },
                    new Tag { Id =  564 ,  Name ="ITT ICS", TagCategoryId =     8  },
                    new Tag { Id =  565 ,  Name ="Inteligent design cycles", TagCategoryId =     8  },
                    new Tag { Id =  566 ,  Name ="Porsche", TagCategoryId =     8  },
                    new Tag { Id =  567 ,  Name ="EV Manufacturers", TagCategoryId =     8  },
                    new Tag { Id =  568 ,  Name ="Yamaha", TagCategoryId =     8  },
                    new Tag { Id =  569 ,  Name ="Suzuki", TagCategoryId =     8  },
                    new Tag { Id =  570 ,  Name ="Kawazaki", TagCategoryId =     8  },
                    new Tag { Id =  571 ,  Name ="Pod Point", TagCategoryId =     8  },
                    new Tag { Id =  572 ,  Name ="Elektromotive", TagCategoryId =     8  },
                    new Tag { Id =  573 ,  Name ="Chargemaster", TagCategoryId =     8  },
                    new Tag { Id =  574 ,  Name ="Rolec", TagCategoryId =     8  },
                    new Tag { Id =  575 ,  Name ="GE", TagCategoryId =     8  },
                    new Tag { Id =  576 ,  Name ="All Powerlock", TagCategoryId =     8  },
                    new Tag { Id =  577 ,  Name ="COSL", TagCategoryId =     8  },
                    new Tag { Id =  578 ,  Name ="Xioptics", TagCategoryId =     8  },
                    new Tag { Id =  579 ,  Name ="Airbus", TagCategoryId =     8  },
                    new Tag { Id =  580 ,  Name ="Eurocopter", TagCategoryId =     8  },
                    new Tag { Id =  581 ,  Name ="Agusta", TagCategoryId =     8  },
                    new Tag { Id =  582 ,  Name ="Alenia", TagCategoryId =     8  },
                    new Tag { Id =  583 ,  Name ="VT Miltope", TagCategoryId =     8  },
                    new Tag { Id =  584 ,  Name ="ZHENGZHOU YUTONG BUS", TagCategoryId =     8  },
                    new Tag { Id =  585 ,  Name ="Intelligent Textiles", TagCategoryId =     8  },
                    new Tag { Id =  586 ,  Name ="Changan UK R&D Ltd", TagCategoryId =     8  },
                    new Tag { Id =  587 ,  Name ="QinetiQ", TagCategoryId =     8  },
                    new Tag { Id =  588 ,  Name ="Advantech", TagCategoryId =     8  },
                    new Tag { Id =  589 ,  Name ="B&K Medical", TagCategoryId =     8  },
                    new Tag { Id =  590 ,  Name ="Friand", TagCategoryId =     8  },
                    new Tag { Id =  591 ,  Name ="Sierra Nevada Corp", TagCategoryId =     8  },
                    new Tag { Id =  592 ,  Name ="Manz Automation", TagCategoryId =     8  },
                    new Tag { Id =  593 ,  Name ="Thrane & Thrane", TagCategoryId =     8  },
                    new Tag { Id =  594 ,  Name ="CCI", TagCategoryId =     8  },
                    new Tag { Id =  595 ,  Name ="Baer Cargolift GmbH", TagCategoryId =     8  },
                    new Tag { Id =  596 ,  Name ="Trane & Trane", TagCategoryId =     8  },
                    new Tag { Id =  597 ,  Name ="Standard Vision", TagCategoryId =     8  },
                    new Tag { Id =  598 ,  Name ="Aircell", TagCategoryId =     8  },
                    new Tag { Id =  599 ,  Name ="YuChai", TagCategoryId =     8  },
                    new Tag { Id =  600 ,  Name ="North Atlantic Ind.", TagCategoryId =     8  },
                    new Tag { Id =  601 ,  Name ="Richard Halm GmbH", TagCategoryId =     8  },
                    new Tag { Id =  602 ,  Name ="Global European Market", TagCategoryId =     8  },
                    new Tag { Id =  603 ,  Name ="Cisco", TagCategoryId =     8  },
                    new Tag { Id =  604 ,  Name ="Avnet", TagCategoryId =     8  },
                    new Tag { Id =  605 ,  Name ="UTC", TagCategoryId =     8  },
                    new Tag { Id =  606 ,  Name ="Dynamics", TagCategoryId =     8  },
                    new Tag { Id =  607 ,  Name ="Heinzmann", TagCategoryId =     8  },
                    new Tag { Id =  608 ,  Name ="Ford", TagCategoryId =     8  },
                    new Tag { Id =  609 ,  Name ="Turkey", TagCategoryId =     8  },
                    new Tag { Id =  610 ,  Name ="Entwistle", TagCategoryId =     8  },
                    new Tag { Id =  611 ,  Name ="Electro-Wire", TagCategoryId =     8  },
                    new Tag { Id =  612 ,  Name ="AXPO POWER AG", TagCategoryId =     8  },
                    new Tag { Id =  613 ,  Name ="NL1748    :BELKO INKOOPOPDRACHT", TagCategoryId =     8  },
                    new Tag { Id =  614 ,  Name ="kassbohrer", TagCategoryId =     8  },
                    new Tag { Id =  615 ,  Name ="Persistent", TagCategoryId =     8  },
                    new Tag { Id =  616 ,  Name ="Dongfeng", TagCategoryId =     8  },
                    new Tag { Id =  617 ,  Name ="Belko", TagCategoryId =     8  },
                    new Tag { Id =  618 ,  Name ="WuXi Kailong", TagCategoryId =     8  },
                    new Tag { Id =  619 ,  Name ="Kontron", TagCategoryId =     8  },
                    new Tag { Id =  620 ,  Name ="Thales", TagCategoryId =     8  },
                    new Tag { Id =  621 ,  Name ="Boeing", TagCategoryId =     8  },
                    new Tag { Id =  622 ,  Name ="Palladium", TagCategoryId =     8  },
                    new Tag { Id =  623 ,  Name ="Govt", TagCategoryId =     8  },
                    new Tag { Id =  624 ,  Name ="Parker Hannifin", TagCategoryId =     8  },
                    new Tag { Id =  625 ,  Name ="Husqvarna", TagCategoryId =     8  },
                    new Tag { Id =  626 ,  Name ="AGCO Sisu", TagCategoryId =     8  },
                    new Tag { Id =  627 ,  Name ="Sonoscape China", TagCategoryId =     8  },
                    new Tag { Id =  628 ,  Name ="Domatech", TagCategoryId =     8  },
                    new Tag { Id =  629 ,  Name ="Toro", TagCategoryId =     8  },
                    new Tag { Id =  630 ,  Name ="INDU", TagCategoryId =     8  },
                    new Tag { Id =  631 ,  Name ="LEX", TagCategoryId =     8  },
                    new Tag { Id =  632 ,  Name ="Carlisle", TagCategoryId =     8  },
                    new Tag { Id =  633 ,  Name ="Applied Research Associates", TagCategoryId =     8  },
                    new Tag { Id =  634 ,  Name ="Applied Research Associates Inc", TagCategoryId =     8  },
                    new Tag { Id =  635 ,  Name ="Simplified Energy Solutions", TagCategoryId =     8  },
                    new Tag { Id =  636 ,  Name ="NAVAIR", TagCategoryId =     8  },
                    new Tag { Id =  637 ,  Name ="Deutz AG", TagCategoryId =     8  },
                    new Tag { Id =  638 ,  Name ="Sonoscape", TagCategoryId =     8  },
                    new Tag { Id =  639 ,  Name ="CRRC", TagCategoryId =     8  },
                    new Tag { Id =  640 ,  Name ="Zaptec", TagCategoryId =     8  },
                    new Tag { Id =  641 ,  Name ="Unumotors", TagCategoryId =     8  },
                    new Tag { Id =  642 ,  Name ="AddEnergie", TagCategoryId =     8  },
                    new Tag { Id =  643 ,  Name ="Elmec", TagCategoryId =     8  },
                    new Tag { Id =  644 ,  Name ="GN Resound", TagCategoryId =     8  },
                    new Tag { Id =  645 ,  Name ="Koni US", TagCategoryId =     8  },
                    new Tag { Id =  646 ,  Name ="Bebro", TagCategoryId =     8  },
                    new Tag { Id =  647 ,  Name ="DKEA US", TagCategoryId =     8  },
                    new Tag { Id =  648 ,  Name ="IUSA S.A. de C.V", TagCategoryId =     8  },
                    new Tag { Id =  649 ,  Name ="Elbilgrossisten", TagCategoryId =     8  },
                    new Tag { Id =  650 ,  Name ="Technovative", TagCategoryId =     8  },
                    new Tag { Id =  651 ,  Name ="Mitel", TagCategoryId =     8  },
                    new Tag { Id =  652 ,  Name ="Persistent Systems", TagCategoryId =     8  },
                    new Tag { Id =  653 ,  Name ="Velodyne", TagCategoryId =     8  },
                    new Tag { Id =  654 ,  Name ="Aurora-Byton", TagCategoryId =     8  },
                    new Tag { Id =  655 ,  Name ="Drive AI", TagCategoryId =     8  },
                    new Tag { Id =  656 ,  Name ="Telefonix", TagCategoryId =     8  },
                    new Tag { Id =  657 ,  Name ="Innoveering", TagCategoryId =     8  },
                    new Tag { Id =  658 ,  Name ="Aselsan", TagCategoryId =     8  },
                    new Tag { Id =  659 ,  Name ="Cotzworks", TagCategoryId =     8  },
                    new Tag { Id =  660 ,  Name ="Digital Receiver Technology", TagCategoryId =     8  },
                    new Tag { Id =  661 ,  Name ="NA", TagCategoryId =     8  },
                    new Tag { Id =  662 ,  Name ="Bren-tronics", TagCategoryId =     8  },
                    new Tag { Id =  663 ,  Name ="microsoft", TagCategoryId =     8  },
                    new Tag { Id =  664 ,  Name ="Evercharge", TagCategoryId =     8  },
                    new Tag { Id =  665 ,  Name ="Nedtrain", TagCategoryId =     8  },
                    new Tag { Id =  666 ,  Name ="Hyundai", TagCategoryId =     8  },
                    new Tag { Id =  667 ,  Name ="Tesla/Daimler/Volvo", TagCategoryId =     8  },
                    new Tag { Id =  668 ,  Name ="Inventus", TagCategoryId =     8  },
                    new Tag { Id =  669 ,  Name ="New Warrior PEO Project Office", TagCategoryId =     8  },
                    new Tag { Id =  670 ,  Name ="Broad Market", TagCategoryId =     8  }, // To DO  --  Delete this row 
                    //Product Line
                    new Tag { Id =  671, Name ="2mm", TagCategoryId =  9},
                    new Tag { Id =  672, Name ="38999 Series I, II, and III", TagCategoryId =  9},
                    new Tag { Id =  673, Name ="AD", TagCategoryId =  9},
                    new Tag { Id =  674, Name ="Audio", TagCategoryId =  9},
                    new Tag { Id =  675, Name ="BIW", TagCategoryId =  9},
                    new Tag { Id =  676, Name ="CA Bayonet", TagCategoryId =  9},
                    new Tag { Id =  677, Name ="CIC", TagCategoryId =  9},
                    new Tag { Id =  678, Name ="CLC", TagCategoryId =  9},
                    new Tag { Id =  679, Name ="CMX", TagCategoryId =  9},
                    new Tag { Id =  680  , Name ="D Sub", TagCategoryId =  9},
                    new Tag { Id =  681  , Name ="DL", TagCategoryId =  9},
                    new Tag { Id =  682  , Name ="Fakra", TagCategoryId =  9},
                    new Tag { Id =  683  , Name ="Fiber Optics", TagCategoryId =  9},
                    new Tag { Id =  684  , Name ="Filters", TagCategoryId =  9},
                    new Tag { Id =  685  , Name ="Hermetics", TagCategoryId =  9},
                    new Tag { Id =  686  , Name ="High Frequency Specials", TagCategoryId =  9},
                    new Tag { Id =  687  , Name ="MDM", TagCategoryId =  9},
                    new Tag { Id =  688  , Name ="Metr1X", TagCategoryId =  9},
                    new Tag { Id =  689  , Name ="Micros", TagCategoryId =  9},
                    new Tag { Id =  690  , Name ="MIL-DTL 26482 Series I & Mil Battery", TagCategoryId =  9},
                    new Tag { Id =  691  , Name ="MIL-DTL 5015 Firewall", TagCategoryId =  9},
                    new Tag { Id =  692  , Name ="MIL-DTL 5015 Series I", TagCategoryId =  9},
                    new Tag { Id =  693  , Name ="Mini RF", TagCategoryId =  9},
                    new Tag { Id =  694  , Name ="Nemesis", TagCategoryId =  9},
                    new Tag { Id =  695  , Name ="PCMCIA/CF", TagCategoryId =  9},
                    new Tag { Id =  696  , Name ="PV/CV/KPD", TagCategoryId =  9},
                    new Tag { Id =  697  , Name ="QLC", TagCategoryId =  9},
                    new Tag { Id =  698  , Name ="Rack & Panel", TagCategoryId =  9},
                    new Tag { Id =  699  , Name ="RF50", TagCategoryId =  9},
                    new Tag { Id =  700  , Name ="RF75", TagCategoryId =  9},
                    new Tag { Id =  701  , Name ="SLC", TagCategoryId =  9},
                    new Tag { Id =  702  , Name ="SLE", TagCategoryId =  9},
                    new Tag { Id =  703  , Name ="Space & Specials", TagCategoryId =  9},
                    new Tag { Id =  704  , Name ="Trident", TagCategoryId =  9},
                    new Tag { Id =  705  , Name ="Trinity MKJ", TagCategoryId =  9},
                    new Tag { Id =  706  , Name ="Universal Contact", TagCategoryId =  9},
                    new Tag { Id =  707  , Name ="Veam CIR, VBN, Other", TagCategoryId =  9},
                    new Tag { Id =  708  , Name ="Veam J1772", TagCategoryId =  9},
                    new Tag { Id =  709  , Name ="Veam M12", TagCategoryId =  9},
                    new Tag { Id =  710  , Name ="Veam Powerlock & Snaplock", TagCategoryId =  9},
                    new Tag { Id = 711  , Name ="Veam PT", TagCategoryId =  9},
                    new Tag { Id =  712  , Name ="Vector/APD/Harness/Others", TagCategoryId =  9},
                    new Tag { Id =  713  , Name ="Wireless", TagCategoryId =  9},
                    new Tag { Id =  714  , Name ="New Technology", TagCategoryId =  9},
                    new Tag { Id =  715  , Name ="CTC", TagCategoryId =  9},
                    new Tag { Id = 716  , Name ="VEAM VRPC", TagCategoryId =  9},
                    new Tag { Id =  717  , Name ="Complex Cable Assembly", TagCategoryId =  9},
                    //Risk Types
                    new Tag { Id =  718  , Name ="Cost", TagCategoryId =  10},
                    new Tag { Id =  719  , Name ="Schedule", TagCategoryId =  10},
                    new Tag { Id =  720  , Name ="Scope", TagCategoryId =  10},
                    new Tag { Id =  721  , Name ="Resource", TagCategoryId =  10},
                    new Tag { Id =  722  , Name ="Quality", TagCategoryId =  10},
                    new Tag { Id =  723  , Name ="Others", TagCategoryId =  10},

                    //Risk Probability
                    new Tag { Id =  729  , Name ="10%", TagCategoryId =  11},
                    new Tag { Id =  730  , Name ="30%", TagCategoryId =  11},
                    new Tag { Id =  731  , Name ="50%", TagCategoryId =  11},
                    new Tag { Id =  732  , Name ="70%", TagCategoryId =  11},
                    new Tag { Id =  733  , Name ="90%", TagCategoryId =  11},

                     //Risk Impact
                    new Tag { Id =  724  , Name ="1", TagCategoryId =  12},
                    new Tag { Id =  725  , Name ="2", TagCategoryId =  12},
                    new Tag { Id =  726  , Name ="3", TagCategoryId =  12},
                    new Tag { Id =  727  , Name ="4", TagCategoryId =  12},
                    new Tag { Id =  728  , Name ="5", TagCategoryId =  12},

                    //Design authority
                    new Tag { Id =  734  , Name ="Santa Rosa", TagCategoryId =  13},
                    new Tag { Id =  735  , Name ="Watertown", TagCategoryId =  13},
                    new Tag { Id =  736  , Name ="Basingstoke", TagCategoryId =  13},
                    new Tag { Id =  737  , Name ="Weinstadt", TagCategoryId =  13},
                    new Tag { Id =  738  , Name ="Zama", TagCategoryId =  13},
                    new Tag { Id =  739  , Name ="Shenzhen", TagCategoryId =  13},
                    new Tag { Id =  740  , Name ="Lainate", TagCategoryId =  13 },
                    new Tag { Id =  741  , Name ="Irvine", TagCategoryId =  13},

                    //End-Use Destination
                    new Tag { Id =  742  , Name ="Space Application", TagCategoryId =  14},
                    new Tag { Id =  743  , Name ="Military Application", TagCategoryId =  14},

                    // Stage Files
                    new Tag { Id = 744, Name = "Business Case - Work Required", TagCategoryId = 15 },
                    new Tag { Id = 745, Name = "Deliverable Register - Design Concepts", TagCategoryId = 15 },
                    new Tag { Id= 746, Name =  "Deliverable Register - DMFEA" , TagCategoryId = 15},
                    new Tag { Id= 747, Name =  "Deliverable Register - DVT Test Plan" , TagCategoryId = 15},
                    new Tag { Id= 748, Name =  "Deliverable Register - Qual Test Plan" , TagCategoryId = 15},
                    new Tag { Id= 749, Name =  "Deliverable Register - Design Review" , TagCategoryId = 15},
                    new Tag { Id= 750, Name =  "Deliverable Register - PFMEA" , TagCategoryId = 15},
                    new Tag { Id= 751, Name =  "Deliverable Register - ESH Checklist" , TagCategoryId = 15},
                    new Tag { Id= 752, Name =  "Deliverable Register - Product Safety Analysis" , TagCategoryId = 15},
                    new Tag { Id= 753, Name =  "Deliverable Register - Packaging Definition" , TagCategoryId = 15},
                    new Tag { Id= 754, Name =  "Deliverable Register - Product Drawings ", TagCategoryId = 15 },
                    new Tag { Id= 755, Name =  "Deliverable Register - Specifications" , TagCategoryId = 15},
                    new Tag { Id= 756, Name =  "Deliverable Register - Customer Drawings" , TagCategoryId = 15},
                    new Tag { Id= 757, Name =  "Deliverable Register - Tool Drawings" , TagCategoryId = 15},
                    new Tag { Id= 758, Name =  "Deliverable Register - Process Flow Diagram" , TagCategoryId = 15},
                    new Tag { Id= 759, Name =  "Deliverable Register - Routers" , TagCategoryId = 15 },
                    new Tag { Id= 760, Name =  "Deliverable Register - Process Documentation" , TagCategoryId = 15},
                    new Tag { Id = 761, Name = "Deliverable Register - MSA", TagCategoryId = 15 },
                    new Tag { Id = 762, Name = "Deliverable Register - FAI", TagCategoryId = 15 },
                    new Tag { Id = 763, Name = "Product Infrigment Patentability - Upload any pertinent documentation", TagCategoryId = 15 },
                    new Tag { Id = 764, Name = "Customer Design Approval - Upload any pertinent documentation", TagCategoryId = 15 },
                    new Tag { Id = 765, Name = "Ramp and Resource Plan", TagCategoryId = 15 },
                    new Tag { Id = 766, Name = "Qualification Testing", TagCategoryId = 15 },

                    new Tag { Id = 767,  Name = "Customer Specific", TagCategoryId = 5 },
                    new Tag { Id =  768  , Name ="Santa Ana", TagCategoryId =  13},

                    //Technical Capabilities
                    new Tag { Id =  769  , Name ="None", TagCategoryId =  16},
                    new Tag { Id =  770  , Name ="Some", TagCategoryId =  16},
                    new Tag { Id =  771  , Name ="Significant", TagCategoryId =  16},

                    new Tag { Id =  772  , Name ="Deliverable Register - Parts List", TagCategoryId =  15},
                    new Tag { Id =  773  , Name ="Deliverable Register - CAS Scoping Document", TagCategoryId =  15},
                    new Tag { Id =  774  , Name ="Deliverable Register - DFMEA", TagCategoryId =  15},
                    new Tag { Id =  775  , Name ="Deliverable Register - Control Plan", TagCategoryId =  15},
                    new Tag { Id =  776  , Name = "Deliverable Register - FAI Approval", TagCategoryId = 15 },

                    new Tag { Id = 777, Name = "Εngineering Checklist", TagCategoryId = 15 },

                    //Components
                    new Tag { Id = 778, Name = "Risk assessment", TagCategoryId = 17 },
                    new Tag { Id = 779, Name = "Business Case", TagCategoryId = 17 },
                    new Tag { Id = 780, Name = "Schedules", TagCategoryId = 17 },
                    new Tag { Id = 781, Name = "Customer Design Approval", TagCategoryId = 17 },
                    new Tag { Id = 782, Name = "Investment Plan", TagCategoryId = 17 },
                    new Tag { Id = 783, Name = "Key Characteristics", TagCategoryId = 17 },
                    new Tag { Id = 784, Name = "Post Launch Review", TagCategoryId = 17 },
                    new Tag { Id = 785, Name = "Production Infringement Patentability", TagCategoryId = 17 },
                    new Tag { Id = 786, Name = "Product Intro Checklist", TagCategoryId = 17 },
                    new Tag { Id = 787, Name = "Project Justification", TagCategoryId = 17 },
                    new Tag { Id = 788, Name = "Ramp Resource Plan", TagCategoryId = 17 },
                    new Tag { Id = 789, Name = "Qualification Testing", TagCategoryId = 17 },
                    new Tag { Id = 790, Name = "Design Concept", TagCategoryId = 17 },

                    new Tag { Id =  791  , Name ="Nuclear Application", TagCategoryId =  14},
                    new Tag { Id =  792  , Name ="Commercial Aerospace Application", TagCategoryId =  14},
                    new Tag { Id =  793  , Name ="Commercial Items in Defense Application", TagCategoryId =  14},
                    new Tag { Id =  794  , Name ="None of the Above", TagCategoryId =  14},
            });
            #endregion
            #region  Gate Seed Data

            builder.Entity<GateKeeperConfig>().HasData(new List<GateKeeperConfig>()
            {
                new GateKeeperConfig(){StageConfigId=1, ModifiedByUser="system", Id= 1, Keeper="BU Director Product Management/Marketing" },
                new GateKeeperConfig(){StageConfigId=1, ModifiedByUser="system", Id= 2, Keeper="BU Director Engineering" },
                new GateKeeperConfig(){StageConfigId=4, ModifiedByUser="system", Id= 3, Keeper="BU Director Product Management/Marketing" },
                new GateKeeperConfig(){StageConfigId=4, ModifiedByUser="system", Id= 4, Keeper="BU Director Engineering" },
                new GateKeeperConfig(){StageConfigId=4, ModifiedByUser="system", Id= 5, Keeper="BU GM" },
                new GateKeeperConfig(){StageConfigId=4, ModifiedByUser="system", Id= 6, Keeper="BU Controller" },
                new GateKeeperConfig(){StageConfigId=4, ModifiedByUser="system", Id= 7, Keeper="BU Director Manufacturing Site" },
                new GateKeeperConfig(){StageConfigId=3, ModifiedByUser="system", Id= 8, Keeper="BU Director Product Management/Marketing" },
                new GateKeeperConfig(){StageConfigId=3, ModifiedByUser="system", Id= 9, Keeper="BU Director Engineering" },
                new GateKeeperConfig(){StageConfigId=3, ModifiedByUser="system", Id= 10, Keeper="BU GM" },
                new GateKeeperConfig(){StageConfigId=3, ModifiedByUser="system", Id= 11, Keeper="BU Controller" },
                new GateKeeperConfig(){StageConfigId=3, ModifiedByUser="system", Id= 12, Keeper="BU Director Manufacturing Site" },
                new GateKeeperConfig(){StageConfigId=2, ModifiedByUser="system", Id= 13, Keeper="BU Director Product Management/Marketing" },
                new GateKeeperConfig(){StageConfigId=2, ModifiedByUser="system", Id= 14, Keeper="BU Director Engineering" },
                new GateKeeperConfig(){StageConfigId=2, ModifiedByUser="system", Id= 15, Keeper="BU GM" },
                new GateKeeperConfig(){StageConfigId=2, ModifiedByUser="system", Id= 16, Keeper="BU Controller" },
                new GateKeeperConfig(){StageConfigId=2, ModifiedByUser="system", Id= 17, Keeper="BU Director Manufacturing Site" }
            });

            builder.Entity<LiteGateKeeperConfig>().HasData(new List<LiteGateKeeperConfig>() {
                // gate A
                new LiteGateKeeperConfig(){StageConfigId=1, ModifiedByUser="system", Id= 1, Keeper="BU Director Product Management/Marketing" },
                new LiteGateKeeperConfig(){StageConfigId=1, ModifiedByUser="system", Id= 2, Keeper="BU Director Engineering" },
                new LiteGateKeeperConfig(){StageConfigId=1, ModifiedByUser="system", Id= 3, Keeper="BU GM" },
                new LiteGateKeeperConfig(){StageConfigId=1, ModifiedByUser="system", Id= 4, Keeper="BU Controller" },
                new LiteGateKeeperConfig(){StageConfigId=1, ModifiedByUser="system", Id= 5, Keeper="BU Director Manufacturing Site" },
                new LiteGateKeeperConfig(){StageConfigId=1, ModifiedByUser="system", Id= 6, Keeper="BU VBPD Champion / Facilitator" },

                // gate B
                new LiteGateKeeperConfig(){StageConfigId=2, ModifiedByUser="system", Id= 7, Keeper="BU Director Product Management/Marketing" },
                new LiteGateKeeperConfig(){StageConfigId=2, ModifiedByUser="system", Id= 8, Keeper="BU Director Engineering" },
                new LiteGateKeeperConfig(){StageConfigId=2, ModifiedByUser="system", Id= 9, Keeper="BU GM" },
                new LiteGateKeeperConfig(){StageConfigId=2, ModifiedByUser="system", Id= 10, Keeper="BU Controller" },
                new LiteGateKeeperConfig(){StageConfigId=2, ModifiedByUser="system", Id= 11, Keeper="BU Director Manufacturing Site" },
                new LiteGateKeeperConfig(){StageConfigId=2, ModifiedByUser="system", Id= 12, Keeper="BU VBPD Champion / Facilitator" },
            });

            #endregion
            #region Role Seed Data
            builder.Entity<Role>().HasData(new List<Role>()
            {
                new Role(){ Id=1,
                            FriendlyName="System Admin",
                            Key="system-admin",
                            CanInitiateProject = true,
                            IsAdmin= true,
                            ManagesBusinessCases= true,
                            ManagesCustomerDesignApproval= true,
                            ManagesDeliverableRegister= true,
                            ManagesIntellectualProperty= true,
                            ManagesInvestmentPlan= true,
                            ManagesMarketingPlan= true,
                            ManagesParts= true,
                            ManagesPauseProject= true,
                            ManagesProjectDetail= true,
                            ManagesProjectRequirements= true,
                            ManagesProjectTeam= true,
                            ManagesQualificationTesting= true,
                            ManagesRampAndResourcePlan= true,
                            ManagesRiskAnalysis= true,
                            ManagesScheduling= true,
                            ModifiedByUser = "system"
                            },
                new Role(){ Id=2,
                            FriendlyName="User",
                            Key="user",
                            CanInitiateProject = true,
                            IsAdmin= false,
                            ManagesBusinessCases= true,
                            ManagesCustomerDesignApproval= true,
                            ManagesDeliverableRegister= true,
                            ManagesIntellectualProperty= true,
                            ManagesInvestmentPlan= true,
                            ManagesMarketingPlan= true,
                            ManagesParts= true,
                            ManagesPauseProject= true,
                            ManagesProjectDetail= true,
                            ManagesProjectRequirements= true,
                            ManagesProjectTeam= true,
                            ManagesQualificationTesting= true,
                            ManagesRampAndResourcePlan= true,
                            ManagesRiskAnalysis= true,
                            ManagesScheduling= true,
                            ModifiedByUser = "system"
                            }

            });
            #endregion
            #region User Seed Data
            builder.Entity<User>().HasData(new List<User>()
            {
                new User(){ NetworkUsername=@"global\georgia.kalyva", Id=1, ModifiedByUser="system", RoleId=1 },
                new User(){ NetworkUsername=@"global\efthimios.dellis", Id=2, ModifiedByUser="system", RoleId=1 },
                new User(){ NetworkUsername=@"global\christoph.hadjimylo", Id=3, ModifiedByUser="system", RoleId=1 },
                new User(){ NetworkUsername=@"global\georgia.bogri", Id=4, ModifiedByUser="system", RoleId=1 },
                new User(){ NetworkUsername=@"global\konstantinos.marolac", Id=5, ModifiedByUser="system", RoleId=1 },
                new User(){ NetworkUsername=@"global\ioannis.giannakop", Id=6, ModifiedByUser="system", RoleId=1 },
                new User(){ NetworkUsername=@"global\christos.zaragkidis", Id=7, ModifiedByUser="system", RoleId=1 },
                new User(){ NetworkUsername=@"global\george.karagiannakis", Id=8, ModifiedByUser="system", RoleId=1 },
            });
            #endregion

            builder.Entity<StageFileConfig>().HasData(new List<StageFileConfig>()
            {
                //Stage 2
                new StageFileConfig { Id = 1, StageConfigId = 2, RequiredFileTagId = 744 },
                new StageFileConfig { Id = 2, StageConfigId = 2, RequiredFileTagId = 745 },

                //Stage 3
                new StageFileConfig { Id = 3, StageConfigId = 3, RequiredFileTagId = 761 },
                new StageFileConfig { Id = 4, StageConfigId = 3, RequiredFileTagId = 762 },
                new StageFileConfig { Id = 5, StageConfigId = 3, RequiredFileTagId = 763 },
                new StageFileConfig { Id = 6, StageConfigId = 3, RequiredFileTagId = 744 },
                new StageFileConfig { Id = 7, StageConfigId = 3, RequiredFileTagId = 746 },
                new StageFileConfig { Id = 8, StageConfigId = 3, RequiredFileTagId = 747 },
                new StageFileConfig { Id = 9, StageConfigId = 3, RequiredFileTagId = 748 },
                new StageFileConfig { Id = 10, StageConfigId = 3, RequiredFileTagId = 749 },
                new StageFileConfig { Id = 11, StageConfigId = 3, RequiredFileTagId = 750 },
                new StageFileConfig { Id = 12, StageConfigId = 3, RequiredFileTagId = 751 },
                new StageFileConfig { Id = 13, StageConfigId = 3, RequiredFileTagId = 752 },
                new StageFileConfig { Id = 14, StageConfigId = 3, RequiredFileTagId = 753 },
                new StageFileConfig { Id = 15, StageConfigId = 3, RequiredFileTagId = 754 },
                new StageFileConfig { Id = 16, StageConfigId = 3, RequiredFileTagId = 755 },
                new StageFileConfig { Id = 17, StageConfigId = 3, RequiredFileTagId = 756 },
                new StageFileConfig { Id = 18, StageConfigId = 3, RequiredFileTagId = 757 },
                new StageFileConfig { Id = 19, StageConfigId = 3, RequiredFileTagId = 758 },
                new StageFileConfig { Id = 20, StageConfigId = 3, RequiredFileTagId = 759 },
                new StageFileConfig { Id = 21, StageConfigId = 3, RequiredFileTagId = 760 },

                //Stage 4
                new StageFileConfig { Id = 22, StageConfigId = 4, RequiredFileTagId = 766 },
                new StageFileConfig { Id = 23, StageConfigId = 4, RequiredFileTagId = 764 },
                new StageFileConfig { Id = 24, StageConfigId = 4, RequiredFileTagId = 744 },
                new StageFileConfig { Id = 25, StageConfigId = 4, RequiredFileTagId = 759 },
                new StageFileConfig { Id = 26, StageConfigId = 4, RequiredFileTagId = 760 },

                // Stage 1
                new StageFileConfig { Id = 27, StageConfigId = 1, RequiredFileTagId = 777 },
            });


            builder.Entity<LiteStageConfig>().HasData(new List<LiteStageConfig>() {
                new LiteStageConfig { Id = 1, StageNumber = 1, ModifiedByUser = "system", MinProjectJustifications = 1, MinBusinessCases = 1, },
                new LiteStageConfig { Id = 2, StageNumber = 2, ModifiedByUser = "system", AllowInsertRiskAssesments = true, MinInvestmentPlans = 1, MinProductIntroChecklist = 1, MinBusinessCases = 1,  },
            });

            builder.Entity<LiteStageFileConfig>().HasData(new List<LiteStageFileConfig>() {
                new LiteStageFileConfig { Id = 1, RequiredFileTagId = 761, StageConfigId = 2 },
                new LiteStageFileConfig { Id = 2, RequiredFileTagId = 762, StageConfigId = 2 },
                new LiteStageFileConfig { Id = 3, RequiredFileTagId = 765, StageConfigId = 2 },
                
                new LiteStageFileConfig { Id = 4, RequiredFileTagId = 744, StageConfigId = 1 },
            });
        }

        private void SeedDataAfter(ModelBuilder builder)
        {

            builder.Entity<StageConfig_RequiredSchedule>().HasData(new List<StageConfig_RequiredSchedule>() {
                 new StageConfig_RequiredSchedule(){ StageConfigId=3, RequiredScheduleTagId=1, Id=1, ModifiedByUser="system"},
                 new StageConfig_RequiredSchedule(){ StageConfigId=3, RequiredScheduleTagId=2, Id=2, ModifiedByUser="system"},
                 new StageConfig_RequiredSchedule(){ StageConfigId=3, RequiredScheduleTagId=3, Id=3, ModifiedByUser="system"},
                 new StageConfig_RequiredSchedule(){ StageConfigId=3, RequiredScheduleTagId=4, Id=4, ModifiedByUser="system"},
                 new StageConfig_RequiredSchedule(){ StageConfigId=3, RequiredScheduleTagId=5, Id=5, ModifiedByUser="system"},
                 new StageConfig_RequiredSchedule(){ StageConfigId=3, RequiredScheduleTagId=6, Id=6, ModifiedByUser="system"},
                 new StageConfig_RequiredSchedule(){ StageConfigId=3, RequiredScheduleTagId=7, Id=7, ModifiedByUser="system"},
                 new StageConfig_RequiredSchedule(){ StageConfigId=3, RequiredScheduleTagId=8, Id=8, ModifiedByUser="system"},
                 new StageConfig_RequiredSchedule(){ StageConfigId=3, RequiredScheduleTagId=9, Id=9, ModifiedByUser="system"},
                 new StageConfig_RequiredSchedule(){ StageConfigId=3, RequiredScheduleTagId=10, Id=10, ModifiedByUser="system"},
                 new StageConfig_RequiredSchedule(){ StageConfigId=3, RequiredScheduleTagId=11, Id=11, ModifiedByUser="system"},
                 new StageConfig_RequiredSchedule(){ StageConfigId=3, RequiredScheduleTagId=12, Id=12, ModifiedByUser="system"},
                 new StageConfig_RequiredSchedule(){ StageConfigId=3, RequiredScheduleTagId=13, Id=13, ModifiedByUser="system"},
                 new StageConfig_RequiredSchedule(){ StageConfigId=3, RequiredScheduleTagId=14, Id=14, ModifiedByUser="system"},
                 new StageConfig_RequiredSchedule(){ StageConfigId=3, RequiredScheduleTagId=15, Id=15, ModifiedByUser="system"},
                });

            builder.Entity<LiteRequiredSchedule>().HasData(new List<LiteRequiredSchedule>() {
                 new LiteRequiredSchedule(){ StageConfigId=2, RequiredScheduleTagId=1, Id=1, ModifiedByUser="system"},
                 new LiteRequiredSchedule(){ StageConfigId=2, RequiredScheduleTagId=2, Id=2, ModifiedByUser="system"},
                 new LiteRequiredSchedule(){ StageConfigId=2, RequiredScheduleTagId=3, Id=3, ModifiedByUser="system"},
                 new LiteRequiredSchedule(){ StageConfigId=2, RequiredScheduleTagId=4, Id=4, ModifiedByUser="system"},
                 new LiteRequiredSchedule(){ StageConfigId=2, RequiredScheduleTagId=5, Id=5, ModifiedByUser="system"},
                 new LiteRequiredSchedule(){ StageConfigId=2, RequiredScheduleTagId=6, Id=6, ModifiedByUser="system"},
                 new LiteRequiredSchedule(){ StageConfigId=2, RequiredScheduleTagId=7, Id=7, ModifiedByUser="system"},
                 new LiteRequiredSchedule(){ StageConfigId=2, RequiredScheduleTagId=8, Id=8, ModifiedByUser="system"},
                 new LiteRequiredSchedule(){ StageConfigId=2, RequiredScheduleTagId=9, Id=9, ModifiedByUser="system"},
                 new LiteRequiredSchedule(){ StageConfigId=2, RequiredScheduleTagId=10, Id=10, ModifiedByUser="system"},
                 new LiteRequiredSchedule(){ StageConfigId=2, RequiredScheduleTagId=11, Id=11, ModifiedByUser="system"},
                 new LiteRequiredSchedule(){ StageConfigId=2, RequiredScheduleTagId=12, Id=12, ModifiedByUser="system"},
                 new LiteRequiredSchedule(){ StageConfigId=2, RequiredScheduleTagId=13, Id=13, ModifiedByUser="system"},
                 new LiteRequiredSchedule(){ StageConfigId=2, RequiredScheduleTagId=14, Id=14, ModifiedByUser="system"},
                 new LiteRequiredSchedule(){ StageConfigId=2, RequiredScheduleTagId=15, Id=15, ModifiedByUser="system"},
            });
        }
    }
}
