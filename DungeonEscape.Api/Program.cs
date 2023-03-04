using DungeonEscape.Api.Authentication;
using DungeonEscape.Api.GameManagement;
using DungeonEscape.Game;

// Init all static game objects
Mappings.Init();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();
builder.Services.AddSingleton(new TokenProvider("***secret-key***"));
builder.Services.AddSingleton<DataService>();
builder.Services.AddSingleton<GameService>();
builder.Services.AddSingleton<GameOptions>();

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
