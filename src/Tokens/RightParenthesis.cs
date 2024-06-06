namespace Indra.Astra.Tokens {
  public record RightParenthesis
  : TokenType<RightParenthesis>,
    IRightDelimiter {

    public string Value
      => ")";

    public LeftParenthesis Left
      => LeftParenthesis.Type;

    ILeftDelimiter IRightDelimiter.Left
      => Left;
  }
}
