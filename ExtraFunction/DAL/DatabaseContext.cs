using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using ExtraFunction.Model;
using User = ExtraFunction.Model.User;
using System;
using ExtraFunction.Utils;
using DayOfWeek = ExtraFunction.Model.DayOfWeek;

namespace ExtraFunction.DAL
{
    public class DatabaseContext : DbContext
    {

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<TermsAndConditions> TermsAndConditions { get; set; } = null!;
        public DbSet<Disclaimers> Disclaimers { get; set; } = null!;
        public DbSet<BubbleMessage> BubbleMessages { get; set; } = null!;
        public DbSet<Schedule> Schedules { get; set; } = null!;

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            //secure connection string later

            optionsBuilder.UseCosmos(Environment.GetEnvironmentVariable("DBUri"),
                           Environment.GetEnvironmentVariable("DbKey"),
                           Environment.GetEnvironmentVariable("DbName"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var converter = new EnumCollectionJsonValueConverter<DayOfWeek>();

            modelBuilder.Entity<User>()
                .ToContainer("Users")
                .HasPartitionKey(c => c.Id);

            modelBuilder.Entity<Admin>()
                .ToContainer("Admins")
                .HasPartitionKey(c => c.Id);
            
            modelBuilder.Entity<TermsAndConditions>()
                .ToContainer("TermsAndConditions")
                .HasPartitionKey(c => c.Id);   
            
            modelBuilder.Entity<Disclaimers>()
                .ToContainer("Disclaimers")
                .HasPartitionKey(c => c.Id);

            modelBuilder.Entity<User>()
                .OwnsMany(u => u.Friends);

            modelBuilder.Entity<User>()
                .OwnsMany(u => u.Achievements);

            modelBuilder.Entity<BubbleMessage>().
                ToContainer("BubbleMessages").
                HasPartitionKey(c => c.Id);

            modelBuilder.Entity<Schedule>().OwnsMany(s => s.Tags);
            modelBuilder
              .Entity<Schedule>()
              .Property(s => s.DaysOfWeek)
              .HasConversion(converter);

        }
    }
}
