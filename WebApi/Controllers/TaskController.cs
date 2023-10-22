using Application.Feature.Tasks.Create;
using Application.Feature.Tasks.Delete;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "ProjectManager,Director,Employee")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskCommand command)
        {
            var currentUser = HttpContext.User;

            var userId = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userRole = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            var isDirector = userRole == "Director";

            if (!isDirector && userId != command.EmployeeId.ToString())
            {
                return BadRequest("employees can create tasks only for themselves");
            }

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Created("/api/Task", result.Value.Id);

            return BadRequest(result.Reasons);
        }

        [Authorize(Roles = "ProjectManager,Director,Employee")]
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteTaskByIdsCommand command)
        {
            var currentUser = HttpContext.User;

            var userId = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var userRole = currentUser.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

            var isDirector = userRole == "Director";

            if (!isDirector && userId != command.EmployeeId.ToString())
            {
                return BadRequest("employees can delete tasks only for themselves");
            }

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
                return Ok(result.Value);

            return BadRequest(result.Reasons);
        }
    }
}
