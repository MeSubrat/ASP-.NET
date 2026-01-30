//Create a class for custom middleware
namespace WebApp.MiddleComponents
{
    public class MyCustomMiddleWare : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("My Custom Middleware  : Before Calling next\r\n");

            await next(context);

            await context.Response.WriteAsync("My Custom Middleware  : After Calling next\r\n");
        }
    }
}
