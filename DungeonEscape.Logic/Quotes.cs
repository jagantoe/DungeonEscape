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
		"That’s enough now... No one will blame you.",
		"What are you still doing here?",
		"We await thy return.",
		"What a rotten place to die...",
		"What now?",
		"Plip, plop, plip, plop...",
		"Splish, splash, splish, splash...",
		"This night is long, but morning always comes.",
		"Are your feet as fat as your wits?",
		"Have mercy on the poor bastard.",
		"What were you thinking?",
		"You must accept your death. Forget the dream, wake up to the morning. Be freed from the night...",
		"This spot marks our grave, but you may rest here too, if you would like..."
	};
	private static readonly List<string> ResetQuotes = new()
	{
		"How differently we would live if we knew when to die.",
		"A simple action yet the consequences can be so dire.",
		"No judge, no jury, just the executioner.",
		"Are you a martyr?",
		"Turn back.",
		"You can't go on like this...",
		"You shall not abscond your crimes.",
		"Such impudence.",
		"Very well. Feel the spreading corruption burn.",
		"Ooh, what's that smell... The sweet blood",
		"You damned fool... You let the blood get the best of you...",
		"Nothing changes, such is the nature of man...",
		"What a sham you were, from beginning to end.",
		"Unending death awaits those who pry into the unknown...",
		"Is it the blood, or are you just raving mad?",
		"You are no more than a beast. I should've known..."
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
