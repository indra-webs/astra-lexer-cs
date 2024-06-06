namespace Indra.Astra.Tokens {
  public record RightBracket
  : TokenType<RightBracket>,
    IRightDelimiter {
    public string Value
      => "]";

    public LeftBracket Left
      => LeftBracket.Type;

    ILeftDelimiter IRightDelimiter.Left
      => Left;
  }
}
