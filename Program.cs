using Core;
using Core.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
/*
builder.Services.Configure<FruitOptions>(options =>
{
    options.Name = "watermelon";
});
*/

builder.Services.AddTransient<IResponseFormatter, GuidService>();
var app = builder.Build();

/*
app.Use(async (context, next) =>
{
    if(context.Request.Method == HttpMethods.Get && context.Request.Query["custom"] == "true")
    {
        context.Response.ContentType = "text/plain";
        await context.Response.WriteAsync("Custom Middleware\n");
    }
    await next();
});
*/


/*
app.Use(async (context, next) =>
{   
    if(context.Request.Path == "/short")
    {
        await context.Response.WriteAsync("Request-short-circuited");
    }
    else
    {
        await next();
    }
});

*/



/*
((IApplicationBuilder)app).Map("/branch", branch =>
{
    branch.Run(async (HttpContext context) =>
    {
        await context.Response.WriteAsync("Branch middleware");
    });
});
*/

/*
((IApplicationBuilder)app).Map("/branch", branch =>
{
    branch.Run(new Middleware().Invoke);
});
*/

//app.MapGet("/fruit", async(HttpContext context, IOptions<FruitOptions> FruitOptions )=>
//{   
//    FruitOptions options = FruitOptions.Value;
//    await context.Response.WriteAsync($"{options.Name}, {options.Color}");
//});
//app.UseMiddleware<FruitMiddleware>();

//IResponseFormatter formatter = new TextResponseFormatter();

app.MapGet("/formatter1", async (HttpContext context, IResponseFormatter formatter) =>
{
    await formatter.Format(context, "Fromatter 1");
});

app.MapGet("/formatter2", async (HttpContext context, IResponseFormatter formatter) =>
{
    await formatter.Format(context, "Fromatter 2");
});


app.UseMiddleware<CustomMiddleware>();
app.UseMiddleware<CustomMiddleware2>();

app.MapGet("/endpoint", CustomEndpoint.EndPoint);
app.MapGet("/", () => "Hello World!");


app.Run();
