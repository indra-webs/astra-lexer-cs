namespace Indra.Astra.Tokens {
  public record DoubleAnd
    : TokenType<DoubleAnd>,
      IOperator {

    public string Value
      => "&&";
  }
}
