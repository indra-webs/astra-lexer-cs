namespace Indra.Astra.Tokens {
  public record DoubleQuote
  : TokenType<DoubleQuote>,
    IQuote<DoubleQuote> {
    public string Value
      => "\"";
  }
}
