namespace Indra.Astra.Tokens {

  public record LeftParenthesis
    : TokenType<LeftParenthesis>,
      IParenthesis,
      ILeftDelimiter {

    public string Value
      => "(";

    public RightParenthesis Right
      => RightParenthesis.Type;

    IRightDelimiter ILeftDelimiter.Right
      => Right;
  }
}
