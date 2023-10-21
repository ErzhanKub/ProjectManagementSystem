using Application.Feature.Employees.Delete;
using Application.Feature.Employees.Get.GetAll;
using Application.Feature.Employees.Get.GetOne;
using Application.Feature.Employees.InteractionWithProject;
using Application.Feature.Employees.Update;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "ProjectManager,Director")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var request = new GetAllEmployeeRequest();
            var result = await _mediator.Send(request);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Reasons);
        }

        [Authorize(Roles = "ProjectManager,Director")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var result = await _mediator.Send(new GetOneEmployeeRequest { Id = id });
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Reasons);
        }

        [HttpDelete()]
        public async Task<IActionResult> Delete(DeleteEmployeeByIdCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Reasons);
        }

        [HttpPatch("update")]
        public async Task<IActionResult> Update(UpdateEmployeeByIdCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Reasons);
        }


        [Authorize(Roles = "ProjectManager,Director")]
        [HttpPatch("addToProject")]
        public async Task<IActionResult> AddToProject(AddEmployeeToProjectCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok("Added");
            return BadRequest(result.Reasons);
        }

        [Authorize(Roles = "ProjectManager,Director")]
        [HttpPatch("removeFromProject")]
        public async Task<IActionResult> RemoveFromProject(RemoveEmployeeFromProjectCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok("Deleted");
            return BadRequest(result.Reasons);
        }

        [HttpPatch("appoint")]
        public async Task<IActionResult> Appoint(AppointEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok("Appointed");
            return BadRequest(result.Reasons);
        }
    }
}
