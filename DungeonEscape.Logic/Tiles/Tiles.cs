namespace DungeonEscape.Logic;
public enum TileType
{
    // Floors
    Floor = 0,
    Start,

    // Walls
    Wall,
    CrackedWall,
    BrokenWall,
    IllusionWall,
    SecretWall,


    //Traps
    Spikes,
    Pit,
    FirePlate,
    FalseFloor,
    ICE,

    PILLAR,
    LEVER,

    // Doors
    RedDoor,
    BlueDoor,
    YellowDoor,
    PurpleDoor,
    GreenDoor,
    OpenDoor,


    // Chests
    RedKeyChest = 101,
    BlueKeyChest = 102,
    GreenKeyChest = 103,
    YellowKeyChest = 104,
    PurpleKeyChest = 105
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
        Chest.Init();
        Door.Init();
        Engraving.Init();
        Floor.Init();
        Trap.Init();
        Wall.Init();
    }
}