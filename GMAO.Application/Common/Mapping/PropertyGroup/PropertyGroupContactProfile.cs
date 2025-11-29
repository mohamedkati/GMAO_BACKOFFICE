using AutoMapper;
using GMAO.Application.Features.property_group.Commands.CreateContactGroup;
using GMAO.Application.Features.property_group.Commands.UpdateContactGroup;
using GMAO.Application.Features.property_group.queries.PropertyGroupContacts;
using GMAO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Mapping.PropertyGroup
{
    public class PropertyGroupContactProfile : Profile
    {
        public PropertyGroupContactProfile()
        {
            this.CreateMap<PropertyGroupContact, PropertyGroupContactsDto>();

            this.CreateMap<CreateContactGroupCommand, PropertyGroupContact>();
            this.CreateMap<UpdateContactGroupCommand, PropertyGroupContact>();
        }
    }
}
