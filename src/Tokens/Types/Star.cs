namespace Indra.Astra.Tokens {
  public record Star
    : TokenType<Star>,
      IOperator,
      IAllowedAsWordLink {

    public char Value
      => '*';
  }
}
