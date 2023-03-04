namespace DungeonEscape.Logic;
public abstract class Chest : Tile
{
	public Item Item { get; set; }
	public Chest(Item item)
	{
		Name = "Chest";
		Description = "A chest, perhaps there is something inside";
		DetailedDescription = "A chest, there must be something inside";
		Walkable = false;
		BlocksVision = false;
		TileKind = TileKind.Interactable;
	}

	static Chest()
	{
		Tiles.ConfigTile(TileType.RedKeyChest, new RedKeyChest());
		Tiles.ConfigTile(TileType.BlueKeyChest, new BlueKeyChest());
		Tiles.ConfigTile(TileType.GreenKeyChest, new GreenKeyChest());
		Tiles.ConfigTile(TileType.YellowKeyChest, new YellowKeyChest());
		Tiles.ConfigTile(TileType.PurpleKeyChest, new PurpleKeyChest());
	}
	public static void Init() { }
}
public sealed class RedKeyChest : Chest { public RedKeyChest() : base(Item.Red_Key) { } }
public sealed class BlueKeyChest : Chest { public BlueKeyChest() : base(Item.Blue_Key) { } }
public sealed class GreenKeyChest : Chest { public GreenKeyChest() : base(Item.Green_Key) { } }
public sealed class YellowKeyChest : Chest { public YellowKeyChest() : base(Item.Yellow_Key) { } }
public sealed class PurpleKeyChest : Chest { public PurpleKeyChest() : base(Item.Purple_Key) { } }