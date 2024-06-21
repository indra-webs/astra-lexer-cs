namespace Indra.Astra.Tokens {
  public record Comma
    : TokenType<Comma>,
      ISeparator,
      INotAllowedInWord {
    public char Value
      => ',';
  }
}
