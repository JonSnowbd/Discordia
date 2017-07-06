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

        public static string Prefix = "!";
        public bool Running = true;
        
        public MaidCore()
        {
            // Hook up all default triggers.
            AddCommand(new TRoll());
            AddCommand(new TFlip());
            AddCommand(new TCoin());
            AddCommand(new TJob());
            AddCommand(new TEcho());
            AddCommand(new TPet());
            AddCommand(new T8Ball());
            AddCommand(new TMath());
        }

        public void AddCommand(ITrigger command)
        {
            command.Bot = this;
            Triggers.Add(command);
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
                // Getting the first word, without the Prefix.
                string Word = Message.Content.Split(' ')[0].Substring(Prefix.Length);

                // Implement Help message.
                if (Word.ToLower() == "help")
                {
                    string HelpLine = "";
                    foreach (ITrigger Trig in Triggers)
                    {
                        HelpLine += "" + Trig.HelpLine + " For example: ```";
                        foreach(string example in Trig.Examples)
                        {
                            HelpLine += example + "\n";
                        }
                        HelpLine += "```";

                        // Avoid message capping.
                        if(HelpLine.Length > 1250)
                        {
                            await Message.Channel.SendMessageAsync(HelpLine);
                            HelpLine = "";
                        }
                    }
                    await Message.Channel.SendMessageAsync(HelpLine);
                    return;
                }

                // Work through each trigger, if the trigger method returns true, activate the trigger and return.
                foreach (ITrigger Trig in Triggers)
                {
                    if (Trig.Activator(Message.Content))
                    {
                        try
                        {
                            await Trig.OnTrigger(Message);
                            return;
                        }
                        catch(Exception e)
                        {
                            Trig.Destroy(Message);
                            Triggers.Remove(Trig);
                            Console.WriteLine(e.Message);
                            return;
                        }

                    }
                }
            };

            // Log in and await end.
            await Client.LoginAsync(TokenType.Bot, System.Environment.GetEnvironmentVariable("MaidToken"));
            await Client.StartAsync();
            Console.WriteLine("Logged in");

            // Infinitely run until thread dies.
            await Task.Delay(-1);
        }
    }
}
