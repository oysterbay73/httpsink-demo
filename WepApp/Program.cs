using Serilog;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.DurableHttpUsingFileSizeRolledBuffers(
                    requestUri: "https://www.mylogs.com",
                    httpClient: null,
                    bufferBaseFileName: Path.Combine(builder.Environment.ContentRootPath, "buffer"), // use root or absolute path otherwise logs to location of iis worker process (if running under iis)
                    bufferFileShared: true) // required for multi threaded app - ensure app pool has full control
    .Enrich.FromLogContext()
    .CreateLogger();

builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
