using LoginAndRegister.Service.Services.Interfaces;
using LoginAndRegister.Service.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace LoginAndRegister.Service.Services
{
    public class TokenGeneratorService: ITokenGeneratorService
    {
        private readonly JwtSettings _jwt;

        public TokenGeneratorService(IOptions<JwtSettings> jwtOptions)
        {
            _jwt = jwtOptions.Value;
        }

        public string GenerateJwtToken()
        {

            //User has provided valid login credentials, JWT token is created and returned from here
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var jwtToken = new JwtSecurityToken(issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}