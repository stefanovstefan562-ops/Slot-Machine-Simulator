namespace SlotMachineSimulator
{
    public class Reel
    {
        public int Id { get; set; }
        public required List<Symbol> Symbols { get; set; }
        public Symbol Spin()
        {
            Random random = new Random();
            int randomIndex = random.Next(0, Symbols.Count);
            return Symbols[randomIndex];
        }
    }
}