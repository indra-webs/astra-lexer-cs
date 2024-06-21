namespace Indra.Astra.Tokens {
  public record Dash
    : TokenType<Dash>,
      IOperator,
      IAllowedAsWordLink {

    public char Value
      => '-';
  }
}
