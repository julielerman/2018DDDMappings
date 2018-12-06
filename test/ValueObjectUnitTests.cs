using System;
using System.Drawing;
using System.Linq;
using Domain;
using SharedKernel;
using Xunit;

namespace Test {
  public class ValueObjectUnitTests {

    [Fact]
    public void CompareEqualValueObjectsWithEqualsReturnsTrue () {
      var pfnJulie = PersonFullName.Create ("Julie", "Lerman");
      var pfnJulie2 = PersonFullName.Create ("Julie", "Lerman");
      Assert.Equal (pfnJulie, pfnJulie2);
    }

    [Fact]
    public void CompareDifferentValueObjectsWithEqualsAreNotEqual () {
      var pfnJulie = PersonFullName.Create ("Julie", "Lerman");
      var pfnKermit = PersonFullName.Create ("Kermit", "Thefrog");
      Assert.NotEqual (pfnJulie, pfnKermit);
    }

    [Fact]
    public void CompareNullValueObjectsWithEqualsAreNotEqual () {
      var pfnJulie = PersonFullName.Create ("Julie", "Lerman");

      Assert.False (pfnJulie.Equals (null));
    }

    [Fact]
    public void CompareEqualValueObjectsWithSymbolReturnsTrue () {
      var pfnJulie = PersonFullName.Create ("Julie", "Lerman");
      var pfnJulie2 = PersonFullName.Create ("Julie", "Lerman");
      Assert.True (pfnJulie == pfnJulie2);
    }

    [Fact]
    public void CompareDifferentValueObjectsWithSymbolAreNotEqual () {
      var pfnJulie = PersonFullName.Create ("Julie", "Lerman");
      var pfnKermit = PersonFullName.Create ("Kermit", "Thefrog");
      Assert.False (pfnJulie == pfnKermit);
    }

    [Fact]
    public void CanReturnReverseFullName () {
      var pfnJulie = PersonFullName.Create ("Julie", "Lerman");
      Assert.Equal ( "Lerman, Julie",pfnJulie.FullNameReverse);
    }
  }
}