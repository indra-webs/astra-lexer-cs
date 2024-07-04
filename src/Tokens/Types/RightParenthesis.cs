namespace Indra.Astra.Tokens {
  public record CloseParenthesis
    : TokenType<CloseParenthesis>,
      ICloseDelimiter,
      IParenthesis,
      INotAllowedInWord {

    public char Value
      => ')';

    public OpenParenthesis Open
      => OpenParenthesis.Type;

    IOpenDelimiter ICloseDelimiter.Open
      => Open;
  }
}
