using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SharedKernel;

public class Team {
  public Team (string teamName, string nickname, string yearFounded, string homeStadium) : this () {
    TeamName = teamName;
    Nickname = nickname;
    YearFounded = yearFounded;
    HomeStadium = homeStadium;
    Id = Guid.NewGuid ();
    //newly created team starts out with empty players
    //team retrieved from database including players will have instantiated Players,
    //whereas from db without including players, Players will be null
    Players = new List<Player> ();
  }
  private Team () {
    // Players=new List<Player>();
  }
  public Guid Id { get; private set; }
  public string TeamName { get; private set; }
  public string Nickname { get; private set; }
  public string YearFounded { get; private set; }
  public string HomeStadium { get; private set; } //encapsulate
  public List<Player> Players { get; private set; }
  public bool AddPlayer (string firstName, string lastname, out string response) {
    if (Players == null) {
      //this will need to be tested with integration test
      response = "You must first retrieve this team's existing list of players";
      return false;
    }
    var fullName = PersonFullName.Create (firstName, lastname);
    var foundPlayer = Players.Where (p => p.Name.Equals (fullName)).FirstOrDefault ();
    if (foundPlayer == null) {
      Players.Add (new Player (firstName, lastname));
      response = "Player added to team";
      return true;
    } else {
      response = "Duplicate player";
      return false;
    }
  }

  public Manager Manager { get; private set; }
  public UniformColors HomeColors { get; private set; }
  public UniformColors AwayColors { get; private set; }
  public void ChangeManagement (Manager newManager) {
    {
      if (Manager != null) {
        if (Manager.Name != newManager.Name) {
          Manager.PastTeams.Add (Manager.CurrentTeamId);
        }
      }
      Manager = newManager;
    }
  }
  public bool SpecifyHomeUniformColors (Color shirt1, Color shirt2, Color shirt3, Color shorts1, Color shorts2, Color socks, bool force) {
    var colorSet = new UniformColors (shirt1, shirt2, shirt3, shorts1, shorts2, socks);
    if(AwayColors is null)
    {HomeColors=colorSet;
    return true;}
    if (!colorSet.Equals (AwayColors) || (colorSet.Equals (AwayColors) && force)) {
      HomeColors = colorSet;
      return true;
    } else {
      return false;
    }
  }
   public bool SpecifyAwayUniformColors (Color shirt1, Color shirt2, Color shirt3, Color shorts1, Color shorts2, Color socks, bool force) {
    var colorSet = new UniformColors (shirt1, shirt2, shirt3, shorts1, shorts2, socks);
    if(HomeColors is null)
    {AwayColors=colorSet;
    return true;}
    if (!colorSet.Equals (HomeColors) || (colorSet.Equals (HomeColors) && force)) {
      AwayColors = colorSet;
      return true;
    } else {
      return false;
    }
  }
}