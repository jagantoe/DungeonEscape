namespace DungeonEscape.Logic;
public sealed class OpenChest : Tile
{
	public OpenChest()
	{
		Name = "Chest";
		Description = "A open chest, there is nothing inside";
		DetailedDescription = "A open chest, the must have been something inside";
		Walkable = false;
		BlocksVision = false;
		TileKind = TileKind.PointOfInterest;
	}
}
public abstract class Chest : Tile, IInteract
{
	public Item Item { get; set; }
	public Chest(Item item)
	{
		Item = item;
		Name = "Chest";
		Description = "A chest, perhaps there is something inside";
		DetailedDescription = "A chest, there must be something inside";
		Walkable = false;
		BlocksVision = false;
		TileKind = TileKind.PointOfInterest;
	}

	static Chest()
	{
		Tiles.ConfigTile(TileType.OpenChest, new OpenChest());
		Tiles.ConfigTile(TileType.RedKeyChest, new RedKeyChest());
		Tiles.ConfigTile(TileType.BlueKeyChest, new BlueKeyChest());
		Tiles.ConfigTile(TileType.GreenKeyChest, new GreenKeyChest());
		Tiles.ConfigTile(TileType.YellowKeyChest, new YellowKeyChest());
		Tiles.ConfigTile(TileType.PurpleKeyChest, new PurpleKeyChest());
		Tiles.ConfigTile(TileType.BlackKeyChest, new BlackKeyChest());
		Tiles.ConfigTile(TileType.WhiteKeyChest, new WhiteKeyChest());
	}
	public static void Init() { }

	public ActResult Interact(Vector2 pos, Player player, Map map)
	{
		player.Items.Add(Item);
		map.ChangeState(pos, TileType.OpenChest);
		return new SuccessResult($"You have gained {Item}");
	}
}
public sealed class RedKeyChest : Chest { public RedKeyChest() : base(Item.Red_Key) { } }
public sealed class BlueKeyChest : Chest { public BlueKeyChest() : base(Item.Blue_Key) { } }
public sealed class GreenKeyChest : Chest { public GreenKeyChest() : base(Item.Green_Key) { } }
public sealed class YellowKeyChest : Chest { public YellowKeyChest() : base(Item.Yellow_Key) { } }
public sealed class PurpleKeyChest : Chest { public PurpleKeyChest() : base(Item.Purple_Key) { } }
public sealed class BlackKeyChest : Chest { public BlackKeyChest() : base(Item.Black_Key) { } }
public sealed class WhiteKeyChest : Chest { public WhiteKeyChest() : base(Item.White_Key) { } }
