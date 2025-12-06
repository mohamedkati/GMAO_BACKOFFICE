using AutoMapper;
using GMAO.Application.Features.property_group.Commands.CreateContactGroup;
using GMAO.Application.Features.property_group.Commands.UpdateContactGroup;
using GMAO.Application.Features.property_group.queries.PropertyGroupContacts;
using GMAO.Domain.Entities;

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
