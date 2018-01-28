using System;
using Domain;
using Microsoft.EntityFrameworkCore;
using SharedKernel;

namespace Data {
    public class TeamContext : DbContext {
        public TeamContext (DbContextOptions<TeamContext> options) : base (options) { }
        public TeamContext () {

        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Manager> Managers { get; set; }
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlite ("Data Source=TeamData.db");
            }
        }
        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            // modelBuilder.Ignore<ManagerTeamHistory>();
            modelBuilder.Entity<ManagerTeamHistory> ().HasKey (m => new { m.ManagerId, m.TeamId });
            modelBuilder.Ignore<UniformColors> ();
            // modelBuilder.Ignore<PersonFullName>();
            modelBuilder.Entity<Team> ()
                .Property (b => b.TeamName)
                .HasField ("_teamname");

            var navigation = modelBuilder.Entity<Team> ()
                .Metadata.FindNavigation (nameof (Team.Players));
            navigation.SetPropertyAccessMode (PropertyAccessMode.Field);

            modelBuilder.Entity<Player> ().OwnsOne (p => p.NameFactory);
            modelBuilder.Entity<Manager> ().OwnsOne (p => p.NameFactory);

        }
    }
}