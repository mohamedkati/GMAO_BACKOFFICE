using GMAO.Domain.Common;
using GMAO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class Site : BaseAuditableEntity
    {
        public string Reference { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public SiteType Type { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public GeoCoordinates? Coordinates { get; set; }
        public int? BuildingYear { get; set; }
        public decimal? TotalArea { get; set; }
        public int? FloorsCount { get; set; }
        public int? UnitsCount { get; set; }
        public ICollection<Unit> Units { get; set; } = new List<Unit>();
        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
        public ICollection<ServiceRequest> ServiceRequests { get; set; } = new List<ServiceRequest>();
        public  CustomerContact ClientContact { get; set; }
        public Guid? ClientContactId { get; set; }
        public  MarketType MarketType { get; set; }
        public Guid MarketTypeId { get; set; }
        public  SiteClientType SiteClientType { get; set; }
        public Guid? SiteClientTypeId { get; set; }
        public  Staff OperationsManager { get; set; }
        public Guid? OperationsManagerId { get; set; }
        public  Staff Commercial { get; set; }
        public Guid? CommercialId { get; set; }
        public  Staff SectorManager { get; set; }
        public Guid? SectorManagerId { get; set; }
        public  Staff Technician1 { get; set; }
        public Guid? Technician1Id { get; set; }
        public  Staff Technician2 { get; set; }
        public Guid? Technician2Id { get; set; }
       
        public string Comment { get; set; }
        public ICollection<Quote> Quotes { get; set; } = new List<Quote>();
    }
}
