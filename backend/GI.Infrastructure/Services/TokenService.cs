using GI.Application.Common.Interfaces;
using GI.Core.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace GI.Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private IConfigurationSection jwtSection { get; set; }
        public TokenService(IConfiguration configuration) { 
            _configuration = configuration;
            jwtSection = _configuration.GetSection(JWT.JwtSetting);
        }

        public string GenerateAccessToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection[JWT.SecretKey]!));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(UserClaims.BusinessName, user.BusinessName)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSection[JWT.Issuer],
                audience: jwtSection[JWT.Audience],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(int.Parse(jwtSection[JWT.AccessTokenExpiry]!)),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public RefreshToken GenerateRefreshToken(User user)
        {
            var token = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)).Replace('+','-').Replace('/','c').Replace('=','o'),
                ExpiresAt = DateTime.UtcNow.AddDays(int.Parse(jwtSection[JWT.RefreshTokenExpiry]!)),
                CreatedAt = DateTime.UtcNow,
                IsRevoked = false,
                User = user
            };

            return token;
        }
    }
}
