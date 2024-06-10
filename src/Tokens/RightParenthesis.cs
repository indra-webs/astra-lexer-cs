namespace Indra.Astra.Tokens {
  public record RightParenthesis
    : TokenType<RightParenthesis>,
      IRightDelimiter,
      IParenthesis,
      INotAllowedInWord {

    public char Value
      => ')';

    public LeftParenthesis Left
      => LeftParenthesis.Type;

    ILeftDelimiter IRightDelimiter.Left
      => Left;
  }
}
