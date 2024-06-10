namespace Indra.Astra.Tokens {
  public record OpenBlockComment
    : TokenType<OpenBlockComment>,
      ILeftDelimiter,
      IBlockComment {

    public string Value
      => "/*";

    public CloseBlockComment Right
      => CloseBlockComment.Type;

    IRightDelimiter ILeftDelimiter.Right
      => Right;

  }
}
