using GMAO.Application.SharedBusiness.Dtos;
using GMAO.Domain.Entities;

namespace GMAO.Application.Common.Interfaces.Repositories
{
    public interface IPropertyGroupRepository : IRepository<PropertyGroup>
    {
        Task<IReadOnlyList<TResult>> GetPropertyGroupContactsByIdAsync<TResult>(Guid id, CancellationToken cancellation = default!);
        Task<(IReadOnlyList<TResult>, int)> GetPropertyGroupsAsync<TResult>(string? searchString, int? type, int? status, int? legalForm, CancellationToken cancellation = default!);
        Task<(IReadOnlyList<TResult>, int)> GetPropertyGroupsAsync<TResult>(string? searchString, IEnumerable<int>? type, IEnumerable<int>? status, IEnumerable<int>? legalForm, CancellationToken cancellation = default!);
        Task UpdateGroupContactAsync(PropertyGroupContact contact);
        Task<Guid> CreateGroupContactAsync(PropertyGroupContact contact, CancellationToken cancellation = default!);
        Task<PropertyGroupContact?> GetPropertyGroupContactByIdAsync(Guid id, CancellationToken cancellation = default!);
        Task<bool> DeleteGroupContactAsync(Guid id, CancellationToken cancellationToken);
        Task<IReadOnlyList<PropertyGroupAsKeyValue>> GetPropertyGroupsAsKeyValueAsync(string search,CancellationToken cancellation = default!); 
    }
}
