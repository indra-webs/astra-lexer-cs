namespace Indra.Astra.Tokens {
  public record DoubleBangEqual
    : TokenType<DoubleBangEqual>,
      IOperator {

    public string Value
      => "!!=";
  }
}
