using AutoMapper;
using GMAO.Application.Features.me.DTOs;
using GMAO.Domain.Entities;

namespace GMAO.Application.Common.Mapping.Me
{
    public class MeMappingProfile : Profile
    {
        public MeMappingProfile()
        {
            this.CreateMap<Tenant, TenantDto>();
        }
    }
}
