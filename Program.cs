using Core;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<FruitOptions>(options =>
{
    options.Name = "watermelon";
});

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
app.UseMiddleware<FruitMiddleware>();

app.MapGet("/", () => "Hello World!");

app.Run();
