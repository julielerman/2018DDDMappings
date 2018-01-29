using System;
using System.Drawing;
using System.Linq;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace test {
    public class EFCoreSQLiteTests {
        //no need for SQLite ref in this project. The data project uses it by default.
        private static Team CreateTeamAjax () {
            return new Team ("AFC Ajax", "The Lancers", "1900", "Amsterdam Arena");
        }

        [Fact]
        public void CanStoreAndMaterializeImmutableTeamNameFromDataStore () {
            var team = CreateTeamAjax ();
            using (var context = new TeamContext ()) {
                context.Database.EnsureDeleted ();
                context.Database.EnsureCreated ();
                context.Teams.Add (team);
                context.SaveChanges ();
            }
            using (var context = new TeamContext ()) {
                var storedTeam = context.Teams.FirstOrDefault ();
                Assert.Equal ("AFC Ajax", storedTeam.TeamName);
            }
        }

        [Fact]
        public void CanStoreAndRetrievePlayerName () {
            var team = CreateTeamAjax ();
            team.AddPlayer ("André", "Onana", out string response);

            using (var context = new TeamContext ()) {
                context.Database.EnsureDeleted ();
                context.Database.EnsureCreated ();
                context.Teams.Add (team);
                context.SaveChanges ();
            }
            using (var context = new TeamContext ()) {
                //note a current bug:
                //https://github.com/aspnet/EntityFrameworkCore/issues/9210
                //requires a workaround of including the owned entity of an included navigation property
                var storedTeam = context.Teams.Include (t => t.Players).ThenInclude (p => p.NameFactory).FirstOrDefault ();
                Assert.Equal (1, storedTeam.Players.Count ());
                Assert.Equal ("André Onana", storedTeam.Players.First ().Name);

            }
        }

        [Fact]
        public void CanStoreAndRetrieveTeamPlayers () {
            var team = CreateTeamAjax ();
            team.AddPlayer ("André", "Onana", out string response);

            using (var context = new TeamContext ()) {
                context.Database.EnsureDeleted ();
                context.Database.EnsureCreated ();
                context.Teams.Add (team);
                context.SaveChanges ();
            }
            using (var context = new TeamContext ()) {
                var storedTeam = context.Teams.Include (t => t.Players).FirstOrDefault ();
                Assert.Equal (1, storedTeam.Players.Count ());
            }
        }

        [Fact]
        public void TeamPreventsAddingPlayersToExistingTeamWhenPlayersNotInMemory () {
            var team = CreateTeamAjax ();
            team.AddPlayer ("André", "Onana", out string response);

            using (var context = new TeamContext ()) {
                context.Database.EnsureDeleted ();
                context.Database.EnsureCreated ();
                context.Teams.Add (team);
                context.SaveChanges ();
            }
            using (var context = new TeamContext ()) {
                var storedTeam = context.Teams.FirstOrDefault ();
                storedTeam.AddPlayer ("Matthijs", "de Ligt", out response);
                Assert.Equal ("You must first retrieve", response.Substring (0, 23));
            }
        }

        [Fact]
        public void TeamAllowsAddingPlayersToExistingTeamWhenPlayersAreLoaded () {
            var team = CreateTeamAjax ();
            team.AddPlayer ("André", "Onana", out string response);

            using (var context = new TeamContext ()) {
                context.Database.EnsureDeleted ();
                context.Database.EnsureCreated ();
                context.Teams.Add (team);
                context.SaveChanges ();
            }
            using (var context = new TeamContext ()) {
                var storedTeam = context.Teams.Include (t => t.Players).ThenInclude (p => p.NameFactory).FirstOrDefault ();
                storedTeam.AddPlayer ("Matthijs", "de Ligt", out response);
                Assert.Equal (2, storedTeam.Players.Count ());
            }
        }


        [Fact]
        public void CanStoreAndRetrieveManagerTeamHistory () {
            var team = CreateTeamAjax ();
            team.AddPlayer ("André", "Onana", out string response);
            var firstmanager = new Manager ("Marcel", "Keizer");
            team.ChangeManagement (firstmanager);
            team.ChangeManagement (new Manager ("Erik", "ten Hag"));

            using (var context = new TeamContext ()) {
                context.Database.EnsureDeleted ();
                context.Database.EnsureCreated ();
                context.AddRange (team, firstmanager);
                context.SaveChanges ();
            }
            using (var context = new TeamContext ()) {
                var M1 = context.Managers.Include (m => m.PastTeams).FirstOrDefault (m => m.NameFactory.Last == "Keizer");
                var M2 = context.Managers.Include (m => m.PastTeams).FirstOrDefault (m => m.NameFactory.Last == "ten Hag");
                Assert.Equal (new { M1 = "Marcel Keizer", M1Count = 1, M2 = "Erik ten Hag", M2Count = 0 },
                    new { M1 = M1.Name, M1Count = M1.PastTeams.Count, M2 = M2.Name, M2Count = M2.PastTeams.Count });
            }

        }

#if true
      [Fact]  public void CanStoreAndRetrieveTeamManager () {
            var team = CreateTeamAjax ();
            var firstmanager = new Manager ("Marcel", "Keizer");
            team.ChangeManagement (firstmanager);

              using (var context = new TeamContext ()) {
                context.Database.EnsureDeleted ();
                context.Database.EnsureCreated ();
               context.Teams.Add (team);
                context.SaveChanges ();
            }
            using (var context = new TeamContext ()) {
               //remember the bug. have to include the ownedentity of an included type
                var storedTeam = context.Teams.Include (t => t.Manager).ThenInclude(m=>m.NameFactory).FirstOrDefault ();
                Assert.Equal (firstmanager.Name, storedTeam.Manager.Name);
                Assert.Equal (storedTeam.Id, storedTeam.Manager.CurrentTeamId);
            }
        }
#endif
    }
}