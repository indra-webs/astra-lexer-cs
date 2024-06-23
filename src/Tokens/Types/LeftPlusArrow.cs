namespace Indra.Astra.Tokens {
  public record LeftPlusArrow
    : TokenType<LeftPlusArrow>,
      IAssigner {

    public string Value
      => "<+";
  }
}