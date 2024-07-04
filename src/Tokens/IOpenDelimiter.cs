namespace Indra.Astra.Tokens {
  public interface IOpenDelimiter
    : IDelimiter {

    public ICloseDelimiter Close { get; }
    IDelimiter IDelimiter.Pair
      => Close;
  }
}
