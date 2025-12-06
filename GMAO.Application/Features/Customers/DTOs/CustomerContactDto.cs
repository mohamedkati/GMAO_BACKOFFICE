using GMAO.Domain.Entities;
using GMAO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Customers.DTOs
{
    public class CustomerContactDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
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
}
