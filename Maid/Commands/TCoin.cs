using Discord.WebSocket;
using Maid.Commands.SpecialCoins;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Maid.Commands
{
    public sealed class TCoin : ITrigger
    {

        SpecialCoinMachine coinState;
        private Random rng = new Random();

        public TCoin() {
            coinState = new SpecialCoinMachine();
            coinState.LoadOrCreate();
        }

        public string[] Compliments =
        {
            "They didn't deserve it.",
            "How doman of you. Pathetic.",
            "This doesn't feel right."
        };

        public string RandomCompliment()
        {
            return Compliments[(int)rng.Next(Compliments.Count())];
        }

        public string Activator { get; set; } = "special";

        public string HelpLine { get; set; } = "**!special** - Let someone know they are loved and special with `!special give @mention with an optional note here` or see the top memesters with `!special list`";

        public void Destroy(SocketMessage Message)
        {
            Message.Channel.SendMessageAsync("Ahh! Problems with my Special coin parade. This is not good, disabling that module. With no survivors.");
        }

        public async Task OnTrigger(SocketMessage Message)
        {
            string command;
            try
            {
                command = Message.Content.Split(' ')[1];
            }catch
            {
                await Message.Channel.SendMessageAsync("Error parsing command.");
                return;
            }

            if(command.ToLower() == "give")
            {
                try
                {
                    SocketUser userToSpecial;

                    string[] args = Message.Content.Split(' ');
                    userToSpecial = Message.MentionedUsers.First();

                    string[] notearray = args.Skip(3).ToArray();

                    string note = String.Join(" ", notearray);

                    if (userToSpecial.Id == Message.Author.Id)
                    {
                        await Message.Channel.SendMessageAsync("You cannot self proclaim specialness. It is a gift given from friend to friend :'D");
                        return;
                    }

                    await Message.Channel.SendMessageAsync("Giving " + userToSpecial.Username + " a special coin. " + RandomCompliment());
                    coinState.GiveCoinToUser(userToSpecial.Id, note);
                }
                catch
                {
                    await Message.Channel.SendMessageAsync("User not found.");
                    return;
                }

                
            }

            // When the command is to LIST the top 'specialists'
            if(command.ToLower() == "list")
            {
                try
                {
                    string Output = "The top 5 specialists are: \n \n";
                    foreach (SpecialUser usr in coinState.Users.OrderByDescending(x => x.Coins).Take(5))
                    {
                        string un;
                        try
                        {
                            un = Message.Channel.GetUserAsync(usr.ID).Result.Username;
                        }
                        catch
                        {
                            un = "Unknown";
                        }
                        Output += "**" + un + "** has *" + usr.Coins + "* special coins! " + RandomCompliment();
                        Output += " Notes:\n";
                        foreach (string note in usr.RecentNotes)
                        {
                            if(note == "" || note == " ")
                            {
                                continue;
                            }
                            Output += "- " + note + "\n";
                        }
                        Output += "\n";
                    }
                    await Message.Channel.SendMessageAsync(Output);
                }
                catch
                {
                    await Message.Channel.SendMessageAsync("There are no special people :(");
                    return;
                }
            }
        }

    }
}
