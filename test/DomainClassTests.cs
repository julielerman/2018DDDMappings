using System;
using System.Drawing;
using System.Linq;
using Domain;
using Xunit;

namespace test {
    public class DomainClassTests {

        private static Team CreateTeamAjax () {
            return new Team ("AFC Ajax", "The Lancers", "1900", "Amsterdam Arena");
        }

        [Fact]
        public void NewTeamGetsId () {
            var team = CreateTeamAjax ();
            Assert.NotEqual (Guid.Empty, team.Id);
        }

        [Fact]
        public void TeamAllowsNewPlayer () {
            var team = CreateTeamAjax ();
            var playerWasAdded = team.AddPlayer ("André", "Onana", out string response);
            Assert.Equal ("André Onana", team.Players.First ().Name);

        }

        [Fact]
        public void TeamAllowsMultiplePlayers () {
            var team = CreateTeamAjax ();
            team.AddPlayer ("André", "Onana", out string response);
            team.AddPlayer ("Matthijs", "de Ligt", out response);
            Assert.Equal (2, team.Players.Count ());
        }

        [Fact]
        public void TeamPreventsDuplicatePlayer () {
            var team = CreateTeamAjax ();
            team.AddPlayer ("André", "Onana", out string response);
            team.AddPlayer ("André", "Onana", out response);
            Assert.Single (team.Players);
        }

        [Fact]
        public void TeamReturnsDuplicateMessageForDuplicatePlayer () {
            var team = CreateTeamAjax ();
            team.AddPlayer ("André", "Onana", out string response);
            team.AddPlayer ("André", "Onana", out response);
            Assert.Equal ("Duplicate player", response);
        }

        [Fact]
        public void CanChangeManager () {
            var team = CreateTeamAjax ();
            team.ChangeManagement (new Manager ("Erik", "ten Hag"));
            Assert.Equal ("Erik ten Hag", team.Manager.Name);
        }

        [Fact]
        public void CanSetHomeColors () {
            var team = CreateTeamAjax ();
            team.SpecifyHomeUniformColors (Color.White, Color.Red, Color.Empty, Color.White, Color.Empty, Color.White);
            Assert.Equal (Color.White.Name, team.HomeColors.ShirtPrimary.Name);
        }
     
    }
}