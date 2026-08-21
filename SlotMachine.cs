namespace SlotMachineSimulator
{
    public class SlotMachine
    {
        public int Id { get; set; }
        public required List<Reel> Reels { get; set; }
        public required List<PayoutRule> PayoutRules { get; set; }

        public List<Symbol> SpinAll()
        {
            List<Symbol> results = new List<Symbol>();
            foreach (Reel reel in Reels)
            {
                results.Add(reel.Spin());
            }
            return results;
        }

        public Dictionary<Symbol, int> CountSymbols(List<Symbol> results)
        {
            Dictionary<Symbol, int> counts = new Dictionary<Symbol, int>();
            foreach (Symbol symbol in results)
            {
                if (counts.ContainsKey(symbol))
                {
                    counts[symbol]++;
                }
                else
                {
                    counts[symbol] = 1;
                }
            }
            return counts;
        }

        public decimal CalculateWinnings(List<Symbol> results)
        {
            Dictionary<Symbol, int> counts = CountSymbols(results);
            decimal totalWinnings = 0;

            foreach (PayoutRule rule in PayoutRules)
            {
                if (counts.ContainsKey(rule.Symbol) && counts[rule.Symbol] >= rule.RequiredCount)
                {
                    totalWinnings += rule.PayoutAmount;
                }
            }

            return totalWinnings;
        }

        public decimal Play(Player player, decimal betAmount)
        {

                if (player.Balance < betAmount)
            {
                
               return 0;
            }
                else
            {
                player.Balance -= betAmount;
                List<Symbol> results = SpinAll();

                decimal winnings = CalculateWinnings(results);
                player.Balance += winnings;

                return winnings;
            }
        }
    }
}