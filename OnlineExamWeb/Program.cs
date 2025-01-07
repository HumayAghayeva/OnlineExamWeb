using Abstraction;
using Abstraction.Command;
using Abstraction.Queries;
using Business.BackGroundServices;
using Business.Repositories;
using Business.Repositories.Command;
using Domain.OptionDP;
using Infrastructure.DataContext.Write;
using Infrastructure.Persistent.Read;
using Infrastructure.Repositories;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog.Web;
using OnlineExamWeb.Middleware;
using OnlineExamWeb.Utilities;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Logging.AddConsole();
var logger =NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

   logger.Info("init main");
    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Information);
    builder.Host.UseNLog();

builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsetting.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",optional:true).AddEnvironmentVariables();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddDbContext<OEPWriteDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WriteDbContext")));

builder.Services.AddDbContext<OEPReadDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReadDbContext")));


builder.Services.InjectDependencies(builder.Configuration);

//builder.Services.AddHostedService<TransferDataFromWriteToRead>();

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();

}
app.UseHttpsRedirection();
    app.UseStaticFiles();

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseRouting();
    app.UseAuthorization();

app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();