using System;
using System.Drawing;
using System.Linq;
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
            modelBuilder.Entity<ManagerTeamHistory> ().HasKey (m => new { m.ManagerId, m.TeamId });
            modelBuilder.Entity<Team> ()
                .Property (b => b.TeamName)
                .HasField ("_teamname");
         
            modelBuilder.Entity<Team> ().Property<DateTime> ("Created");
            modelBuilder.Entity<Team> ().Property<DateTime> ("LastModified");

            var navigation = modelBuilder.Entity<Team> ()
                .Metadata.FindNavigation (nameof (Team.Players));
            navigation.SetPropertyAccessMode (PropertyAccessMode.Field);

            modelBuilder.Entity<Player> ().OwnsOne (p => p.NameFactory);
            modelBuilder.Entity<Manager> ().OwnsOne (p => p.NameFactory);
            modelBuilder.Entity<Team> ()
                .HasOne (typeof (Manager), "Manager").WithOne ()
                .HasForeignKey (typeof (Manager), "CurrentTeamId");

            modelBuilder.Entity<UniformColors> ()
                .Property (u => u.ShirtPrimary).HasConversion (c => c.Name, s => Color.FromName (s));
            modelBuilder.Entity<UniformColors> ()
                .Property (u => u.ShirtSecondary).HasConversion (c => c.Name, s => Color.FromName (s));
            modelBuilder.Entity<UniformColors> ()
                .Property (u => u.ShirtTertiary).HasConversion (c => c.Name, s => Color.FromName (s));
            modelBuilder.Entity<UniformColors> ()
                .Property (u => u.ShortsPrimary).HasConversion (c => c.Name, s => Color.FromName (s));
            modelBuilder.Entity<UniformColors> ()
                .Property (u => u.ShortsSecondary).HasConversion (c => c.Name, s => Color.FromName (s));
            modelBuilder.Entity<UniformColors> ()
                .Property (u => u.Socks).HasConversion (c => c.Name, s => Color.FromName (s));

            //from the docs: 
            //There is currently no way to specify in one place that every property
            //of a given type must use the same value converter.
            //This feature will be considered for a future release.
        }
        public override int SaveChanges () {
            var timestamp = DateTime.Now;
            foreach (var entry in ChangeTracker.Entries ()
                    .Where (e => e.Entity is Team &&
                        (e.State == EntityState.Added || e.State == EntityState.Modified)
                    )) {
                entry.Property ("LastModified").CurrentValue = timestamp;
                //PUT THE DARNED IF STATEMENTY IN HERE!!
                entry.Property ("Created").CurrentValue = timestamp;
            }
            return base.SaveChanges ();
        }
    }
}