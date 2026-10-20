using System.Security.Claims;
using System.Text.Json;

namespace Neox.Extensions.Roles.AspNetCore;

public static class ClaimsPrincipalExtensions {

    public static void AddRealmRoles(this ClaimsPrincipal? principal, string roleClaimType) {
        ArgumentException.ThrowIfNullOrWhiteSpace(roleClaimType);

        if (principal?.Identity is not ClaimsIdentity identity) {
            return;
        }

        var realmAccess = identity.FindFirst("realm_access")?.Value;

        if (string.IsNullOrWhiteSpace(realmAccess)) {
            return;
        }

        using var document = JsonDocument.Parse(realmAccess);

        if (!document.RootElement.TryGetProperty("roles", out var roles) || roles.ValueKind != JsonValueKind.Array) {
            return;
        }

        foreach (var role in roles.EnumerateArray()) {
            var name = role.GetString();

            if (string.IsNullOrEmpty(name)) {
                continue;
            }

            identity.AddClaim(new Claim(roleClaimType, name));
        }
    }
}
