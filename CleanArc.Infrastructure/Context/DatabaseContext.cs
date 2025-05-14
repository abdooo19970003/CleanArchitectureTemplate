using CleanArc.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanArc.Infrastructure.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        // Define DbSets for your entities here
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional model configuration can go here
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                // Keep table names singular
                var tableName = entity.GetTableName();
                if (tableName != null)
                {
                    var newTableName = tableName.Substring(0, tableName.Length - 1);
                    entity.SetTableName(newTableName);
                }
            }

            // Configure soft delete for all entities inheriting from Entity
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);

            // Set query filter for soft delete
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
