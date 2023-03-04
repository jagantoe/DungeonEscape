namespace DungeonEscape.Logic;
public static class Quotes
{
	private static readonly List<string> DeathQuotes = new()
	{
		"Inflated confidence is so easily perforated.",
		"Death springs just as readily from the ground as life does.",
		"An ascendant might be waylaid by a single misplaced step.",
		"Decisions don't kill people, consequences do.",
		"What divides the conqueror from the conquered? Perseverance.",
		"Everyone knows how to die. Some just need a little nudge to get them started.",
		"Apparently this is the ending you deserve",
		"Justice is served",
		"No destiny is forged without a little breakage.",
		"Time may be slowed but never slain.",
		"You are condemned",
		"There is no escape",
		"There's no shame in falling down. True shame is to not stand up again!",
	};
	private static readonly List<string> ResetQuotes = new()
	{
		"How differently we would live if we knew when to die.",
		"A simple action yet the consequences can be so dire.",
		"No judge, no jury, just the executioner.",
		"Are you a martyr?",
	};

	public static string GetDeathQuote()
	{
		return DeathQuotes[Random.Shared.Next(DeathQuotes.Count)];
	}
	public static string GetResetQuote()
	{
		return ResetQuotes[Random.Shared.Next(ResetQuotes.Count)];
	}
}
