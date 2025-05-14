using System.Linq.Expressions;
using CleanArc.Domain.Common;
using CleanArc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArc.Infrastructure.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options) { }

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
            // set query filter for soft delete
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsDeleted);
        }
    }
}
