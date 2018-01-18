using System.Collections.Generic;

public class Team
{
  public Team(string teamName, string nickname, string yearFounded, string homeStadium)
{
    TeamName=teamName;
    Nickname=nickname;
    YearFounded=yearFounded;
    HomeStadium=homeStadium;
}
  public int Id { get; private set; }
  public string TeamName  { get; private set; }
  public string Nickname { get; private set; }
  public string YearFounded { get; private set; }
  public string HomeStadium { get; private set; } //encapsulate
  public List<Player> Players { get; set; }
  //for test 7/8, encapsulate Players

  //public List<Game> Games { get; set; }
  
  //Add for Test 3: public Owner Owner { get; set; }
  
 //add for test 9 public UniformColors HomeColors { get; set; }
 //add for test 9 public UniformColors AwayColors { get; set; }
    
}