using ElderlyCare.Application.Common.Interfaces.Authentication;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ElderlyCare.Infrastructure.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    public string GenerateToken(Guid userId, string firstName, string lastName)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes("super-secret-key")), 
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

            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var securityToken = new JwtSecurityToken(
            issuer: "ElderlyCare",
            expires: DateTime.Now.AddDays(1),
            claims: claims,
            signingCredentials: signingCredentials
            );
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
