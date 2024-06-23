namespace Indra.Astra.Tokens {
  public record DoubleQuote
    : TokenType<DoubleQuote>,
      IQuote<DoubleQuote>,
      INotAllowedInWord {

    public char Value
      => '"';
  }
}
