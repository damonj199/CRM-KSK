using CRM_KSK.Application.Interfaces;
using CRM_KSK.Core.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CRM_KSK.Infrastructure;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;
    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
    }
    public string GenerateToken(Admin admin)
    {
        Claim[] claims = [new("adminId", admin.Id.ToString())];

        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256);

        var toket = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpitesHours));

        var tokinValue = new JwtSecurityTokenHandler().WriteToken(toket);

        return tokinValue;
    }
}
