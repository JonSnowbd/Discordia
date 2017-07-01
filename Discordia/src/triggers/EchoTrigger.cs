﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.WebSocket;

namespace Discordia.src.triggers
{
    class EchoTrigger : Trigger
    {
        public string Activator { get; set; } = "echo";
        public string HelpLine { get; set; } = "**!echo** - Makes the bot talk into the mentioned channel. `!echo #general Hey guys.`";

        public void Destroy(SocketMessage Message)
        {
            Message.Channel.SendMessageAsync("Echo broke? Hmm. Disabling that until master fixes it.");
        }

        public async Task OnTrigger(SocketMessage Message)
        {
            try
            {
                ISocketMessageChannel x = (ISocketMessageChannel)Message.MentionedChannels.First();
                await x.SendMessageAsync(String.Join(" ", Message.Content.Split(' ').Skip(2)));
            }
            catch
            {
                await Message.Channel.SendMessageAsync("Failed to echo into that channel.");
            }
        }
    }
}