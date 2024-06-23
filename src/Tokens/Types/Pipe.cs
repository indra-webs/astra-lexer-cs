namespace Indra.Astra.Tokens {
  public record Pipe
    : TokenType<Pipe>,
      IOperator,
      INotAllowedInWord {

    public char Value
      => '|';
  }
}
