using Application.Feature.Employees.Create;
using Application.Feature.Employees.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IMediator mediator, ILogger<AuthController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest query)
        {
            var token = await _mediator.Send(query);
            if (token.IsSuccess)
                return Ok(token.Value);
            return BadRequest(token.Reasons);
        }
        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Created($"/api/Employee/{result.Value}", result.Value);
            return BadRequest(result.Reasons);
        }
    }
}