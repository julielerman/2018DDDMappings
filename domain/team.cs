using System;
using System.Collections.Generic;
using System.Linq;
using SharedKernel;

public class Team {
  public Team (string teamName, string nickname, string yearFounded, string homeStadium) {
    TeamName = teamName;
    Nickname = nickname;
    YearFounded = yearFounded;
    HomeStadium = homeStadium;
    Id = Guid.NewGuid ();
  }
  public Guid Id { get; private set; }
  public string TeamName { get; private set; }
  public string Nickname { get; private set; }
  public string YearFounded { get; private set; }
  public string HomeStadium { get; private set; } //encapsulate
  public List<Player> Players { get; private set; }
  public void AddPlayer (string firstName, string lastname) {
      var fullName = PersonFullName.Create (firstName, lastname);
      var foundPlayer = Players.Where (p => p.Name.Equals (fullName)).FirstOrDefault ();
      if (foundPlayer != null) {
        Players.Add (new Player (firstName, lastname));
        }
      }

      public Owner Owner { get; private set; }
      public UniformColors HomeColors { get; set; }
      public UniformColors AwayColors { get; set; }
      public void ChangeOwnership (Owner newOwner) {
        {
          if (Owner.Name != null) {
            if (Owner.Name != newOwner.Name) {
              Owner.PastTeams.Add (Owner.CurrentTeamId);
            }
          }

        }
      }
}