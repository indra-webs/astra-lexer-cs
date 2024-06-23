namespace Indra.Astra.Tokens {
  public record DoubleSlash
    : TokenType<DoubleSlash>,
      IOperator {

    public string Value
      => "//";
  }
}
