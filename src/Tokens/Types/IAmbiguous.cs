namespace Indra.Astra.Tokens {
  public interface IAmbiguous<TBetween, TEither, TOr>
    : IToken<TBetween>
    where TBetween : TEither, TOr, IToken<TBetween>
    where TEither : IToken
    where TOr : IToken {

    public static IToken A
      => TEither.Type;
    public static IToken B
      => TOr.Type;

    public IToken Primary
      => A;

    public IToken Alternate
      => B;
  }
}
