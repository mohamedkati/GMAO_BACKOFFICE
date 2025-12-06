using GMAO.Application.Features.property_group.Commands;
using GMAO.Application.Features.property_group.Commands.CreateContactGroup;
using GMAO.Application.Features.property_group.Commands.DeleteContactGroup;
using GMAO.Application.Features.property_group.Commands.Update;
using GMAO.Application.Features.property_group.Commands.UpdateContactGroup;
using GMAO.Application.Features.property_group.queries.DetailedPropertyGroup;
using GMAO.Application.Features.property_group.queries.GetPropertyGroupsSelectAsKeyValue;
using GMAO.Application.Features.property_group.queries.ListAllPropertyGroups;
using GMAO.Application.Features.property_group.queries.PropertyGroupContacts;
using GMAO.Application.Helpers.Responses;

namespace GMAO.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class PropertyGroupsController : AuthorizedController
    {
        public PropertyGroupsController()
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetGroups([FromQuery] ListAllPropertyGroupsQuery query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroupById([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new GetPropertyGroupByIdQuery() { Id = id }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] CreatePropertyGroupCommand query)
        {
            return Ok(await Mediator.Send(query));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGroup([FromBody] UpdateGroupPropertyCommand command, [FromRoute] Guid id)
        {
            if (id != command.Id)
                return BadRequest(ResponseResult<string>.FailResult("Id in the route is not the same Id in the body"));
            return Ok(await Mediator.Send(command));
        }

        [HttpGet("{id}/contacts")]
        public async Task<IActionResult> GetGroupContacts([FromRoute] Guid id)
        {
            return Ok(await Mediator.Send(new PropertyGroupContactsQuery() { PropertyGroupId = id }));
        }

        [HttpPost("{id}/contacts")]
        public async Task<IActionResult> AddGroupContact([FromRoute] Guid id, [FromBody] CreateContactGroupCommand command)
        {
            //if(id != command.PropertyGroupId)
            //    return BadRequest(ResponseResult<string>.FailResult("Id in the route is not the same Id in the body"));
            command.PropertyGroupId = id;//TODO send id directly form ui
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}/contacts/{contactId}")]
        public async Task<IActionResult> UpdateGroupContact([FromRoute] Guid id, [FromRoute] Guid contactId, [FromBody] UpdateContactGroupCommand command)
        {
            if (contactId != command.Id)
                return BadRequest(ResponseResult<string>.FailResult("Id in the route is not the same Id in the body"));
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}/contacts/{contactId}")]
        public async Task<IActionResult> DeleteGroupContact([FromRoute] Guid id, [FromRoute] Guid contactId)
        {
            return Ok(await Mediator.Send(new DeleteContactGroupCommand() { Id = contactId }));
        }


        [HttpGet("select-as-key-value")]
        public async Task<IActionResult> GetPropertyGroupsSelectAsKeyValue([FromQuery] GetPropertyGroupsSelectAsKeyValueQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
