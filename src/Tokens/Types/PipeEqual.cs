namespace Indra.Astra.Tokens {
  public record PipeEqual
    : TokenType<PipeEqual>,
      IOperator {

    public string Value
      => "|=";
  }
}
