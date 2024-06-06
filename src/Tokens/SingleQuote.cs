namespace Indra.Astra.Tokens {
  public record SingleQuote
  : TokenType<SingleQuote>,
    IQuote<SingleQuote> {
    public string Value
      => "'";
  }
}
