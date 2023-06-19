namespace DungeonEscape.Logic;
public static class Vision
{
	private static Vector2 Max = Vector2.One;
	private static Vector2 Min = Vector2.One * -1;
	public static List<Vector2> MovementGrid = new List<Vector2>();
	private static List<Vector2> Cross = new List<Vector2>();
	private static Dictionary<int, List<Vector2>> VisionGrids = new Dictionary<int, List<Vector2>>();
	private static Dictionary<int, List<Vector2>> VisionRings = new Dictionary<int, List<Vector2>>();

	static Vision()
	{
		MovementGrid = new List<Vector2>()
		{
			new Vector2(0,-1), // Top
			new Vector2(1,0),  // Right
			new Vector2(0,1),  // Bottom
			new Vector2(-1,0)  // Left
		};

		// Cross
		Cross = MovementGrid.ToList();
		Cross.Add(new Vector2(0, 0));

		// Generate Ranges
		// 0 Range
		VisionGrids.Add(0, GenerateGrids(0));
		// 1 Range
		VisionGrids.Add(1, GenerateGrids(1));
		// 2 Range
		VisionGrids.Add(2, GenerateGrids(2));
		// 3 Range
		VisionGrids.Add(3, GenerateGrids(3));

		// Generate Rings (needs to happen after ranges)
		// 0 Range
		VisionRings.Add(0, GenerateRings(0));
		// 1 Range
		VisionRings.Add(1, GenerateRings(1));
		// 2 Range
		VisionRings.Add(2, GenerateRings(2));
		// 3 Range
		VisionRings.Add(3, GenerateRings(3));
	}
	public static void Init() { }

	private static List<Vector2> GenerateGrids(int range)
	{
		var gridSize = 1 + range * 2;
		var center = new Vector2((gridSize - 1) / 2, (gridSize - 1) / 2);
		var visionGrid = new List<Vector2>();
		for (int y = 0; y < gridSize; y++)
		{
			for (int x = 0; x < gridSize; x++)
			{
				visionGrid.Add(new Vector2(x, y) - center);
			}
		}
		return visionGrid;
	}
	private static List<Vector2> GenerateRings(int range)
	{
		var grid = GetVisionGrid(range);
		if (range == 0) return grid;
		var smallerGrid = GetVisionGrid(range - 1);
		var ring = grid.Where(x => !smallerGrid.Contains(x)).ToList();
		return ring;
	}

	public static List<Vector2> GetVisionGrid(int range)
	{
		if (VisionGrids.TryGetValue(range, out var visionGrid)) return visionGrid;
		visionGrid = GenerateGrids(range);
		VisionGrids.Add(range, visionGrid);
		return visionGrid;
	}
	public static List<Vector2> GetVisionRing(int range)
	{
		if (VisionRings.TryGetValue(range, out var visionRing)) return visionRing;
		visionRing = GenerateRings(range);
		VisionRings.Add(range, visionRing);
		return visionRing;
	}

	public static List<VisionTile> GetVision(Map map, int range, Vector2 pos)
	{
		var grid = GetVisionGrid(range).ToList();
		if (range > 1)
		{
			var notVisible = new Dictionary<Vector2, Vector2>();
			for (int i = 1; i < range; i++)
			{
				var ring = GetVisionRing(i);
				foreach (var tile in ring)
				{
					var blocksVision = map[tile + pos].BlocksVision;
					var targetClamp = blocksVision ? Vector2.Clamp(tile, Min, Max) : notVisible.ContainsKey(tile) ? notVisible[tile] : Vector2.Zero;
					if (targetClamp == Vector2.Zero) continue;
					var target = tile + targetClamp;
					var hidden = Cross.Select(x => x + target).ToList();
					hidden.Remove(tile);
					foreach (var hid in hidden)
					{
						if (Math.Abs(tile.X) != Math.Abs(tile.Y) && Math.Abs(hid.X) == Math.Abs(hid.Y)) continue;
						notVisible.TryAdd(hid, targetClamp);
					}
				}
			}
			grid.RemoveAll(notVisible.Keys.Contains);
		}
		return grid.Select(x => new VisionTile(x + pos, map[x + pos].TileKind)).ToList();
	}
}
