using CleanTemplate.Application.UseCases.Customer.Commands;
using CleanTemplate.Application.UseCases.Customer.Commands.UpdateCommand;
using CleanTemplate.Application.UseCases.Customer.Queries.GetAllQuery;
using CleanTemplate.Application.UseCases.Customer.Queries.GetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CleanTemplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> CustomerList([FromQuery] GetAllCustomerQuery query)
        {
            var result = await _mediator.Send(query);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("{CustomerId:int}")]
        public async Task<IActionResult> GetCustomerById(int CustomerId)
        {
            var response = await _mediator.Send(new GetCustomerByIdQuery() { CustomerId = CustomerId });
            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> CustomerRegister([FromBody] CreateCustomerCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }

        
        [HttpPut("Update")]
        public async Task<IActionResult> CustomerUpdate([FromBody] UpdateCustomerCommand command)
        {
            var response = await _mediator.Send(command);
            if (response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}
