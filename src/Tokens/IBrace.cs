namespace Indra.Astra.Tokens {
  public interface IBrace
    : IDelimiter {
    public new IBrace Pair
      => (IBrace)(this as IDelimiter).Pair;
  }
}
