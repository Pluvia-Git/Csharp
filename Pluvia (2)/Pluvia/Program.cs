using Microsoft.EntityFrameworkCore;
using Pluvia.Data;
using Pluvia.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração do DbContext com Oracle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseOracle(builder.Configuration.GetConnectionString("DefaultConnection")));

// Injeção do serviço de clima
builder.Services.AddScoped<ClimaService>();

builder.Services.AddHttpClient();
// Razor e Controllers com Views
builder.Services.AddControllersWithViews();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger no ambiente de dev
app.UseSwagger();
app.UseSwaggerUI();


app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Clima}/{action=Index}/{id?}"); // Página inicial: /Clima/Index

app.Run();
