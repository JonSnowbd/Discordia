using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discordia.src;

namespace Discordia
{
    class Program
    {
        static void Main(string[] args) =>
            new DiscordiaBot().Start().GetAwaiter().GetResult();
    }
}
