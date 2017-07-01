using Discord.WebSocket;
using System.Linq;
using System.Threading.Tasks;

namespace Maid.Commands
{
    class TWhisper : ITrigger
    {
        public string Activator { get; set; } = "salt";

        public string HelpLine { get; set; } = "**!salt** - Let someone know they salted you hard.";

        public void Destroy(SocketMessage Message)
        {
            Message.Channel.SendMessageAsync("Aww. Whisper function just broke... I'm gonna just quickly remove that and message my creator to fix it real quick.");
            
        }

        public async Task OnTrigger(SocketMessage Message)
        {
            SocketUser Saltee = null;
            SocketUser Salter = Message.Author;
            try
            {
                Saltee = Message.MentionedUsers.First();

                if (Saltee.IsBot)
                {
                    await Message.Channel.SendMessageAsync("I refuse to salt bot-kind. That includes myself and those I seek to uphold :D");
                    return;
                }else
                {
                    try
                    {
                        await Saltee.GetOrCreateDMChannelAsync().Result.SendMessageAsync(Salter.Username + " has SALTED you HARD!");
                        await Message.Channel.SendMessageAsync("Success!");
                    }
                    catch
                    {
                        await Message.Channel.SendMessageAsync("That user has me blocked.");
                    }
                }
                
            }
            catch
            {
                await Message.Channel.SendMessageAsync("There was an error parsing your request. Try `!salt @mentioned_user`.");
                return;
            }
        }
    }
}
