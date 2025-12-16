using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Application.Features.Permissions.DTOs;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities.Auth;
using Microsoft.AspNetCore.Authorization;

namespace GMAO.API.Controllers.v1.Admin
{
    [ApiController]
    [Authorize]
    public class PermissionsController : AuthorizedController
    {
        private readonly IPermissionService _permissionService;
        private readonly IAuthenticatedUser _authenticatedUser;

        public PermissionsController(IPermissionService permissionService, IAuthenticatedUser authenticatedUser)
        {
            _permissionService = permissionService;
            _authenticatedUser = authenticatedUser;
        }

        // ===== PERMISSIONS =====

        [HttpGet]
        [HttpGet("config")]
        public async Task<ActionResult<ResponseResult<List<PermissionDto>>>> GetAllPermissions()
        {
            var permissions = await _permissionService.GetAllPermissionsAsync();
            return Ok(ResponseResult<List<PermissionDto>>.OkResult(permissions));
        }

        [HttpPost("check")]
        public async Task<ActionResult<ResponseResult<bool>>> CheckPermission([FromBody] PermissionCheckerDto permission)
        {
            var haspermission = await _permissionService.UserHasPermissionAsync(_authenticatedUser.UserId, permission.Resource, permission.Action);
            return Ok(ResponseResult<bool>.OkResult(haspermission));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseResult<PermissionDto>>> GetPermission(Guid id)
        {
            var permission = await _permissionService.GetPermissionByIdAsync(id);
            return Ok(ResponseResult<PermissionDto>.OkResult(permission));
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ResponseResult<PermissionDto>>> CreatePermission([FromBody] CreatePermissionDto dto)
        {
            var permission = await _permissionService.CreatePermissionAsync(dto);
            return Ok(ResponseResult<PermissionDto>.OkResult(permission));
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseResult<PermissionDto>>> UpdatePermission(Guid id, [FromBody] UpdatePermissionDto dto)
        {
            var permission = await _permissionService.UpdatePermissionAsync(id, dto);
            return Ok(ResponseResult<PermissionDto>.OkResult(permission));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> DeletePermission(Guid id)
        {
            await _permissionService.DeletePermissionAsync(id);
            return Ok(ResponseResult<bool>.OkResult(true));
        }

        [HttpPost("sync")]
        //[Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> SyncPermissions()
        {
            await _permissionService.SyncPermissionsFromConfigAsync();
            return Ok(ResponseResult<string>.OkResult("Permissions synchronisés avec succès"));
        }

        [HttpGet("{userId}/permissions")]
        public async Task<ActionResult<ResponseResult<List<PermissionDto>>>> GetUserPermissions(Guid userId)
        {
            var permissions = await _permissionService.GetUserPermissionsAsync(userId);
            return Ok(ResponseResult<List<PermissionDto>>.OkResult(permissions));
        }

        [HttpGet("{userId}/effective")]
        public async Task<ActionResult<ResponseResult<List<string>>>> GetEffectivePermissions(Guid userId)
        {
            var permissions = await _permissionService.GetEffectiveUserPermissionsAsync(userId);
            return Ok(ResponseResult<List<string>>.OkResult(permissions));
        }

        [HttpPost("{permissionId}/grant")]
        [Authorize]
        public async Task<ActionResult> GrantPermission(Guid permissionId)
        {

            await _permissionService.GrantPermissionToUserAsync(_authenticatedUser.UserId, permissionId);
            return Ok(ResponseResult<string>.OkResult("Permission accordée avec succès"));
        }

        [HttpPost("{permissionId}/revoke")]
        [Authorize]
        public async Task<ActionResult> RevokePermission(Guid permissionId)
        {
            await _permissionService.RevokePermissionFromUserAsync(_authenticatedUser.UserId, permissionId);
            return Ok(ResponseResult<string>.OkResult("Permission Révoquée avec succès"));
        }

        [HttpGet("audits")]
        public async Task<ActionResult<ResponseResult<List<PermissionAuditDto>>>> GetAuditLogs(
           [FromQuery] PermissionAuditFilter filter)
        {
            var audits = await _permissionService.GetPermissionAuditAsync(filter);
            return Ok(ResponseResult<List<PermissionAuditDto>>.OkResult(audits));
        }

        [HttpGet("me")]
        public async Task<ActionResult<ResponseResult<UserPermissionDto>>> GetMyEffectivePermissions()
        {
            var permissions = await _permissionService.GetPermissionsForCurrentUserAsync();
            return Ok(ResponseResult<UserPermissionDto>.OkResult(permissions));
        }
    }
    public record PermissionCheckerDto(string Resource, string Action);
}