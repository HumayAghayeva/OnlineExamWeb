using Abstraction;
using Abstraction.Command;
using Abstraction.Queries;
using Business.Repositories;
using Business.Repositories.Command;
using Infrastructure.DataContext.Write;
using Infrastructure.Persistent.Read;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NLog.Web;
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

/************************************************
 * Add connection string to services container - using EF pooling for performance
 ************************************************/

builder.Services.AddDbContext<OEPWriteDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WriteDbContext")));


builder.Services.AddDbContext<OEPReadDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ReadDbContext")));

//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IStudentCommandRepository, StudentCommandRepository>();
builder.Services.AddScoped<IStudentQueryRepository, StudentQueryRepository>();


/************************************************
 Add AutoMaper DI to services container
 ************************************************/
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

    app.UseRouting();

    app.UseAuthorization();

app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Student}/{action=Create}/{id?}");

app.Run();