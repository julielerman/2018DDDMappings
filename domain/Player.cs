
using System;
using SharedKernel;

public class Player
{
  public Player(string firstname, string lastname)
  {
    Name=PersonFullName.Create(firstname,lastname);
      Id=Guid.NewGuid();
  }
  public Guid Id { get; set; }
  public PersonFullName Name { get; set; }
}