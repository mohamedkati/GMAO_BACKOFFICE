using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.Permissions.DTOs
{
    public class UserPermissionDto
    {
        public List<string> Permissions { get; set; }
        public Dictionary<string, string[]> PermissionsByResource { get; set; }
    }
}
