using GMAO.Application.Features.sites.Commands.Units.CreateUnit;
using GMAO.Application.Helpers.Responses;
using GMAO.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Features.sites.Commands.Units.UpdateUnit
{
    public class UpdateUnitCommand : IRequest<ResponseResult<bool>>, IUnitCommand
    {
        public Guid Id { get; set; }
        public string Reference { get; set; } = string.Empty;
        public Guid SiteId { get; set; }
        public UnitType Type { get; set; }
        public UnitStatus Status { get; set; }
        public string? Floor { get; set; }
        public string? DoorNumber { get; set; }
        public decimal? SurfaceArea { get; set; }
        public int? Rooms { get; set; }
        public int OwnershipSharesCount { get; set; }
    }
}
