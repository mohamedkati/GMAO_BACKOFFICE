using GMAO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.SharedBusiness.Dtos.payment_method
{
    public class PaymentMethodAsKeyValueDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Terms { get; set; }
        public string DueDays { get; set; }
        public string Days { get; set; }
        public DueDateType TypeDueDate { get; set; }    
    }
}
