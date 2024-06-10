namespace Indra.Astra.Tokens {
  public interface ILeftDelimiter
    : IDelimiter {

    public IRightDelimiter Right { get; }
    IDelimiter IDelimiter.Pair
      => Right;
  }
}
