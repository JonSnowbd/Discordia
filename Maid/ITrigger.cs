using Discord.WebSocket;
using System.Threading.Tasks;

namespace Maid
{
    public delegate bool TriggerMethod(string message);
    public interface ITrigger
    {
        MaidCore Bot { get; set; }
        TriggerMethod Activator { get; set; }
        string HelpLine { get; set; }
        string[] Examples { get; set; }
        Task OnTrigger(SocketMessage Message);
        void Destroy(SocketMessage Message);
    }
}
