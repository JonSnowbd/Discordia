using System;
using Maid;

namespace MaidConsole
{
    class Program
    {
        static void Main(string[] args) =>
            new MaidCore().Start().GetAwaiter().GetResult();
    }
}