using System;
using SharedKernel;

public class Player
{
  public Player(string firstname, string lastname)
  {
    NameFactory=PersonFullName.Create(firstname,lastname);
      Id=Guid.NewGuid();
  }
 private Player(){}

  public Guid Id { get; private set; }
  private PersonFullName NameFactory;// { get; private set; }
  public string Name=>NameFactory.FullName;
}