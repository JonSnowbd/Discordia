using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecialCoins
{
    public sealed class SpecialUser
    {
        public ulong ID { get; set; }
        public int Coins { get; set; } = 0;
        public string[] RecentNotes = { "", "" };
    }
}
