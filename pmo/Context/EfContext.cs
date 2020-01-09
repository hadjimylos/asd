using Microsoft.EntityFrameworkCore;
using dbModels;
using System.Linq;

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
        public DbSet<BusinessDesignConcept> BusinessDesignConcepts{ get; set; }
        public DbSet<GateUploadedDocumentation> GateUploadedDocumentations { get; set; }
        public DbSet<ProductInfrigmentPatentabilityUploadedDocumentation> ProductInfrigmentPatentabilityUploadedDocumentations { get; set; }
        public DbSet<CustomerDesignApprovalUploadedDocumentation> CustomerDesignApprovalUploadedDocumentations { get; set; }
        public DbSet<ProjectDetail_SalesRegions> ProjectDetail_SalesRegions { get; set; }
        public DbSet<ProjectDetail_EndUserCountries> ProjectDetail_EndUserCountries { get; set; }
        public DbSet<ProjectDetail_Customers> ProjectDetail_Customers { get; set; }
        public DbSet<StageConfig_RequiredDesignConcepts> StageConfig_RequiredDesignConcepts { get; set; }
        public DbSet<StageConfig_RequiredSchedules> StageConfig_RequiredSchedules { get; set; }
        public DbSet<BusinessCase_ManufacturingLocations> BusinessCase_ManufacturingLocations { get; set; }


        public EfContext(DbContextOptions<EfContext> options)
            : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            // set all foreign keys to restrict delete
            var fks = builder.Model.GetEntityTypes().SelectMany (
                s => s.GetForeignKeys()
            ).ToList();
            fks.ForEach(f => f.DeleteBehavior = DeleteBehavior.Restrict);
        }
    }
}
