using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BibliotecaUteco.Utilities;

public static class UserIdentityUtility
{
    /// <summary>
    /// Gets the User Id from the current http request
    /// </summary>
    /// <param name="user">Claims principal from the current HttpContext</param>
    /// <returns>The user Id from the HttpContext</returns>
    public static int GetUserIdFromClaims(ClaimsPrincipal user)
    {
        var subClaim = user.FindFirst(JwtRegisteredClaimNames.Sub);
        if (subClaim == null)
        {
            subClaim = user.FindFirst(ClaimTypes.NameIdentifier);
        }

        if (subClaim != null && int.TryParse(subClaim.Value, out var value))
        {
            return value;
        }

        throw new UnauthorizedAccessException();
    }
}
