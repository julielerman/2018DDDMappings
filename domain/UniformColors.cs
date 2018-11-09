using System.Drawing;
using JimmyBogardRocks;

namespace Domain {
  public class UniformColors : ValueObject<UniformColors> {
    public int Id { get; set; }
    
     public UniformColors (Color shirtPrimary, Color shirtSecondary, Color shirtTertiary, Color shortsPrimary, Color shortsSecondary, Color socks) {
      ShirtPrimary = shirtPrimary;
      ShirtSecondary = shirtSecondary;
      ShirtTertiary = shirtTertiary;
      ShortsPrimary = shortsPrimary;
      ShortsSecondary = shortsSecondary;
      Socks = socks;

    }
    public Color ShirtPrimary { get; private set; }
    public Color ShirtSecondary { get; private set; }
    public Color ShirtTertiary { get; private set; }
    public Color ShortsPrimary { get; private set; }
    public Color ShortsSecondary { get; private set; }
    public Color Socks { get; private set; }

    public UniformColors RevisedColors (Color shirt1, Color shirt2, Color shirt3, Color shorts1, Color shorts2, Color socks) {
      var newUniformColors = new UniformColors (
        (shirt1 != null) ? shirt1 : ShirtPrimary,
        (shirt2 != null) ? shirt2 : ShirtSecondary,
        (shirt3 != null) ? shirt3 : ShirtTertiary,
        (shorts1 != null) ? shorts1 : ShortsPrimary,
        (shorts2 != null) ? shorts2 : ShortsSecondary,
        (socks != null) ? socks : Socks
      );
      return newUniformColors;
    }
  }
}