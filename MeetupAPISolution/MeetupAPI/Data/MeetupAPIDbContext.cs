using MeetupAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetupAPI.Data
{
    public class MeetupAPIDbContext : DbContext
    {
        public MeetupAPIDbContext(DbContextOptions<MeetupAPIDbContext> options)
            : base(options)
        {
            _ = Database.EnsureCreated();
        }

        public DbSet<MeetupModel> MeetupModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MeetupModel>()
                .HasMany(x => x.Speakers)
                .WithOne(x => x.MeetupModel)
                .HasForeignKey(x => x.MeetupModelId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MeetupModel>()
                .Property(x => x.Budget)
                .HasPrecision(10, 2);

            modelBuilder.Entity<MeetupModel>()
                .HasKey(x => x.Id);
        }
    }
}
