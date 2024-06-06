namespace Indra.Astra.Tokens {
  public record RightBlockComment
  : TokenType<RightBlockComment>,
    IRightDelimiter,
    IBlockComment {
    public string Value
      => "*/";

    public LeftBlockComment Left
      => LeftBlockComment.Type;

    ILeftDelimiter IRightDelimiter.Left
      => Left;
  }
}
