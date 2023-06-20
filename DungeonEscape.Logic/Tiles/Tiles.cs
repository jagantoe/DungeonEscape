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
	TileToggleChecker = 14,
	LeapLedge = 15,
	CleansingPool = 16,


	// Walls
	Wall = 20,
	CrackedWall = 21,
	BrokenWall = 22,
	IllusionWall = 23,
	SecretWall = 24,
	InvisibleWall = 25,

	// Traps
	VisibleSpikes = 28,
	VisibleFire = 29,
	Spikes = 30,
	FirePlate = 31,
	Pit = 32,
	FalseFloor = 33,
	FireWall = 34,

	// Automation
	WallMover = 40,
	FloorMover = 41,
	SpikeMover = 42,
	FireMover = 43,

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
	Demon = 112,
	Vulture = 113,
	Human = 114,

	// Engravings
	Engraving = 200
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
		Automation.Init();
	}
}
