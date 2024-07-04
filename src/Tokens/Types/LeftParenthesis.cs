namespace Indra.Astra.Tokens {

  public record OpenParenthesis
    : TokenType<OpenParenthesis>,
      IParenthesis,
      IOpenDelimiter,
      INotAllowedInWord {

    public char Value
      => '(';

    public CloseParenthesis Close
      => CloseParenthesis.Type;

    ICloseDelimiter IOpenDelimiter.Close
      => Close;
  }
}
