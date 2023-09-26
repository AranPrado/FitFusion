using FitFusion.Controllers;
using FitFusion.Database;
using FitFusion.Repositores;
using FitFusion.Repositores.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Conexão com o banco de dados

builder.Services.AddDbContext<AppDbContext>(options =>
options.UseMySql(builder.Configuration.GetConnectionString("DataBase"),
    new MySqlServerVersion(new Version(8, 0, 26)),
    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

//fim

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IExerciciosRepositore, ExerciciosController>();
builder.Services.AddScoped<ITreinosRepositore, TreinoController>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minha API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
