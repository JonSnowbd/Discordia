using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;

namespace Discordia.src.triggers
{
    class RollTrigger : Trigger
    {

        public string Activator { get; set; } = "roll";

        public string HelpLine { get; set; } = "**!roll** - Roll any sided die. `!roll` to default to 6, `!roll 10` to roll for example a 10 sided die.";

        public string[] UnluckyPhrases =
        {
            "Wow, unlucky. Better luck next time, ",
            "Aww... That's bad luck, ",
            "How unlucky, ",
            "Wew, I wouldn't play hearthstone with that luck, ",
            "Not the best of rolls, ",
            "... Well I mean, if we're going by 'lower=better', youre doing great, "
        };

        public void Destroy(SocketMessage Message)
        {
            Message.Channel.SendMessageAsync("Ahh! Problems with my Rolling capabilities. This is not good, disabling that module. Heesh.");
        }

        public async Task OnTrigger(SocketMessage Message)
        {
            int DieSides;

            try
            {
                DieSides = int.Parse(Message.Content.Split(' ')[1]);
            }
            catch
            {
                DieSides = 6;
            }

            int RNG = new Random().Next(1, DieSides + 1);
            await Message.Channel.SendMessageAsync("Rolling a " + DieSides + " sided die. I rolled a **" + RNG.ToString() + "**.");
            if(RNG < DieSides / 3)
            {
                string Name = Message.Author.Username;
                string UnluckyPhrase = UnluckyPhrases[new Random().Next(0, UnluckyPhrases.Length)];
                await Message.Channel.SendMessageAsync(UnluckyPhrase+Name+".");
            }
        }

    }
}
