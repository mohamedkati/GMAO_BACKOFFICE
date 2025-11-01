using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Auth.Queries.Login.DTOs
{
    public class LoggedUserDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public Guid TenantId { get; set; }
        public string FullName { get; set; }
        public bool AccountConfirmed { get; set; }
        public string Token { get; set; }
    }
}
