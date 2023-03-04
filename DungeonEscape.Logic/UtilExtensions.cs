namespace DungeonEscape.Logic;
public static class UtilExtensions
{
	public static bool IsType<TType>(this object obj, out TType type) where TType : class
	{
		type = obj as TType;
		return type != null;
	}
}
