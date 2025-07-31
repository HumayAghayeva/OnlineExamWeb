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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using Hazelcast;
using Autofac.Core;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsetting.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",optional:true).AddEnvironmentVariables();

builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.Configure<JWTSettings>(builder.Configuration.GetSection("JWT"));

builder.Services.AddDbContext<OEPWriteDB>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WriteDbContext")));


// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext() 
    .WriteTo.Console() 
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day) 
    .CreateLogger();

builder.Services.InjectDependencies(builder.Configuration);

//builder.Services.AddHostedService<TransferDataFromWriteToRead>();

// Add Hazelcast Client as a Singleton
builder.Services.AddSingleton<Task<IHazelcastClient>>(async provider =>
{
    var options = new HazelcastOptionsBuilder()
        .With(o =>
        {
            o.Networking.Addresses.Add("127.0.0.1:5701"); // Ensure Hazelcast server is running here
            o.Networking.ReconnectMode = Hazelcast.Networking.ReconnectMode.ReconnectAsync;
        })
        .Build();

    return await HazelcastClientFactory.StartNewClientAsync(options);
});
#region jwt auth
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options => {
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        IssuerSigningKey =
        new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"]))
    };
});
#endregion

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