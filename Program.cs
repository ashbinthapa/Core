using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout= TimeSpan.FromSeconds(5);
    options.Cookie.IsEssential= true;
});
var app = builder.Build();
app.UseSession();

app.MapGet("/session", async context =>
{
    int counter = (context.Session.GetInt32("counter") ?? 0) + 1;

    context.Session.SetInt32("counter", counter);

    await context.Session.CommitAsync();

    await context.Response.WriteAsync($"Session:{counter}");
    
});

app.MapGet("/", () => "Hello World!");
app.Run();
