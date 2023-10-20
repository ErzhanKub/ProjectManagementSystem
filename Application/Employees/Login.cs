using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Users.Commands
{
    public record LoginRequest : IRequest<Result<string>>
    {
        public required string Email { get; init; }
        public required string Password { get; init; }
    }

    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(l => l.Email).NotEmpty().EmailAddress();
            RuleFor(l => l.Password).NotEmpty().Length(1, 200);
        }
    }
    public class LoginHandler : IRequestHandler<LoginRequest, Result<string>>
    {
        private readonly IConfiguration _configuration;
        private readonly IEmployeeRepository _employeeRepository;

        public LoginHandler(IConfiguration configuration, IEmployeeRepository employeeRepository)
        {
            _configuration = configuration;
            _employeeRepository = employeeRepository;
        }

        public async Task<Result<string>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.CheckUserCredentialsAsync(request.Email, request.Password);
            if (employee is not null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email, employee.Email),
                    new Claim(ClaimTypes.Role, employee.Role.ToString())
                };
                var tokenString = GetTokenString(claims, DateTime.UtcNow.AddMinutes(30));
                return Result.Ok(tokenString);
            }
            return Result.Fail("Email or password is incorrect");
        }

        private string GetTokenString(List<Claim> claims, DateTime exp)
        {
            var key = _configuration["Jwt"] ?? throw new Exception();
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));


            var token = new JwtSecurityToken(
                claims: claims,
                expires: exp,
                signingCredentials: new SigningCredentials(
                    securityKey, SecurityAlgorithms.HmacSha256));

            var handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}