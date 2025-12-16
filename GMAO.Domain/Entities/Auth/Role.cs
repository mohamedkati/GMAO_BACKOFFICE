using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities.Auth
{
    public class Role : BaseEntity<Guid>
    {
        public string Name { get;  set; } = default!;
        public string DisplayName { get; set; } // "Administrateur", "Manager", etc.
        public string Description { get; set; }
        public bool IsSystem { get; set; } // true pour les rôles par défaut (non supprimables)
        public int Priority { get; set; }
        public ICollection<RolePermission> Permissions { get; set; } = new List<RolePermission>();
        public ICollection<TenantUser> Users { get; set; } = new List<TenantUser>();
        //private readonly List<Permission> _permissions = new();
        //public IReadOnlyCollection<Permission> Permissions => _permissions.AsReadOnly();

        //private Role() { }
        //public Role(string name, string? description = null)
        //{
        //    Name = string.IsNullOrWhiteSpace(name) ? throw new ArgumentException("Role name required") : name.Trim();
        //    Description = description?.Trim() ?? string.Empty;
        //}

        //public void GrantPermission(Permission p)
        //{ if (_permissions.All(x => x.Id != p.Id)) _permissions.Add(p); }

        //public void RevokePermission(Guid permissionId)
        //{ var p = _permissions.FirstOrDefault(x => x.Id == permissionId); if (p != null) _permissions.Remove(p); }
    }
}
