using Fritz.StreamLib.Core;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fritz.StreamTools.Hubs
{
	public interface IAttentionHubClient
	{

		// Cheer 200 parithon 12/18/2018
		// Cheer 500 pharewings 12/18/2018
		Task AlertFritz();
		Task PlaySound(string soundName);
		Task StopSound();
		Task ClientConnected(string connectionId);
	}

	public class AttentionHub : Hub<IAttentionHubClient>
	{
		public override Task OnConnectedAsync()
		{
			return this.Clients.Others.ClientConnected(this.Context.ConnectionId);
		}

		public Task PlaySound(string soundName) 
		{
			return this.Clients.Others.PlaySound(soundName);
		}

		public Task AlertFritz()
		{
			return this.Clients.Others.AlertFritz();
		}

		public Task StopSound()
		{
			return this.Clients.Others.StopSound();
		}
	}
}
