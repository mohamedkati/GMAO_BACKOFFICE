using GMAO.Application.Features.sites.Commands.Validators;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Enums;
using GMAO.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.CreateSite
{
    public class CreateSiteCommand : IRequest<ResponseResult<Guid>>, ISiteCommand
    {
        public string Reference { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public SiteType Type { get; set; }
        public Guid CustomerId { get; set; }
        public Address Address { get; set; } = null!;
        public Address BillingAddress { get; set; } = null!;
        public GeoCoordinates? Coordinates { get; set; }
        public int? SurfaceArea { get; set; }
        public int? BuildingYear { get; set; }
        public decimal? TotalArea { get; set; }
        public int? FloorsCount { get; set; }
        public int? UnitsCount { get; set; }

        public Guid? ClientContactId { get; set; }
        public Guid SectorTypeId { get; set; }
        public Guid ClientTypeId { get; set; }

        public string Comment { get; set; }

        public Guid VatId { get; set; }
        public SiteAccess SiteAccessInfo { get; set; }
        public Guid? PaymentMethodId { get; set; }
        public string Siren { get; set; }
        public string Siret { get; set; }
        public string MainMailAddress { get; set; }
        public string InvoiceMailAddress { get; set; }
        public string CommentReport { get; set; }

        public Guid? OperationsManagerId { get; set; }
        public Guid? CommercialId { get; set; }
        public Guid? SectorManagerId { get; set; }
        public Guid? Technician1Id { get; set; }
        public Guid? Technician2Id { get; set; }
    }
}
