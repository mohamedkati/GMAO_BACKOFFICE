using GMAO.Application.SharedBusiness.Dtos.customer;
using GMAO.Application.SharedBusiness.Dtos.Staff;
using GMAO.Domain.Enums;
using GMAO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GMAO.Domain.Authorization.ResourceSpecificActions;

namespace GMAO.Application.Features.sites.DTOs
{
    public class SiteListItemDto
    {
        public string Id { get; set; }
        public string Reference { get; set; }
        public string Name { get; set; }
        public SiteType Type { get; set; }
        public string CustomerName { get; set; }
        public string City { get; set; }
        public int UnitsCount { get; set; }
        public Address Address { get; set; }
        public int? BuildingYear { get; set; }
        public SharedCustomerDto? Customer { get; set; }
        public StaffAsKeyValueDto? Commercial { get; set; }
        public GeoCoordinates? Coordinates { get; set; }
        public int? SurfaceArea { get; set; }
        public string? CommercialName { get; set; }
        public DateTime CreatedAt { get; set; }

    }
}
