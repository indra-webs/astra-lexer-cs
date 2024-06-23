namespace Indra.Astra.Tokens {
  public record RightDashArrow
    : TokenType<RightDashArrow>,
      IAssigner {

    public string Value
      => "->";
  }
}