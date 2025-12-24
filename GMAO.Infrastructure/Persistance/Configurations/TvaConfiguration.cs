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
    public class TvaConfiguration : IEntityTypeConfiguration<TVA>
    {
        public void Configure(EntityTypeBuilder<TVA> builder)
        {
            builder.ToTable("vats");
            builder.HasKey(x=> x.Id);
            builder.Property(w=> w.Code).HasMaxLength(20).IsRequired();

            builder.Property(x=> x.Name).HasMaxLength(150).IsRequired(false); 

            builder.Property(x=> x.ValuRate).IsRequired(true);

        }
    }
}
