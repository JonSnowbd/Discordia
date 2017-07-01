namespace Maid.Commands.SpecialCoins
{
    public sealed class SpecialUser
    {
        public ulong ID { get; set; }
        public int Coins { get; set; } = 0;
        public string[] RecentNotes = { "", "" };
    }
}
