namespace Indra.Astra.Tokens {
  public record LeftBracket
  : TokenType<LeftBracket>,
    ILeftDelimiter {
    public string Value
      => "[";

    public RightBracket Right
      => RightBracket.Type;

    IRightDelimiter ILeftDelimiter.Right
      => Right;
  }
}
