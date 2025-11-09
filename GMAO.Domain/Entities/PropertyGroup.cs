using GMAO.Domain.Common;

namespace GMAO.Domain.Entities
{
    public class PropertyGroup : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<Customer> Customers { get; set; } = new List<Customer>();
    }
}