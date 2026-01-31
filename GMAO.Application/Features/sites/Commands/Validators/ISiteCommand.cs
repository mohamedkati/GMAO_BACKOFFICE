using GMAO.Domain.Enums;
using GMAO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.Validators
{
    public interface ISiteCommand
    {
        string Reference { get; }
        string Name { get; }
        SiteType Type { get; }
        Guid CustomerId { get; }
        Guid SectorTypeId { get; }
        Guid ClientTypeId { get; }
        Guid VatId { get; }
        Guid? PaymentMethodId { get; }
        Guid? ClientContactId { get; }
        Address Address { get; }
        Address BillingAddress { get; }
        GeoCoordinates Coordinates { get; }
        int? BuildingYear { get; }
        decimal? TotalArea { get; }
        int? FloorsCount { get; }
        int? UnitsCount { get; }
        string? Siren { get; }
        string? Siret { get; }
        string? MainMailAddress { get; }
        string? InvoiceMailAddress { get; }
        string? Comment { get; }
        string? CommentReport { get; }
        Guid? CommercialId { get; }
        Guid? OperationsManagerId { get; }
        Guid? SectorManagerId { get; }
        Guid? Technician1Id { get; }
        Guid? Technician2Id { get; }
        SiteAccess SiteAccessInfo { get; }

    }
}
