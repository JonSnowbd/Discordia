using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;
using Maid.Utility;

namespace Maid.Commands
{
    class T8Ball : ITrigger
    {
        public MaidCore Bot { get; set; }
        public string Activator { get; set; } = "8ball";
        public string HelpLine { get; set; } = "Shake up an 8ball and get your answers.";
        public string[] Examples { get; set; } = new string[]
        {
            "!8ball will sharu ever get to tank for the FC"
        };

        private string[] Responses = new string[]
        {
            "Absolutely!",
            "One day, but not now.",
            "Yes, but it will not be easy",
            "Possibly, but its not looking good.",
            "Definitely not, don't even try.",
            "Not in your lifetime."
        };

        public void Destroy(SocketMessage Message)
        {
            Message.Channel.SendMessageAsync("The 8ball... Just broke. I don't know how you did it, but I'm impressed. Disabling 8ball.");
        }

        public async Task OnTrigger(SocketMessage Message)
        {
            await Message.Channel.SendMessageAsync(Rng.Choice(Responses));
        }
    }
}
