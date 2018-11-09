using System;
using System.Drawing;
using System.Linq;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace test {
    public class EFCoreInMemoryTests {

        private static Team CreateTeamAjax () {
            return new Team ("AFC Ajax", "The Lancers", "1900", "Amsterdam Arena");
        }

        [Fact]
        public void CanStoreAndMaterializeImmutableTeamNameFromDataStore () {
            var team = CreateTeamAjax ();
            var options = new DbContextOptionsBuilder<TeamContext> ().UseInMemoryDatabase ("immutableTeamName").Options;
            using (var context = new TeamContext (options)) {
                context.Teams.Add (team);
                context.SaveChanges ();
            }
            using (var context = new TeamContext (options)) {
                var storedTeam = context.Teams.FirstOrDefault ();
                Assert.Equal ("AFC Ajax", storedTeam.TeamName);
            }
        }

        [Fact]
        public void CanStoreAndRetrievePlayerName () {
            var team = CreateTeamAjax ();
            team.AddPlayer ("André", "Onana", out string response);

            var options = new DbContextOptionsBuilder<TeamContext> ().UseInMemoryDatabase ("playername").Options;
            using (var context = new TeamContext (options)) {
                context.Teams.Add (team);
                context.SaveChanges ();
            }
            using (var context = new TeamContext (options)) {
                var storedTeam = context.Teams.Include (t => t.Players).FirstOrDefault ();
                Assert.Single (storedTeam.Players);
                Assert.Equal ("André Onana", storedTeam.Players.First ().Name);
            }
        }

        [Fact]
        public void CanStoreAndRetrieveTeamPlayers () {
            var team = CreateTeamAjax ();
            team.AddPlayer ("André", "Onana", out string response);

            var options = new DbContextOptionsBuilder<TeamContext> ().UseInMemoryDatabase ("storeretrieveplayer").Options;
            using (var context = new TeamContext (options)) {
                context.Teams.Add (team);
                context.SaveChanges ();
            }
            using (var context = new TeamContext (options)) {
                var storedTeam = context.Teams.Include (t => t.Players).FirstOrDefault ();
                Assert.Single (storedTeam.Players);
            }
        }

        [Fact]
        public void TeamPreventsAddingPlayersToExistingTeamWhenPlayersNotInMemory () {
            var team = CreateTeamAjax ();
            team.AddPlayer ("André", "Onana", out string response);

            var options = new DbContextOptionsBuilder<TeamContext> ().UseInMemoryDatabase ("preventplayeronteamwithplayersnotloaded").Options;
            using (var context = new TeamContext (options)) {
                context.Teams.Add (team);
                context.SaveChanges ();
            }
            using (var context = new TeamContext (options)) {
                var storedTeam = context.Teams.FirstOrDefault ();
                storedTeam.AddPlayer ("Matthijs", "de Ligt", out response);
                Assert.Equal ("You must first retrieve", response.Substring (0, 23));
            }
        }

        [Fact]
        public void TeamAllowsAddingPlayersToExistingTeamWhenPlayersAreLoaded () {
            var team = CreateTeamAjax ();
            team.AddPlayer ("André", "Onana", out string response);

            var options = new DbContextOptionsBuilder<TeamContext> ().UseInMemoryDatabase ("allowplayeronteamwithplayersloaded").Options;
            using (var context = new TeamContext (options)) {
                context.Teams.Add (team);
                context.SaveChanges ();
            }
            using (var context = new TeamContext (options)) {
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

            var options = new DbContextOptionsBuilder<TeamContext> ().UseInMemoryDatabase ("storemanagerhistory").Options;
            using (var context = new TeamContext (options)) {
                context.AddRange (team, firstmanager);
                context.SaveChanges ();
            }
            using (var context = new TeamContext (options)) {
                var M1 = context.Managers.Include (m => m.PastTeams).FirstOrDefault (m => m.NameFactory.Last == "Keizer");
                var M2 = context.Managers.Include (m => m.PastTeams).FirstOrDefault (m => m.NameFactory.Last == "ten Hag");
                Assert.Equal (new { M1 = "Marcel Keizer", M1Count = 1, M2 = "Erik ten Hag", M2Count = 0 },
                    new { M1 = M1.Name, M1Count = M1.PastTeams.Count, M2 = M2.Name, M2Count = M2.PastTeams.Count });
            }

        }


        [Fact]
        public void CanStoreAndRetrieveTeamManager () {
            var team = CreateTeamAjax ();
            var firstmanager = new Manager ("Marcel", "Keizer");
            team.ChangeManagement (firstmanager);

            var options = new DbContextOptionsBuilder<TeamContext> ().UseInMemoryDatabase ("CanStoreAndRetrieveTeamManager").Options;
            using (var context = new TeamContext (options)) {
                context.Teams.Add (team);
                context.SaveChanges ();
            }
            using (var context = new TeamContext (options)) {
                var storedTeam = context.Teams.Include ("Manager").Include ("Manager.NameFactory").FirstOrDefault ();
                Assert.Equal (firstmanager.Name, storedTeam.ManagerName);
                var storedManager = context.Teams.Select (t => EF.Property<Manager> (t, "Manager")).FirstOrDefault ();
                Assert.Equal (storedTeam.Id, storedManager.CurrentTeamId);
            }
        }


    }
}