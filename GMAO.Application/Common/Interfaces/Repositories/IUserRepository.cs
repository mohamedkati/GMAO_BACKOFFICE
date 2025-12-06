using GMAO.Application.SharedBusiness.Dtos.Staff;
using GMAO.Domain.Entities;
using GMAO.Domain.Entities.Auth;

namespace GMAO.Application.Common.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<Staff>
    {
        Task<IReadOnlyList<StaffAsKeyValueDto>> GetUsersAsKeyValue(string role, string? search, CancellationToken cancelToken = default!);
    }
}
