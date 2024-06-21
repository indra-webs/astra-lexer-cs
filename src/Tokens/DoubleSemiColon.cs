namespace Indra.Astra.Tokens {
  public record DoubleSemiColon
    : TokenType<DoubleSemiColon>,
      IOperator {

    public string Value
      => ";;";
  }
}
