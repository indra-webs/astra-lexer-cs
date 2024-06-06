namespace Indra.Astra.Tokens {
  public record RightBrace
  : TokenType<RightBrace>,
    IRightDelimiter {
    public string Value
      => "}";

    public LeftBrace Left
      => LeftBrace.Type;

    ILeftDelimiter IRightDelimiter.Left
      => Left;
  }
}
