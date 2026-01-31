using GMAO.Domain.Common;
using GMAO.Domain.Entities.Auth;
using GMAO.Domain.Entities.siteAggregate;
using GMAO.Domain.Events;
using GMAO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Domain.Entities
{
    /// <summary>
    ///   // id of this entity will be the same id as the ApplicationUser.Id
    /// </summary>
    public class Staff : BaseAuditableEntity
    {
        public string EmployeeNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Address Address { get; set; }
        //public string Phone { get; set; }
        public string CellPhone { get; set; }
        public StaffStatus Status { get; set; }
        public List<string> ServiceZones { get; set; }
        public List<Guid> ManagedCustomerIds { get; set; }
        public string Email { get; set; }
        //public Guid RoleId { get; set; } = default!;
        //public Role Role { get; set; } = default!;
        public bool IsPdaActive { get; set; }
        
        public ICollection<Customer> ClientCommercials { get; set; } = new List<Customer>();
        public ICollection<Site> SiteCommercials { get; set; }
        public ICollection<ServiceRequest> QuotesFor { get; set; }
        public ICollection<Site> OperationManagerForSites { get; set; }
        public ICollection<Site> SiteSectorManagers { get; set; }
        public ICollection<Site> Technician1ForSites { get; set; }
        public ICollection<Site> Technician2ForSites { get; set; }
        public ICollection<TenantUser> Tenants { get; set; }
        public ICollection<UserPermission> UserPermissions { get; set; } = new List<UserPermission>();


        #region Contructors
        public Staff()
        {
        }
        public Staff(Guid id, Guid tenantId, string firstName, string lastName, string email, string phoneNumber, string userName, Guid roleId, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CellPhone = phoneNumber;
            EmployeeNumber = userName;
            //RoleId = roleId;
            this.Id = id;
            this.TenantId = tenantId;

            // Raise Domain Event to send email with password to the new staff
            AddDomainEvent(new StaffCreatedEvent(id, tenantId, password, email));
        }
        public Staff(Guid id, Guid tenantId, string firstName, string lastName, string email, string phoneNumber, string userName, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            CellPhone = phoneNumber;
            EmployeeNumber = userName;
            //RoleId = roleId;
            this.Id = id;
            this.TenantId = tenantId;

            // Raise Domain Event to send email with password to the new staff
            AddDomainEvent(new StaffCreatedEvent(id, tenantId, password, email));
        }
        #endregion
    }
}
