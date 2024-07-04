namespace Indra.Astra.Tokens {
  public interface ICloseDelimiter
    : IDelimiter {
    public IOpenDelimiter Open { get; }
    IDelimiter IDelimiter.Pair
      => Open;
  }
}
