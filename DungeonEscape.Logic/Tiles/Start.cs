namespace DungeonEscape.Logic;
public sealed class Start : Tile
{
	public Start()
	{
		Name = "Start";
		Description = "A flat floor";
		DetailedDescription = "";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Interactable;
	}
}
