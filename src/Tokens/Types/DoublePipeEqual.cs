namespace Indra.Astra.Tokens {
  public record DoublePipeEqual
    : TokenType<DoublePipeEqual>,
      IOperator {

    public string Value
      => "||=";
  }
}
