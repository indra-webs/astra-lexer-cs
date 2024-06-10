namespace Indra.Astra.Tokens {
  public record QuestionEqual
    : TokenType<QuestionEqual>,
      IOperator {

    public string Value
      => "?=";
  }
}
