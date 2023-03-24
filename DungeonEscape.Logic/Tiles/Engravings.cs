namespace DungeonEscape.Logic;
public abstract class Engraving : Tile
{
	public Engraving(string description, string detailedDescription)
	{
		Name = $"Engraving";
		Description = description;
		DetailedDescription = detailedDescription;
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.PointOfInterest;
	}
	public Engraving(string description)
	{
		Name = $"Engraving";
		Description = description;
		DetailedDescription = description;
		Walkable = false;
		BlocksVision = true;
		TileKind = TileKind.PointOfInterest;
	}

	static Engraving()
	{
		Tiles.ConfigTile(TileType.EndEngraving, new End());
		Tiles.ConfigTile(TileType.Puzzle1, new Puzzle1());
		Tiles.ConfigTile(TileType.Puzzle2, new Puzzle2());
		// Puzzle 3
		Tiles.ConfigTile(TileType.Puzzle3_1, new Puzzle3_1());
		Tiles.ConfigTile(TileType.Puzzle3_2, new Puzzle3_2());
		Tiles.ConfigTile(TileType.Puzzle3_3, new Puzzle3_3());
		// Puzzle 4
		Tiles.ConfigTile(TileType.Puzzle4_1, new Puzzle4_1());
		Tiles.ConfigTile(TileType.Puzzle4_2, new Puzzle4_2());
		Tiles.ConfigTile(TileType.Puzzle4_3, new Puzzle4_3());
		Tiles.ConfigTile(TileType.Puzzle4_4, new Puzzle4_4());
		Tiles.ConfigTile(TileType.Puzzle4_5, new Puzzle4_5());
		Tiles.ConfigTile(TileType.Puzzle4_6, new Puzzle4_6());
		Tiles.ConfigTile(TileType.Puzzle4_7, new Puzzle4_7());
		Tiles.ConfigTile(TileType.Puzzle4_8, new Puzzle4_8());
		Tiles.ConfigTile(TileType.Puzzle4_9, new Puzzle4_9());
		Tiles.ConfigTile(TileType.Puzzle4_10, new Puzzle4_10());
		// Puzzle 5
		Tiles.ConfigTile(TileType.Puzzle5_1, new Puzzle5_1());
		Tiles.ConfigTile(TileType.Puzzle5_2, new Puzzle5_2());
		// Puzzle 6
		Tiles.ConfigTile(TileType.Puzzle6_1, new Puzzle6_1());
		Tiles.ConfigTile(TileType.Puzzle6_2, new Puzzle6_2());
	}
	public static void Init() { }
}
public sealed class Puzzle1 : Engraving { public Puzzle1() : base("Blind Strength", "A strong body makes the path and a strong mental maintains it") { } }
public sealed class Puzzle2 : Engraving { public Puzzle2() : base("Seek Light", "When all are light the passage will be clear") { } }
public sealed class Puzzle3_1 : Engraving { public Puzzle3_1() : base("keigbawhko", "kei gba wh ko") { } }
public sealed class Puzzle3_2 : Engraving { public Puzzle3_2() : base("rebmaeroxzsf", "reb mae rox zs f") { } }
public sealed class Puzzle3_3 : Engraving { public Puzzle3_3() : base("dwtvfcaskp", "dw tv f cas kp") { } }
public sealed class Puzzle4_1 : Engraving { public Puzzle4_1() : base("Two brothers though they share the same blood, their lives could not be more different. One Always did what was right and the other thought only of himself. Good(LEFT) | Bad(RIGHT)") { } }
public sealed class Puzzle4_2 : Engraving { public Puzzle4_2() : base("When the sickness was spreading. The weak will be culled.(LEFT) | The strong take care of the weak.(RIGHT)") { } }
public sealed class Puzzle4_3 : Engraving { public Puzzle4_3() : base("When there was not enough food to go around. We stick together and share what we have.(LEFT) | It's everyone for themselves.(RIGHT)") { } }
public sealed class Puzzle4_4 : Engraving { public Puzzle4_4() : base("A local disaster has displaced some farmers. Empty houses are always easy to plunder.(LEFT) | Their problems are our problems, we help any way we can.(RIGHT)") { } }
public sealed class Puzzle4_5 : Engraving { public Puzzle4_5() : base("The local taxes are being increased again. Those are the rules of the land we live by.(LEFT) | Not impacted if you don't pay any taxes.(RIGHT)") { } }
public sealed class Puzzle4_6 : Engraving { public Puzzle4_6() : base(". Good(LEFT) | Bad(RIGHT)") { } }
public sealed class Puzzle4_7 : Engraving { public Puzzle4_7() : base(". Bad(LEFT) | Good(RIGHT)") { } }
public sealed class Puzzle4_8 : Engraving { public Puzzle4_8() : base(". Bad(LEFT) | Good(RIGHT)") { } }
public sealed class Puzzle4_9 : Engraving { public Puzzle4_9() : base("Follow the same path as your brother did at his 3rd obstacle. (LEFT) | (RIGHT)") { } }
public sealed class Puzzle4_10 : Engraving { public Puzzle4_10() : base("Follow the opposite path your brother followed at his 4th obstacle. (LEFT) | (RIGHT)") { } }
public sealed class Puzzle5_1 : Engraving { public Puzzle5_1() : base("6x W | 5x N | 7x E | 2x S", "Total: 6x W | 5x N | 7x E | 2x S") { } }
public sealed class Puzzle5_2 : Engraving { public Puzzle5_2() : base("7x W | 6x N | 7x E | 2x S", "Total: 7x W | 6x N | 7x E | 2x S") { } }
public sealed class Puzzle6_1 : Engraving { public Puzzle6_1() : base("Feel what you cannot see", "Feel what you cannot see") { } }
public sealed class Puzzle6_2 : Engraving { public Puzzle6_2() : base("The angel weeps because it does not see what you see", "The angel weeps because IT DOES NOT SEE WHAT YOU SEE") { } }
public sealed class End : Engraving { public End() : base("Congratulations you have reached the end, thank you for playing, I hope you had fun!", "Congratulations you have reached the end, thank you for playing, I hope you had fun!") { } }