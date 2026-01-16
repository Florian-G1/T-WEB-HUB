using Npgsql;
using Microsoft.EntityFrameworkCore;
using Gunpla_600.Infrastructure.Data;
using Gunpla_600.Infrastructure.Repositories;
using Gunpla_600.Application.Interfaces;
using Gunpla_600.Application.Services;
using Gunpla_600.Infrastructure.Services.Security;
using Gunpla_600.Application.Interfaces.Security;

var builder = WebApplication.CreateBuilder(args);

var dbHost = Environment.GetEnvironmentVariable("POSTGRES_HOST");
var dbPort = Environment.GetEnvironmentVariable("POSTGRES_PORT");
var dbName = Environment.GetEnvironmentVariable("POSTGRES_NAME");
var dbUser = Environment.GetEnvironmentVariable("POSTGRES_USER");
var dbPassword = Environment.GetEnvironmentVariable("POSTGRES_PASSWORD");
var serverPort = Environment.GetEnvironmentVariable("SERVER_PORT");

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(int.Parse(serverPort)); // écoute sur toutes les interfaces
});


var connectionString =
    $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPassword}";

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasherService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

app.Run();
