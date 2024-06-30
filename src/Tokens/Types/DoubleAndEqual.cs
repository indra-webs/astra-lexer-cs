namespace Indra.Astra.Tokens {
  public record DoubleAndEqual
    : TokenType<DoubleAndEqual>,
      IOperator {

    public string Value
      => "&&=";
  }
}
