using Microsoft.EntityFrameworkCore;
using dbModels;
using System.Linq;
using System;

namespace pmo {
    public class EfContext : DbContext {
        public DbSet<BusinessCase> BusinessCases { get; set; }
        public DbSet<CustomerDesignApproval> CustomerDesignApprovals { get; set; }
        public DbSet<DesignConcept> DesignConcepts { get; set; }
        public DbSet<Gate> Gates { get; set; }
        public DbSet<GateApprovers> GateApprovers { get; set; }
        public DbSet<GateConfig> GateConfigs { get; set; }
        public DbSet<InvestmentPlan> InvestmentPlans { get; set; }
        public DbSet<KeyCharacteristic> KeyCharacteristics { get; set; }
        public DbSet<Mitigation> Mitigations { get; set; }
        public DbSet<ProductInfrigmentPatentability> ProductInfrigmentPatentabilities { get; set; }
        public DbSet<ProductIntroChecklist> ProductIntroChecklists { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectDetail> ProjectDetails { get; set; }
        public DbSet<ProjectJustification> ProjectJustifications { get; set; }
        public DbSet<QualificationTesting> QualificationTestings { get; set; }
        public DbSet<RampResourcePlan> RampResourcePlans { get; set; }
        public DbSet<Risk> Risks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Stage> Stages { get; set; }
        public DbSet<StageConfig> StageConfigs { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagCategory> TagCategories { get; set; }
        public DbSet<UploadedDocumentation> UploadedDocumentation { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<User_CitizenShip> UserCitizenShip { get; set; }
        public DbSet<StageDesignConcept> StageDesignConcepts { get; set; }
        public DbSet<BusinessDesignConcept> BusinessDesignConcepts { get; set; }
        public DbSet<GateUploadedDocumentation> GateUploadedDocumentations { get; set; }
        public DbSet<ProductInfrigmentPatentabilityUploadedDocumentation> ProductInfrigmentPatentabilityUploadedDocumentations { get; set; }
        public DbSet<CustomerDesignApprovalUploadedDocumentation> CustomerDesignApprovalUploadedDocumentations { get; set; }
        public DbSet<ProjectDetail_SalesRegions> ProjectDetail_SalesRegions { get; set; }
        public DbSet<ProjectDetail_EndUserCountries> ProjectDetail_EndUserCountries { get; set; }
        public DbSet<ProjectDetail_Customers> ProjectDetail_Customers { get; set; }
        public DbSet<StageConfig_RequiredSchedules> StageConfig_RequiredSchedules { get; set; }
        public DbSet<BusinessCase_ManufacturingLocations> BusinessCase_ManufacturingLocations { get; set; }


        public EfContext(DbContextOptions<EfContext> options)
            : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
            // insert seed data if does not already exist
            SeedData(builder);

            // set all foreign keys to restrict delete
            var fks = builder.Model.GetEntityTypes().SelectMany(
                s => s.GetForeignKeys()
            ).ToList();
            fks.ForEach(f => f.DeleteBehavior = DeleteBehavior.Restrict);
        }

        private void SeedData(ModelBuilder builder) {
            var created = DateTime.Now;
            
            // Required Schedules
            builder.Entity<TagCategory>().HasData(new TagCategory {
                Id = 1,
                CreateDate = created,
                FriendlyName = "Required Schedules",
                Key = "required-schedules",
                IsFixed = true,
            });

            builder.Entity<Tag>().HasData(new Tag { Id = 1, CreateDate = created, Name = "Gate 1", TagCategoryId = 1 });
            builder.Entity<Tag>().HasData(new Tag { Id = 2, CreateDate = created, Name = "Design concept(s)", TagCategoryId = 1 });
            builder.Entity<Tag>().HasData(new Tag { Id = 3, CreateDate = created, Name = "Gate 2 / Gate A", TagCategoryId = 1 });
            builder.Entity<Tag>().HasData(new Tag { Id = 4, CreateDate = created, Name = "Draft manufacturing drawings", TagCategoryId = 1 });
            builder.Entity<Tag>().HasData(new Tag { Id = 5, CreateDate = created, Name = "Design Review", TagCategoryId = 1 });
            builder.Entity<Tag>().HasData(new Tag { Id = 6, CreateDate = created, Name = "Submit PAR", TagCategoryId = 1 });
            builder.Entity<Tag>().HasData(new Tag { Id = 7, CreateDate = created, Name = "Release Product Documentation", TagCategoryId = 1 });
            builder.Entity<Tag>().HasData(new Tag { Id = 8, CreateDate = created, Name = "Create Samples / Prototypes", TagCategoryId = 1 });
            builder.Entity<Tag>().HasData(new Tag { Id = 9, CreateDate = created, Name = "DVT testing & Test Report", TagCategoryId = 1 });
            builder.Entity<Tag>().HasData(new Tag { Id = 10, CreateDate = created, Name = "Gate 3", TagCategoryId = 1 });
            builder.Entity<Tag>().HasData(new Tag { Id = 11, CreateDate = created, Name = "First Article Approval", TagCategoryId = 1 });
            builder.Entity<Tag>().HasData(new Tag { Id = 12, CreateDate = created, Name = "Qualification Testing & Test Report", TagCategoryId = 1 });
            builder.Entity<Tag>().HasData(new Tag { Id = 13, CreateDate = created, Name = "Customer Approval / Release", TagCategoryId = 1 });
            builder.Entity<Tag>().HasData(new Tag { Id = 14, CreateDate = created, Name = "Gate 4 / Gate B", TagCategoryId = 1 });
            builder.Entity<Tag>().HasData(new Tag { Id = 15, CreateDate = created, Name = "PLR Review", TagCategoryId = 1 });

            // Citizenships
            builder.Entity<TagCategory>().HasData(new TagCategory {
                Id = 2,
                CreateDate = created,
                FriendlyName = "Citizenships",
                Key = "citizenships",
                IsFixed = true,
            });

            builder.Entity<Tag>().HasData(new Tag { Id = 16, CreateDate = created, Name = "Afghanistan", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 17, CreateDate = created, Name = "Albania", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 18, CreateDate = created, Name = "Algeria", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 19, CreateDate = created, Name = "Andorra", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 20, CreateDate = created, Name = "Angola", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 21, CreateDate = created, Name = "Antigua and Barbuda", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 22, CreateDate = created, Name = "Argentina", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 23, CreateDate = created, Name = "Armenia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 24, CreateDate = created, Name = "Australia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 25, CreateDate = created, Name = "Austria", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 26, CreateDate = created, Name = "Azerbaijan", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 27, CreateDate = created, Name = "Bahamas", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 28, CreateDate = created, Name = "Bahrain", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 29, CreateDate = created, Name = "Bangladesh", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 30, CreateDate = created, Name = "Barbados", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 31, CreateDate = created, Name = "Belarus", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 32, CreateDate = created, Name = "Belgium", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 33, CreateDate = created, Name = "Belize", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 34, CreateDate = created, Name = "Benin", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 35, CreateDate = created, Name = "Bhutan", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 36, CreateDate = created, Name = "Bolivia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 37, CreateDate = created, Name = "Bosnia and Herzegovina", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 38, CreateDate = created, Name = "Botswana", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 39, CreateDate = created, Name = "Brazil", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 40, CreateDate = created, Name = "Brunei", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 41, CreateDate = created, Name = "Bulgaria", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 42, CreateDate = created, Name = "Burkina Faso", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 43, CreateDate = created, Name = "Burundi", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 44, CreateDate = created, Name = "Cabo Verde", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 45, CreateDate = created, Name = "Cambodia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 46, CreateDate = created, Name = "Cameroon", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 47, CreateDate = created, Name = "Canada", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 48, CreateDate = created, Name = "Central African Republic (CAR)", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 49, CreateDate = created, Name = "Chad", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 50, CreateDate = created, Name = "Chile", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 51, CreateDate = created, Name = "China", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 52, CreateDate = created, Name = "Colombia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 53, CreateDate = created, Name = "Comoros", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 54, CreateDate = created, Name = "Congo", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 55, CreateDate = created, Name = "Democratic Republic of the", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 56, CreateDate = created, Name = "Congo", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 57, CreateDate = created, Name = "Republic of the", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 58, CreateDate = created, Name = "Costa Rica", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 59, CreateDate = created, Name = "Cote d'Ivoire", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 60, CreateDate = created, Name = "Croatia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 61, CreateDate = created, Name = "Cuba", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 62, CreateDate = created, Name = "Cyprus", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 63, CreateDate = created, Name = "Czechia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 64, CreateDate = created, Name = "Denmark", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 65, CreateDate = created, Name = "Djibouti", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 66, CreateDate = created, Name = "Dominica", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 67, CreateDate = created, Name = "Dominican Republic", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 68, CreateDate = created, Name = "Ecuador", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 69, CreateDate = created, Name = "Egypt", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 70, CreateDate = created, Name = "El Salvador", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 71, CreateDate = created, Name = "Equatorial Guinea", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 72, CreateDate = created, Name = "Eritrea", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 73, CreateDate = created, Name = "Estonia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 74, CreateDate = created, Name = "Eswatini (formerly Swaziland)", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 75, CreateDate = created, Name = "Ethiopia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 76, CreateDate = created, Name = "Fiji", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 77, CreateDate = created, Name = "Finland", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 78, CreateDate = created, Name = "France", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 79, CreateDate = created, Name = "Gabon", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 80, CreateDate = created, Name = "Gambia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 81, CreateDate = created, Name = "Georgia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 82, CreateDate = created, Name = "Germany", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 83, CreateDate = created, Name = "Ghana", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 84, CreateDate = created, Name = "Greece", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 85, CreateDate = created, Name = "Grenada", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 86, CreateDate = created, Name = "Guatemala", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 87, CreateDate = created, Name = "Guinea", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 88, CreateDate = created, Name = "Guinea-Bissau", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 89, CreateDate = created, Name = "Guyana", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 90, CreateDate = created, Name = "Haiti", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 91, CreateDate = created, Name = "Honduras", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 92, CreateDate = created, Name = "Hungary", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 93, CreateDate = created, Name = "Iceland", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 94, CreateDate = created, Name = "India", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 95, CreateDate = created, Name = "Indonesia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 96, CreateDate = created, Name = "Iran", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 97, CreateDate = created, Name = "Iraq", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 98, CreateDate = created, Name = "Ireland", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 99, CreateDate = created, Name = "Israel", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 100, CreateDate = created, Name = "Italy", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 101, CreateDate = created, Name = "Jamaica", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 102, CreateDate = created, Name = "Japan", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 103, CreateDate = created, Name = "Jordan", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 104, CreateDate = created, Name = "Kazakhstan", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 105, CreateDate = created, Name = "Kenya", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 106, CreateDate = created, Name = "Kiribati", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 107, CreateDate = created, Name = "Kosovo", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 108, CreateDate = created, Name = "Kuwait", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 109, CreateDate = created, Name = "Kyrgyzstan", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 110, CreateDate = created, Name = "Laos", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 111, CreateDate = created, Name = "Latvia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 112, CreateDate = created, Name = "Lebanon", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 113, CreateDate = created, Name = "Lesotho", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 114, CreateDate = created, Name = "Liberia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 115, CreateDate = created, Name = "Libya", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 116, CreateDate = created, Name = "Liechtenstein", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 117, CreateDate = created, Name = "Lithuania", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 118, CreateDate = created, Name = "Luxembourg", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 119, CreateDate = created, Name = "Madagascar", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 120, CreateDate = created, Name = "Malawi", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 121, CreateDate = created, Name = "Malaysia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 122, CreateDate = created, Name = "Maldives", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 123, CreateDate = created, Name = "Mali", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 124, CreateDate = created, Name = "Malta", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 125, CreateDate = created, Name = "Marshall Islands", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 126, CreateDate = created, Name = "Mauritania", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 127, CreateDate = created, Name = "Mauritius", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 128, CreateDate = created, Name = "Mexico", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 129, CreateDate = created, Name = "Micronesia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 130, CreateDate = created, Name = "Moldova", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 131, CreateDate = created, Name = "Monaco", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 132, CreateDate = created, Name = "Mongolia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 133, CreateDate = created, Name = "Montenegro", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 134, CreateDate = created, Name = "Morocco", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 135, CreateDate = created, Name = "Mozambique", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 136, CreateDate = created, Name = "Myanmar (formerly Burma)", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 137, CreateDate = created, Name = "Namibia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 138, CreateDate = created, Name = "Nauru", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 139, CreateDate = created, Name = "Nepal", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 140, CreateDate = created, Name = "Netherlands", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 141, CreateDate = created, Name = "New Zealand", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 142, CreateDate = created, Name = "Nicaragua", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 143, CreateDate = created, Name = "Niger", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 144, CreateDate = created, Name = "Nigeria", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 145, CreateDate = created, Name = "North Korea", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 146, CreateDate = created, Name = "North Macedonia (formerly Macedonia)", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 147, CreateDate = created, Name = "Norway", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 148, CreateDate = created, Name = "Oman", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 149, CreateDate = created, Name = "Pakistan", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 150, CreateDate = created, Name = "Palau", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 151, CreateDate = created, Name = "Palestine", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 152, CreateDate = created, Name = "Panama", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 153, CreateDate = created, Name = "Papua New Guinea", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 154, CreateDate = created, Name = "Paraguay", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 155, CreateDate = created, Name = "Peru", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 156, CreateDate = created, Name = "Philippines", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 157, CreateDate = created, Name = "Poland", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 158, CreateDate = created, Name = "Portugal", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 159, CreateDate = created, Name = "Qatar", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 160, CreateDate = created, Name = "Romania", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 161, CreateDate = created, Name = "Russia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 162, CreateDate = created, Name = "Rwanda", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 163, CreateDate = created, Name = "Saint Kitts and Nevis", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 164, CreateDate = created, Name = "Saint Lucia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 165, CreateDate = created, Name = "Saint Vincent and the Grenadines", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 166, CreateDate = created, Name = "Samoa", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 167, CreateDate = created, Name = "San Marino", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 168, CreateDate = created, Name = "Sao Tome and Principe", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 169, CreateDate = created, Name = "Saudi Arabia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 170, CreateDate = created, Name = "Senegal", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 171, CreateDate = created, Name = "Serbia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 172, CreateDate = created, Name = "Seychelles", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 173, CreateDate = created, Name = "Sierra Leone", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 174, CreateDate = created, Name = "Singapore", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 175, CreateDate = created, Name = "Slovakia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 176, CreateDate = created, Name = "Slovenia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 177, CreateDate = created, Name = "Solomon Islands", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 178, CreateDate = created, Name = "Somalia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 179, CreateDate = created, Name = "South Africa", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 180, CreateDate = created, Name = "South Korea", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 181, CreateDate = created, Name = "South Sudan", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 182, CreateDate = created, Name = "Spain", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 183, CreateDate = created, Name = "Sri Lanka", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 184, CreateDate = created, Name = "Sudan", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 185, CreateDate = created, Name = "Suriname", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 186, CreateDate = created, Name = "Sweden", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 187, CreateDate = created, Name = "Switzerland", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 188, CreateDate = created, Name = "Syria", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 189, CreateDate = created, Name = "Taiwan", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 190, CreateDate = created, Name = "Tajikistan", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 191, CreateDate = created, Name = "Tanzania", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 192, CreateDate = created, Name = "Thailand", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 193, CreateDate = created, Name = "Timor-Leste", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 194, CreateDate = created, Name = "Togo", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 195, CreateDate = created, Name = "Tonga", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 196, CreateDate = created, Name = "Trinidad and Tobago", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 197, CreateDate = created, Name = "Tunisia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 198, CreateDate = created, Name = "Turkey", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 199, CreateDate = created, Name = "Turkmenistan", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 200, CreateDate = created, Name = "Tuvalu", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 201, CreateDate = created, Name = "Uganda", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 202, CreateDate = created, Name = "Ukraine", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 203, CreateDate = created, Name = "United Arab Emirates (UAE)", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 204, CreateDate = created, Name = "United Kingdom (UK)", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 205, CreateDate = created, Name = "United States of America (USA)", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 206, CreateDate = created, Name = "Uruguay", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 207, CreateDate = created, Name = "Uzbekistan", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 208, CreateDate = created, Name = "Vanuatu", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 209, CreateDate = created, Name = "Vatican City (Holy See)", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 210, CreateDate = created, Name = "Venezuela", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 211, CreateDate = created, Name = "Vietnam", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 212, CreateDate = created, Name = "Yemen", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 213, CreateDate = created, Name = "Zambia", TagCategoryId = 2 });
            builder.Entity<Tag>().HasData(new Tag { Id = 214, CreateDate = created, Name = "Zimbabwe", TagCategoryId = 2 });
        }
    }
}
