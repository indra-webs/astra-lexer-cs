namespace Indra.Astra.Tokens {
  public record LeftTildeArrow
    : TokenType<LeftTildeArrow>,
      IAssigner {

    public string Value
      => "<~";
  }
}