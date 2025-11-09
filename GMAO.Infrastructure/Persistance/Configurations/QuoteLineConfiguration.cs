using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class QuoteLineConfiguration : IEntityTypeConfiguration<QuoteLine>
    {
        public void Configure(EntityTypeBuilder<QuoteLine> builder)
        {
            builder.ToTable("quote_lines");
            builder.HasKey(l => l.Id);

            builder.Property(l => l.LineNumber).IsRequired();
            builder.Property(l => l.Type).IsRequired().HasConversion<int>();
            builder.Property(l => l.Category).HasMaxLength(100);
            builder.Property(l => l.Description).IsRequired().HasMaxLength(1000);
            builder.Property(l => l.Quantity).IsRequired().HasPrecision(10, 2);
            builder.Property(l => l.Unit).HasMaxLength(20);
            builder.Property(l => l.CostPrice).IsRequired().HasPrecision(10, 2);
            builder.Property(l => l.Coefficient).IsRequired().HasPrecision(5, 2);
            builder.Property(l => l.UnitPrice).IsRequired().HasPrecision(10, 2);
            builder.Property(l => l.DiscountPercent).HasPrecision(5, 2);
            builder.Property(l => l.GeneratedWorkOrderId);

            builder.Ignore(l => l.LineTotal);
            builder.Ignore(l => l.TotalCost);
            builder.Ignore(l => l.Margin);
            builder.Ignore(l => l.MarginPercent);

            builder.HasOne(l => l.Quote).WithMany(q => q.Lines)
                .HasForeignKey(l => l.QuoteId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(l => l.QuoteId);
        }
    }


}
