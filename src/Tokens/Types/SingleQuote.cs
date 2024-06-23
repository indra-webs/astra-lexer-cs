namespace Indra.Astra.Tokens {
  public record SingleQuote
    : TokenType<SingleQuote>,
      IQuote<SingleQuote>,
      IAllowedAsWordLink {

    public char Value
      => '\'';
  }
}
