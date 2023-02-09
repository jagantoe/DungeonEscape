using DungeonEscape.Logic;
using Mapster;
using System.Numerics;
using System.Text.Json;

namespace DungeonEscape.Game;
public static class MapLoader
{
	public static Dictionary<Vector2, TileType> LoadMap(string map)
	{
		try
		{
			using StreamReader r = new StreamReader($"Maps/{map}.json");
			string json = r.ReadToEnd();
			var tiles = JsonSerializer.Deserialize<List<MapTile>>(json);
			var x = tiles.Adapt<Dictionary<Vector2, TileType>>();
			return x;
		}
		catch (Exception)
		{
			return null;
		}
	}
}

public class MapTile
{
	public int X { get; set; }
	public int Y { get; set; }
	public TileType Value { get; set; }
}