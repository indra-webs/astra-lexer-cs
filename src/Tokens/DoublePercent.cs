namespace Indra.Astra.Tokens {
  public record DoublePercent
    : TokenType<DoublePercent>,
      IOperator {

    public string Value
      => "%%";
  }
}
