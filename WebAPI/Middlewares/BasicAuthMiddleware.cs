//////////////////////////////////////////////
// generated BasicAuthMiddleware.cs //
//////////////////////////////////////////////
using Microsoft.Extensions.Options;
using System.Text;
using WebAPI.AppSetting;

namespace WebAPI.Middlewares
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly BasicAuthentication _basicAuthentication;
        public BasicAuthMiddleware(RequestDelegate next, IOptions<BasicAuthentication> basicAuthentication)
        {
            _next = next;
            _basicAuthentication = basicAuthentication.Value;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string authHeader = httpContext.Request.Headers["Authorization"];
            if (authHeader != null)
            {
                string auth = authHeader.Split(new char[] { ' ' })[1];
                Encoding encoding = Encoding.GetEncoding("UTF-8");
                var usernameAndPassword = encoding.GetString(Convert.FromBase64String(auth));
                string username = usernameAndPassword.Split(new char[] { ':' })[0];
                string password = usernameAndPassword.Split(new char[] { ':' })[1];
                if (_basicAuthentication.User == username && _basicAuthentication.Password == password)
                {
                    await _next(httpContext);
                }
                else
                {
                    httpContext.Response.StatusCode = 401;
                    return;
                }
            }
            else
            {
                httpContext.Response.StatusCode = 401;
                return;
            }
        }

    }
}
