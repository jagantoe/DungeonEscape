using DungeonEscape.Api.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace DungeonEscape.Api.Client.SignalR;

[Authorize]
public class DashboardHub : Hub
{
	private int UserId => Context.User.GetUserId();
	public override Task OnConnectedAsync()
	{
		Groups.AddToGroupAsync(Context.ConnectionId, UserId.ToString());

		return base.OnConnectedAsync();
	}
}