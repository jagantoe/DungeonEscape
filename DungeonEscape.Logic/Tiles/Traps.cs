﻿namespace DungeonEscape.Logic;
public abstract class Trap : Tile, IOnEnter
{
	static Trap()
	{
		Tiles.ConfigTile(TileType.VisibleSpikes, new VisibleSpikes());
		Tiles.ConfigTile(TileType.VisibleFire, new VisibleFire());
		Tiles.ConfigTile(TileType.Spikes, new Spikes());
		Tiles.ConfigTile(TileType.FirePlate, new FirePlate());
		Tiles.ConfigTile(TileType.Pit, new Pit());
		Tiles.ConfigTile(TileType.FalseFloor, new FalseFloor());
		Tiles.ConfigTile(TileType.FireWall, new FireWall());
	}
	public static void Init() { }

	public abstract ActResult OnEnter(Vector2 pos, Player player, Map map);
}
public sealed class VisibleSpikes : Trap, IStanding
{
	public VisibleSpikes()
	{
		Name = "Spikes";
		Description = "Spikes shooting out of the ground";
		DetailedDescription = "Spikes shooting out of the ground, best not to step on them";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Danger;
	}

	public override ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		player.CurrentHealth -= 3;
		return new GeneralResult("You stand on spikes and hurt yourself");
	}
}
public sealed class VisibleFire : Trap, IStanding
{
	public VisibleFire()
	{
		Name = "Fire";
		Description = "A burst of first erupts from the ground";
		DetailedDescription = "A burst of first erupts from the ground, don't get burnt";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Danger;
	}

	public override ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		player.CurrentHealth -= 4;
		return new GeneralResult("The fire engulfs you and leaves you scorched");
	}
}
public sealed class Spikes : Trap
{
	public Spikes()
	{
		Name = Floor.Name;
		Description = "A perforated floor";
		DetailedDescription = "A perforated floor, best to avoid it lest you also wish to be perforated";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}

	public override ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		player.CurrentHealth -= 1;
		return new GeneralResult("Spikes shoot out of the ground");
	}
}
public sealed class FirePlate : Trap
{
	public FirePlate()
	{
		Name = Floor.Name;
		Description = "A slightly raised floor";
		DetailedDescription = "A scorching floor, best not stay too long";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}

	public override ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		player.CurrentHealth -= 2;
		return new GeneralResult("Fire bursts from the ground");
	}
}
public sealed class Pit : Trap, IStanding
{
	public Pit()
	{
		Name = "Pit";
		Description = "A bottomless pit";
		DetailedDescription = "A bottomless pit and yet death waits below";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Danger;
	}

	public override ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		player.CurrentHealth = 0;
		return new GeneralResult("You fall to your death");
	}
}
public sealed class FalseFloor : Trap
{
	public FalseFloor()
	{
		Name = Floor.Name;
		Description = Floor.Description;
		DetailedDescription = "It LOOKS like a flat floor";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.Walkable;
	}

	public override ActResult OnEnter(Vector2 pos, Player player, Map map)
	{
		player.CurrentHealth = 0;
		return new GeneralResult("Where there was a floor there is none now");
	}
}
public sealed class FireWall : Tile, IInteract
{
	public FireWall()
	{
		Name = Wall.Name;
		Description = "A wall with a round hole";
		DetailedDescription = "A wall with a round hole, it's covered in soot";
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.PointOfInterest;
	}

	public ActResult Interact(Vector2 pos, Player player, Map map)
	{
		player.CurrentHealth -= 5;
		return new GeneralResult("A burst of fire erupts from the hole");
	}
}