using GMAO.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Persistance.Configurations
{
    public partial class ServiceRequestConfig : IEntityTypeConfiguration<ServiceRequest>
    {
        public void Configure(EntityTypeBuilder<ServiceRequest> builder)
        {

            builder.ToTable("service_requests");
            builder.HasKey(sr => sr.Id);

            builder.Property(sr => sr.Reference).IsRequired().HasMaxLength(50);
            builder.Property(sr => sr.Type).IsRequired().HasConversion<int>();
            builder.Property(sr => sr.OriginType).IsRequired().HasConversion<int>();
            builder.Property(sr => sr.Channel).IsRequired().HasConversion<int>();
            builder.Property(sr => sr.Title).IsRequired().HasMaxLength(200);
            builder.Property(sr => sr.Description).HasMaxLength(2000);
            builder.Property(sr => sr.Urgency).IsRequired().HasConversion<int>();
            builder.Property(sr => sr.Status).IsRequired().HasConversion<int>();
            builder.Property(sr => sr.RequestedAt).IsRequired();
            builder.Property(sr => sr.AcknowledgedAt);
            builder.Property(sr => sr.SLADeadline);

            builder.HasOne(sr => sr.Customer).WithMany(x=> x.Events)
                .HasForeignKey(sr => sr.CustomerId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(sr => sr.Site).WithMany(x=> x.ServiceRequests)
                .HasForeignKey(sr => sr.SiteId).OnDelete(DeleteBehavior.Restrict).IsRequired();
            builder.HasOne(sr => sr.Unit).WithMany(x=> x.ServiceRequests)
                .HasForeignKey(sr => sr.UnitId).OnDelete(DeleteBehavior.NoAction).IsRequired(false);
            builder.HasOne(sr => sr.Asset).WithMany(x => x.ServiceRequests)
                .HasForeignKey(sr => sr.AssetId).OnDelete(DeleteBehavior.SetNull).IsRequired(false);
            builder.HasOne(sr => sr.Occupant).WithMany(x=> x.ServiceRequests)
                .HasForeignKey(sr => sr.OccupantId).OnDelete(DeleteBehavior.SetNull).IsRequired(false);
            builder.HasMany(sr => sr.WorkOrders).WithOne(wo => wo.ServiceRequest)
                .HasForeignKey(wo => wo.ServiceRequestId).OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(sr => sr.Reference).IsUnique();
            builder.HasIndex(sr => sr.CustomerId);
            builder.HasIndex(sr => sr.SiteId);
            builder.HasIndex(sr => sr.UnitId);
            builder.HasIndex(sr => sr.OccupantId);
            builder.HasIndex(sr => sr.Status);
            builder.HasIndex(sr => sr.Urgency);
            builder.HasIndex(sr => sr.RequestedAt);
            builder.HasIndex(sr => sr.TenantId);
            builder.Ignore(sr => sr.DomainEvents);
          
            

            builder.HasOne(p => p.QuoteFor)
                .WithMany(p => p.QuotesFor)
                .HasForeignKey(p => p.QuoteForId)
                .IsRequired(false);

            builder.HasOne(p => p.EventReason)
                .WithMany(p => p.Events)
                .HasForeignKey(p => p.EventReasonId)
                .IsRequired(false);

            builder.Property(p => p.Comment)
                .HasMaxLength(500);

        }
    }
}
