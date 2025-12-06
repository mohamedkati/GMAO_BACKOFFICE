using GMAO.Application.Helpers.Responses;
using GMAO.Application.SharedBusiness.Dtos.Staff;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Staffs.Queries.GetStaffsAsKeyValue
{
    public class GetStaffsAsKeyValueQuery : IRequest<ResponseResult<IReadOnlyList<StaffAsKeyValueDto>>>
    {
        public string Role { get; set; }
        public string Search { get; set; }
    }
}
