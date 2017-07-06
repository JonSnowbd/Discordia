using Discord.WebSocket;
using Maid.Utility;
using System.Linq;
using System.Threading.Tasks;

namespace Maid.Commands
{
    class TPet : ITrigger
    {
        public TriggerMethod Activator { get; set; } = TriggerUtil.ByName("pet");
        public string HelpLine { get; set; } = "**pet** - Pet anyone but master.";
        public MaidCore Bot { get; set; }
        public string[] Examples { get; set; } = new string[]
        {
            "!pet @Xyrlyn"
        };

        public void Destroy(SocketMessage Message)
        {
            Message.Channel.SendMessageAsync("Petting malfunction. Removing petting capabilities.");
        }

        public async Task OnTrigger(SocketMessage Message)
        {
            try
            {
                SocketUser pettee = Message.MentionedUsers.First();
                SocketUser petter = Message.Author;
                if(pettee.Id == 137267770537541632L)
                {
                    await Message.Channel.SendMessageAsync("I will not pet Floe.");
                    return;
                }
                await Message.Channel.SendMessageAsync("**"+ petter.Username + "** pets **" + pettee.Username +"**");
            }
             catch
            {
                await Message.Channel.SendMessageAsync("Mention someone in your message to pet them.");
            }
        }
    }
}
