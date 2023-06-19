namespace DungeonEscape.Logic;
public abstract class ItemPickup : Tile, IInteract
{
	public Item Item { get; set; }
	public ItemPickup(Item item, string description)
	{
		Item = item;
		Name = Item.ToString();
		Description = $"A {Item}";
		DetailedDescription = description;
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.PointOfInterest;
	}

	static ItemPickup()
	{
		Tiles.ConfigTile(TileType.Pickaxe, new Pickaxe());
		Tiles.ConfigTile(TileType.Lantern, new Lantern());
	}
	public static void Init() { }

	public ActResult Interact(Vector2 pos, Player player, Map map)
	{
		if (player.Items.Contains(Item)) return new GeneralResult($"You already have a {Item}");
		player.Items.Add(Item);
		map.ChangeState(pos, TileType.Floor);
		return new SuccessResult($"You have gained {Item}");
	}
}
public sealed class Pickaxe : ItemPickup { public Pickaxe() : base(Item.Pickaxe, "A Pickaxe, should help to break down walls") { } }
public sealed class Lantern : ItemPickup { public Lantern() : base(Item.Lantern, "A Lantern, should help to see better") { } }
