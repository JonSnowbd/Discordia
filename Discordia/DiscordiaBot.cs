using Discord;
using Discord.WebSocket;
using Discordia.src;
using Discordia.src.triggers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Discordia
{
    public sealed class DiscordiaBot
    {
        public DiscordSocketClient Client;
        public static string Prefix = "!";
        public List<Trigger> Triggers = new List<Trigger>();

        public DiscordiaBot()
        {
            Triggers.Add(new RollTrigger());
            Triggers.Add(new FlipTrigger());
            Triggers.Add(new WhisperTrigger());
            Triggers.Add(new SpecialCoinTrigger());
            Triggers.Add(new JobTrigger());
            Triggers.Add(new EchoTrigger());
            Triggers.Add(new PetTrigger());
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
                    foreach(Trigger Trig in Triggers)
                    {
                        HelpLine += Trig.HelpLine;
                        HelpLine += "\n";
                    }
                    await Message.Channel.SendMessageAsync(HelpLine);
                    return;
                }

                // Work through each trigger, if the activator is the same as the given Word, run its OnTrigger.
                foreach (Trigger Trig in Triggers)
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
