using Microsoft.EntityFrameworkCore;
using SneakerCollectionAPI.Domain.Entities;
using System.Collections.Generic;

namespace SneakerCollectionAPI.DataAccess
{
    public class SneakerCollectionContext : DbContext
    {
        public SneakerCollectionContext(DbContextOptions<SneakerCollectionContext> options) : base(options)
        { }

        public DbSet<User> Users => Set<User>();
        public DbSet<Sneaker> Sneakers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasMany(u => u.Sneakers)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);

            base.OnModelCreating(modelBuilder);
        }
    }
   
}
