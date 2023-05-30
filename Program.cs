global using API_YU.Models;
global using API_YU.Services.CharacterService;
global using API_YU.Dtos.Character;
global using AutoMapper;
global using Microsoft.EntityFrameworkCore;
global using API_YU.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// PostgreSQL string connection "Host=localhost;Username=postgres;Password=toor;Database=Librodb"

builder.Services.AddDbContext<DataContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddScoped<ICharacterService, CharacterService>();

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
