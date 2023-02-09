namespace DungeonEscape.Logic;

public abstract class Tile
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string DetailedDescription { get; set; }
    public bool Walkable { get; set; } = false;
    public bool BlocksVision { get; set; } = false;
    public TileKind TileKind { get; set; } = TileKind.NonWalkable;

    public virtual void OnInteract(TileOptions options, Player player)
    {

    }

    public virtual void OnEvent()
    {

    }

    public virtual void OnEnter(Player player)
    {

    }

    public virtual string GetDescription(int id, TileOptions options, bool detailed = false)
    {
        return Description;
    }

    public virtual (bool Walkable, bool BlocksVision) GetState(int id, TileOptions options)
    {
        return (false, false);
    }
}

public enum TileKind
{
    Walkable = 0,
    NonWalkable = 1,
    Interactable = 2
}