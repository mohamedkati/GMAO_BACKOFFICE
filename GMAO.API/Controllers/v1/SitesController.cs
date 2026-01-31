using GMAO.Application.Features.sites.Commands.assets.CreateAsset;
using GMAO.Application.Features.sites.Commands.assets.UpdateAsset;
using GMAO.Application.Features.sites.Commands.CreateSite;
using GMAO.Application.Features.sites.Commands.GetSiteContacts;
using GMAO.Application.Features.sites.Commands.Occupants.CreateOccupant;
using GMAO.Application.Features.sites.Commands.Occupants.UpdateOccupant;
using GMAO.Application.Features.sites.Commands.SiteContacts.CreateSiteContact;
using GMAO.Application.Features.sites.Commands.SiteContacts.UpdateSiteContact;
using GMAO.Application.Features.sites.Commands.SiteKeeper.CreateSiteKeeper;
using GMAO.Application.Features.sites.Commands.SiteKeeper.UpdateSiteKeeper;
using GMAO.Application.Features.sites.Commands.Units.CreateUnit;
using GMAO.Application.Features.sites.Commands.Units.UpdateUnit;
using GMAO.Application.Features.sites.Commands.UpdateSite;
using GMAO.Application.Features.sites.DTOs;
using GMAO.Application.Features.sites.Queries.GetDetailedSiteById;
using GMAO.Application.Features.sites.Queries.GetOccupantsBySiteId;
using GMAO.Application.Features.sites.Queries.GetSiteEquipements;
using GMAO.Application.Features.sites.Queries.GetSiteKeepers;
using GMAO.Application.Features.sites.Queries.GetSiteManagedTeam;
using GMAO.Application.Features.sites.Queries.GetSites;
using GMAO.Application.Features.sites.Queries.GetSiteUnits;
using GMAO.Application.Helpers.Responses;

