using System;
using System.Reflection;
using JimmyBogardRocks;

namespace SharedKernel {
  public class PersonFullName : IEquatable<PersonFullName> { //:ValueObject<PersonFullName>{

    public static PersonFullName Create (string first, string last) {
      return new PersonFullName (first, last);
    }
  
    private PersonFullName () { }

//Empty factory method and IsEmpty method are part of a temporary workaround for Owned Entities
//EF expects them to be populated. Hopefully fixed in EF COre 3.
//see https://msdn.microsoft.com/magazine/mt846463
  public static PersonFullName Empty () {
      return new PersonFullName (null, null);
    }
    public bool IsEmpty () {
      if (string.IsNullOrEmpty (First) && string.IsNullOrEmpty (Last)) {
        return true;
      } else {
        return false;
      }
    }
    private PersonFullName (string first, string last) {
      First = first;
      Last = last;
    }

    public string First { get; private set; }
    public string Last { get; private set; }

    public string FullName => First + " " + Last;
    public string FullNameReverse=>$"{Last}, {First}";

    public override bool Equals (object obj) {
      return Equals (obj as PersonFullName);
    }
    public bool Equals (PersonFullName other) {
      return !(other is null) && First == other.First && Last == other.Last;
    }

    public override int GetHashCode () {
      var hashCode = 352033288;
      hashCode = hashCode * 1521134295 + First.GetHashCode ();
      hashCode = hashCode * 1521134295 + Last.GetHashCode ();
      return hashCode;

    }


    public static bool operator == (PersonFullName x, PersonFullName y) {
      return x.Equals (y);
    }

    public static bool operator != (PersonFullName x, PersonFullName y) {
      return !(x == y);
    }

  }
}