using GMAO.Domain.ValueObjects;

namespace GMAO.Application.Features.me.DTOs
{
    public class TenantDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public bool IsActive { get; set; } = true;
        public string Subdomain { get; set; } = string.Empty;
        public string? Logo { get; set; }
        public string? Website { get; set; }
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Address? Address { get; set; }
        public TenantSettings? Settings { get; set; } = new();
        public TenantFeatures? Features { get; set; } = new();
        public TenantSubscription? Subscription { get; set; } = new();
        public TenantLimits? TenantLimits { get; set; } = new();
        //public Dictionary<string, object>? Metadata { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid? LastModifiedBy { get; set; }
        public DateTime? LastModifiedAt { get; set; }
    }
}