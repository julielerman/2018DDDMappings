using System.Drawing;
using JimmyBogardRocks;
namespace Domain {
  public class UniformColors : ValueObject<UniformColors> {
    public UniformColors (Color shirt1, Color shirt2, Color shirt3, Color shorts1, Color shorts2, Color socks) {
      ShirtPrimary = shirt1.Name;
      ShortsSecondary = shirt2.Name;
      ShirtTertiary = shirt3.Name;
      ShortsPrimary = shorts1.Name;
      ShortsSecondary = shorts2.Name;
      Socks = socks.Name;

    }
    public string ShirtPrimary { get; private set; }
    public string ShirtSecondary { get; private set; }
    public string ShirtTertiary { get; private set; }
    public string ShortsPrimary { get; private set; }
    public string ShortsSecondary { get; private set; }
    public string Socks { get; private set; }

    public UniformColors RevisedColors (Color shirt1, Color shirt2, Color shirt3, Color shorts1, Color shorts2, Color socks) {
      var newUniformColors = new UniformColors (
        (shirt1 != null) ? shirt1 : Color.FromName(ShirtPrimary),
        (shirt2 != null) ? shirt2 : Color.FromName(ShirtSecondary),
        (shirt3 != null) ? shirt3 : Color.FromName(ShirtTertiary),
        (shorts1 != null) ? shorts1 : Color.FromName(ShortsPrimary),
        (shorts2 != null) ? shorts2 : Color.FromName(ShortsSecondary),
        (socks != null) ? socks : Color.FromName(Socks)
      );
      return newUniformColors;
    }
  }
}