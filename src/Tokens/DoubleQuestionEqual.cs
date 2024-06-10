namespace Indra.Astra.Tokens {
  public record DoubleQuestionEqual
    : TokenType<DoubleQuestionEqual>,
      IOperator {

    public string Value
      => "??=";
  }
}
