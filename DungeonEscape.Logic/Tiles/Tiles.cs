namespace DungeonEscape.Logic;
public enum TileType
{
	// Floors
	Floor = 0,
	IceFloor = 1,
	SmokeFloor = 2,
	Teleporter = 3,

	//Start
	Start = 10,

	// Walls
	Wall = 20,
	CrackedWall = 21,
	BrokenWall = 22,
	IllusionWall = 23,
	SecretWall = 24,

	//Traps
	Spikes = 30,
	FirePlate = 31,
	Pit = 32,
	FalseFloor = 33,
	FireWall = 34,

	// Item Pickups
	Pickaxe = 80,
	Lantern = 81,

	// Doors
	OpenDoor = 90,
	RedDoor = 91,
	BlueDoor = 92,
	GreenDoor = 93,
	YellowDoor = 94,
	PurpleDoor = 95,
	BlackDoor = 96,

	// Chests
	OpenChest = 100,
	RedKeyChest = 101,
	BlueKeyChest = 102,
	GreenKeyChest = 103,
	YellowKeyChest = 104,
	PurpleKeyChest = 105,
	BlackKeyChest = 106,
	WhiteKeyChest = 107
}

public static class Tiles
{
	private readonly static Dictionary<TileType, Tile> TileInstances = new Dictionary<TileType, Tile>();

	internal static void ConfigTile(TileType type, Tile tile)
	{
		TileInstances.Add(type, tile);
	}

	public static Tile GetTile(TileType type)
	{
		return TileInstances[type];
	}

	public static void Init()
	{
		Console.WriteLine("here asdasd asd asd asd ");
		Chest.Init();
		Door.Init();
		Engraving.Init();
		Floor.Init();
		ItemPickup.Init();
		Trap.Init();
		Wall.Init();
	}
}
