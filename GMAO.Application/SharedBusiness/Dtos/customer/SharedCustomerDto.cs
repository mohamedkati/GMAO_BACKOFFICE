using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.SharedBusiness.Dtos.customer
{
    public class SharedCustomerDto
    {
        public string Id { get; set; }
        public string Reference { get; set; }
        public string CompanyName { get; set; }
        public string? PropertyGroupId { get; set; }
        public string? PropertyGroupName { get; set; }
        public string? CommercialId { get; set; }
        public string? PaymentMethodId { get; set; }
    }
}
