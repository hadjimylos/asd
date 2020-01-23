using Microsoft.EntityFrameworkCore;
using dbModels;
using System.Linq;
using System.Collections.Generic;

namespace pmo {
    public class EfContext : DbContext {
        public DbSet<GateKeeperConfig> GateKeeperConfigs { get; set; }
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
        public DbSet<ProjectDetail_SalesRegion> ProjectDetail_SalesRegions { get; set; }
        public DbSet<ProjectDetail_EndUserCountry> ProjectDetail_EndUserCountries { get; set; }
        public DbSet<ProjectDetail_Customer> ProjectDetail_Customers { get; set; }
        public DbSet<StageConfig_RequiredSchedule> StageConfig_RequiredSchedules { get; set; }
        public DbSet<BusinessCase_ManufacturingLocation> BusinessCase_ManufacturingLocations { get; set; }


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
            // Required Schedules
            builder.Entity<TagCategory>().HasData(new List<TagCategory>() { 
                new TagCategory { Id = 1, IsFixed = true, FriendlyName = "Required Schedules", Key = "required-schedules" },
                new TagCategory { Id = 2, IsFixed = true, FriendlyName = "Citizenships", Key = "citizenships" },
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
            });


        }
    }
}
