namespace Indra.Astra.Tokens {
  public record Tilde
    : TokenType<Tilde>,
      IOperator,
      IAllowedAsWordLink {

    public char Value
      => '~';
  }
}
