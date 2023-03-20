using DungeonEscape.Api.Authentication;
using DungeonEscape.Api.BackgroundServices;
using DungeonEscape.Api.Client.SignalR;
using DungeonEscape.Api.GameManagement;
using DungeonEscape.Game;
using GitHubGistStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

// Init all static game objects
Mappings.Init();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Authentication and tokens
var key = Encoding.ASCII.GetBytes(Secret.Code);
builder.Services.AddSingleton(new TokenProvider(key));
builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
	.AddJwtBearer(x =>
	{
		x.RequireHttpsMetadata = false;
		x.SaveToken = true;
		x.TokenValidationParameters = new TokenValidationParameters
		{
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(key),
			ValidateIssuer = false,
			ValidateAudience = false
		};
	});

// Gist Storage
var githubToken = "";
var gistId = "";
builder.Services.ConfigureGistStorage("DungeonEscape", githubToken, gistId);

// Data and game services
builder.Services.AddMemoryCache();
builder.Services.AddSingleton(new TokenProvider(Secret.Code));
builder.Services.AddSingleton<DataService>();
builder.Services.AddSingleton<GameService>();
builder.Services.AddSingleton<GameOptions>();

// Background service
builder.Services.AddHostedService<GameBackgroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<GameHub>("/hub/Game");
app.MapHub<DashboardHub>("/hub/Dashboard");

app.Run();


public static class Secret
{
	public static readonly string Code = "*** secret admin password ***";
}
