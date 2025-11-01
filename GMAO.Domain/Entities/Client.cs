using GMAO.Domain.Common;
using GMAO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    public class Client : BaseAuditableEntity
    {
        public string Name { get; set; } = default!;
        public string CompanyName { get; set; } = default!; // Raison sociale
        public string RegistrationNumber { get; set; } = default!; // SIREN
        public string Comment { get; set; } = default!;

        public Guid CommercialId { get; set; }
        //public Guid? InvoiceAddressId { get; set; }
        //public Guid? MailingAddressId { get; set; }
        public Guid? PaymentMethodId { get; set; } // default payment term ( mode réglement )
        public AddressObj InvoiceAddress { get; set; } = default!;
        public AddressObj MailingAddress { get; set; } = default!;
        public PaymentMethod PaymentMethod { get; set; } = default!;
        public Staff Commercial { get; set; }
        public ICollection<ClientSite> Sites { get; set; } = new List<ClientSite>();
        public ICollection<ClientContact> Contacts { get; set; } = new List<ClientContact>();
        public ICollection<Event> Events { get; set; } = new List<Event>();
    }
}
