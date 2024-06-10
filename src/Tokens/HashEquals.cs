namespace Indra.Astra.Tokens {
  public record HashEqual
    : TokenType<HashEqual>,
      IComparer {

    public string Value
      => "#=";
  }
}
