using Microsoft.EntityFrameworkCore;
using OpenGMT;
using OpenGMT.SignalR;

var builder = WebApplication.CreateBuilder(args);

var dataDir = Path.Combine(builder.Environment.ContentRootPath, "data");
if (!Directory.Exists(dataDir))
    Directory.CreateDirectory(dataDir);

builder.Services.AddCors();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddDbContext<OpenGMTContext>(
    options =>
    {
        options.UseSqlite($"Filename={dataDir}/OpenGMT.sqlite");
        options.EnableDetailedErrors();
        options.EnableSensitiveDataLogging();
    });

var app = builder.Build();

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

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<OpenGMTContext>();
    db.Database.Migrate();
}

app.Run();
