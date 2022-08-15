using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using OpenGMT;
using OpenGMT.SignalR;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddDbContext<OpenGMTContext>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<OpenGMTContext>();
    context.Database.Migrate();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<OpenGMTContext>();
    context.ImportJson();
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
