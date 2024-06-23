namespace Indra.Astra.Tokens {
  public interface IParenthesis
    : IDelimiter {
    public new IParenthesis Pair
      => (IParenthesis)(this as IDelimiter).Pair;
  }
}
