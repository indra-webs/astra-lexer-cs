namespace Indra.Astra.Tokens {
  public record RightBracket
    : TokenType<RightBracket>,
      IRightDelimiter,
      IBracket,
      INotAllowedInWord {

    public char Value
      => ']';

    public LeftBracket Left
      => LeftBracket.Type;

    ILeftDelimiter IRightDelimiter.Left
      => Left;
  }
}
