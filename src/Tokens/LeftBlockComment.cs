namespace Indra.Astra.Tokens {
  public record LeftBlockComment
  : TokenType<LeftBlockComment>,
    ILeftDelimiter,
    IBlockComment {
    public string Value
      => "/*";

    public RightBlockComment Right
      => RightBlockComment.Type;

    IRightDelimiter ILeftDelimiter.Right
      => Right;

  }
}
