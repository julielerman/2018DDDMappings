using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SharedKernel;

namespace Domain {
  public class Team {
    private Team () { }
    public Team (string teamName, string nickname, string yearFounded, string homeStadium) {
      _teamname = teamName;
      Nickname = nickname;
      YearFounded = yearFounded;
      HomeStadium = homeStadium;
      Id = Guid.NewGuid ();
      //newly created team starts out with empty players
      //team retrieved from database including players will have instantiated Players,
      //whereas from db without including players, Players will be null
      _players = new List<Player> ();
    }
    public Guid Id { get; private set; }
    private string _teamname;
    public string TeamName =>_teamname;

    public string Nickname { get; private set; }
    public string YearFounded { get; private set; }
    public string HomeStadium { get; private set; } 
    public IEnumerable<Player> Players => _players.ToList ();

    private ICollection<Player> _players;
    public bool AddPlayer (string firstName, string lastname, out string response) {
      if (_players == null) {
        //this will need to be tested with integration test
        response = "You must first retrieve this team's existing list of players";
        return false;
      }
      var fullName = PersonFullName.Create (firstName, lastname).FullName;
      var foundPlayer = _players.Where (p => p.Name.Equals (fullName)).FirstOrDefault ();
      if (foundPlayer == null) {
        _players.Add (new Player (firstName, lastname));
        response = "Player added to team";
        return true;
      } else {
        response = "Duplicate player";
        return false;
      }
    }

    private Manager Manager =>_manager ;//{get;set;}
    private Manager _manager;
    public string ManagerName=>_manager.Name;

     public UniformColors HomeColors { get; private set; }
    public void ChangeManagement (Manager newManager) {
        if (_manager is null || _manager.Name != newManager.Name) {
          // Manager.PastTeams.Add (new ManagerTeamHistory(Manager.Id,Id));
          //Manager.CurrentTeamId=Guid.Empty();
          _manager?.RemoveFromTeam(Id);
          newManager.BecameTeamManager (Id);
          _manager = newManager;
       
        }
      // newManager.BecameTeamManager(Id);
      // Manager = newManager;
  //   }
   }
  public void SpecifyHomeUniformColors (Color shirt1, Color shirt2, Color shirt3, Color shorts1, Color shorts2, Color socks) {
    HomeColors = new UniformColors (shirt1, shirt2, shirt3, shorts1, shorts2, socks);
    
  }
 
}
}