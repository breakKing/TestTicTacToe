using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using FastEndpoints.Security;
using Identity.Core.Common.Identity.Entites;
using Identity.Core.Common.Jwt;
using Microsoft.Extensions.Options;

namespace Identity.Core.Features;

internal sealed class TokenService
{
    public const string SubClaimName = "sub";
    
    private readonly JwtConfiguration _jwtConfig;

    public TokenService(IOptions<JwtConfiguration> jwtOptions)
    {
        _jwtConfig = jwtOptions.Value;
    }
    
    public string GenerateToken(User user)
    {
        var jwtToken = JWTBearer.CreateToken(
            signingKey: _jwtConfig.TokenSigningKey,
            expireAt: DateTime.UtcNow.AddSeconds(_jwtConfig.TokenLifeTimeInSeconds),
            claims: new []{ new Claim(SubClaimName, user.Id.ToString()) });

        return jwtToken;
    }
}