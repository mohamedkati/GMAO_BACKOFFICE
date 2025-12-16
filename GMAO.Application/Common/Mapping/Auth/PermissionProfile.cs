using AutoMapper;
using GMAO.Application.Features.Permissions.DTOs;
using GMAO.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Mapping.Auth
{
    public class PermissionProfile : Profile
    {
        public PermissionProfile()
        {
            CreateMap<CreatePermissionDto, Domain.Entities.Auth.Permission>();
            CreateMap<UpdatePermissionDto, Domain.Entities.Auth.Permission>();
            CreateMap<Domain.Entities.Auth.Permission, PermissionDto>();
            CreateMap<PermissionAudit, PermissionAuditDto>();
            CreateMap<Role, RoleDto>()
                .ForMember(x => x.PermissionCount, t => t.MapFrom(r => r.Permissions.Count))
                .ForMember(x => x.UserCount, t => t.MapFrom(r => r.Users.Count));
            CreateMap<CreateRoleDto, Role>();

        }
    }
}
