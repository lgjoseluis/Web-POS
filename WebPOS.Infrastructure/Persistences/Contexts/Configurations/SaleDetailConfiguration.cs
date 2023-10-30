using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebPOS.Domain.Entities;

namespace WebPOS.Infrastructure.Persistences.Contexts.Configurations
{
    public class SaleDetailConfiguration : IEntityTypeConfiguration<SaleDetail>
    {
        public void Configure(EntityTypeBuilder<SaleDetail> builder)
        {
            builder.HasKey(e => e.SaleDetailId).HasName("PK__SaleDeta__70DB14FE72DDFC55");

            builder.Property(e => e.Discount).HasColumnType("decimal(18, 2)");
            builder.Property(e => e.Price).HasColumnType("decimal(18, 2)");

            builder.HasOne(d => d.Product).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__SaleDetai__Produ__571DF1D5");

            builder.HasOne(d => d.Sale).WithMany(p => p.SaleDetails)
                .HasForeignKey(d => d.SaleId)
                .HasConstraintName("FK__SaleDetai__SaleI__5812160E");
        }
    }
}
