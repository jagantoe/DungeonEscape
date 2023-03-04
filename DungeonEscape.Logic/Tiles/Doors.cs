namespace DungeonEscape.Logic;
public abstract class Door : Tile
{
	public Item Key { get; set; }
	public Door(string color, Item key)
	{
		Name = $"{color} Door";
		Description = $"A closed {color} door";
		DetailedDescription = $"Where there is a {color} door there must be a {color} key";
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.Interactable;
		Key = key;
	}
	static Door()
	{
		Tiles.ConfigTile(TileType.RedDoor, new RedDoor());
		Tiles.ConfigTile(TileType.BlueDoor, new BlueDoor());
		Tiles.ConfigTile(TileType.YellowDoor, new YellowDoor());
		Tiles.ConfigTile(TileType.PurpleDoor, new PurpleDoor());
		Tiles.ConfigTile(TileType.OpenDoor, new OpenDoor());
	}
	public static void Init() { }
}
public sealed class RedDoor : Door
{
	public RedDoor() : base("Red", Item.Red_Key) { }
}
public sealed class BlueDoor : Door
{
	public BlueDoor() : base("Blue", Item.Blue_Key) { }
}
public sealed class YellowDoor : Door
{
	public YellowDoor() : base("Yellow", Item.Yellow_Key) { }
}
public sealed class PurpleDoor : Door
{
	public PurpleDoor() : base("Purple", Item.Purple_Key) { }
}
public sealed class GreenDoor : Door
{
	public GreenDoor() : base("Green", Item.Green_Key) { }
}
public sealed class OpenDoor : Tile
{
	public OpenDoor()
	{
		Name = "Open Door";
		Description = "An open door, perhaps it leads to freedom...";
		DetailedDescription = "An open door, perhaps it leads to freedom... or your DOOM!";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}
}
public abstract class Doorsasdasd : Tile
{
	//public string OpenedDescription { get; set; }
	//public Item Key { get; set; }

	//public override bool Walkable { get => Opened; }
	//public override bool BlocksVision { get => !Opened; }

	//public override void OnInteract(int id, TileOptions options, Player player)
	//{
	//	bool? open = options.GetOptions($"{Name}-{id}");
	//	if (open == true)
	//	{
	//		player.AddActionResult(Resources.DOORISOPEN);
	//	}
	//	else
	//	{
	//		if (player.Items.Contains(Key))
	//		{
	//			options.SetOption($"{Name}-{id}", true);
	//		}
	//		else

	//		{
	//			player.AddActionResult("You need some sort of key to open this door");
	//		}
	//	}
	//}

	//public override (bool Walkable, bool BlocksVision) GetState(int id, TileOptions options)
	//{
	//	bool? open = options.GetOptions($"{Name}-{id}");
	//	if (open == true)
	//	{
	//		return (true, true);
	//	}
	//	return (false, false);
	//}

	//public override string GetDescription(int id, TileOptions options, bool detailed = false)
	//{
	//	bool? open = options.GetOptions($"{Name}-{id}");
	//	if (open == true)
	//	{
	//		return OpenedDescription;
	//	}
	//	return Description;
	//}
}

