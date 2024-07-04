namespace Indra.Astra.Tokens {
  public record OpenBracket
    : TokenType<OpenBracket>,
      IBracket,
      IOpenDelimiter,
      INotAllowedInWord {

    public char Value
      => '[';

    public CloseBracket Close
      => CloseBracket.Type;

    ICloseDelimiter IOpenDelimiter.Close
      => Close;
  }
}
