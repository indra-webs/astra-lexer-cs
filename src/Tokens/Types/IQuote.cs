namespace Indra.Astra.Tokens {
  public interface IQuote
  : IDelimiter {
    public new IQuote Pair { get; }
    IDelimiter IDelimiter.Pair
      => Pair;
  }

  public interface IQuote<TSelf>
    : IQuote,
      IDelimiter,
      IToken<TSelf>
    where TSelf : IQuote<TSelf> {
    public new TSelf Pair
      => TSelf.Type;

    IQuote IQuote.Pair
      => Pair;
  }
}
