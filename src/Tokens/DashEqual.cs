namespace Indra.Astra.Tokens {
  public record DashEqual
    : TokenType<DashEqual>,
      IOperator {

    public string Value
      => "-=";
  }
}
