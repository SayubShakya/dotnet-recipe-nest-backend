using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace RecipeNest.Util.Impl;

public class TokenUtil
{
    private static string secretKey = "Y7m@Zq3N#t4!XpF2v%RbKjL^wA6$EeUC";

    public static string GenerateToken(string email)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        DateTime now = DateTime.Now;
        DateTimeOffset dto = new DateTimeOffset(now);
        long unixTimestamp = dto.ToUnixTimeSeconds();
        var claims = new[]
        {
            new Claim("_email", email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, unixTimestamp.ToString())
        };

        var token = new JwtSecurityToken(
            "recipe_nest",
            "user",
            claims,
            expires: now.AddHours(1),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public static ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(secretKey);

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "recipe_nest",
            ValidAudience = "user",
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };

        return tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
    }
}