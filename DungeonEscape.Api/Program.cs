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

// Gist Storage
builder.Services.ConfigureGistStorage("DungeonEscape", Secret.GitHubToken, Secret.GistId);

// Data and game services
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<DataService>();
builder.Services.AddSingleton<GameService>();
builder.Services.AddSingleton<GameOptions>();

// Background service
builder.Services.AddHostedService<GameBackgroundService>();

// Authentication and tokens
var key = Encoding.ASCII.GetBytes(Secret.Key);
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
		x.Events = new JwtBearerEvents
		{
			OnMessageReceived = context =>
			{
				var accessToken = context.Request.Query["access_token"];

				// If the request is for our hub...
				var path = context.HttpContext.Request.Path;
				if (!string.IsNullOrEmpty(accessToken))
				{
					// Read the token out of the query string
					context.Token = accessToken;
				}
				return Task.CompletedTask;
			}
		};
	});

// Cors
builder.Services.AddCors(o => o.AddPolicy("AllowAll", builder =>
{
	builder
		.WithOrigins("http://localhost:4200")
		.AllowAnyMethod()
		.AllowAnyHeader()
		.AllowCredentials();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});
app.MapHub<GameHub>("/hub/Game");
app.MapHub<DashboardHub>("/hub/Dashboard");

app.Run();

public static class Secret
{
	public static readonly string AdminCode = "*** secret admin password ***";
	public static readonly string Key = "*** secret key ***";
	public static readonly string GitHubToken = "*** github token ***";
	public static readonly string GistId = "*** gist id ***";
}
