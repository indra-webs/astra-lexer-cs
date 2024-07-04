namespace Indra.Astra.Tokens {
  public record CloseBracket
    : TokenType<CloseBracket>,
      ICloseDelimiter,
      IBracket,
      INotAllowedInWord {

    public char Value
      => ']';

    public OpenBracket Open
      => OpenBracket.Type;

    IOpenDelimiter ICloseDelimiter.Open
      => Open;
  }
}
