using Microsoft.EntityFrameworkCore;

namespace SlotMachineSimulator
{
    public class SlotMachineDbContext : DbContext
    {
        public DbSet<Symbol> Symbols { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Bet> Bets { get; set; }
        public DbSet<PayoutRule> PayoutRules { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer("Server=localhost,1433;Database=SlotMachineDb;User Id=sa;Password=YOUR_PASSWORD_HERE;TrustServerCertificate=True;");
        }
    }
}