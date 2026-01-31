using AutoMapper;
using GMAO.Application.Features.Customers.Commands.CreateBudget;
using GMAO.Application.Features.Customers.Commands.CreateCustomer;
using GMAO.Application.Features.Customers.Commands.CreateCustomerContact;
using GMAO.Application.Features.Customers.Commands.UpdateBudget;
using GMAO.Application.Features.Customers.Commands.UpdateCustomer;
using GMAO.Application.Features.Customers.Commands.UpdateCustomerContact;
using GMAO.Application.Features.Customers.DTOs;
using GMAO.Application.SharedBusiness.Dtos.customer;
using GMAO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Mapping.CustomerProfile
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            // Customer Mapping
            CreateMap<Customer, CustomerDetailedDto>()
                .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => src.Contacts))
                .ForMember(dest => dest.MaintenanceBudgets, opt => opt.MapFrom(src => src.MaintenanceBudgets))
                .ReverseMap();
            CreateMap<CreateCustomerCommand, Customer>();
            CreateMap<UpdateCustomerCommand, Customer>();

            // Contact Mapping
            CreateMap<CustomerContact, CustomerContactDto>()
                .ReverseMap();
            CreateMap<CreateCustomerContactCommand, CustomerContact>();
            CreateMap<UpdateCustomerContactCommand, CustomerContact>();


            // Budget Mapping
            CreateMap<MaintenanceBudget, CustomerBudgetDto>();
            CreateMap<CreateBudgetCommand, MaintenanceBudget>();
            CreateMap<UpdateBudgetCommand, MaintenanceBudget>();

            CreateMap<Customer, SharedCustomerDto>();
            CreateMap<Customer, CustomerForSelectControlDto>();
        }
    }
}
