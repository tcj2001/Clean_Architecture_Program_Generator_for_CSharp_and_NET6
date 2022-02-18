//////////////////////////////////////////
// generated AddBasicAuthExtension.cs //
//////////////////////////////////////////
using WebAPI.Middlewares;

namespace WebAPI.Extensions
{
    public static class AddBasicAuthExtension
    {
        public static IApplicationBuilder UseBasicAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicAuthMiddleware>();
        }
    }
}
