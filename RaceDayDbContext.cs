using Microsoft.EntityFrameworkCore;
using RaceDayAPI.Models;

namespace RaceDayAPI.Data
{
    public class RaceDayDbContext : DbContext
    {
        public RaceDayDbContext(DbContextOptions<RaceDayDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<EventOrganiser> EventOrganisers { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ParticipantEvent> ParticipantEvents { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<PerformanceHistory> PerformanceHistories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ParticipantEvent>()
                .HasOne(pe => pe.Event)
                .WithMany()
                .HasForeignKey(pe => pe.EventID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ParticipantEvent>()
                .HasOne(pe => pe.Category)
                .WithMany()
                .HasForeignKey(pe => pe.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ParticipantEvent>()
                .HasOne(pe => pe.Participant)
                .WithMany()
                .HasForeignKey(pe => pe.ParticipantID)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}