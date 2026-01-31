using GMAO.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.Units
{
    public interface IUnitCommand
    {
        string Reference { get; }
        Guid SiteId { get; }
        UnitType Type { get; }
        UnitStatus Status { get; }
        string? Floor { get; }
        string? DoorNumber { get; }
        decimal? SurfaceArea { get; }
        int? Rooms { get; }
        int OwnershipSharesCount { get; }
    }
}
