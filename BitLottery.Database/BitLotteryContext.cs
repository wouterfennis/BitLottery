﻿using BitLottery.Models;
using Microsoft.EntityFrameworkCore;

namespace BitLottery.Database
{
    public class BitLotteryContext : DbContext
    {
        public BitLotteryContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public BitLotteryContext(DbContextOptions<BitLotteryContext> options)
        : base(options)
        { }

        public DbSet<Draw> Draws { get; set; }
        public DbSet<Ballot> Ballots { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<int>("DrawNumbers", schema: "shared")
                .StartsAt(1000)
                .IncrementsBy(1);

            modelBuilder.Entity<Draw>()
                .Property(draw => draw.Number)
                .HasDefaultValueSql("NEXT VALUE FOR shared.DrawNumbers");

            modelBuilder.HasSequence<int>("CustomerNumbers", schema: "shared")
                .StartsAt(1000)
                .IncrementsBy(2);

            modelBuilder.Entity<Customer>()
                .Property(customer => customer.Number)
                .HasDefaultValueSql("NEXT VALUE FOR shared.CustomerNumbers");
        }
    }
}
