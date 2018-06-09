using System;
using System.Drawing;
using System.Linq;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace test
{
    public class EFCoreSQLiteTests
    {

        //no need for SQLite ref in this project. The data project uses it by default.
        private static Team CreateTeamAjax()
        {
            return new Team("AFC Ajax", "The Lancers", "1900", "Amsterdam Arena");
        }

        [Fact]
        public void CanStoreAndRetrieveHomeColors()
        {
            var team = CreateTeamAjax();
            team.SpecifyHomeUniformColors(Color.Blue, Color.Red, Color.Empty, Color.White, Color.Empty, Color.White);

            using (var context = new TeamContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Teams.Add(team);
                context.SaveChanges();
            }

            Team storedTeam;
            using (var context = new TeamContext())
            {
                storedTeam = context.Teams.Include(t => t.HomeColors).FirstOrDefault();
            }

            Assert.Equal(Color.Blue, storedTeam.HomeColors.ShirtPrimary);
        }

        [Fact]
        public void CanStoreAndRetrievePlayer()
        {
            var team = CreateTeamAjax();
            Assert.True(team.AddPlayer("Romelu", "Lukaku", out var response));
            var player = team.Players.First();

            using (var context = new TeamContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Teams.Add(team);
                context.SaveChanges();
            }

            Team teamFound;
            using (var context = new TeamContext())
            {
                teamFound = context.Teams.Include(t => t.Players).FirstOrDefault(t => t.Id == team.Id);
            }

            Assert.Equal(player.Name, teamFound.Players.First().Name);
        }


        [Fact]
        public void CanStoreAndUpdatePlayer()
        {
            var firstname = "Romelu";
            var lastname = "Lukaku";
            var firstname2 = "Eden";
            var lastname2 = "Hazard";

            var team = CreateTeamAjax();
            Assert.True(team.AddPlayer(firstname, lastname, out var response));
            var player = team.Players.First();

            using (var context = new TeamContext())
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Teams.Add(team);
                context.SaveChanges();
            }

            Team teamFound;
            using (var context = new TeamContext())
            {
                teamFound = context.Teams.Include(t => t.Players).FirstOrDefault(t => t.Id == team.Id);
            }

            Assert.Equal(player.Name, teamFound.Players.First().Name);
            Assert.True(teamFound.RemovePlayer(firstname, lastname, out response));
            Assert.True(teamFound.AddPlayer(firstname2, lastname2, out response));

            using (var context = new TeamContext())
            {
                var storedTeam = context.Teams.Include(t => t.Players).FirstOrDefault(t => t.Id == team.Id);
                context.Update(storedTeam);
                storedTeam.SyncPlayers(teamFound.Players);
                context.SaveChanges();
            }

            using (var context = new TeamContext())
            {
                teamFound = context.Teams.Include(t => t.Players).FirstOrDefault(t => t.Id == team.Id);
            }

            Assert.Equal(new Player(firstname2, lastname2).Name, teamFound.Players.First().Name);
        }
    }
}