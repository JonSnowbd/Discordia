using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Discordia.src
{
    public interface Trigger
    {
        string Activator { get; set; }
        string HelpLine { get; set; }
        Task OnTrigger(SocketMessage Message);
        void Destroy(SocketMessage Message);
    }
}
