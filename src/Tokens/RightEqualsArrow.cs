namespace Indra.Astra.Tokens {
  public record RightEqualArrow
    : TokenType<RightEqualArrow>,
      IAssigner {

    public string Value
      => "=>";
  }

  public record Number
    : TokenType<Number>,
      IVariable;

  public record Word
    : TokenType<Word>,
      IVariable;
}