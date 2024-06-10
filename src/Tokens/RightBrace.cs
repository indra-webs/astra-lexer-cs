namespace Indra.Astra.Tokens {
  public record RightBrace
    : TokenType<RightBrace>,
      IRightDelimiter,
      IBrace,
      INotAllowedInWord {

    public char Value
      => '}';

    public LeftBrace Left
      => LeftBrace.Type;

    ILeftDelimiter IRightDelimiter.Left
      => Left;
  }
}
