using System.Numerics;

namespace DungeonEscape.Logic.Actions;
public class InspectAction : PlayerAction
{
	public Vector2 Target { get; set; }

	public InspectAction(int playerId, Vector2 target) : base(playerId)
	{
		Target = target;
	}
}
