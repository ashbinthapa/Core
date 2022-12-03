using Core.Services;
using Microsoft.Extensions.Options;
using System.Runtime.Serialization;

namespace Core
{
    public class CustomMiddleware2
    {
        private RequestDelegate _next;
        private IResponseFormatter _formatter;

        public CustomMiddleware2(RequestDelegate next, IResponseFormatter formatter)
        {
            _next = next;
            _formatter = formatter;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/middleware2")
            {
                await _formatter.Format(context, "Custom Middleware");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
