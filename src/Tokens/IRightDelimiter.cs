namespace Indra.Astra.Tokens {
  public interface IRightDelimiter
    : IDelimiter {
    public ILeftDelimiter Left { get; }
    IDelimiter IDelimiter.Pair
      => Left;
  }
}
