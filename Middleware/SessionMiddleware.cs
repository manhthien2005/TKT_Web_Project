using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Tiki_Web_Project.Middleware
{
    public class SessionMiddleware
    {
        private readonly RequestDelegate _next;

        public SessionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Lấy thông tin từ claims đã được lưu trong authentication cookie
            if (context.User.Identity.IsAuthenticated)
            {
                context.Items["Username"] = context.User.Identity.Name;
                context.Items["Email"] = context.User.FindFirst("Email")?.Value;
                context.Items["Name"] = context.User.FindFirst("Name")?.Value;
                context.Items["Phone"] = context.User.FindFirst("Phone")?.Value;
            }

            await _next(context);
        }
    }
}
