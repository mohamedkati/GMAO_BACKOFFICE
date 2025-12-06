using GMAO.Application.Features.Customers.Commands.CreateBudget;
using GMAO.Application.Features.Customers.Commands.CreateCustomer;
using GMAO.Application.Features.Customers.Commands.CreateCustomerContact;
using GMAO.Application.Features.Customers.Commands.DeleteBudget;
using GMAO.Application.Features.Customers.Commands.DeleteCustomerContact;
using GMAO.Application.Features.Customers.Commands.UpdateBudget;
using GMAO.Application.Features.Customers.Commands.UpdateCustomer;
using GMAO.Application.Features.Customers.Commands.UpdateCustomerContact;
using GMAO.Application.Features.Customers.Queries.GetCustomerBudgetById;
using GMAO.Application.Features.Customers.Queries.GetCustomerBudgets;
using GMAO.Application.Features.Customers.Queries.GetCustomerById;
using GMAO.Application.Features.Customers.Queries.GetCustomerContactById;
using GMAO.Application.Features.Customers.Queries.GetCustomerContacts;
using GMAO.Application.Features.Customers.Queries.GetCustomers;

namespace GMAO.API.Controllers.v1
{
    [Route("api/v1/[controller]")]
    public class CustomersController : AuthorizedController
    {
        public CustomersController()
        {

        }

        [HttpGet("{customerId}/contacts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCustomerContacts([FromRoute] Guid customerId)
        {
            var query = new GetCustomerContactsQuery { CustomerId = customerId };
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("{customerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCustomerById([FromRoute] Guid customerId)
        {
            var query = new GetCustomerByIdQuery { Id = customerId };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAllCustomers([FromQuery] GetCustomersQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command) => Ok(await Mediator.Send(command));


        //[HttpDelete("{customerId}")]
        //public async Task<IActionResult> DeleteCustomer([FromRoute] Guid customerId)
        //{
        //    return Ok(await Mediator.Send(new DeleteCustomerCommand { Id = customerId }));
        //}

        [HttpPost("{customerId}/contacts")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomerContact([FromRoute] Guid customerId, [FromBody] CreateCustomerContactCommand command)
        {
            command.CustomerId = customerId;
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCustomer([FromRoute] Guid id, [FromBody] UpdateCustomerCommand command)
        {
            command.Id = id;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpPut("{customerId}/contacts/{contactId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCustomerContact([FromRoute] Guid customerId, [FromRoute] Guid contactId, [FromBody] UpdateCustomerContactCommand command)
        {
            command.Id = contactId;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{customerId}/contacts/{contactId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteCustomerContact([FromRoute] Guid customerId, [FromRoute] Guid contactId) => Ok(await Mediator.Send(new DeleteCustomerContactCommand { CustomerId = customerId, Id = contactId }));


        [HttpGet("{customerId}/contacts/{contactId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCustomerContact([FromRoute] Guid customerId, [FromRoute] Guid contactId) => Ok(await Mediator.Send(new GetCustomerContactByIdQuery { CustomerId = customerId, ContactId = contactId }));

        [HttpPost("{customerId}/budgets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateBudgetForCustomer([FromRoute] Guid customerId, [FromBody] CreateBudgetCommand command)
        {
            command.CustomerId = customerId;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{customerId}/budgets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCustomerBudgets([FromRoute] Guid customerId)
        {
            var query = new GetCustomerBudgetsQuery { CustomerId = customerId };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("{customerId}/budgets/{budgetId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCustomerBudget([FromRoute] Guid customerId, [FromRoute] Guid budgetId, [FromBody] UpdateBudgetCommand command)
        {
            command.Id = budgetId;
            var result = await Mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{customerId}/budgets/{budgetId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteCustomerBudget([FromRoute] Guid customerId, [FromRoute] Guid budgetId)
        {
            return Ok(await Mediator.Send(new DeleteBudgetCommand() { CustomerId = customerId, Id = budgetId }));
        }

        [HttpGet("{customerId}/budgets/{budgetId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCustomerBudget([FromRoute] Guid customerId, [FromRoute] Guid budgetId)
        {
            return Ok(await Mediator.Send(new GetCustomerBudgetByIdQuery() { CustomerId = customerId, BudgetId = budgetId }));
        }

    }
}
