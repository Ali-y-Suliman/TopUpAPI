using Microsoft.EntityFrameworkCore;
using TopUpAPI.Models;

namespace TopUpAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<TopUpBeneficiary> TopUpBeneficiaries { get; set; }
        public DbSet<TopUpOption> TopUpOptions { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UsersTopUpBeneficiaries> UsersTopUpBeneficiaries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints here

            modelBuilder.Entity<User>()
                .HasMany(u => u.UsersTopUpBeneficiaries)
                .WithOne(utb => utb.User)
                .HasForeignKey(utb => utb.UserId);

            modelBuilder.Entity<TopUpBeneficiary>()
                .HasMany(tub => tub.UsersTopUpBeneficiaries)
                .WithOne(utb => utb.TopUpBeneficiary)
                .HasForeignKey(utb => utb.TopUpBeneficiaryId);

            modelBuilder.Entity<UsersTopUpBeneficiaries>()
                .HasMany(utb => utb.Transactions)
                .WithOne(t => t.UsersTopUpBeneficiaries)
                .HasForeignKey(t => t.UsersTopUpBeneficiariesId);

            modelBuilder.Entity<User>()
                    .HasIndex(u => u.Email)
                    .IsUnique();
                    

            // Seed data for TopUpOptions
            modelBuilder.Entity<TopUpOption>().HasData(
                new TopUpOption { Id = 1, Amount = 5.00m },
                new TopUpOption { Id = 2, Amount = 10.00m },
                new TopUpOption { Id = 3, Amount = 20.00m },
                new TopUpOption { Id = 4, Amount = 30.00m },
                new TopUpOption { Id = 5, Amount = 50.00m },
                new TopUpOption { Id = 6, Amount = 75.00m },
                new TopUpOption { Id = 7, Amount = 100.00m }
            );


            // Seed data for TopUpBeneficiaries
            modelBuilder.Entity<TopUpBeneficiary>().HasData(
                new TopUpBeneficiary { Id = 1, Nickname = "Beneficiary1" },
                new TopUpBeneficiary { Id = 2, Nickname = "Beneficiary2" },
                new TopUpBeneficiary { Id = 3, Nickname = "Beneficiary3" },
                new TopUpBeneficiary { Id = 4, Nickname = "Beneficiary4" }
            );  

            // Seed data for Users
            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, Name = "User1", Email = "user1@example.com", IsVerified = true },
                new User { Id = 2, Name = "User2", Email = "user2@example.com", IsVerified = false },
                new User { Id = 3, Name = "User3", Email = "user3@example.com", IsVerified = true },
                new User { Id = 4, Name = "User4", Email = "user4@example.com", IsVerified = false }
            );

            modelBuilder.Entity<TopUpOption>()
            .Property(t => t.Amount)
            .HasPrecision(6, 2);

            modelBuilder.Entity<Transactions>()
                .Property(t => t.TopUpAmount)
                .HasPrecision(6, 2); 

            modelBuilder.Entity<Transactions>()
                .Property(t => t.TopUpFeeAmount)
                .HasPrecision(6, 2); 

            modelBuilder.Entity<Transactions>()
                .Property(t => t.TopUpTotalAmount)
                .HasPrecision(6, 2); 
        }
    }
}
