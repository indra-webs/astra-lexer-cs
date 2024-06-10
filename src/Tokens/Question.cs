namespace Indra.Astra.Tokens {
  public record Question
    : TokenType<Question>,
      IOperator,
      INotAllowedInWord {

    public char Value
      => '?';
  }
}
