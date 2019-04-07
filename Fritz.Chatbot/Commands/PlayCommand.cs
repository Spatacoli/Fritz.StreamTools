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
    class PlayCommand : IBasicCommand
    {
        private readonly IConfiguration Configuration;

        public ILogger Logger { get; }
        public IHubContext<AttentionHub, IAttentionHubClient> HubContext { get; }
        public string Trigger => "play";
			
        public PlayCommand(IConfiguration configuration, IHubContext<AttentionHub, IAttentionHubClient> hubContext, ILoggerFactory loggerFactory)
        {
            this.Configuration = configuration;
            this.Logger = loggerFactory.CreateLogger("PlayCommand");

            this.HubContext = hubContext;
        }

        public string Description => "Play an audio clip to entertain and wow viewers.";

#if DEBUG
        public TimeSpan? Cooldown => TimeSpan.FromSeconds(10);
#else
		public TimeSpan? Cooldown => TimeSpan.Parse(Configuration["FritzBot:PlayCommand:Cooldown"]);
#endif
        public async Task Execute(IChatService chatService, string userName, ReadOnlyMemory<char> rhs)
        {
					if (rhs.IsEmpty)
					{
						return;
					}
					var soundName = rhs.ToString();
					await this.HubContext.Clients.All.PlaySound(soundName);

					var attentionText = Configuration["FritzBot:PlayCommand:TemplateText"];

					await chatService.SendMessageAsync(string.Format(attentionText, soundName, userName));
        }
    }
}
