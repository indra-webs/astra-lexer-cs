namespace Indra.Astra.Tokens {
  public record BangEqual
    : TokenType<BangEqual>,
      IOperator {

    public string Value
      => "!=";
  }
}
