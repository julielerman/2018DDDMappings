using System;
using System.Collections.Generic;
using SharedKernel;
namespace Domain {
  public class Manager {
    public Manager (string firstname, string lastname) : this () {
      NameFactory = PersonFullName.Create (firstname, lastname);
      Id = Guid.NewGuid ();
    }
    private Manager () {
      PastTeams = new List<ManagerTeamHistory> ();
    }
    public Guid Id { get; set; }
    public PersonFullName NameFactory { get; private set; }
    public string Name => NameFactory.FullName;
    public Guid CurrentTeamId { get; set; }
    public List<ManagerTeamHistory> PastTeams { get; set; }
  }
}