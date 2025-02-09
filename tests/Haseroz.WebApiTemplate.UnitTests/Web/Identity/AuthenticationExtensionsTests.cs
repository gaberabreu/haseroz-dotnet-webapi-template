using System.Reflection;
using System.Security.Claims;
using System.Text.Json;
using Haseroz.WebApiTemplate.Web.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;

namespace Haseroz.WebApiTemplate.UnitTests.Web.Identity;

public class AuthenticationExtensionsTests
{
    [Fact]
    public async Task ProcessRolesFromToken_AddsRolesToClaimsIdentity()
    {
        // Arrange
        var clientId = "test-client";
        var rolesJson = JsonSerializer.Serialize(new Dictionary<string, object>
        {
            [clientId] = new
            {
                roles = new List<string>() { "admin", "user" }
            }
        });

        var claims = new List<Claim>
        {
            new("resource_access", rolesJson)
        };

        var identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);
        var principal = new ClaimsPrincipal(identity);

        var authTicket = new AuthenticationTicket(principal, JwtBearerDefaults.AuthenticationScheme);

        var context = new TokenValidatedContext(
            new DefaultHttpContext(),
            new AuthenticationScheme(JwtBearerDefaults.AuthenticationScheme, null, typeof(JwtBearerHandler)),
            new JwtBearerOptions()
        )
        {
            Principal = principal
        };

        // Act
        await InvokeProcessRolesFromToken(context, clientId);

        // Assert
        var roleClaims = identity.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();
        Assert.Contains("admin", roleClaims);
        Assert.Contains("user", roleClaims);
    }

    private static async Task InvokeProcessRolesFromToken(TokenValidatedContext context, string clientId)
    {
        var method = typeof(AuthenticationExtensions).GetMethod("ProcessRolesFromToken", BindingFlags.NonPublic | BindingFlags.Static) 
            ?? throw new InvalidOperationException("Method ProcessRolesFromToken not found via reflection.");

        await (Task)method.Invoke(null, [context, clientId])!;
    }
}
