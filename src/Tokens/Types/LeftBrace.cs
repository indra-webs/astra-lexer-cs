namespace Indra.Astra.Tokens {
  public record OpenBrace
    : TokenType<OpenBrace>,
      IBrace,
      IOpenDelimiter,
      INotAllowedInWord {

    public char Value
      => '{';

    public CloseBrace Close
      => CloseBrace.Type;

    ICloseDelimiter IOpenDelimiter.Close
      => Close;
  }
}
