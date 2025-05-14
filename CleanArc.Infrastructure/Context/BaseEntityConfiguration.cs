using CleanArc.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArc.Infrastructure.Context
{
    internal abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(e => e.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(e => e.UpdatedAt).IsRequired(false);

            builder.Property(e => e.IsDeleted)
                .HasDefaultValue(false)
                .IsRequired();

            builder.HasQueryFilter(e => !e.IsDeleted);
        }
    }
}
