namespace DungeonEscape.Logic;
public abstract class ActResult
{
	public ActionResultType Type { get; set; }
	public string Result { get; set; }
	public ActResult(ActionResultType type, string result)
	{
		Type = type;
		Result = result;
	}
}
public sealed class ActionResult : ActResult
{
	public ActionResult(string result) : base(ActionResultType.Action, result) { }
}
public sealed class SuccessResult : ActResult
{
	public SuccessResult(string result) : base(ActionResultType.Success, result) { }
}
public sealed class GeneralResult : ActResult
{
	public GeneralResult(string result) : base(ActionResultType.General, result) { }
}
public sealed class ErrorResult : ActResult
{
	public ErrorResult(string result) : base(ActionResultType.Error, result) { }

	public static ErrorResult ConfigMissing(Vector2 pos)
	{
		return new ErrorResult($"Config missing for {pos}, contact admin");
	}
	public static ErrorResult TargetConfigMissing(Vector2 pos, Vector2 target)
	{
		return new ErrorResult($"Target config {target} missing for {pos}, contact admin");
	}
}
public sealed class DeathResult : ActResult
{
	public DeathResult(string result) : base(ActionResultType.Death, result) { }
}
public enum ActionResultType
{
	Action,
	Success,
	General,
	Error,
	Death
}