using System;
using SharedKernel;

namespace Domain {
  public class Player {
    public Player (string firstname, string lastname) {
      Name = PersonFullName.Create (firstname, lastname);
      Id = Guid.NewGuid ();
    }
    private Player(){}
    public Guid Id { get; set; }
    public PersonFullName Name { get; set; }
  }
}