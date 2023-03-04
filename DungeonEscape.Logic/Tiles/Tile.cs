namespace DungeonEscape.Logic;
public abstract class Tile
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string DetailedDescription { get; set; }
    public bool Walkable { get; set; } = false;
    public bool BlocksVision { get; set; } = false;
    public TileKind TileKind { get; set; } = TileKind.NonWalkable;

    public Inspection Inspect(bool detailed)
    {
        return new()
        {
            Name = Name,
            Description = detailed ? DetailedDescription : Description
        };
    }
}

public interface IOnEnter
{
    ActResult OnEnter(Vector2 pos, Player player, Map map);
}
public interface IInteract
{
    ActResult Interact(Vector2 pos, Player player, Map map);
}

public enum TileKind
{
    Walkable = 0,
    NonWalkable = 1,
    Interactable = 2
}