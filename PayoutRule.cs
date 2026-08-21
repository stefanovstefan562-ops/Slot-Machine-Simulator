namespace SlotMachineSimulator
{
    public class PayoutRule
    {
        public int Id {get; set;}
        public required Symbol Symbol {get; set;}
        public  int RequiredCount {get; set;}
        public decimal PayoutAmount{get; set;}
    }
}