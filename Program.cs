using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OrderAPI.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("MyDbConnection");

builder.Services.AddControllers();
// builder.Services.AddDbContext<OrderContext>(opt =>
// opt.UseInMemoryDatabase("OrderList"));
builder.Services.AddSqlServer<OrderContext>(connectionString);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

