using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class QuoteRequestConfiguration : IEntityTypeConfiguration<QuoteRequest>
    {
        public void Configure(EntityTypeBuilder<QuoteRequest> builder)
        {
            builder.ToTable("quote_requests");
            builder.HasKey(qr => qr.Id);

            builder.Property(qr => qr.Reference).IsRequired().HasMaxLength(50);
            builder.Property(qr => qr.Status).IsRequired().HasConversion<int>();
            builder.Property(qr => qr.RequestedAt).IsRequired();
            builder.Property(qr => qr.InspectionScheduledAt);
            builder.Property(qr => qr.InspectionCompletedAt);
            builder.Property(qr => qr.InspectedBy);
            builder.Property(qr => qr.InspectionNotes).HasMaxLength(2000);

            //builder.HasOne(qr => qr.ServiceRequest).WithMany()
            //    .HasForeignKey(qr => qr.ServiceRequestId).OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany(qr => qr.Items).WithOne(i => i.QuoteRequest)
            //    .HasForeignKey(i => i.QuoteRequestId).OnDelete(DeleteBehavior.Cascade);
            //builder.HasMany(qr => qr.Quotes).WithOne(q => q.QuoteRequest)
            //    .HasForeignKey(q => q.QuoteRequestId).OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(qr => qr.Reference).IsUnique();
            //builder.HasIndex(qr => qr.ServiceRequestId);
            builder.HasIndex(qr => qr.Status);
            builder.HasIndex(qr => qr.TenantId);
            builder.Ignore(qr => qr.DomainEvents);
        }
    }
}
