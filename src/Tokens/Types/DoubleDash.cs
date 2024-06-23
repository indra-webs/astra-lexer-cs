namespace Indra.Astra.Tokens {
  public record DoubleDash
    : TokenType<DoubleDash>,
      IOperator {

    public string Value
      => "--";
  }
}
