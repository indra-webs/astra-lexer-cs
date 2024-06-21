namespace Indra.Astra.Tokens {
  public record Bang
    : TokenType<Bang>,
      IOperator,
      INotAllowedInWord {

    public char Value
      => '!';
  }
}
