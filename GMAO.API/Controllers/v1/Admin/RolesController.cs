using GMAO.Application.Common.Interfaces.Authentication;
using GMAO.Application.Features.Permissions.DTOs;
using GMAO.Application.Helpers.Responses;

namespace GMAO.API.Controllers.v1.Admin
{

    public class RolesController : AuthorizedController
    {
        private readonly IPermissionService _permissionService;
        private readonly IAuthenticatedUser _authenticatedUser;
        public RolesController(IPermissionService permissionService, IAuthenticatedUser authenticatedUser)
        {
            _permissionService = permissionService;
            _authenticatedUser = authenticatedUser;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseResult<List<RoleDto>>>> GetRolesWithPermissions()
        {
            var roles = await _permissionService.GetAllRolesAsync();
            return Ok(ResponseResult<List<RoleDto>>.OkResult(roles));
        }

        [HttpDelete("{roleId}")]
        public async Task<ActionResult<ResponseResult<string>>> DeleteRole(Guid roleId)
        {
            await _permissionService.DeleteRoleAsync(roleId);
            return Ok(ResponseResult<string>.OkResult("Role deleted successfully."));
        }

        [HttpDelete("{roleId}/permissions/{permissionId}")]
        public async Task<ActionResult<ResponseResult<string>>> RemovePermissionFromRole(Guid roleId, Guid permissionId)
        {
            await _permissionService.RemovePermissionFromRoleAsync(roleId, permissionId);
            return Ok(ResponseResult<string>.OkResult("Permission removed from role successfully."));
        }

        [HttpPost]
        public async Task<ActionResult<ResponseResult<RoleDto>>> CreateRole([FromBody] CreateRoleDto dto)
        {
            var role = await _permissionService.CreateDomainRoleAsync(dto);
            return Ok(ResponseResult<RoleDto>.OkResult(role, "Role created successfully."));
        }

        [HttpPut("{roleId}")]
        public async Task<ActionResult<ResponseResult<RoleDto>>> UpdateRole(Guid roleId, [FromBody] UpdateRoleDto dto)
        {
            var role = await _permissionService.UpdateRoleAsync(roleId, dto);
            return Ok(ResponseResult<RoleDto>.OkResult(role, "Role updated successfully."));
        }

        [HttpPost("{roleId}/permissions")]
        public async Task<ActionResult<ResponseResult<string>>> AssignPermissionsToRole(Guid roleId, [FromBody] AssignPermissionsDto dto)
        {
            await _permissionService.AssignPermissionsToRoleAsync(roleId, dto);
            return Ok(ResponseResult<string>.OkResult("Permissions assigned to role successfully."));
        }

        [HttpGet("{roleId}/permissions")]
        public async Task<ActionResult<ResponseResult<List<PermissionDto>>>> GetRolePermissions(Guid roleId)
        {
            var permissions = await _permissionService.GetRolePermissionsAsync(roleId);
            return Ok(ResponseResult<List<PermissionDto>>.OkResult(permissions));
        }

        [HttpGet("{roleId}")]
        public async Task<ActionResult<ResponseResult<RoleDto>>> GetRoleById(Guid roleId)
        {
            var role = await _permissionService.GetRoleByIdAsync(roleId);
            return Ok(ResponseResult<RoleDto>.OkResult(role));
        }

    }
}
