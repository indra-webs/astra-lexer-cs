namespace Indra.Astra.Tokens {
  public record DoublePipe
    : TokenType<DoublePipe>,
      IOperator {

    public string Value
      => "||";
  }
}
