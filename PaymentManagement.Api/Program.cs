using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using PaymentManagement.Api.Repository;
using PaymentManagement.Api.Repository.Implementation;
using PaymentManagement.Api.Repository.Interface;
using PaymentManagement.Api.Services.Implementation;
using PaymentManagement.Api.Services.Interface;
using PaymentManagement.Api.Services.Utilities;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddCors(c => 
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddAutoMapper(typeof(PaymentProfile));

builder.Services.AddDbContext<PaymentDbContext>(options => options.UseSqlServer(configuration.GetConnectionString
   ("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(option => option.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
