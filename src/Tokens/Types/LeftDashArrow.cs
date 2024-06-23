namespace Indra.Astra.Tokens {
  public record LeftDashArrow
    : TokenType<LeftDashArrow>,
      IAssigner {

    public string Value
      => "<-";
  }
}