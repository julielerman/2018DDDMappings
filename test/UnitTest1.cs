using System;
using System.Drawing;
using System.Linq;
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
            string response = "";
            var playerWasAdded = team.AddPlayer ("André", "Onana", out response);
            Assert.Equal ("André Onana", team.Players.First ().Name.FullName);

        }

        [Fact]
        public void TeamAllowsMultiplePlayers () {
            var team = CreateTeamAjax ();
            string response = "";
            team.AddPlayer ("André", "Onana", out response);
            team.AddPlayer ("Matthijs", "de Ligt", out response);
            Assert.Equal (2, team.Players.Count ());
        }

        [Fact]
        public void TeamPreventsDuplicatePlayer () {
            var team = CreateTeamAjax ();
            string response;
            team.AddPlayer ("André", "Onana", out response);
            team.AddPlayer ("André", "Onana", out response);
            Assert.Equal (1, team.Players.Count ());
        }

        [Fact]
        public void TeamReturnsDuplicateMessageForDuplicatePlayer () {
            var team = CreateTeamAjax ();
            string response;
            team.AddPlayer ("André", "Onana", out response);
            team.AddPlayer ("André", "Onana", out response);
            Assert.Equal ("Duplicate player", response);
        }

        [Fact]
        public void CanChangeManager () {
            var team = CreateTeamAjax ();
            team.ChangeManagement (new Manager ("Erik", "ten Hag"));
            Assert.Equal ("Erik ten Hag", team.Manager.Name.FullName);
        }

        [Fact]
        public void CanSetHomeColorsWhenAwayColorsAreNotSet () {
            var team = CreateTeamAjax ();
            team.SpecifyHomeUniformColors (Color.White, Color.Red, Color.Empty, Color.White, Color.Empty, Color.White, false);
            Assert.Equal (Color.White, team.HomeColors.ShirtPrimary);
        }

        [Fact]
        public void CanSetHomeColorsWhenAwayColorsAreDifferent () {
            var team = CreateTeamAjax ();
            team.SpecifyAwayUniformColors (Color.Blue, Color.Red, Color.Empty, Color.Blue, Color.Empty, Color.Blue, false);
            team.SpecifyHomeUniformColors (Color.White, Color.Red, Color.Empty, Color.White, Color.Empty, Color.White, false);
            Assert.Equal (Color.White, team.HomeColors.ShirtPrimary);
        }
          [Fact]
        public void CannotSetHomeColorsWhenAwayColorsAreSameAndNotForced () {
            var team = CreateTeamAjax ();
            team.SpecifyAwayUniformColors (Color.White, Color.Red, Color.Empty, Color.White, Color.Empty, Color.White, false);
           var response=team.SpecifyHomeUniformColors (Color.White, Color.Red, Color.Empty, Color.White, Color.Empty, Color.White, false);
            Assert.Equal (response,false);
        }
    }
}