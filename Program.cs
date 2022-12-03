using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/cookie", async context =>
{
    int counter = int.Parse(context.Request.Cookies["counter"] ?? "0") + 1;
    context.Response.Cookies.Append(
        "counter",
        counter.ToString(),
        new CookieOptions
        {
            MaxAge = TimeSpan.FromSeconds(100)
        }
    );
    await context.Response.WriteAsync($"Cookie:{counter}");
    
});

app.MapGet("/clear", context =>
{
    context.Response.Cookies.Delete("counter");
    context.Response.Redirect("/");
    return Task.CompletedTask;

});
app.MapGet("/", () => "Hello World!");
app.Run();
