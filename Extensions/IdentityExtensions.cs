using System.Security.Claims;
using System.Security.Principal;

namespace TeamUp.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserCustomName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("CustomName");
            // Test for null to avoid issues during local testing
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
