using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Maid.Commands
{
    class TFlip : ITrigger
    {
        public string Activator { get; set; } = "flip";

        public string HelpLine { get; set; } = "**!flip** - Flip a coin.";

        public void Destroy(SocketMessage Message)
        {
            Message.Channel.SendMessageAsync("Aww. Flip function just broke... I'm gonna just quickly remove that and message my creator to fix it real quick.");
        }

        public async Task OnTrigger(SocketMessage Message)
        {
            int RNG = new Random().Next(1, 101);
            if(RNG >= 50)
            {
                await Message.Channel.SendMessageAsync("Flipping a coin! **HEADS**! ");
            }else
            {
                await Message.Channel.SendMessageAsync("Flipping a coin! **TAILS**! ");
            }
        }
    }
}
