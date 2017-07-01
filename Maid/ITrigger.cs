using Discord.WebSocket;
using System.Threading.Tasks;

namespace Maid
{
    public interface ITrigger
    {
        string Activator { get; set; }
        string HelpLine { get; set; }
        Task OnTrigger(SocketMessage Message);
        void Destroy(SocketMessage Message);
    }
}
