namespace Indra.Astra.Tokens {
  public record DoubleTilde
    : TokenType<DoubleTilde>,
      IOperator {

    public string Value
      => "~~";
  }
}
