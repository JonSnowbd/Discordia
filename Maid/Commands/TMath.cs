using Discord.WebSocket;
using Maid.Utility;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NCalc;

namespace Maid.Commands
{
    class TMath : ITrigger
    {
        private static Regex QuickDetect = new Regex(@"{.*?}", RegexOptions.Compiled);
        private static Regex CaptureRegex = new Regex(@"{(.*?)}", RegexOptions.Compiled);
        public MaidCore Bot { get; set; }
        public TriggerMethod Activator { get; set; } = TriggerUtil.ByRegex(QuickDetect);
        public string HelpLine { get; set; } = "**{any mathematical expression}** - Put this anywhere in your message to do quick math";
        public string[] Examples { get; set; } = new string[]
        {
            "Crafting is simple, you just need {4*10%2} lightning shards."
        };

        public void Destroy(SocketMessage Message)
        {
            Message.Channel.SendMessageAsync("That math broke me somehow. Removing math capabilities until a fix is given.");
        }

        public async Task OnTrigger(SocketMessage Message)
        {
            string Reply = "Parsing math interpolations \n";
            int cycle = 1;
            foreach(Match x in CaptureRegex.Matches(Message.Content))
            {
                try
                {
                    Reply += "**" + cycle + " evaluated to : ** " + new Expression(x.Value.ToString()).Evaluate() + "\n";
                    cycle++;
                }
                catch
                {
                    Reply += "**" + cycle +  "** N/A Could not parse.";
                }
                
            }
            await Message.Channel.SendMessageAsync(Reply);
        }
    }
}
