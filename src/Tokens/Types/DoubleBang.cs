namespace Indra.Astra.Tokens {
  public record DoubleBang
    : TokenType<DoubleBang>,
      IOperator {

    public string Value
      => "!!";
  }
}
