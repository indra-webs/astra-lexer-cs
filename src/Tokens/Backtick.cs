namespace Indra.Astra.Tokens {
  public record Backtick
  : TokenType<Backtick>,
    IQuote<Backtick> {
    public string Value
      => "`";
  }
}
