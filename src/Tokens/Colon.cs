namespace Indra.Astra.Tokens {

  public record Colon
    : TokenType<Colon>,
      IAssigner,
      IOperator,
      IAmbiguous<Colon, IAssigner, IOperator>,
      INotAllowedInWord {

    public char Value
      => ':';
  }
}
