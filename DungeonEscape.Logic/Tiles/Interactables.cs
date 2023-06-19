namespace DungeonEscape.Logic;

public abstract class Lever : TileWithConfig, IInteract
{
	public Lever()
	{
		Name = $"Lever";
		Description = $"A lever";
		DetailedDescription = $"A lever, it must control something";
		Walkable = true;
		BlocksVision = false;
		TileKind = TileKind.PointOfInterest;
	}

	static Lever()
	{
		Tiles.ConfigTile(TileType.ToggleLever, new ToggleLever());
		Tiles.ConfigTile(TileType.SinglePullLever, new SinglePullLever());
		Tiles.ConfigTile(TileType.TileSwitcherLever, new TileSwitcherLever());
	}
	public static void Init() { }

	public abstract ActResult Interact(Vector2 pos, Player player, Map map);
}

public sealed class ToggleLever : Lever
{
	public override ActResult Interact(Vector2 pos, Player player, Map map)
	{
		var config = map.GetConfig(pos);
		if (config.MissingConfigTarget()) return ErrorResult.ConfigMissing(pos);
		foreach (var target in config.Targets)
		{
			var targetConfig = map.GetConfig(target);
			if (targetConfig is null) return ErrorResult.TargetConfigMissing(pos, target);
			targetConfig.Toggle();
		}
		return new SuccessResult("You pull the lever and hear some gears turning");
	}
}
public sealed class SinglePullLever : Lever
{
	public override ActResult Interact(Vector2 pos, Player player, Map map)
	{
		var config = map.GetConfig(pos);
		if (config is null) return ErrorResult.ConfigMissing(pos);
		else if (config.Active) return new GeneralResult("It's stuck");
		config.Active = true;
		return new SuccessResult("You pull the lever and hear some gears turning");
	}
}
public sealed class TileSwitcherLever : Lever
{
	public override ActResult Interact(Vector2 pos, Player player, Map map)
	{
		var config = map.GetConfig(pos);
		if (config.MissingConfigTarget()) return ErrorResult.ConfigMissing(pos);
		foreach (var target in config.Targets)
		{
			var targetTile = map.GetTileAt(target);
			if (targetTile is null) return ErrorResult.TargetConfigMissing(pos, target);
			else if (targetTile is TileType.ToggleFloorOn) map.ChangeState(target, TileType.ToggleFloorOff);
			else if (targetTile is TileType.ToggleFloorOff) map.ChangeState(target, TileType.ToggleFloorOn);
		}
		return new SuccessResult("You pull the lever and hear some gears turning");
	}
}