namespace DungeonEscape.Game;
public static class Mappings
{
	static Mappings()
	{
		// Map load mapping
		TypeAdapterConfig<List<MapTile>, Dictionary<Vector2, TileType>>
			.ForType()
			.MapWith(src => src.ToDictionary(tile => new Vector2(tile.X, tile.Y), x => x.Value));
		// Vector mapping
		TypeAdapterConfig<Vector2, string>
			.ForType()
			.MapWith(src => $"{src.X}-{src.Y}");
		TypeAdapterConfig<string, Vector2>
			.ForType()
			.MapWith(src => new Vector2(float.Parse(src.Split('-', StringSplitOptions.None)[0]), float.Parse(src.Split('-', StringSplitOptions.None)[1])));
		// Map state mapping
		TypeAdapterConfig<Dictionary<Vector2, TileType>, Dictionary<string, TileType>>
			.ForType()
			.MapWith(src => src.ToDictionary(x => x.Key.Adapt<string>(), x => x.Value));
		TypeAdapterConfig<Dictionary<Vector2, TileType?>, Dictionary<string, TileType>>
			.ForType()
			.MapWith(src => src.ToDictionary(x => x.Key.Adapt<string>(), x => (TileType)x.Value!));
		TypeAdapterConfig<Dictionary<string, TileType>, Dictionary<Vector2, TileType>>
			.ForType()
			.MapWith(src => src.ToDictionary(x => x.Key.Adapt<Vector2>(), x => x.Value));
		// Map config mapping
		TypeAdapterConfig<Dictionary<Vector2, TileConfig>, Dictionary<string, TileConfigDTO>>
			.ForType()
			.MapWith(src => src.ToDictionary(x => x.Key.Adapt<string>(), x => x.Value.Adapt<TileConfigDTO>()));
		TypeAdapterConfig<Dictionary<string, TileConfigDTO>, Dictionary<Vector2, TileConfig>>
			.ForType()
			.MapWith(src => src.ToDictionary(x => x.Key.Adapt<Vector2>(), x => x.Value.Adapt<TileConfig>()));
		// Player mapping
		TypeAdapterConfig<PlayerStorageDTO, Player>
			.NewConfig()
			.Map(x => x.Position, x => new Vector2(x.PositionX, x.PositionY));
	}
	public static void Init()
	{
		Initializer.Init();
	}
}
