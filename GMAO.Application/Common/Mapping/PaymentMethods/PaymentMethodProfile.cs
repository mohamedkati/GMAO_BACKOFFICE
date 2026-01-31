using AutoMapper;
using GMAO.Application.SharedBusiness.Dtos.payment_method;
using GMAO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Mapping.PaymentMethods
{
    public class PaymentMethodProfile : Profile
    {
        public PaymentMethodProfile()
        {
            CreateMap<PaymentMethod, SharedPaymentMethodDto>();
        }
    }
}
