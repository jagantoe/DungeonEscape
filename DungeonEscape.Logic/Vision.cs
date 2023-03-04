namespace DungeonEscape.Logic;
public static class Vision
{
	public static List<Vector2> MovementGrid = new List<Vector2>();
	private static Dictionary<int, List<Vector2>> VisionGrids = new Dictionary<int, List<Vector2>>();

	static Vision()
	{
		MovementGrid = new List<Vector2>()
		{
			new Vector2(0,-1), // Top
			new Vector2(1,0),  // Right
			new Vector2(0,1),  // Bottom
			new Vector2(-1,0)  // Left
		};

		// 0 Range
		VisionGrids.Add(0, GenerateGrids(0));
		// 1 Range
		VisionGrids.Add(1, GenerateGrids(1));
		// 2 Range
		VisionGrids.Add(2, GenerateGrids(2));
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

	public static List<Vector2> GetVisionGrid(int range = 1)
	{
		if (VisionGrids.TryGetValue(range, out var visionGrid)) return visionGrid;
		visionGrid = GenerateGrids(range);
		VisionGrids.Add(range, GenerateGrids(range));
		return visionGrid;
	}
}
