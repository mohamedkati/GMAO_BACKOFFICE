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
    public class EventConfig : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {
            builder.HasOne(p => p.Site)
            .WithMany(p => p.Events)
            .HasForeignKey(p => p.SiteId)
            .IsRequired();

            builder.HasOne(p=> p.Asset)
                .WithMany(p => p.Events)
                .HasForeignKey(p => p.AssetId)
                .IsRequired(false);

            builder.HasOne(p => p.Client)
                .WithMany(p => p.Events)
                .HasForeignKey(p => p.ClientId)
                .IsRequired();

            builder.Property(p => p.OtherInterlocutor)
                .HasMaxLength(100);

            builder.HasOne(p => p.QuoteFor)
                .WithMany(p => p.QuotesFor)
                .HasForeignKey(p => p.QuoteForId)
                .IsRequired(false);

            builder.Property(p => p.ServiceOrderReference)
                .HasMaxLength(100);

            builder.HasOne(p => p.EventReason)
                .WithMany(p => p.Events)
                .HasForeignKey(p => p.EventReasonId)
                .IsRequired(false);


            builder.Property(p => p.Comment)
                .HasMaxLength(500);

            builder.HasOne(p => p.OriginFile)
                .WithMany(p => p.OriginFilesEvents)
                .HasForeignKey(p => p.OriginFileId)
                .IsRequired(false);

            builder.HasOne(p => p.ServiceOrderFile)
                .WithMany(p => p.ServiceOrderFileEvents)
                .HasForeignKey(p => p.ServiceOrderFileId)
                .IsRequired(false);
        }
    }
}
