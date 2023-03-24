namespace DungeonEscape.Logic;
public enum TileType
{
	// Floors
	Floor = 0,
	SmokeFloor = 2,
	Teleporter = 3,
	PressurePlate = 4,
	PressurePlateReseter = 5,
	PressurePlateChecker = 6,
	ToggleFloorOn = 7,
	ToggleFloorOff = 8,
	FalsePressurePlate = 9,

	//Start
	Start = 10,
	ToggleLever = 11,
	SinglePullLever = 12,
	TileSwitcherLever = 13,
	Puzzle2Checker = 14,


	// Walls
	Wall = 20,
	CrackedWall = 21,
	BrokenWall = 22,
	IllusionWall = 23,
	SecretWall = 24,
	InvisibleWall = 25,

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
	WhiteDoor = 97,
	StateDoor = 98,

	// Chests
	OpenChest = 100,
	RedKeyChest = 101,
	BlueKeyChest = 102,
	GreenKeyChest = 103,
	YellowKeyChest = 104,
	PurpleKeyChest = 105,
	BlackKeyChest = 106,
	WhiteKeyChest = 107,

	// Decoration
	Boulder = 110,
	Angel = 111,

	// Engravings
	EndEngraving = 200,
	Puzzle1 = 201,
	Puzzle2 = 202,
	Puzzle3_1 = 203,
	Puzzle3_2 = 204,
	Puzzle3_3 = 205,
	Puzzle4_1 = 206,
	Puzzle4_2 = 207,
	Puzzle4_3 = 208,
	Puzzle4_4 = 209,
	Puzzle4_5 = 210,
	Puzzle4_6 = 211,
	Puzzle4_7 = 212,
	Puzzle4_8 = 213,
	Puzzle4_9 = 214,
	Puzzle4_10 = 215,
	Puzzle5_1 = 216,
	Puzzle5_2 = 217,
	Puzzle6_1 = 218,
	Puzzle6_2 = 219,
	Puzzle6_3 = 220
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
		Start.Init();
		Chest.Init();
		Door.Init();
		Engraving.Init();
		Floor.Init();
		ItemPickup.Init();
		Trap.Init();
		Wall.Init();
		Lever.Init();
		Decoration.Init();
	}
}
