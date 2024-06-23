namespace Indra.Astra.Tokens {
  public record RightTildeArrow
    : TokenType<RightTildeArrow>,
      IAssigner {

    public string Value
      => "~>";
  }
}