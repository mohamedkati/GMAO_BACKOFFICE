using Microsoft.AspNetCore.Identity;

namespace GMAO.Infrastructure.Persistance.Identity
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string Description { get; set; } = string.Empty;
    }
}
