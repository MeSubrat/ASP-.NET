using WebApp.MiddleComponents;
var builder = WebApplication.CreateBuilder(args);

//Register cutsom middleware
builder.Services.AddTransient<MyCustomMiddleWare>();
builder.Services.AddTransient<MyCustomExceptionHandler>();

var app = builder.Build();

app.UseMiddleware<MyCustomExceptionHandler>();

//Middleware #1
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #1 : Before Calling next\r\n");

    //We pass the context to next middleware. By default next() is an async method.
    await next(context);

    await context.Response.WriteAsync("Middleware #1 : After Calling next\r\n");
});

app.UseMiddleware<MyCustomMiddleWare>();

//app.Map("/employees", (appBuilder) =>
//{
//    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
//    {
//        await context.Response.WriteAsync("Middleware #4 : Before Calling next\r\n");

//        //We pass the context to next middleware. By default next() is an async method.
//        await next(context);

//        await context.Response.WriteAsync("Middleware #4 : After Calling next\r\n");
//    });
//    appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
//    {
//        await context.Response.WriteAsync("Middleware #5 : Before Calling next\r\n");

//        //We pass the context to next middleware. By default next() is an async method.
//        await next(context);

//        await context.Response.WriteAsync("Middleware #5 : After Calling next\r\n");
//    });
//});

//Here the mapWhen() method creates a separate branch of middlewares, after m-4&5, m-2&3 will never execute.
//app.MapWhen(
//    //Parameter-1 : It will return boolean value, if it returns true, then it will goes to middleware-4,5, otherwise it will never goes to this middleware. 
//    (context) => {
//        return context.Request.Path.StartsWithSegments("/employees") && context.Request.Query.ContainsKey("id");
//    },
//    //Parameter-2
//    (appBuilder) =>
//    {
//        appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
//        {
//            await context.Response.WriteAsync("Middleware #4 : Before Calling next\r\n");

//            //We pass the context to next middleware. By default next() is an async method.
//            await next(context);

//            await context.Response.WriteAsync("Middleware #4 : After Calling next\r\n");
//        });
//        appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
//        {
//            await context.Response.WriteAsync("Middleware #5 : Before Calling next\r\n");

//            //We pass the context to next middleware. By default next() is an async method.
//            await next(context);

//            await context.Response.WriteAsync("Middleware #5 : After Calling next\r\n");
//        });
//    }
//);

//Here it will create a branching from the m-5 to m-2 then m-3.
//app.UseWhen(
//    //Parameter-1 : It will return boolean value, if it returns true, then it will goes to middleware-4,5, otherwise it will never goes to this middleware. 
//    (context) => {
//        return context.Request.Path.StartsWithSegments("/employees") && context.Request.Query.ContainsKey("id");
//    },
//    //Parameter-2
//    (appBuilder) =>
//    {
//        appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
//        {
//            await context.Response.WriteAsync("Middleware #4 : Before Calling next\r\n");

//            //We pass the context to next middleware. By default next() is an async method.
//            await next(context);

//            await context.Response.WriteAsync("Middleware #4 : After Calling next\r\n");
//        });
//        appBuilder.Use(async (HttpContext context, RequestDelegate next) =>
//        {
//            await context.Response.WriteAsync("Middleware #5 : Before Calling next\r\n");

//            //We pass the context to next middleware. By default next() is an async method.
//            await next(context);

//            await context.Response.WriteAsync("Middleware #5 : After Calling next\r\n");
//        });
//    }
//);



//Middleware #2
app.Use(async (HttpContext context, RequestDelegate next) => // We can also omit the types of the context and next.
{
    throw new ApplicationException("Exception for testing.");
    await context.Response.WriteAsync("Middleware #2 : Before Calling next\r\n");

    //Suppose we want to short circuit the middleware just omit this line.
    await next(context);

    await context.Response.WriteAsync("Middleware #2 : After Calling next\r\n");
});

//app.run() always creates terminal middleware, the middlewares right after this will not be executed.
//app.Run(async (context) =>
//{
//    await context.Response.WriteAsync("Middleware-Terminal : Processed!");
//});

//Middleware #3
app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("Middleware #3 : Before Calling next\r\n");

    //We pass the context to next middleware. By default next() is an async method.
    await next(context);

    await context.Response.WriteAsync("Middleware #3  : After Calling next\r\n");
});


//Run the Kestrel Server
app.Run();