namespace GMAO.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    public class SitesController : AuthorizedController
    {
        public SitesController()
        {

        }

        [HttpGet("all")]
        public async Task<IActionResult> GetSites([FromQuery] GetSitesQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSiteById([FromRoute] Guid id)
        {
            var query = new GetDetailedSiteByIdQuery { SiteId = id };
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}/team-members")]
        public async Task<IActionResult> GetSiteMembersById([FromRoute] Guid id)
        {
            var query = new GetSiteManagedTeamQuery { SiteId = id };
            return Ok(await Mediator.Send(query));
        }


        /// <summary>
        /// Crée un nouveau site
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateSiteCommand command, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(command, cancellationToken));
        }

        /// <summary>
        /// Met à jour un site
        /// </summary>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSiteCommand command, CancellationToken cancellationToken)
        {
            if (id != command.Id)
                return BadRequest(new { message = "L'ID de la route ne correspond pas à l'ID du body" });

            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        // ══════════════════════════════════════════════════════════════════════════
        // UNITS
        // ══════════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Récupère les lots d'un site
        /// </summary>
        [HttpGet("{id:guid}/units")]
        [ProducesResponseType(typeof(ResponseResult<IReadOnlyList<SiteUnitDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}/units")]
        public async Task<IActionResult> GetSiteUnitsById([FromRoute] Guid id)
        {
            var query = new GetSiteUnitsQuery { SiteId = id };
            return Ok(await Mediator.Send(query));
        }
        /// <summary>
        /// Récupère un lot par son ID
        /// </summary>
        //[HttpGet("{siteId:guid}/units/{unitId:guid}")]
        //[ProducesResponseType(typeof(UnitDetailDto), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetUnitById(Guid siteId, Guid unitId, CancellationToken cancellationToken)
        //{
        //    var result = await Mediator.Send(new GetUnitByIdQuery { SiteId = siteId, Id = unitId }, cancellationToken);
        //    return Ok(result);
        //}

        /// <summary>
        /// Ajoute un lot au site
        /// </summary>
        [HttpPost("{siteId:guid}/units")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUnit(Guid siteId, [FromBody] CreateUnitCommand command, CancellationToken cancellationToken)
        {
            command.SiteId = siteId;
            var id = await Mediator.Send(command, cancellationToken);
            return Ok(id);
        }

        /// <summary>
        /// Met à jour un lot
        /// </summary>
        [HttpPut("{siteId:guid}/units/{unitId:guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUnit(Guid siteId, Guid unitId, [FromBody] UpdateUnitCommand command, CancellationToken cancellationToken)
        {
            if (unitId != command.Id || siteId != command.SiteId)
                return BadRequest(new { message = "Les IDs de la route ne correspondent pas aux IDs du body" });

            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        // ══════════════════════════════════════════════════════════════════════════
        // ASSETS
        // ══════════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Récupère les équipements d'un site
        /// </summary>



        [HttpGet("{siteId:guid}/assets")]
        public async Task<IActionResult> GetSiteAssetsById([FromRoute] Guid siteId)
        {
            var query = new GetSiteEquipementsQuery { SiteId = siteId };
            return Ok(await Mediator.Send(query));
        }

        /// <summary>
        /// Ajoute un équipement au site
        /// </summary>
        [HttpPost("{siteId:guid}/assets")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAsset(Guid siteId, [FromBody] CreateAssetCommand command, CancellationToken cancellationToken)
        {
            command.SiteId = siteId;
            var id = await Mediator.Send(command, cancellationToken);
            return Ok(id);
        }

        /// <summary>
        /// Met à jour un équipement
        /// </summary>
        [HttpPut("{siteId:guid}/assets/{assetId:guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAsset(Guid siteId, Guid assetId, [FromBody] UpdateAssetCommand command, CancellationToken cancellationToken)
        {
            if (assetId != command.Id || siteId != command.SiteId)
                return BadRequest(new { message = "Les IDs de la route ne correspondent pas aux IDs du body" });

            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }



        // ══════════════════════════════════════════════════════════════════════════
        // OCCUPANTS
        // ══════════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Récupère les occupants d'un lot
        /// </summary>
        [HttpGet("{siteId:guid}/units/occupants")]
        [ProducesResponseType(typeof(List<OccupantUnitSite>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOccupants(Guid siteId, Guid unitId, [FromQuery] bool includeInactive = false, CancellationToken cancellationToken = default)
        {
            var result = await Mediator.Send(new GetOccupantsBySiteIdQuery
            {
                SiteId = siteId,
            }, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Ajoute un occupant à un lot
        /// </summary>
        [HttpPost("{siteId:guid}/units/{unitId:guid}/occupants")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateOccupant(Guid siteId, Guid unitId, [FromBody] CreateOccupantCommand command, CancellationToken cancellationToken)
        {
            command.SiteId = siteId;
            command.UnitId = unitId;
            var id = await Mediator.Send(command, cancellationToken);
            return Ok(id);
        }

        /// <summary>
        /// Met à jour un occupant
        /// </summary>
        [HttpPut("{siteId:guid}/units/{unitId:guid}/occupants/{occupantId:guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateOccupant(Guid siteId, Guid unitId, Guid occupantId, [FromBody] UpdateOccupantCommand command, CancellationToken cancellationToken)
        {
            if (occupantId != command.Id || siteId != command.SiteId || unitId != command.UnitId)
                return BadRequest(new { message = "Les IDs de la route ne correspondent pas aux IDs du body" });

            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// Supprime un occupant
        /// </summary>
        //[HttpDelete("{siteId:guid}/units/{unitId:guid}/occupants/{occupantId:guid}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> DeleteOccupant(Guid siteId, Guid unitId, Guid occupantId, CancellationToken cancellationToken)
        //{
        //    await Mediator.Send(new DeleteOccupantCommand
        //    {
        //        SiteId = siteId,
        //        UnitId = unitId,
        //        Id = occupantId
        //    }, cancellationToken);
        //    return NoContent();
        //}

        // ══════════════════════════════════════════════════════════════════════════
        // SITE KEEPERS
        // ══════════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Récupère les gardiens d'un site
        /// </summary>
        [HttpGet("{siteId:guid}/keepers")]
        [ProducesResponseType(typeof(List<SiteKeeperDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSiteKeepers(Guid siteId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetSiteKeepersQuery { SiteId = siteId }, cancellationToken);
            return Ok(result);
        }


        /// <summary>
        /// Ajoute un gardien au site
        /// </summary>
        [HttpPost("{siteId:guid}/keepers")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSiteKeeper(Guid siteId, [FromBody] CreateSiteKeeperCommand command, CancellationToken cancellationToken)
        {
            command.SiteId = siteId;
            var id = await Mediator.Send(command, cancellationToken);
            return Ok(id);
        }

        /// <summary>
        /// Met à jour un gardien
        /// </summary>
        [HttpPut("{siteId:guid}/keepers/{keeperId:guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSiteKeeper(Guid siteId, Guid keeperId, [FromBody] UpdateSiteKeeperCommand command, CancellationToken cancellationToken)
        {
            if (keeperId != command.Id || siteId != command.SiteId)
                return BadRequest(new { message = "Les IDs de la route ne correspondent pas aux IDs du body" });

            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        ///// <summary>
        ///// Supprime un gardien
        ///// </summary>
        //[HttpDelete("{siteId:guid}/keepers/{keeperId:guid}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> DeleteSiteKeeper(Guid siteId, Guid keeperId, CancellationToken cancellationToken)
        //{
        //    await Mediator.Send(new DeleteSiteKeeperCommand { SiteId = siteId, Id = keeperId }, cancellationToken);
        //    return NoContent();
        //}

        // ══════════════════════════════════════════════════════════════════════════
        // SITE CONTACTS
        // ══════════════════════════════════════════════════════════════════════════

        /// <summary>
        /// Récupère les contacts d'un site
        /// </summary>
        [HttpGet("{siteId:guid}/contacts")]
        [ProducesResponseType(typeof(List<SiteContactDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSiteContacts(Guid siteId, [FromQuery] Guid? contactTypeId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new GetSiteContactsQuery
            {
                SiteId = siteId,
            }, cancellationToken);
            return Ok(result);
        }


        /// <summary>
        /// Ajoute un contact au site
        /// </summary>
        [HttpPost("{siteId:guid}/contacts")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSiteContact(Guid siteId, [FromBody] CreateSiteContactCommand command, CancellationToken cancellationToken)
        {
            command.SiteId = siteId;
            var id = await Mediator.Send(command, cancellationToken);
            return Ok(id);
        }

        /// <summary>
        /// Met à jour un contact
        /// </summary>
        [HttpPut("{siteId:guid}/contacts/{contactId:guid}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateSiteContact(Guid siteId, Guid contactId, [FromBody] UpdateSiteContactCommand command, CancellationToken cancellationToken)
        {
            if (contactId != command.Id || siteId != command.SiteId)
                return BadRequest(new { message = "Les IDs de la route ne correspondent pas aux IDs du body" });

            var result = await Mediator.Send(command, cancellationToken);
            return Ok(result);
        }

        ///// <summary>
        ///// Supprime un contact
        ///// </summary>
        //[HttpDelete("{siteId:guid}/contacts/{contactId:guid}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> DeleteSiteContact(Guid siteId, Guid contactId, CancellationToken cancellationToken)
        //{
        //    await Mediator.Send(new DeleteSiteContactCommand { SiteId = siteId, Id = contactId }, cancellationToken);
        //    return NoContent();
        //}
    }
}
