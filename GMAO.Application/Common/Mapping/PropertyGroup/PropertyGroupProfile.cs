using AutoMapper;
using GMAO.Application.Features.property_group.Commands;
using GMAO.Application.Features.property_group.Commands.Update;
using GMAO.Application.Features.property_group.queries.DetailedPropertyGroup;
using GMAO.Application.Features.property_group.queries.ListAllPropertyGroups;
using GMAO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyGroupEntity = GMAO.Domain.Entities.PropertyGroup;
namespace GMAO.Application.Common.Mapping.PropertyGroup
{
    public class PropertyGroupProfile : Profile
    {
        public PropertyGroupProfile()
        {
            this.CreateMap<PropertyGroupEntity, ListPropertyGroupDto>()
                .ForMember(m => m.Clients, dest => dest.MapFrom(x => x.Customers.Count))
                .ForMember(m => m.Sites, dest => dest.MapFrom(x => x.Customers.Sum(c=> c.Sites.Count)))
                .ForMember(m => m.TotalAnnualRevenue, dest => dest.Ignore());// TODO - Calculer le revenu annuel total réel

            //(src, dest) =>
            //    {
            //        dest.Clients = src.Customers?.Count ?? 0;
            //        dest.Sites = src.Customers?.Sum(c => c.Sites?.Count ?? 0) ?? 0;
            //        dest.TotalAnnualRevenue = 1500; 
            //    });


            this.CreateMap<PropertyGroupEntity, DetailedPropertyGroupDto>();
            this.CreateMap<CreatePropertyGroupCommand, PropertyGroupEntity>();
            this.CreateMap<UpdateGroupPropertyCommand, PropertyGroupEntity>();

        }
    }
}
