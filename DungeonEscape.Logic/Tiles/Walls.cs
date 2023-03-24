namespace DungeonEscape.Logic;
public sealed class Wall : Tile
{
	public Wall()
	{
		Name = "Wall";
		Description = "It's just a wall";
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
		Name = "Wall";
		Description = "A cracked wall";
		DetailedDescription = "A cracked wall, perhaps with enough strength it can be broken";
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.NonWalkable;
	}

	public ActResult Interact(Vector2 pos, Player player, Map map)
	{
		if (player.Character == PlayerCharacter.StrongMan || player.Items.Contains(Item.Pickaxe))
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
		Name = "Wall";
		Description = "It's just a wall";
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
		Name = "Wall";
		Description = "It's just a wall";
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
		Name = "Floor";
		Description = "A flat floor";
		DetailedDescription = Description;
		Walkable = false;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}
}
