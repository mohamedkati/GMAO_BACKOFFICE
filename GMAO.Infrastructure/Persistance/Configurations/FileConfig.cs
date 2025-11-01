using GMAO.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using GMAO.Domain.Entities;
using File = GMAO.Domain.Entities.File;
namespace GMAO.Infrastructure.Persistance.Configurations
{
    public class FileConfig : IEntityTypeConfiguration<File>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<File> builder)
        {
            builder.ToTable("Files");
            builder.Property(p => p.Filename)
             .HasMaxLength(100);

            builder.Property(p => p.EncryptedFilename)
                .HasMaxLength(100);

            builder.Property(p => p.Extension)
                .HasMaxLength(10);

            builder.Property(p => p.UploadFolder)
                .HasMaxLength(500);

            builder.Property(p => p.InTmp)
                .HasDefaultValue(false);
        }
    }
}
