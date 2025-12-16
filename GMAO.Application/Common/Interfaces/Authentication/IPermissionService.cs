using GMAO.Application.Features.Permissions.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Interfaces.Authentication
{
    public interface IPermissionService
    {
        // Permissions
        Task<List<PermissionDto>> GetAllPermissionsAsync();
        Task<PermissionDto> GetPermissionByIdAsync(Guid id);
        Task<PermissionDto> CreatePermissionAsync(CreatePermissionDto dto);
        Task<PermissionDto> UpdatePermissionAsync(Guid id, UpdatePermissionDto dto);
        Task DeletePermissionAsync(Guid id);
        Task SyncPermissionsFromConfigAsync();

        // Roles
        Task<List<RoleDto>> GetAllRolesAsync();
        Task<RoleDto> GetRoleByIdAsync(Guid id);
        Task<RoleDto> CreateDomainRoleAsync(CreateRoleDto dto);
        Task<RoleDto> UpdateRoleAsync(Guid id, UpdateRoleDto dto);
        Task DeleteRoleAsync(Guid id);

        // Role Permissions
        Task<List<PermissionDto>> GetRolePermissionsAsync(Guid roleId);
        Task AssignPermissionsToRoleAsync(Guid roleId, AssignPermissionsDto dto);
        Task RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId);

        // User Permissions
        Task<List<PermissionDto>> GetUserPermissionsAsync(Guid userId);
        Task<List<string>> GetEffectiveUserPermissionsAsync(Guid userId);
        Task GrantPermissionToUserAsync(Guid userId, Guid permissionId);
        Task RevokePermissionFromUserAsync(Guid userId, Guid permissionId);
        Task<bool> UserHasPermissionAsync(Guid userId, string resource, string action);

        // Audit
        Task<List<PermissionAuditDto>> GetPermissionAuditAsync(PermissionAuditFilter filter);

        Task<UserPermissionDto> GetPermissionsForCurrentUserAsync();
    }

}
