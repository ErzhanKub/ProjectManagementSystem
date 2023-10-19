using Application.Employees;
using Application.Employees.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new GetAllEmployeeRequest();
            var result = await _mediator.Send(request);
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> GetOne(GetOneEmployeeRequest request)
        {
            var result = await _mediator.Send(request);
            return Ok(result.Value);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result.Value);
        }


    }
}
