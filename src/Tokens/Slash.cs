namespace Indra.Astra.Tokens {
  public record Slash
    : TokenType<Slash>,
      IOperator,
      INotAllowedInWord {

    public char Value
      => '/';
  }
}
