using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Data {
    public class TeamContext : DbContext {
        public TeamContext (DbContextOptions<TeamContext> options) : base (options) { }

        public DbSet<Team> Teams { get; set; }

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            if (optionsBuilder == null) {
                optionsBuilder.UseSqlite ("Data Source=TeamData.db");
            }
        }
    }
}