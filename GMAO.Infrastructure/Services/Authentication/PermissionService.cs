using AutoMapper;
using AutoMapper.QueryableExtensions;
using GMAO.Application.Common.Exceptions;
using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Application.Features.Permissions.DTOs;
using GMAO.Domain.Authorization;
using GMAO.Domain.Entities.Auth;
using GMAO.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Infrastructure.Services.Authentication
{
    public class PermissionService : IPermissionService
    {
        private readonly AppDbContext _context;
        private readonly ILogger<PermissionService> _logger;
        private readonly IAuthenticatedUser _authenticatedUser;
        private readonly IMapper mapper;

        public PermissionService(AppDbContext context, ILogger<PermissionService> logger, IAuthenticatedUser authenticatedUser, IMapper mapper)
        {
            this._context = context;
            this._logger = logger;
            this._authenticatedUser = authenticatedUser;
            this.mapper = mapper;
        }
        public async Task AssignPermissionsToRoleAsync(Guid roleId, AssignPermissionsDto dto)
        {
            var role = await _context.DomainRoles.FindAsync(roleId);
            if (role == null)
            {
                throw new AppValidationException(nameof(roleId), $"Role with id {roleId} not found.");
            }

            var permissions = await _context.Permissions
                .Where(p => dto.PermissionIds.Contains(p.Id))
                .ToListAsync();

            if (permissions.Count != dto.PermissionIds.Count)
            {
                var foundIds = permissions.Select(p => p.Id);
                var notFoundIds = dto.PermissionIds.Except(foundIds);
                throw new AppValidationException(nameof(dto.PermissionIds), $"Permissions with ids {string.Join(", ", notFoundIds)} not found.");
            }

            foreach (var permission in permissions)
            {
                if (!await _context.RolePermissions.AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permission.Id))
                {
                    var rolePermission = new RolePermission
                    {
                        RoleId = roleId,
                        PermissionId = permission.Id
                    };
                    await _context.RolePermissions.AddAsync(rolePermission);
                }
            }

            await _context.SaveChangesAsync();
            await AuditAsync("ROLE_PERMISSIONS_ASSIGNED", $"Assigned permissions to roleId {roleId} with ${permissions.Count} permissions", null, roleId);
        }

        public async Task<PermissionDto> CreatePermissionAsync(CreatePermissionDto dto)
        {
            var existingPermission = await _context.Permissions
                .AnyAsync(p => p.Resource == dto.Resource && p.Action == dto.Action);
            if (existingPermission)
            {
                throw new AppValidationException(nameof(dto.Resource), $"Permission pour resource '{dto.Resource}' et l'action '{dto.Action}' exite déjà.");
            }

            var permission = mapper.Map<Permission>(dto);
            //permission.Code = $"{dto.Resource}:{dto.Action}";
            permission.Action = dto.Action.ToLowerInvariant();
            permission.Resource = dto.Resource.ToLowerInvariant();

            await _context.Permissions.AddAsync(permission);
            await _context.SaveChangesAsync();
            await AuditAsync("PERMISSION_CREATED", $"Created permission {permission.Code}", permission.Code, permissionId: permission.Id);
            return mapper.Map<PermissionDto>(permission);
        }

        public async Task<RoleDto> CreateDomainRoleAsync(CreateRoleDto dto)
        {
            var roleExists = await _context.DomainRoles.AnyAsync(r => r.Name == dto.Name);
            if (roleExists)
            {
                throw new AppValidationException(nameof(dto.Name), $"Role avec le nom '{dto.Name}' déjà existant.");
            }

            var role = mapper.Map<Role>(dto);
            await _context.DomainRoles.AddAsync(role);
            await _context.SaveChangesAsync();
            await AuditAsync("ROLE_CREATED", $"Created role {role.Name}", null, targetRoleId: role.Id);
            return mapper.Map<RoleDto>(role);
        }

        public async Task DeletePermissionAsync(Guid id)
        {
            var permission = await _context.Permissions.FindAsync(id);

            if (permission == null)
            {
                throw new AppValidationException(nameof(id), $"La permission n'existe pas");
            }

            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
            await AuditAsync("PERMISSION_DELETED", $"Deleted permission {permission.Code}", permission.Code, permissionId: permission.Id);
        }

        public async Task DeleteRoleAsync(Guid id)
        {
            var role = await _context.DomainRoles.FindAsync(id);
            if (role == null)
            {
                throw new AppValidationException(nameof(id), $"Le rôle n'existe pas");
            }
            if (role.IsSystem)
            {
                throw new AppValidationException(nameof(id), $"Le rôle système ne peut pas être supprimé");
            }

            _context.DomainRoles.Remove(role);
            await _context.SaveChangesAsync();
            await AuditAsync("ROLE_DELETED", $"Deleted role {role.Name}", null, targetRoleId: role.Id);
        }

        public async Task<List<PermissionDto>> GetAllPermissionsAsync()
        {
            var permissions = await _context.Permissions
                .AsNoTracking()
                .ProjectTo<PermissionDto>(mapper.ConfigurationProvider)
                .ToListAsync();

            return permissions;
        }

        public async Task<List<RoleDto>> GetAllRolesAsync()
        {
            var roles = await _context.DomainRoles
                .Include(x => x.Permissions)
                .ThenInclude(x => x.Permission)
                .Include(x => x.Users)
                 .AsNoTracking()
                 .ProjectTo<RoleDto>(mapper.ConfigurationProvider)
                 .ToListAsync();
            return roles;
        }

        public async Task<List<string>> GetEffectiveUserPermissionsAsync(Guid userId)
        {
            var userPermissions = await _context
                .UserPermissions
                .Include(x => x.Permission)
                 .Where(up => up.UserId == userId)
                 .ToListAsync();

            var rolePermissions = await _context.DomainRoles
                .Include(x => x.Permissions)
                .ThenInclude(p => p.Permission)
                .Include(x => x.Users)
                .Where(r => r.Users.Any(ru => ru.UserId == userId))
                .SelectMany(r => r.Permissions)
                .Select(rp => rp.Permission.Code)
                .ToHashSetAsync();

            // Apply user-specific grants and revokes
            foreach (var up in userPermissions)
            {
                if (up.IsGranted && !rolePermissions.Contains(up.Permission.Code))
                {
                    rolePermissions.Add(up.Permission.Code);
                }
                else if (!up.IsGranted)
                {
                    rolePermissions.Remove(up.Permission.Code);
                }
            }

            return rolePermissions.ToList();
        }

        public async Task<List<PermissionAuditDto>> GetPermissionAuditAsync(PermissionAuditFilter filter)
        {
            var query = _context.PermissionAudits.AsQueryable();
            if (filter.UserId.HasValue)
            {
                query = query.Where(pa => pa.UserId == filter.UserId.Value);
            }
            if (filter.TargetUserId.HasValue)
            {
                query = query.Where(pa => pa.TargetUserId == filter.TargetUserId.Value);
            }
            if (filter.TargetRoleId.HasValue)
            {
                query = query.Where(pa => pa.TargetRoleId == filter.TargetRoleId.Value);
            }
            if (!string.IsNullOrEmpty(filter.Action))
            {
                query = query.Where(pa => pa.Action == filter.Action);
            }
            if (filter.StartDate.HasValue)
            {
                query = query.Where(pa => pa.Timestamp >= filter.StartDate.Value);
            }
            if (filter.EndDate.HasValue)
            {
                query = query.Where(pa => pa.Timestamp <= filter.EndDate.Value);
            }
            query = query.OrderByDescending(pa => pa.Timestamp);
            if (filter.Limit.HasValue)
            {
                query = query.Take(filter.Limit.Value);
            }
            return await query
                .AsNoTracking()
                .ProjectTo<PermissionAuditDto>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task<PermissionDto> GetPermissionByIdAsync(Guid id)
        {
            var pemission = await _context.Permissions.FirstOrDefaultAsync(predicate: p => p.Id == id);
            if (pemission == null)
            {
                throw new AppValidationException(nameof(id), $"Permission with id {id} not found.");
            }

            return mapper.Map<PermissionDto>(pemission);
        }

        public async Task<RoleDto> GetRoleByIdAsync(Guid id)
        {
            var role = await _context.DomainRoles.FirstOrDefaultAsync(predicate: r => r.Id == id);
            if (role == null)
            {
                throw new AppValidationException(nameof(id), $"Role with id {id} not found.");
            }
            return mapper.Map<RoleDto>(role);
        }

        public async Task<List<PermissionDto>> GetRolePermissionsAsync(Guid roleId)
        {
            return await _context.Permissions
                  .Where(p => p.Roles.Any(rp => rp.RoleId == roleId))
                  .ProjectTo<PermissionDto>(mapper.ConfigurationProvider)
                  .ToListAsync();
        }

        public async Task<List<PermissionDto>> GetUserPermissionsAsync(Guid userId)
        {
            var userPermissions = await _context
              .UserPermissions
              .Include(x => x.Permission)
               .Where(up => up.UserId == userId)
               .Select(x=> x.Permission)
               .ToListAsync();

            var rolePermissions = await _context.DomainRoles
                .Include(x => x.Permissions)
                .ThenInclude(p => p.Permission)
                .Include(x => x.Users)
                .Where(r => r.Users.Any(ru => ru.UserId == userId))
                .SelectMany(r => r.Permissions)
                .Select(rp => rp.Permission)
                .ToListAsync();

            List<Permission> allPermissions = [.. userPermissions, .. rolePermissions];
            allPermissions = allPermissions.Distinct().ToList();
            return allPermissions
                .Select(p => mapper.Map<PermissionDto>(p))
                .ToList();

            //return await _context.Permissions
            //    .Where(p => p.Users.Any(up => up.UserId == userId))
            //    .ProjectTo<PermissionDto>(mapper.ConfigurationProvider)
            //    .ToListAsync();
        }

        public async Task GrantPermissionToUserAsync(Guid userId, Guid permissionId)
        {
            var user = await _context.Staffs.AnyAsync(s => s.Id == userId);
            if (!user)
            {
                throw new AppValidationException(nameof(userId), $"User avec id {userId} introuvable.");
            }

            var permission = await _context.Permissions.AnyAsync(p => p.Id == permissionId);
            if (!permission)
            {
                throw new AppValidationException(nameof(permissionId), $"Permission avec id {permissionId} introuvable.");
            }

            var userPermission = await _context.UserPermissions
                .FirstOrDefaultAsync(up => up.UserId == userId && up.PermissionId == permissionId);
            if (userPermission is not null)
            {
                userPermission.IsGranted = true;
                _context.UserPermissions.Update(userPermission);
                await _context.SaveChangesAsync();
                await AuditAsync("USER_PERMISSION_GRANTED", $"Granted permission to userId {userId}", null, targetUserId: userId, permissionId: permissionId);
                return;
            }

            userPermission = new UserPermission()
            {
                UserId = userId,
                PermissionId = permissionId,
                IsGranted = true
            };
            await _context.UserPermissions.AddAsync(userPermission);
            await _context.SaveChangesAsync();
            await AuditAsync("USER_PERMISSION_GRANTED", $"Granted permission to userId {userId}", null, targetUserId: userId, permissionId: permissionId);
        }

        public async Task RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId)
        {
            var roleExist = await _context.DomainRoles.AnyAsync(r => r.Id == roleId);
            if (!roleExist)
            {
                throw new AppValidationException(nameof(roleId), $"Role avec id {roleId} introuvable.");
            }

            var permissionExist = await _context.Permissions.AnyAsync(p => p.Id == permissionId);
            if (!permissionExist)
            {
                throw new AppValidationException(nameof(permissionId), $"Permission avec id {permissionId} introuvable.");
            }

            var rolePermission = await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);
            if (rolePermission == null)
            {
                throw new AppValidationException(nameof(permissionId), $"Le rôle avec l'id {roleId} n'a pas la permission avec l'id {permissionId}.");
            }

            _context.RolePermissions.Remove(rolePermission);
            await _context.SaveChangesAsync();

            await AuditAsync("ROLE_PERMISSION_REMOVED", $"Removed permission from roleId {roleId}", null, roleId);
        }

        public async Task RevokePermissionFromUserAsync(Guid userId, Guid permissionId)
        {
            var user = await _context.Staffs.AnyAsync(s => s.Id == userId);
            if (!user)
            {
                throw new AppValidationException(nameof(userId), $"User avec id {userId} introuvable.");
            }

            var permission = await _context.Permissions.AnyAsync(p => p.Id == permissionId);
            if (!permission)
            {
                throw new AppValidationException(nameof(permissionId), $"Permission avec id {permissionId} introuvable.");
            }

            var userPermission = await _context.UserPermissions
                .FirstOrDefaultAsync(up => up.UserId == userId && up.PermissionId == permissionId);
            if (userPermission is not null)
            {
                userPermission.IsGranted = false;
                _context.UserPermissions.Update(userPermission);
                await _context.SaveChangesAsync();
                await AuditAsync("USER_PERMISSION_REVOKED", $"Revoked permission from userId {userId}", null, targetUserId: userId, permissionId: permissionId);
                return;
            }
            userPermission = new UserPermission()
            {
                UserId = userId,
                PermissionId = permissionId,
                IsGranted = false
            };
            await _context.UserPermissions.AddAsync(userPermission);
            await _context.SaveChangesAsync();
            await AuditAsync("USER_PERMISSION_REVOKED", $"Revoked permission from userId {userId}", null, targetUserId: userId, permissionId: permissionId);

        }

        public async Task SyncPermissionsFromConfigAsync()
        {
            var configPermissions = PermissionConfig.GetAllPermissions();
            var existingPermissions = await _context.Permissions.ToListAsync();

            foreach (var configPerm in configPermissions)
            {
                var existing = existingPermissions.FirstOrDefault(p => p.Code == configPerm.Code);

                if (existing == null)
                {
                    // Créer la nouvelle permission
                    var newPermission = new Permission()
                    {
                        Resource = configPerm.Resource.ToString().ToLowerInvariant(),
                        Action = configPerm.Action.ToLowerInvariant(),
                        //Code = configPerm.Code,
                        DisplayName = PermissionConfig.GetPermissionLabel(configPerm),
                        Description = $"Permet de {PermissionConfig.GetPermissionDescription(configPerm).ToLower()}",
                        Category = PermissionConfig.IsStandardAction(configPerm.Action) ? "standard" : "specific",
                        IsDangerous = PermissionConfig.IsDangerousAction(configPerm.Action),
                    };

                    _context.Permissions.Add(newPermission);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<PermissionDto> UpdatePermissionAsync(Guid id, UpdatePermissionDto dto)
        {
            var permission = await _context.Permissions.FirstOrDefaultAsync(p => p.Id == id);
            if (permission == null)
            {
                throw new AppValidationException(nameof(id), $"Permission avec l'id {id} introuvable.");
            }

            permission.DisplayName = dto.DisplayName;
            permission.Description = dto.Description;
            permission.Category = dto.Category;
            permission.IsDangerous = dto.IsDangerous;
            _context.Permissions.Update(permission);
            await _context.SaveChangesAsync();
            await AuditAsync("PERMISSION_UPDATED", $"Updated permission {permission.Code}", permission.Code, permissionId: permission.Id);
            return mapper.Map<PermissionDto>(permission);
        }

        public async Task<RoleDto> UpdateRoleAsync(Guid id, UpdateRoleDto dto)
        {
            var role = await _context.DomainRoles.FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                throw new AppValidationException(nameof(id), $"Role avec l'id {id} introuvable.");
            }
            if (role.IsSystem && role.Name != dto.Name)
            {
                throw new AppValidationException(nameof(dto.Name), $"Le nom du rôle système ne peut pas être modifié.");
            }

            role.DisplayName = dto.DisplayName;
            role.Description = dto.Description;
            role.Priority = dto.Priority;
            _context.DomainRoles.Update(role);
            await _context.SaveChangesAsync();
            await AuditAsync("ROLE_UPDATED", $"Updated role {role.Name}", null, targetRoleId: role.Id);
            return mapper.Map<RoleDto>(role);
        }

        public async Task<bool> UserHasPermissionAsync(Guid userId, string resource, string action)
        {
            var permissions = await GetEffectiveUserPermissionsAsync(userId);
            var code = $"{resource.ToLowerInvariant()}:{action.ToLowerInvariant()}";

            return permissions.Contains(code) ||
                   permissions.Contains($"{resource.ToLowerInvariant()}:*") ||
                   permissions.Contains($"*:{action.ToLowerInvariant()}") ||
                   permissions.Contains("*:*");
        }

        private Task AuditAsync(string action, string details, string permissionCode, Guid? targetUserId = null, Guid? targetRoleId = null, Guid? permissionId = null)
        {
            var audit = new PermissionAudit
            {
                UserId = _authenticatedUser.UserId,
                TargetUserId = targetUserId,
                TargetRoleId = targetRoleId,
                Details = details,
                PermissionCode = permissionCode,
                Action = action,
                Timestamp = DateTime.UtcNow
            };
            _context.PermissionAudits.AddAsync(audit);
            return _context.SaveChangesAsync();
        }

        public async Task<UserPermissionDto> GetPermissionsForCurrentUserAsync()
        {
            var permissions = await GetUserPermissionsAsync(_authenticatedUser.UserId);

            var permissionByResource = permissions.GroupBy(x => x.Resource).ToDictionary(g => g.Key, g => g.Select(p => p.Action).ToArray());
            var permissionsAsArray = permissions.Select(p => p.Code).ToList();
            var userPermissions = new UserPermissionDto()
            {
                Permissions = permissionsAsArray,
                PermissionsByResource = permissionByResource
            };

            return userPermissions;
        }
    }
}
