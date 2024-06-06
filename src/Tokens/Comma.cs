namespace Indra.Astra.Tokens {
  public record Comma
  : TokenType<Comma>,
    ISeparator {
    public string Value
      => ",";
  }
}
