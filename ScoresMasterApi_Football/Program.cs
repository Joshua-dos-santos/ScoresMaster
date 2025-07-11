using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using ScoresMasterApi_Football.ApiServices;
using ScoresMasterApi_Football.Leagues;
using ScoresMasterApi_Football.Teams;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    Env.Load();
}

var testKey = Environment.GetEnvironmentVariable("RAPIDAPI_KEY");
Console.WriteLine("🔑 RAPIDAPI_KEY op Heroku = " + (testKey ?? "NIET GEVONDEN"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITeamsService, TeamsService>();
builder.Services.AddScoped<ILeaguesService, LeaguesService>();
builder.Services.AddHttpClient<IFootballApiService, FootballApiService>();

builder.Services.AddDbContext<ScoresMasterDbContext>(options =>
    options.UseSqlite("Data Source=scoresmaster-football.db"));

var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://+:{port}");


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ScoresMasterDbContext>();
    db.Database.Migrate();
}

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
