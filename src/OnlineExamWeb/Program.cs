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
using Serilog;
using OnlineExamWeb.Middleware;
using OnlineExamWeb.Utilities;
using System.Reflection;
using Quartz;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsetting.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",optional:true).AddEnvironmentVariables();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddDbContext<OEPWriteDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WriteDbContext")));

builder.Services.AddDbContext<OEPReadDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReadDbContext")));

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext() 
    .WriteTo.Console() 
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) 
    .CreateLogger();

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

app.MapHealthChecks("/health");

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseRouting();
    app.UseAuthorization();

app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();