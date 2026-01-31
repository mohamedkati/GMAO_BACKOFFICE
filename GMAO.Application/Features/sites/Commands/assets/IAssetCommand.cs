using GMAO.Domain.Enums;
using GMAO.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.assets
{
    public interface IAssetCommand
    {
        string Reference { get; }
        string Name { get; }

        // Catégorie
        Guid CategoryId { get; }

        // Localisation
        Guid SiteId { get; }
        Guid? UnitId { get; }
        bool IsCommonAsset { get; }
        AssetLocation? Location { get; }

        // Détails techniques
        string? Manufacturer { get; }
        string? Model { get; }
        string? SerialNumber { get; }
        DateTime? InstallationDate { get; }

        // Statut
        AssetStatus Status { get; }
        CriticalityLevel CriticalityLevel { get; }
        AssetHealthStatus HealthStatus { get; }

        // Hiérarchie
        Guid? ParentAssetId { get; }
    }
}
