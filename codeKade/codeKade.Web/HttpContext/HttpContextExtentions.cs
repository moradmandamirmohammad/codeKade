using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using codeKade.Web.HttpContext;

namespace codeKade.Web.HttpContext
{
    public static class HttpContextExtentions
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static long GetUserID(this ClaimsPrincipal prencipal)
        {
            var userID = SiteCurrentContext.Current.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier)?.Value;

            if (!string.IsNullOrEmpty(userID))
            {
                return Convert.ToInt64(userID);
            }
            return default(long);
        }
        public static long GetUserID(this IPrincipal prencipal)
        {
            var claimPrencipal = (ClaimsPrincipal)prencipal;

            return claimPrencipal.GetUserID();
        }
        public static string GetUserName(this ClaimsPrincipal prencipal)
        {
            var userID = SiteCurrentContext.Current.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Name)?.Value;

            if (!string.IsNullOrEmpty(userID))
            {
                return userID;
            }
            return default(string);
        }
        public static string GetUserName(this IPrincipal prencipal)
        {
            var claimPrencipal = (ClaimsPrincipal)prencipal;

            return claimPrencipal.GetUserName();
        }
        public static string GetUserClaimByType(this ClaimsPrincipal principal, CustomClaimType type)
        {
            switch (type)
            {
                case CustomClaimType.Email:
                    return SiteCurrentContext.Current.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Email)?.Value;

                case CustomClaimType.Name:
                    return SiteCurrentContext.Current.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Name)?.Value;

                case CustomClaimType.NameIdentifier:
                    return SiteCurrentContext.Current.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier)?.Value;
            }

            return "";
        }
        public enum CustomClaimType
        {
            Name,
            NameIdentifier,
            Email
        }

    }
}
