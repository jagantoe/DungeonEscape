namespace DungeonEscape.Logic;

public sealed class Spikes : Tile
{
	public Spikes()
	{
		Name = "Floor";
		Description = "A perforated floor";
		DetailedDescription = "A perforated floor, best to avoid it lest you also wish to be perforated";
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.Walkable;
	}

	static Spikes()
	{
		Tiles.ConfigTile(TileType.Spikes, new Wall());
		Tiles.ConfigTile(TileType.Pit, new CrackedWall());
		Tiles.ConfigTile(TileType.FirePlate, new BrokenWall());
		Tiles.ConfigTile(TileType.IllusionWall, new IllusionWall());
	}
}
public sealed class Pit : Tile
{
	public Pit()
	{
		Name = "Pit";
		Description = "A bottomless pit";
		DetailedDescription = "A bottomless pit and yet death waits below";
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.Walkable;
	}
}
public sealed class FirePlate : Tile
{
	public FirePlate()
	{
		Name = "Floor";
		Description = "A slightly raised floor";
		DetailedDescription = "A scorching floor, best not stay too long";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}
}