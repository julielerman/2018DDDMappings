using System;
using System.Collections.Generic;
using System.Linq;
using SharedKernel;

public class Team {
  public Team (string teamName, string nickname, string yearFounded, string homeStadium):this()
   {
    TeamName = teamName;
    Nickname = nickname;
    YearFounded = yearFounded;
    HomeStadium = homeStadium;
    Id = Guid.NewGuid ();
  }
  private Team(){
    Players=new List<Player>();
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
      if (foundPlayer == null) {
        Players.Add (new Player (firstName, lastname));
        }
      }

      public Manager Manager { get; private set; }
      public UniformColors HomeColors { get; set; }
      public UniformColors AwayColors { get; set; }
      public void ChangeManagement (Manager newManager) {
        {
          if (Manager != null) {
            if (Manager.Name != newManager.Name) {
              Manager.PastTeams.Add (Manager.CurrentTeamId);
            }
          }
          Manager=newManager;
          }

        }
      }