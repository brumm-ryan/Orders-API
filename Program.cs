using Microsoft.EntityFrameworkCore;
using OrderAPI.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("Orders") ?? "Data Source=Orders.db";

builder.Services.AddControllers();
// builder.Services.AddDbContext<OrderContext>(opt =>
// opt.UseInMemoryDatabase("OrderList"));
builder.Services.AddSqlite<OrderContext>(connectionString);
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

