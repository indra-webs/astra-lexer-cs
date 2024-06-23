namespace Indra.Astra.Tokens {
  public record PercentEqual
    : TokenType<PercentEqual>,
      IOperator {

    public string Value
      => "%=";
  }
}
