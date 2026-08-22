namespace SlotMachineSimulator
{
    public class Player
    {
        public int Id {get; set;}
        public required string Name {get; set;}
        public decimal Balance {get; set;}
        public required List<Bet> Bets {get; set;}
    }
}
