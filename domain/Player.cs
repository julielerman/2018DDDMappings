using System;
using SharedKernel;

public class Player
{
  //still needed because ctor is using props from the owned type
  private Player(){}
  
  public Player(string firstname, string lastname)
  {
    NameFactory=PersonFullName.Create(firstname,lastname);
      Id=Guid.NewGuid();
  }
  public Guid Id { get; private set; }
  public PersonFullName NameFactory { get;  set; }
  public string Name=>NameFactory.FullName;
}