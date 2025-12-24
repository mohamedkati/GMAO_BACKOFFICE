using GMAO.Domain.Common;
using GMAO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities.siteAggregate
{
    public class Site : BaseAuditableEntity
    {
        public string Reference { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public SiteType Type { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public Address BillingAddress { get; set; } = null!;
        public GeoCoordinates? Coordinates { get; set; }

        public int? BuildingYear { get; set; }
        public decimal? TotalArea { get; set; }
        public int? FloorsCount { get; set; }
        public int? UnitsCount { get; set; }
       
        public Guid? ClientContactId { get; set; }
        public SectorType SectorType { get; set; }
        public Guid SectorTypeId { get; set; }
        public Guid ClientTypeId { get; set; }

        public string Comment { get; set; }

        public TVA VAT { get; set; }
        public Guid VatId { get; set; }
        public SiteAccess SiteAccessInfo { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public string Siren { get; set; }
        public string Siret { get; set; }
        public string MainMailAddress { get; set; }
        public string InvoiceMailAddress { get; set; }
        public string CommentReport { get; set; }

        /// <summary>
        ///  TEAM MANAGER FOR SITE
        /// </summary>
        public Guid? OperationsManagerId { get; set; }
        public Guid? CommercialId { get; set; }
        public Guid? SectorManagerId { get; set; }
        public Guid? Technician1Id { get; set; }
        public Guid? Technician2Id { get; set; }

        public ICollection<Quote> Quotes { get; set; } = new List<Quote>();
        public ICollection<SiteKeeper> SiteKeepers { get; set; } = new List<SiteKeeper>();
        public Staff Commercial { get; set; }
        public Staff OperationsManager { get; set; }// responsable d'exploitation
        public Staff SectorManager { get; set; }
        public Staff Technician1 { get; set; }
        public Staff Technician2 { get; set; }
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        public CustomerContact ClientContact { get; set; }
        public SiteClientType ClientType { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public ICollection<SiteDocument> Documents { get; set; }

        public ICollection<SiteContact> Contacts { get; set; }
    }
}
