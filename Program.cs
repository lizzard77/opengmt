using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using OpenGMT;
using OpenGMT.DB;
using OpenGMT.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddTransient<FileDB>();

var app = builder.Build();

FileDB.DataDir = Path.Combine(app.Environment.ContentRootPath, "data");

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors(options =>
{
    options.AllowAnyMethod().AllowAnyHeader();
    options.SetIsOriginAllowed((host) => true);
    options.AllowCredentials();
});

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();                
    endpoints.MapHub<GameHub>("/hubs/game");
    endpoints.MapControllerRoute(name: "default", pattern: "{controller}/{action=Index}/{id?}");
    endpoints.MapFallbackToFile("index.html").AllowAnonymous();
});



app.Run();
