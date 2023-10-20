using Application.Employees;
using Application.Employees.Get;
using Application.Employees.InteractionWithProject;
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
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Reasons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var result = await _mediator.Send(new GetOneEmployeeRequest { Id = id });
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Reasons);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(DeleteEmployeeByIdCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Reasons);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> Update(UpdateEmployeeProfileByIdCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Reasons);
        }

        [HttpPatch("addToProject")]
        public async Task<IActionResult> AddToProject(AddEmployeeToProjectCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok("Success");
            return BadRequest(result.Reasons);
        }

        [HttpPatch("removeFromProject")]
        public async Task<IActionResult> RemoveFromProject(RemoveEmployeeFromProjectCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok("Success");
            return BadRequest(result.Reasons);
        }
    }
}
