namespace DungeonEscape.Logic;
public sealed class Map
{
	public Dictionary<Vector2, TileType> LoadedMap { get; set; }
	public Dictionary<Vector2, TileType?> MapState { get; set; }
	public Dictionary<Vector2, TileConfig> MapConfigs { get; set; }
	public Vector2 Start { get; set; }

	public Map(Dictionary<Vector2, TileType> map, Dictionary<Vector2, TileType?> mapState, Dictionary<Vector2, TileConfig> mapConfigs)
	{
		LoadedMap = map;
		MapState = mapState;
		Start = map.FirstOrDefault(x => x.Value == TileType.Start).Key;
		MapConfigs = mapConfigs;
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

	public TileConfig? GetConfig(Vector2 pos)
	{
		return MapConfigs.GetValueOrDefault(pos);
	}
}
