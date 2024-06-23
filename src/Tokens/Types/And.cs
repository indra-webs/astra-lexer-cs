namespace Indra.Astra.Tokens {
  public record And
    : TokenType<And>,
      IOperator,
      INotAllowedInWord {

    public char Value
      => '&';
  }
}
