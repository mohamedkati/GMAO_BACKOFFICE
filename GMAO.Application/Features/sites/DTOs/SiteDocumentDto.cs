using GMAO.Domain.Entities.siteAggregate;
using GMAO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.DTOs
{
    public class SiteDocumentDto
    {
        public Guid Id { get; set; }
        public DocumentType Type { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty; // Ou Blob storage URL
        public long FileSize { get; set; }
        public string? MimeType { get; set; }
        public string? Description { get; set; }

        public DateTime? ExpirationDate { get; set; } // Pour certificats
        public bool SendExpirationAlert { get; set; }
        public bool IsPlan { get; set; }
    }
}
