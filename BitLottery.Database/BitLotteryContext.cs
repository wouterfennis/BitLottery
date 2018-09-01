using BitLottery.Models;
using Microsoft.EntityFrameworkCore;

namespace BitLottery.Database
{
    public class BitLotteryContext : DbContext
    {
        public BitLotteryContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Draw> Draws { get; set; }
        public DbSet<Ballot> Ballots { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = @"Server=localhost;Database=BitLottery;Trusted_Connection=True;";
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
