namespace DungeonEscape.Logic;
public sealed class Wall : Tile
{
	public static string Name = "Wall";
	public static string Description = "It's just a wall";
	public Wall()
	{
		base.Name = Name;
		base.Description = Description;
		DetailedDescription = Description;
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.NonWalkable;
	}

	static Wall()
	{
		Tiles.ConfigTile(TileType.Wall, new Wall());
		Tiles.ConfigTile(TileType.CrackedWall, new CrackedWall());
		Tiles.ConfigTile(TileType.BrokenWall, new BrokenWall());
		Tiles.ConfigTile(TileType.IllusionWall, new IllusionWall());
		Tiles.ConfigTile(TileType.SecretWall, new SecretWall());
		Tiles.ConfigTile(TileType.InvisibleWall, new InvisibleWall());
	}
	public static void Init() { }
}
public sealed class CrackedWall : Tile, IInteract
{
	public CrackedWall()
	{
		Name = Wall.Name;
		Description = "A cracked wall";
		DetailedDescription = "A cracked wall, perhaps with enough strength it can be broken";
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.NonWalkable;
	}

	public ActResult Interact(Vector2 pos, Player player, Map map)
	{
		if (player.Character is PlayerCharacter.StrongMan || player.Items.Contains(Item.Pickaxe))
		{
			map.ChangeState(pos, TileType.BrokenWall);
			return new SuccessResult("You break the wall down, allowing passage");
		}
		return new GeneralResult("The wall is too strong");
	}
}
public sealed class BrokenWall : Tile
{
	public BrokenWall()
	{
		Name = "Broken wall";
		Description = "The remains of a broken wall";
		DetailedDescription = "The remains of a broken wall";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}
}
public sealed class IllusionWall : Tile
{
	public IllusionWall()
	{
		Name = Wall.Name;
		Description = Wall.Description;
		DetailedDescription = "There seems to be a breeze flowing from the wall";
		Walkable = true;
		BlocksVision = true;
		TileKind = TileKind.NonWalkable;
	}
}
public sealed class SecretWall : Tile
{
	public SecretWall()
	{
		Name = Wall.Name;
		Description = Wall.Description;
		DetailedDescription = Description;
		Walkable = true;
		BlocksVision = true;
		TileKind = TileKind.NonWalkable;
	}
}
public sealed class InvisibleWall : Tile
{
	public InvisibleWall()
	{
		Name = Wall.Name;
		Description = Wall.Description;
		DetailedDescription = Description;
		Walkable = false;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}
}
