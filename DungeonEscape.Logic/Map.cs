namespace DungeonEscape.Logic;
public sealed class Map
{
	public Dictionary<Vector2, TileType> LoadedMap { get; set; }
	public Dictionary<Vector2, TileType?> MapState { get; set; }
	public Dictionary<Vector2, TileConfig> MapConfigs { get; set; }
	public Vector2 Start { get; set; }
	public List<Vector2> TurnStartTiles { get; set; }

	public Map(Dictionary<Vector2, TileType> map, Dictionary<Vector2, TileType?> mapState, Dictionary<Vector2, TileConfig> mapConfigs)
	{
		LoadedMap = map;
		MapState = mapState;
		Start = map.FirstOrDefault(x => x.Value == TileType.Start).Key;
		MapConfigs = mapConfigs;
		TurnStartTiles = LoadedMap.Keys.Where(x => this[x] is ITurnStart).ToList();
	}

	public Tile this[Vector2 pos]
	{
		get
		{
			TileType? state = MapState.GetValueOrDefault(pos);
			state ??= LoadedMap.GetValueOrDefault(pos);
			return Tiles.GetTile(state ?? TileType.Wall);
		}
	}

	public TileType? GetTileAt(Vector2 pos)
	{
		TileType? state = MapState.GetValueOrDefault(pos);
		state ??= LoadedMap.GetValueOrDefault(pos);
		return state;
	}

	public void ChangeState(Vector2 pos, TileType tile)
	{
		MapState[pos] = tile;
	}
	public void ClearState(Vector2 pos)
	{
		MapState.Remove(pos);
	}

	public TileConfig? GetConfig(Vector2 pos)
	{
		return MapConfigs.GetValueOrDefault(pos);
	}
	public TileConfig AddConfigAt(Vector2 pos)
	{
		var config = new TileConfig();
		MapConfigs.TryAdd(pos, config);
		return config;
	}
}
