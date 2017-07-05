using Discord.WebSocket;
using System.Threading.Tasks;

namespace Maid
{
    public interface ITrigger
    {
        MaidCore Bot { get; set; }
        string Activator { get; set; }
        string HelpLine { get; set; }
        string[] Examples { get; set; }
        Task OnTrigger(SocketMessage Message);
        void Destroy(SocketMessage Message);
    }
}
