using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Enums;
using GMAO.Domain.ValueObjects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.assets.UpdateAsset
{
    public class UpdateAssetCommand : IAssetCommand, IRequest<ResponseResult<bool>>
    {
        public Guid Id { get; set; }

        public string Reference { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public Guid CategoryId { get; set; }

        public Guid SiteId { get; set; }
        public Guid? UnitId { get; set; }
        public bool IsCommonAsset { get; set; }
        public AssetLocation? Location { get; set; }

        public string? Manufacturer { get; set; }
        public string? Model { get; set; }
        public string? SerialNumber { get; set; }
        public DateTime? InstallationDate { get; set; }

        public AssetStatus Status { get; set; }
        public CriticalityLevel CriticalityLevel { get; set; }
        public AssetHealthStatus HealthStatus { get; set; }

        public Guid? ParentAssetId { get; set; }
    }
}
