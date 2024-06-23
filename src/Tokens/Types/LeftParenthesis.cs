namespace Indra.Astra.Tokens {

  public record LeftParenthesis
    : TokenType<LeftParenthesis>,
      IParenthesis,
      ILeftDelimiter,
      INotAllowedInWord {

    public char Value
      => '(';

    public RightParenthesis Right
      => RightParenthesis.Type;

    IRightDelimiter ILeftDelimiter.Right
      => Right;
  }
}
