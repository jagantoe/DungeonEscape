namespace DungeonEscape.Logic;
public sealed class Engraving : TileWithConfig
{
	public Engraving()
	{
		Name = "Engraving";
		Description = Name;
		DetailedDescription = Name;
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.PointOfInterest;
	}
	public Engraving(string description)
	{
		Name = $"Engraving";
		Description = description;
		DetailedDescription = description;
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.PointOfInterest;
	}

	public override Inspection Inspect(bool detailed, Vector2 pos, Map map)
	{
		return new()
		{
			Name = Name,
			Description = map.GetConfig(pos)?.Text ?? ErrorResult.ConfigMissing(pos).Result
		};
	}

	static Engraving()
	{
		Tiles.ConfigTile(TileType.Engraving, new Engraving());
	}
	public static void Init() { }
}
