using GMAO.Application.Features.Shared.Settings.SectorTypes.Queries;
using GMAO.Application.Features.Shared.Settings.SiteClientType.Queries.GetAllSiteClientTypes;
using GMAO.Application.Features.Shared.Settings.Vat.Queries.GetAllVats;

namespace GMAO.API.Controllers.v1
{
    public class SettingsController : AuthorizedController
    {
        public SettingsController()
        {
        }

        [HttpGet("sector-types")]
        public async Task<IActionResult> GetSectorTypes([FromQuery] string? search)
        {
            var query = new GetAllSectorTypesQuery
            {
                Search = search
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("site-client-types")]
        public async Task<IActionResult> GetSiteClientTypes([FromQuery] string? search)
        {
            var query = new GetAllSiteClientTypesQuery
            {
                Search = search
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("vat-rates")]
        public async Task<IActionResult> GetVatRates([FromQuery] string? search)
        {
            var query = new GetAllVatQuery
            {
                Search = search
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
