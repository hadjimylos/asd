namespace itt {
    using Microsoft.EntityFrameworkCore;

    public class EfContext : DbContext
    {
        // register tables
        // public DbSet<Name> Name { get; set; }

        public EfContext(DbContextOptions<EfContext> options)
            : base(options) {

        }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);

            // set defaults and model/column settings:

            // e.g.

            // builder.Entity<object>()
            // 	.HasMany(h => h.Employees)
            // 	.WithOne(w => w.Company)
            // 	.OnDelete(DeleteBehavior.Cascade);

            // builder.Entity<config.models.PendingTask>()
            // 	.Property(p => p.CreateDate)
            // 	.HasDefaultValueSql("getdate()");
        }
    }
}
