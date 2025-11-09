using GMAO.Domain.Common;
using GMAO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class Supplier : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? CompanyRegistrationNumber { get; set; }
        public Address? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Website { get; set; }
        public string? ContactPerson { get; set; }
        public SupplierRating? Rating { get; set; }
        public string? PaymentTerms { get; set; }
        public ICollection<PurchaseOrder> PurchaseOrders { get; set; } = new List<PurchaseOrder>();
        public ICollection<SupplierCatalogItem> CatalogItems { get; set; } = new List<SupplierCatalogItem>();
    }
}
