using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebPOS.Domain.Entities;

namespace WebPOS.Infrastructure.Persistences.Contexts.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A659F0146");

            builder.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
