using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class ClientContactRole : BaseEntity<Guid>
    {
        public string Name { get; set; } = default!;
    }
}