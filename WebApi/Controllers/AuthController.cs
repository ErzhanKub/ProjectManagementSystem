﻿using Application.Employees;
using Application.Users.Commands;
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
                return Ok(token);
            return BadRequest(token.Reasons);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(CreateEmployeeCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
                return Created($"/api/Employee/{result.Value.Id}", result.Value.Id);
            return BadRequest(result.Reasons);
        }
    }
}