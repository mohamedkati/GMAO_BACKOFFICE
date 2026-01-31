using GMAO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.SharedBusiness.Dtos.customer
{
    public class CustomerForSelectControlDto 
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public Address InvoiceAddress { get; set; } = default!;
    }
}
