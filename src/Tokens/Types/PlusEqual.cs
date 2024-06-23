namespace Indra.Astra.Tokens {
  public record PlusEqual
    : TokenType<PlusEqual>,
      IOperator {

    public string Value
      => "+=";
  }
}
