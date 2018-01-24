using SharedKernel;

public class Person
{
  public Person(string firstname,string lastname)
  {
      Name=PersonFullName.Create(firstname,lastname);
  }
public Person(PersonFullName fullName)
{ 
  Name=fullName;
    
}
  public int Id { get; set; }
  public PersonFullName Name { get; set; }

}