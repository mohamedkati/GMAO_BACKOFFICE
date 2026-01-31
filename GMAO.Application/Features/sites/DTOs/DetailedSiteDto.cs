using GMAO.Domain.Entities.siteAggregate;
using GMAO.Domain.Entities;
using GMAO.Domain.Enums;
using GMAO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMAO.Application.SharedBusiness.Dtos.customer;
using GMAO.Application.SharedBusiness.Dtos.payment_method;
using GMAO.Application.Features.Customers.DTOs;
using GMAO.Application.SharedBusiness.Dtos.Staff;

namespace GMAO.Application.Features.sites.DTOs
{
    public class DetailedSiteDto
    {
        public Guid Id { get; set; }
        public string Reference { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public SiteType Type { get; set; }
        public Guid CustomerId { get; set; }
        public SharedCustomerDto Customer { get; set; } = null!;
        public Address Address { get; set; } = null!;
        public Address BillingAddress { get; set; } = null!;
        public GeoCoordinates? Coordinates { get; set; }

        public int? BuildingYear { get; set; }
        public decimal? TotalArea { get; set; }
        public int? FloorsCount { get; set; }
        public int? UnitsCount { get; set; }
        public int? EquipementsCount { get; set; }

        public Guid? ClientContactId { get; set; }
        public SectorTypeDto SectorType { get; set; }
        public Guid SectorTypeId { get; set; }
        public Guid ClientTypeId { get; set; }

        public string Comment { get; set; }

        public TVATypeDto VAT { get; set; }
        public Guid VatId { get; set; }
        public SiteAccess SiteAccessInfo { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public string Siren { get; set; }
        public string Siret { get; set; }
        public string MainMailAddress { get; set; }
        public string InvoiceMailAddress { get; set; }
        public string CommentReport { get; set; }

        /// <summary>
        ///  TEAM MANAGER FOR SITE
        /// </summary>
        public Guid? OperationsManagerId { get; set; }
        public Guid? CommercialId { get; set; }
        public Guid? SectorManagerId { get; set; }
        public Guid? Technician1Id { get; set; }
        public Guid? Technician2Id { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? LastModifiedAt { get; set; }

        public StaffAsKeyValueDto Commercial { get; set; }
        public CustomerContactDto ClientContact { get; set; }
        public SiteClientTypeDto ClientType { get; set; }
        public SharedPaymentMethodDto PaymentMethod { get; set; }
    }

    public class SiteClientTypeDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public SiteCategoryDto SiteCategory { get; set; }
        public SiteClientTypeDto() { }
        public SiteClientTypeDto(Guid id, string code, SiteCategoryDto siteCategory)
        {
            Id = id;
            Code = code;
            SiteCategory = siteCategory;
        }
    }
    public class SectorTypeDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public SectorTypeDto()
        {
            
        }
        public SectorTypeDto(Guid id, string code, string description)
        {
            Id = id;
            Code = code;
            Description = description;
        }
    }

    public class SiteCategoryDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public SiteCategoryDto()
        {
            
        }
        public SiteCategoryDto(Guid id, string code, string description)
        {
            Id = id;
            Code = code;
            Description = description;
        }
    }

    public record TVATypeDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public float ValuRate { get; set; }
    }
}
