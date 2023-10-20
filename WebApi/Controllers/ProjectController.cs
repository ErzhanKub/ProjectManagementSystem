using Application.Projects;
using Application.Projects.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var request = new GetAllProjectRequest();
            var result = await _mediator.Send(request);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Reasons);
        }

        [HttpPost]
        public async Task<IActionResult> GetOne(GetOneProjectByIdRequest request)
        {
            var result = await _mediator.Send(request);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Reasons);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateProjectCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Created("/api/Project", result.Value.Id);
            return BadRequest(result.Reasons);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteProjectByIdCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Reasons);
        }

        [HttpPatch]
        public async Task<IActionResult> Update(UpdateProjectByIdCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Ok(result.Value);
            return BadRequest(result.Reasons);
        }
    }
}
