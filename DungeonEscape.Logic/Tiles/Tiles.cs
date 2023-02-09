namespace DungeonEscape.Logic;
public enum TileType
{
    Floor = 0,
    Start,

    Wall,
    CrackedWall,
    BrokenWall,
    IllusionWall,

    Spikes,
    Pit,
    FirePlate,
    ICE,

    PILLAR,
    LEVER,

    RedDoor,
    BlueDoor,
    YellowDoor,
    PurpleDoor,
    GreenDoor,
    OpenDoor
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
}