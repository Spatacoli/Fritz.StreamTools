using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fritz.StreamLib.Core;
using Fritz.StreamTools.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Fritz.Chatbot.Commands
{
  class StopCommand : IBasicCommand
  {
	private readonly IConfiguration Configuration;

	public ILogger Logger { get; }
	public IHubContext<AttentionHub, IAttentionHubClient> HubContext { get; }

	public StopCommand(IConfiguration configuration, IHubContext<AttentionHub, IAttentionHubClient> hubContext, ILoggerFactory loggerFactory)
	{
	  this.Configuration = configuration;
	  this.Logger = loggerFactory.CreateLogger("AttentionCommand");

	  this.HubContext = hubContext;
	}
	public string Trigger => "stop";

	public string Description => "Stop playing the currently running sound";
#if DEBUG
	public TimeSpan? Cooldown => TimeSpan.FromSeconds(10);
#else
		public TimeSpan? Cooldown => TimeSpan.Parse(Configuration["FritzBot:PlayCommand:Cooldown"]);
#endif

	public async Task Execute(IChatService chatService, string userName, ReadOnlyMemory<char> rhs)
	{
	  await this.HubContext.Clients.All.StopSound();

	  var attentionText = Configuration["FritzBot:PlayCommand:StopTemplateText"];

	  await chatService.SendMessageAsync(string.Format(attentionText, userName));
	}
  }
}
