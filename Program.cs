using Microsoft.EntityFrameworkCore;
using ProvaPub;
using ProvaPub.Interfaces;
using ProvaPub.Repository;
using ProvaPub.Services;
using ProvaPub.Commom;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Faz a injeção de dependência
NativeInjector.RegisterServices(builder.Services);

// Registre o AutoMapper
builder.Services.AddAutoMapper(typeof(Automap));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<RandomService>();
builder.Services.AddDbContext<TestDbContext>(options =>
	options.UseSqlServer(builder.Configuration.GetConnectionString("ctx")));

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

app.Run();
