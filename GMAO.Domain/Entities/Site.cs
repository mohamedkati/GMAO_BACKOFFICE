using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class Site : BaseAuditableEntity
    {
        public string Name { get; set; }
        public  ClientContact ClientContact { get; set; }
        public Guid? ClientContactId { get; set; }
        public  MarketType MarketType { get; set; }
        public Guid MarketTypeId { get; set; }
        public  SiteClientType SiteClientType { get; set; }
        public Guid? SiteClientTypeId { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
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
        public string BillingTitle1 { get; set; }
        public string BillingTitle2 { get; set; }
        public string BillingAddress1 { get; set; }
        public string BillingAddress2 { get; set; }
        public string BillingAddress3 { get; set; }
        public string BillingPostalCode { get; set; }
        public string BillingCity { get; set; }
        public string Comment { get; set; }
        public  ICollection<Event> Events { get; set; }
        public ICollection<Asset> Assets { get; set; } = new List<Asset>();
        public ICollection<WorkOrder> WorkOrders { get; set; } = new List<WorkOrder>();
        public ICollection<Quote> Quotes { get; set; } = new List<Quote>();
        public ICollection<ClientSite> Clients { get; set; } = new List<ClientSite>();
    }
}
