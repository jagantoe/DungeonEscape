using System.Numerics;

namespace DungeonEscape.Logic;
public sealed class Map
{
	public Dictionary<Vector2, TileType> LoadedMap { get; set; }
	public Dictionary<Vector2, TileType?> MapState { get; set; }

	public Map(Dictionary<Vector2, TileType> map, Dictionary<Vector2, TileType?> mapState)
	{
		LoadedMap = map;
		MapState = mapState;
	}

	public Tile this[Vector2 pos]
	{
		get
		{
			TileType? state = MapState.GetValueOrDefault(pos);
			state ??= LoadedMap[pos];
			return Tiles.GetTile(state ?? TileType.Wall);
		}
	}

	public void ChangeState(Vector2 pos, TileType tile)
	{
		MapState[pos] = tile;
	}
}
