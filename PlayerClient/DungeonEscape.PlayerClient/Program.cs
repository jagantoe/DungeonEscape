// Player Client Template

using StackExchange.Redis;
using System.Text.Json;

string hubUri = "https://involvedempire.azurewebsites.net/hub/InvolvedEmpire";

// Use your username + password to retreive a token at https://involvedempire.azurewebsites.net/swagger/index.html#/Admin/Admin_GetToken
// The tokens are valid for 24h
var userToken = "";

// Shared Redis Cache for storing and sharing data within your team
var cacheToken = "";

// ActionLogger: https://kind-hill-0fa661703.1.azurestaticapps.net/
Player player;
List<Tile> vision;

#region Config
// WebSocket Config
var webSocket = new HubConnectionBuilder()
	.WithUrl(hubUri, options =>
	{
		options.AccessTokenProvider = () => Task.FromResult(userToken);
	})
	.WithAutomaticReconnect()
	.Build();

webSocket.On<GameState>("NewRound", async response =>
{
	player = response.Player;
	vision = response.VisionTiles;
	await RoundLogic();
});

webSocket.On<Inspection>("Inspect", async response =>
{
	await InspectionResult(response);
});

await webSocket.StartAsync();

// Redis Cache Config
ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(cacheToken);
var db = redis.GetDatabase();
var subscriber = redis.GetSubscriber();

while (true)
{
	Thread.Sleep(100);
}
#endregion

// Write all code here
// The player and vision variables are auto updated at the start of each round
// You can make 1 action per round, addition actions will overwrite your previous action
// Check your action logger for additional details
async Task RoundLogic()
{

}
async Task InspectionResult(Inspection inspection)
{

}

#region Websocket calls
// Warranty void if code is changed

Task Move(int x, int y)
{
	return webSocket.InvokeAsync("Move", x, y);
}

Task Interact(int x, int y)
{
	return webSocket.InvokeAsync("Interact", x, y);
}

Task Inspect(int x, int y)
{
	return webSocket.InvokeAsync("Inspect", x, y);
}

Task SwitchCharacter(Character character)
{
	return webSocket.InvokeAsync("SwitchClass", character);
}

Task Reset()
{
	return webSocket.InvokeAsync("Reset");
}
#endregion
#region Cache calls
// Warranty void if code is changed

async Task<bool> SetCache(string key, Object obj)
{
	return await db.StringSetAsync(key, JsonSerializer.Serialize(obj));
}

async Task<T> GetCache<T>(string key)
{
	var value = await db.StringGetAsync(key);
	return JsonSerializer.Deserialize<T>(value);
}

// Subscribe and Publish example
subscriber.Publish("test", DateTime.Now.ToString());

subscriber.Subscribe("test", (channel, value) =>
{
	Console.WriteLine(value);

});
#endregion