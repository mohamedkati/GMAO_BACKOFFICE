using GMAO.Application.Common.Interfaces.Repositories;
using GMAO.Application.Helpers.Responses;
using GMAO.Application.SharedBusiness.Dtos.Staff;
using MediatR;

namespace GMAO.Application.Features.Staffs.Queries.GetStaffsAsKeyValue
{
    public class GetStaffsAsKeyValueQueryHandler : IRequestHandler<GetStaffsAsKeyValueQuery, ResponseResult<IReadOnlyList<StaffAsKeyValueDto>>>
    {
        private readonly IUserRepository _userRepository;

        public GetStaffsAsKeyValueQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ResponseResult<IReadOnlyList<StaffAsKeyValueDto>>> Handle(GetStaffsAsKeyValueQuery request, CancellationToken cancellationToken)
        {
            var commercials = await _userRepository.GetUsersAsKeyValue(request.Role, request.Search, cancellationToken);
            return ResponseResult<IReadOnlyList<StaffAsKeyValueDto>>.OkResult(commercials);
        }
    }
}
