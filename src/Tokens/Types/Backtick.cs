namespace Indra.Astra.Tokens {
  public record Backtick
    : TokenType<Backtick>,
      IQuote<Backtick>,
      INotAllowedInWord {

    public char Value
      => '`';
  }
}
