namespace Indra.Astra.Tokens {
  public record DoubleQuestion
    : TokenType<DoubleQuestion>,
      IOperator {

    public string Value
      => "??";
  }
}
