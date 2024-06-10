namespace Indra.Astra.Tokens {
  public record RightPlusArrow
    : TokenType<RightPlusArrow>,
      IAssigner {

    public string Value
      => "+>";
  }
}