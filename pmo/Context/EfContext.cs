using Microsoft.EntityFrameworkCore;
using dbModels;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;

namespace pmo
{
    public class EfContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EfContext(DbContextOptions<EfContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public EfContext() { }
        public DbSet<GateKeeperConfig> GateKeeperConfigs { get; set; }
        public DbSet<BusinessCase> BusinessCases { get; set; }
        public DbSet<CustomerDesignApproval> CustomerDesignApprovals { get; set; }
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
        public DbSet<User> Users { get; set; }
        public DbSet<User_CitizenShip> UserCitizenShip { get; set; }
        public DbSet<ProjectDetail_SalesRegion> ProjectDetail_SalesRegions { get; set; }
        public DbSet<ProjectDetail_EndUserCountry> ProjectDetail_EndUserCountries { get; set; }
        public DbSet<ProjectDetail_Customer> ProjectDetail_Customers { get; set; }
        public DbSet<StageConfig_RequiredSchedule> StageConfig_RequiredSchedules { get; set; }
        public DbSet<BusinessCase_ManufacturingLocation> BusinessCase_ManufacturingLocations { get; set; }
        public DbSet<Project_User> Project_User { get; set; }
        public DbSet<ProjectStateHistory> ProjectStateHistories { get; set; }
        public DbSet<DeliverableRegister> DeliverableRegisters { get; set; }
        public DbSet<DeliverableRegisterConfig> DeliverableRegisterConfigs { get; set; }



        public EfContext(DbContextOptions<EfContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
                
                if (_httpContextAccessor.HttpContext == null) {
                    model.Property(prop => prop.ModifiedByUser).CurrentValue = "system";
                }
                else {
                    model.Property(prop => prop.ModifiedByUser).CurrentValue = _httpContextAccessor.HttpContext.User.Identity.Name;
                }
                model.Property(prop => prop.ModifiedByUser).IsModified = true;
            });
            return base.SaveChanges();
        }
    }
}