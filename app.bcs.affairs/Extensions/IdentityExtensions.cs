using System;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
//using UnifiedFGNBooks.V3.Infrastructure.Entities;
using Microsoft.AspNetCore.Html;

namespace UnifiedFGNBooks.V3.UI.Extensions
{
    public static class IdentityExtensions
    {
        [DebuggerStepThrough]
        private static bool HasRole(this ClaimsPrincipal principal, params string[] roles)
        {
            if (principal == null)
                return default;

            var claims = principal.FindAll(ClaimTypes.Role).Select(x => x.Value).ToSafeList();

            return claims?.Any() == true && claims.Intersect(roles ?? new string[] { }).Any();
        }

       

        [DebuggerStepThrough]
        public static HtmlString AsRaw(this string value) => new HtmlString(value);

        [DebuggerStepThrough]
        public static string ToPage(this string href) => System.IO.Path.GetFileNameWithoutExtension(href)?.ToLower();

        [DebuggerStepThrough]
        public static bool IsVoid(this string href) => href?.ToLower() == "javascript:void(0);";

        //[DebuggerStepThrough]
        //public static bool IsRelatedTo(this ListItem item, string pageName) => item?.Type == ItemType.Parent && item?.Href?.ToPage() == pageName?.ToLower();

    }
}
