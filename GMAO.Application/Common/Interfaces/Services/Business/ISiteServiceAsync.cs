using GMAO.Application.Features.sites.Commands.assets.CreateAsset;
using GMAO.Application.Features.sites.Commands.assets.UpdateAsset;
using GMAO.Application.Features.sites.Commands.CreateSite;
using GMAO.Application.Features.sites.Commands.Occupants.CreateOccupant;
using GMAO.Application.Features.sites.Commands.Occupants.UpdateOccupant;
using GMAO.Application.Features.sites.Commands.Units.CreateUnit;
using GMAO.Application.Features.sites.Commands.Units.UpdateUnit;
using GMAO.Application.Features.sites.Commands.UpdateSite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Interfaces.Services.Business
{
    public interface ISiteServiceAsync
    {
        Task<Guid> CreateSiteAsync(CreateSiteCommand request, CancellationToken cancellationToken);
        Task<bool> UpdateSiteAsync(UpdateSiteCommand request, CancellationToken cancellationToken);

        Task<Guid> AddUnitAsync(CreateUnitCommand unitCommand, CancellationToken cancellationToken);
        Task<Guid> UpdateUnitAsync(UpdateUnitCommand unitCommand, CancellationToken cancellationToken);
        Task<Guid> AddAssetAsync(CreateAssetCommand request, CancellationToken cancellationToken);
        Task<Guid> UpdateAssetAsync(UpdateAssetCommand request, CancellationToken cancellationToken);
        Task<Guid> AddOccupantToUnitAsync(CreateOccupantCommand request, CancellationToken cancellationToken);
        Task<Guid> UpdateOccupantAsync(UpdateOccupantCommand request, CancellationToken cancellationToken);
        Task DeleteOccupantAsync(Guid siteId, Guid unitId, Guid occupantId, CancellationToken cancellationToken);
    }
}
