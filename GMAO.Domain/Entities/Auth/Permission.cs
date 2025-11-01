using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities.Auth
{
    public class Permission : BaseEntity<Guid>
    {
        public string Code { get; private set; } = default!;
        public string Description { get; private set; } = string.Empty;
        private Permission() { }
        public Permission(string code, string description = "")
        {
            if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Permission code required");
            Code = code.Trim();
            Description = description?.Trim() ?? string.Empty;
        }
    }
}
