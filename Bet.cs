namespace SlotMachineSimulator
{
    public class Bet
    {
        public int Id {get; set;}
        public decimal Amount {get; set;}
        public DateTime BetDate {get; set;}
        public required Player Player {get; set;}
    }
}