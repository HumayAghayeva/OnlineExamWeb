
using Infrastructure.DataContext.Write;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Autofac.Core;
using OnlineExamWebApi.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Abstraction.Command;
using Business.Repositories.Command;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("appsettings.api.json", optional: false, reloadOnChange: true);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks().AddSqlServer(builder.Configuration.GetConnectionString("WriteDbContext"), name: "Write DB");
builder.Services.AddHealthChecks()
    .AddCheck<DatabaseHealthCheck<OEPWriteDB>>("Database").
    AddCheck<StudentConfirmationHealthCheck>("student_confirmation");

builder.Services.AddHealthChecksUI(options =>
{
    options.SetEvaluationTimeInSeconds(15); 
    options.MaximumHistoryEntriesPerEndpoint(60);
    options.AddHealthCheckEndpoint("API Health", "/health");
})
.AddSqlServerStorage(builder.Configuration.GetConnectionString("WriteDbContext"));

builder.Services.AddDbContext<OEPWriteDB>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("WriteDbContext")));

builder.Services.AddScoped<IStudentCommandRepository, StudentCommandRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"); });
app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization(); 

app.UseStaticFiles(); 

app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.MapHealthChecksUI(options =>
{
    options.UIPath = "/health-ui"; 
    options.ApiPath = "/health-ui-api"; 
});

app.MapControllers();

app.Run();