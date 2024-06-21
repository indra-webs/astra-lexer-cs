namespace Indra.Astra.Tokens {
  public record DoubleEqual
    : TokenType<DoubleEqual>,
      IComparer {

    public string Value
      => "==";
  }
}