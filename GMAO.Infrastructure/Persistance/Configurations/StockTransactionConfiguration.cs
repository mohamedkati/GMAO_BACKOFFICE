using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class StockTransactionConfiguration : IEntityTypeConfiguration<StockTransaction>
    {
        public void Configure(EntityTypeBuilder<StockTransaction> builder)
        {
            builder.ToTable("stock_transactions");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Type).IsRequired().HasConversion<int>();
            builder.Property(t => t.Quantity).IsRequired().HasPrecision(10, 2);
            builder.Property(t => t.Unit).HasMaxLength(20);
            builder.Property(t => t.UnitCost).HasPrecision(10, 2);
            builder.Property(t => t.Reference).HasMaxLength(100);
            builder.Property(t => t.Notes).HasMaxLength(1000);
            builder.Property(t => t.TransactionDate).IsRequired();

            builder.HasOne(t => t.InventoryItem).WithMany(i => i.Transactions)
                .HasForeignKey(t => t.InventoryItemId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => t.InventoryItemId);
            builder.HasIndex(t => t.TransactionDate);
            builder.HasIndex(t => t.Type);
            builder.HasIndex(t => t.TenantId);
            builder.Ignore(t => t.DomainEvents);
        }
    }

}
