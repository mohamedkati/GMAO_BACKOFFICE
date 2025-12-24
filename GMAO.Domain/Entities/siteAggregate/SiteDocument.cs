using GMAO.Domain.Common;

namespace GMAO.Domain.Entities.siteAggregate
{
    public class SiteDocument : BaseAuditableEntity
    {
        public Guid SiteId { get; set; }
        public Site Site { get; set; } = null!;

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
