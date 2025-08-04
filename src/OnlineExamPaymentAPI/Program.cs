using Abstraction.PaymentApi.Interfaces.CardOperations;
using OnlineExamPaymentAPI.Dtos.Request;
using OnlineExamPaymentAPI.Endpoints;
using OnlineExamPaymentAPI.Services.PaymentApiServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICardValidator, CardValidatorServices>(); // Make sure this class exists
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

#region Minima Api 
// Register Minimal APIs
app.MapCardEndpoints();
#endregion
app.Run();
