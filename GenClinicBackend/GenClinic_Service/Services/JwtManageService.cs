using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GenClinic_Common.Constants;
using GenClinic_Common.Enums;
using GenClinic_Common.Exceptions;
using GenClinic_Common.Utils;
using GenClinic_Entities.DataModels;
using GenClinic_Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GenClinic_Service.Services
{
    public class JwtManageService : IJwtManageService
    {
        #region Properties
        public IConfiguration _configuration;
        public IHttpContextAccessor _httpContext;
        #endregion Properties

        #region Constructor
        public JwtManageService(IConfiguration configuration, IHttpContextAccessor httpContext)
        {
            _configuration = configuration;
            _httpContext = httpContext;
        }
        #endregion Constructor

        #region Interface Method
        public string GenerateToken(User user)
        {
            return GenerateJwtToken(user);
        }

        public string GenerateRefreshToken(User user)
        {
            return GenerateJwtToken(user);
        }

        public string GenerateJwtToken(User user)
        {
            //set key and credential
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //add claim
            var claims = new[]
            {
                new Claim(SystemConstants.USER_ID_CLAIM,user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.Role.ToString()),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Name,user.FirstName+" "+user.LastName),
            };
            //make token
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(48),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public static string GenerateRefreshToken()
        {
            var randomNumber = new Byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal GetPrincipalFormToken(string token)
        {
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "");
            var tokenValidatorParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = false,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidatorParameters, out SecurityToken securityToken);

            JwtSecurityToken jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException(SystemConstants.INVALID_TOKEN);
            }
            return principal;
        }

        public LoggedUser GetLoggedUser()
        {
            string authToken = _httpContext?.HttpContext?.Request.Headers.Authorization.FirstOrDefault() ?? throw new CustomException(StatusCodes.Status401Unauthorized, SystemConstants.UNAUTHORIZE);

            ClaimsPrincipal? claims = GetPrincipalFormToken(authToken);

            return new LoggedUser
            {
                UserId = Convert.ToInt64(claims.FindFirstValue(SystemConstants.USER_ID_CLAIM) ?? string.Empty),
                Role = Enum.Parse<UserRole>(claims?.FindFirstValue(ClaimTypes.Role) ?? string.Empty),
                Name = claims?.FindFirstValue(ClaimTypes.Name) ?? string.Empty,
                Email = claims?.FindFirstValue(ClaimTypes.Email) ?? string.Empty,
            };
        }

        #endregion Interface Method
    }
}