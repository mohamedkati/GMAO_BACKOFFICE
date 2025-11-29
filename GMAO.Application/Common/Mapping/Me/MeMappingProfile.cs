using AutoMapper;
using GMAO.Application.Features.me.DTOs;
using GMAO.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
