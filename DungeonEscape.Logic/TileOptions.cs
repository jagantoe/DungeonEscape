namespace DungeonEscape.Logic;
public class TileOptions
{
	public Dictionary<string, bool?> BoolOptions { get; set; }

	public bool? GetOptions(string key)
	{
		if (BoolOptions.TryGetValue(key, out bool? value)) return value;
		BoolOptions.Add(key, null);
		return null;
	}
	public void SetOption(string key, bool value)
	{
		BoolOptions[key] = value;
	}
}
