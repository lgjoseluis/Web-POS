using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebPOS.Domain.Entities;

namespace WebPOS.Infrastructure.Persistences.Contexts.Configurations
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BED4C583593");

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        }
    }
}
