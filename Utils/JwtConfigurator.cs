using API.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Utils;

public class JwtConfigurator
{
    public static string GetToken(Usuario userInfo, IConfiguration config)
    {
        string SecretKey = config["Jwt:SecretKey"];
        if (SecretKey.Length < 32)
        {
            throw new ArgumentException("La clave secreta debe tener al menos 32 caracteres.");
        }

        string Issuer = config["Jwt:Issuer"];
        string Audience = config["Jwt:Audience"];

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
        new Claim(JwtRegisteredClaimNames.Sub, userInfo.NombreUsuario),
        new Claim("idUsuario", userInfo.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Sid, userInfo.RolUser),
        new Claim("rolUsuario", userInfo.RolUser.ToString())
    };

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static int GetTokenIdUsuario(ClaimsIdentity identity)
    {
        if (identity != null)
        {
            IEnumerable<Claim> claims = identity.Claims;
            foreach (var claim in claims)
            {
                if (claim.Type == "idUsuario")
                {
                    return int.Parse(claim.Value);
                }
            }
        }
        return 0;
    }
}