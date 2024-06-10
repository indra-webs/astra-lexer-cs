namespace Indra.Astra.Tokens {
  public interface IBracket
    : IDelimiter {
    public new IBracket Pair
      => (IBracket)(this as IDelimiter).Pair;
  }
}
