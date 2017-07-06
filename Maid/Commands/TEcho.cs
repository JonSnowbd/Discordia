using Discord.WebSocket;
using Maid.Utility;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Maid.Commands
{
    class TEcho : ITrigger
    {
        public MaidCore Bot { get; set; }

        public TriggerMethod Activator { get; set; } = TriggerUtil.ByName("echo");
        public string HelpLine { get; set; } = "**echo** - Makes the bot mimic your message in the mentioned channel.";
        public string[] Examples { get; set; } = new string[]
        {
            "!echo #general Hey whats up guys."
        };

        public void Destroy(SocketMessage Message)
        {
            Message.Channel.SendMessageAsync("Echo broke? Hmm. Disabling that until master fixes it.");
        }

        public async Task OnTrigger(SocketMessage Message)
        {
            try
            {
                ISocketMessageChannel x = (ISocketMessageChannel)Message.MentionedChannels.First();
                await x.SendMessageAsync(String.Join(" ", Message.Content.Split(' ').Skip(2)));
            }
            catch
            {
                await Message.Channel.SendMessageAsync("Failed to echo into that channel.");
            }
        }
    }
}
