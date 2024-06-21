namespace Indra.Astra.Tokens {
  public record Plus
    : TokenType<Plus>,
      IOperator,
      IAllowedAsWordLink {

    public char Value
      => '+';
  }
}
