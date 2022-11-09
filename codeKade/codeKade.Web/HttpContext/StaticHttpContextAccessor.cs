using Microsoft.AspNetCore.Http;

namespace codeKade.Web.HttpContext
{
    public static class SiteCurrentContext
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static Microsoft.AspNetCore.Http.HttpContext Current => _httpContextAccessor.HttpContext;
    }
}
