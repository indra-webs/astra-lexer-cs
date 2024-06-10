namespace Indra.Astra.Tokens {
  public record LeftBrace
    : TokenType<LeftBrace>,
      IBrace,
      ILeftDelimiter,
      INotAllowedInWord {

    public char Value
      => '{';

    public RightBrace Right
      => RightBrace.Type;

    IRightDelimiter ILeftDelimiter.Right
      => Right;
  }
}
