using GMAO.Domain.Authorization;
using GMAO.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities.Auth
{
    /// <summary>
    /// Permission représentant une action spécifique sur une ressource. La  permission pourrait être structurée comme "resource:action" (ex: "customers:view").
    /// La permission peut être associée à plusieurs rôles et utilisateurs.
    /// j'ai liée la permission aux rôles et aux utilisateurs via des entités de jonction RolePermission et UserPermission pour gérer les relations plusieurs-à-plusieurs.
    /// Pourquoi j'ai ajouté la ralation directe aux utilisateurs ? Pour permettre des permissions spécifiques à un utilisateur en dehors de son rôle.
    /// </summary>
    public class Permission : BaseEntity<Guid>
    {
        public string Resource { get; set; } // "customers", "workorders", etc.
        public string Action { get; set; } // "view", "create", "cancel", etc.
        public string Code => $"{Resource.ToString().ToLowerInvariant()}:{Action.ToLowerInvariant()}";
        public string DisplayName { get; set; } // "Voir les clients"
        public string Description { get; set; }
        public string Category { get; set; } // "standard" ou "specific"
        public bool IsDangerous { get; set; } // Actions sensibles (delete, cancel, etc.)
        public Permission(string resource, string action)
        {
            Resource = resource;
            Action = action.ToLowerInvariant();
        }
        public static Permission Parse(string code)
        {
            var parts = code.Split(':');
            if (parts.Length != 2)
                throw new ArgumentException($"Invalid permission code: {code}");

            return new Permission(parts[0], parts[1]);
        }
        public Permission()
        {
            
        }
        public ICollection<RolePermission> Roles { get; set; } = new List<RolePermission>();
        public ICollection<UserPermission> Users { get; set; } = new List<UserPermission>();
        //private Permission() { }
        //public Permission(string code, string description = "")
        //{
        //    if (string.IsNullOrWhiteSpace(code)) throw new ArgumentException("Permission code required");
        //    Code = code.Trim();
        //    Description = description?.Trim() ?? string.Empty;
        //}
    }
}
