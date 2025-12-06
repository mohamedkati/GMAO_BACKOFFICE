using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Customers.Commands.UpdateCustomerContact;

public class UpdateCustomerContactCommand : IRequest<ResponseResult<bool>>
{
    public Guid Id { get; set; }
    public PersonType Type { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
    public string? Mobile { get; set; }
    public string? Position { get; set; }
    public bool IsPrimary { get; set; }
    public PreferredContactMethod PreferredContactMethod { get; set; }
}


