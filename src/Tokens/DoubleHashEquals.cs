namespace Indra.Astra.Tokens {
  public record DoubleHashEqual
    : TokenType<DoubleHashEqual>,
      IComparer {

    public string Value
      => "##=";
  }
}
