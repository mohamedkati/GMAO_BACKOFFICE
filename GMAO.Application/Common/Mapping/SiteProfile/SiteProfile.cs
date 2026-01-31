using AutoMapper;
using GMAO.Application.Features.sites.Commands.assets.CreateAsset;
using GMAO.Application.Features.sites.Commands.CreateSite;
using GMAO.Application.Features.sites.Commands.Units.CreateUnit;
using GMAO.Application.Features.sites.Commands.Units.UpdateUnit;
using GMAO.Application.Features.sites.Commands.UpdateSite;
using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.SharedBusiness.Dtos.Staff;
using GMAO.Domain.Entities;
using GMAO.Domain.Entities.siteAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMAO.Application.Common.Mapping.SiteProfile
{
    public class SiteProfile : Profile
    {
        public SiteProfile()
        {
            CreateMap<Site, SiteListItemDto>();
            CreateMap<Site, DetailedSiteDto>();
            CreateMap<CreateSiteCommand, Site>();
            CreateMap<UpdateSiteCommand, Site>();
            CreateMap<SectorType, SectorTypeDto>();
            CreateMap<SiteClientType, SiteClientTypeDto>();
            CreateMap<SiteCategory, SiteCategoryDto>();
            CreateMap<TVA, TVATypeDto>();


            CreateMap<Occupant, OccupantUnitSite>();
            CreateMap<Unit, SiteUnitDto>()
                .ForMember(x => x.ActiveOccupant, y => y.MapFrom(t => t.Occupants.FirstOrDefault()));

            CreateMap<Asset, SiteEquipementDto>();
            CreateMap<CreateAssetCommand, Asset>();
            CreateMap<CreateUnitCommand, Unit>();
            CreateMap<UpdateUnitCommand, Unit>();
            CreateMap<SiteDocument, SiteDocumentDto>();
            CreateMap<Staff, SiteStaffDto>();
            CreateMap<Staff, StaffAsKeyValueDto>();

            CreateMap<SiteKeeper, SiteKeeperDto>();
            CreateMap<SiteContact, SiteContactDto>();
            CreateMap<ContactType, SiteContactTypeDto>();
        }
    }
}
