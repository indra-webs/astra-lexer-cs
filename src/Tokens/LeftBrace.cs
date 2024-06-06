namespace Indra.Astra.Tokens {
  public record LeftBrace
  : TokenType<LeftBrace>,
    ILeftDelimiter {
    public string Value
      => "{";

    public RightBrace Right
      => RightBrace.Type;

    IRightDelimiter ILeftDelimiter.Right
      => Right;
  }
}
