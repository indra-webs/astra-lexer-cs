namespace Indra.Astra.Tokens {
  public record StarEqual
    : TokenType<StarEqual>,
      IOperator {

    public string Value
      => "*=";
  }
}
