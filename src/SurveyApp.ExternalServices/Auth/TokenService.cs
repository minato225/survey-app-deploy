using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SurveyApp.Domain.Entities;
using SurveyApp.ExternalServices.Abstractions;
using SurveyApp.ExternalServices.Options;

namespace SurveyApp.ExternalServices.Auth;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _secretKey;
    private readonly JwtOptions _jwtOptions;
    private readonly ILogger<TokenService> _logger;


    public TokenService(IOptions<JwtOptions> options, ILogger<TokenService> logger)
    {
        _logger = logger;
        _jwtOptions = options.Value;
        _secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
    }

    public string GenerateAccessToken(AdminUser user)
    {
        var singingCredentials = new SigningCredentials(_secretKey, SecurityAlgorithms.HmacSha256);
        var claims = GetClaims(user);
        var tokenOptions = GenerateTokenOptions(singingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    private List<Claim> GetClaims(AdminUser user)
    {
        List<Claim> claims =
        [
            new(ClaimTypes.Name, user?.Login ?? string.Empty),
            new(ClaimTypes.NameIdentifier, user?.Id.ToString() ?? string.Empty),
            new(ClaimTypes.Email, user?.Email ?? string.Empty)
        ];

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        return new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtOptions.Expires),
            signingCredentials: signingCredentials
        );
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        var refreshToken = Convert.ToBase64String(randomNumber);
        return refreshToken;
    }
}