using Microsoft.EntityFrameworkCore;
using practica1.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();



//Inyecci�n de datos por la string de conexi�n
builder.Services.AddDbContext<EquiposContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString(
            "equiposDbConnection")
        )
    );



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
