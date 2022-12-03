using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/http", async context =>
{
    await context.Response.WriteAsync($"HTTPS Request: {context.Request.IsHttps}");
});

//enforcong https
app.UseHttpsRedirection();


app.MapGet("/", () => "Hello World!");
app.Run();
