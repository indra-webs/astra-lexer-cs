namespace Indra.Astra.Tokens {
  public record CloseBrace
    : TokenType<CloseBrace>,
      ICloseDelimiter,
      IBrace,
      INotAllowedInWord {

    public char Value
      => '}';

    public OpenBrace Open
      => OpenBrace.Type;

    IOpenDelimiter ICloseDelimiter.Open
      => Open;
  }
}
