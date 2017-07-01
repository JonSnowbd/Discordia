using Discord;
using Discord.WebSocket;
using Maid.Commands;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maid
{
    public sealed class MaidCore
    {
        private DiscordSocketClient Client;
        private List<ITrigger> Triggers = new List<ITrigger>();

        public string Prefix = "!";
        public bool Running = true;
        
        public MaidCore()
        {
            // Hook up all default triggers.
            Triggers.Add(new TRoll());
            Triggers.Add(new TFlip());
            Triggers.Add(new TWhisper());
            Triggers.Add(new TCoin());
            Triggers.Add(new TJob());
            Triggers.Add(new TEcho());
            Triggers.Add(new TPet());
        }

        public async Task Start()
        {
            Client = new DiscordSocketClient();

            // Hook up the message receiver, this is an anonymous
            // Lambda that spreads the message.
            Client.MessageReceived += async (Message) =>
            {
                // If it is our own message, cancel.
                if (Message.Author.IsBot) { return; }

                // If it doesnt start with our prefix, cancel.
                if (!Message.Content.StartsWith(Prefix)) { return; }

                // Getting the first word, without the Prefix.
                string Word = Message.Content.Split(' ')[0].Substring(Prefix.Length);

                // Implement Help message.
                if (Word.ToLower() == "help")
                {
                    string HelpLine = "";
                    foreach (ITrigger Trig in Triggers)
                    {
                        HelpLine += Trig.HelpLine;
                        HelpLine += "\n";
                    }
                    await Message.Channel.SendMessageAsync(HelpLine);
                    return;
                }

                // Work through each trigger, if the activator is the same as the given Word, run its OnTrigger.
                foreach (ITrigger Trig in Triggers)
                {
                    if (Trig.Activator.ToLower() == Word.ToLower())
                    {
                        try
                        {
                            await Trig.OnTrigger(Message);
                            return;
                        }
                        catch
                        {
                            Trig.Destroy(Message);
                            Triggers.Remove(Trig);
                            return;
                        }

                    }
                }
            };

            // Log in and await end.
            await Client.LoginAsync(TokenType.Bot, System.Environment.GetEnvironmentVariable("DissySecret"));
            await Client.StartAsync();
            Console.WriteLine("Logged in");
            await Task.Delay(-1);
        }
    }
}
