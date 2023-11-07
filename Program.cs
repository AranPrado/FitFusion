using FitFusion;
using FitFusion.Controllers;
using FitFusion.Database;
using FitFusion.Repositores;
using FitFusion.Repositores.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.UseCors();
app.Run();
