using System.Globalization;
using System.Security.Claims;
using System.Text;
using BibliotecaUteco.Client.Responses;
using BibliotecaUteco.Settings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace BibliotecaUteco.Identity;

public class JwtBuilder(IOptions<JwtSettings> jwtSettings)
{
    private JwtSettings _jwtSettings = jwtSettings.Value;

    public JwtResponse? GenerateToken(UserResponse user)
    {
        var securityKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _jwtSettings.Key ?? throw new NullReferenceException(nameof(_jwtSettings.Key))
            )
        );

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new(
                [
                    new Claim(JwtRegisteredClaimNames.Nickname, user.Username),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                    new Claim(JwtRegisteredClaimNames.Picture, user.ProfilePictureUrl),
                    new Claim(JwtRegisteredClaimNames.Name, user.FullName),
                    new Claim("role", user.RoleName),
                    new Claim("createdAt", user.CreatedAt.ToString(CultureInfo.InvariantCulture)),
                ]
            ),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials,
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
        };

        var handler = new JsonWebTokenHandler();

        var token = handler.CreateToken(tokenDescriptor);

        if (token is null)
        {
            return null;
        }

        return new()
        {
            ExpirationDate = DateTime.UtcNow.AddDays(7),
            Issuer = _jwtSettings.Issuer,
            Token = token,
        };
    }
}