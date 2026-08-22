using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace SlotMachineSimulator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Symbol cherry = new Symbol { Id = 1, Name = "Cherry", PayoutValue = 10 };

            Symbol bell = new Symbol { Id = 2, Name = "Bell", PayoutValue = 15 };

            Symbol seven = new Symbol { Id = 3, Name = "Seven", PayoutValue = 20 };

            Symbol wild = new Symbol { Id = 4, Name = "Wild", PayoutValue = 25 };

            Reel reel1 = new Reel 
            { 
            Id = 1, 
            Symbols = new List<Symbol> { cherry, bell, seven, wild },
            };

            Reel reel2 = new Reel 
            { 
            Id = 2, 
            Symbols = new List<Symbol> { cherry, bell, seven, wild },
            };

            Reel reel3 = new Reel 
            { 
            Id = 3, 
            Symbols = new List<Symbol> { cherry, bell, seven, wild },
            };

            Reel reel4 = new Reel 
            { 
            Id = 4, 
            Symbols = new List<Symbol> { cherry, bell, seven, wild },
            };

            PayoutRule cherryRule = new PayoutRule 
            { 
            Id = 1, 
            Symbol = cherry, 
            RequiredCount = 2, 
            PayoutAmount = 10 
            };

            PayoutRule bellRule = new PayoutRule 
            { 
            Id = 2, 
            Symbol = bell, 
            RequiredCount = 3, 
            PayoutAmount = 15 
            };

            PayoutRule sevenRule = new PayoutRule 
            { 
            Id = 3, 
            Symbol = seven, 
            RequiredCount = 3, 
            PayoutAmount = 20 
            };

            PayoutRule wildRule = new PayoutRule 
            { 
            Id = 4, 
            Symbol = wild, 
            RequiredCount = 4, 
            PayoutAmount = 25 
            };

            SlotMachine slotMachine = new SlotMachine
            {
            Id = 1,
            Reels = new List<Reel> { reel1, reel2, reel3, reel4 },
            PayoutRules = new List<PayoutRule> { cherryRule, bellRule, sevenRule, wildRule }
            };

            using var db = new SlotMachineDbContext();

            Console.WriteLine("Do you have an existing account? (yes/no)");

            string hasAccount = Console.ReadLine()!.ToLower();

            while (hasAccount != "yes" && hasAccount != "no")
            {
                Console.WriteLine("Please answer yes or no.");
                hasAccount = Console.ReadLine()!.ToLower();
            }

            Player player;

            if (hasAccount == "yes")
            {
                Console.Write("Enter your name: ");
                string name = Console.ReadLine()!;
                Player? existingPlayer = db.Players.FirstOrDefault(p => p.Name == name);

                while (existingPlayer == null)
                {
                    Console.WriteLine("Account not found. Please try again:");
                    name = Console.ReadLine()!;
                    existingPlayer = db.Players.FirstOrDefault(p => p.Name == name);
                }
            
                player = existingPlayer;
            }
            else
            {
                Console.Write("Enter your name: ");
                string name = Console.ReadLine()!;

                while (db.Players.Any(p => p.Name == name))
                {
                    Console.WriteLine("This name is already taken. Please choose another:");
                    name = Console.ReadLine()!;
                }

                player = new Player
                {
                    Name = name,
                    Balance = 100,
                    Bets = new List<Bet>()
                };

                db.Players.Add(player);
                db.SaveChanges();
            }

                bool play = true;

                while(play == true)
                {
                    Console.WriteLine("Your balance:" + player.Balance);
                    Console.Write("Enter your bet amount:");

                    string input = Console.ReadLine()!;

                    bool success = decimal.TryParse(input, out decimal betAmount);

                    if (!success)
                    {
                        Console.WriteLine("Invalid input, please enter a number.");
                        continue;
                    }

                    if (player.Balance < betAmount)
                    {
                        Console.WriteLine("Insufficient balance!");
                        continue;
                    }

                    decimal winnings = slotMachine.Play(player, betAmount);

                    Bet bet = new Bet
                    {
                        Amount = betAmount,
                        BetDate = DateTime.Now,
                        Player = player
                    };

                    db.Bets.Add(bet);
                    db.SaveChanges();

                    Console.WriteLine("You won:" + winnings);

                    Console.WriteLine("New balance:" + player.Balance);

                    Console.WriteLine("Play again? (yes/no)");

                    string answer = Console.ReadLine()!.ToLower();

                    while (answer != "yes" && answer != "no")
                    {
                        Console.WriteLine("Please answer yes or no.");
                    
                        answer = Console.ReadLine()!.ToLower();
                    }

                    if (answer == "no")
                    {
                        play = false;
                    }
                    else if (answer == "yes")
                    {
                        play = true;
                    }
                }
        }
    }
}
