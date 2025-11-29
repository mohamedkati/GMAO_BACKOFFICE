using GMAO.Application.Common.Interfaces;
using GMAO.Application.Common.Interfaces.Services;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.property_group.queries.PropertyGroupContacts
{
    public class PropertyGroupContactsQuery : IRequest<ResponseResult<IReadOnlyList<PropertyGroupContactsDto>>>
    {
        public Guid PropertyGroupId { get; set; }
    }
}
