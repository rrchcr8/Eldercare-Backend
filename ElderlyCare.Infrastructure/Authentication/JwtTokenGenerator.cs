using ElderlyCare.Application.Common.Interfaces.Authentication;
using ElderlyCare.Application.Common.Interfaces.Services;
using ElderlyCare.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ElderlyCare.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _dateTimeProvider;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtSettings)
    {
        _dateTimeProvider = dateTimeProvider;
        _jwtSettings = jwtSettings.Value;
    }

    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)), 
            SecurityAlgorithms.HmacSha256
            );
        var claims = new[]
        {
   /*         
    *    https://datatracker.ietf.org/doc/html/rfc7519#section-4
    *    4.1.2.  "sub" (Subject) Claim
    *    The "sub" (subject) claim identifies the principal that is the
    *    subject of the JWT.  The claims in a JWT are normally statements
    *    about the subject.  The subject value MUST either be scoped to be
    *    locally unique in the context of the issuer or be globally unique.
    *    The processing of this claim is generally application specific.  The
    *   "sub" value is a case-sensitive string containing a StringOrURI
    *   value.  Use of this claim is OPTIONAL.
    */

            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
            audience: _jwtSettings.Audience,
            claims: claims,
            signingCredentials: signingCredentials
            );
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
