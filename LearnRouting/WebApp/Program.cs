
var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "webroot")  
    //Now it will look for 'webroot' folder for static files. But still the useStaticfiles() middleware always look for wwwroot folder.
}); 

builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("pos", typeof(PositionConstraint));
});



var app = builder.Build();

//Add the custom routing constraint into the Service


app.UseRouting();
app.UseStaticFiles(); //It always look for wwwroot folder for the static files.

//Do not use multiple app.UseEndPoints(). Only one useEndPoints() containing all the endpoints.
//It is not top level routing.
app.UseEndpoints(endpoints =>
{
    endpoints.MapGet("/", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Welcome to home page!");
    });

    endpoints.MapGet("/employees", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Get Employees!");
    }); 

    endpoints.MapPost("/employees", async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Employee Created!");
    });
    endpoints.MapDelete("/employees/{id:int}", async(HttpContext context)=>
    {
        await context.Response.WriteAsync($"Delete employee : {context.Request.RouteValues["id"]}");
    });

    //Optional Route parameter.
    endpoints.MapPut("/employees/{id?}", async (HttpContext context) =>
    {
        await context.Response.WriteAsync($"Update employee : {context.Request.RouteValues["id"]}");
    });

    endpoints.MapGet("/employees/position/{position:pos}", async (HttpContext context) =>
    {
        await context.Response.WriteAsync($"Get employees under position : {context.Request.RouteValues["position"]}");
    });
});   

//So now we use this for top level routing
//app.MapGet("/employees", async (HttpContext context) =>
//{
//    await context.Response.WriteAsync("Get Employees!");
//});


app.Run();

//Custom Parameter Constraint Class
class PositionConstraint : IRouteConstraint
{
    public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
    {
        if (!values.ContainsKey(routeKey)) return false;
        if (values[routeKey] is null) return false;
        if (values[routeKey].ToString().Equals("manager", StringComparison.OrdinalIgnoreCase) ||
            values[routeKey].ToString().Equals("developer", StringComparison.OrdinalIgnoreCase))

            return true;
        return false;
    }
}
