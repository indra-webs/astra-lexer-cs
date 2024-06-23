namespace Indra.Astra.Tokens {
  public record LeftBracket
    : TokenType<LeftBracket>,
      IBracket,
      ILeftDelimiter,
      INotAllowedInWord {

    public char Value
      => '[';

    public RightBracket Right
      => RightBracket.Type;

    IRightDelimiter ILeftDelimiter.Right
      => Right;
  }
}
