using System;
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
            team.AddPlayer("André","Onana");
            Assert.Equal("André Onana", team.Players.First().Name.FullName);

        }
  [Fact]
        public void TeamAllowsMultiplePlayers () {
            var team = CreateTeamAjax ();
             team.AddPlayer("André","Onana");
            team.AddPlayer("Matthijs","de Ligt");
          
            Assert.Equal(2,team.Players.Count());
        }
        [Fact]
        public void TeamPreventsDuplicatePlayer () {
            var team = CreateTeamAjax ();
             team.AddPlayer("André","Onana");
            team.AddPlayer("André","Onana");
          
            Assert.Equal(1,team.Players.Count());
        }
        [Fact]
        public void CanChangeManager()
        {
            var team = CreateTeamAjax ();
            team.ChangeManagement(new Manager("Erik", "ten Hag"));
            Assert.Equal("Erik ten Hag",team.Manager.Name.FullName);

        }

    }
}