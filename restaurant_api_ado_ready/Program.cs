using RestauranteAPI.Controllers;
using RestauranteAPI.Repositories;
using RestauranteAPI.Service;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("RestauranteDB");

builder.Services.AddScoped<IPlatoPrincipalRepository, PlatoPrincipalRepository>(provider =>
new PlatoPrincipalRepository(connectionString));

builder.Services.AddScoped<IPostreRepository, PostreRepository>(provider =>
new PostreRepository(connectionString));

builder.Services.AddScoped<IBebidaRepository, BebidaRepository>(provider =>
new BebidaRepository(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add services
builder.Services.AddScoped<IPlatoPrincipalService, PlatoPrincipalService>();
builder.Services.AddScoped<IBebidaService, IBebidaService>();
builder.Services.AddScoped<IPostreService, PostreService>();

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



//PlatoPrincipalController.InicializarDatos();
app.Run();
