namespace Indra.Astra.Tokens {
  public record CloseBlockComment
    : TokenType<CloseBlockComment>,
      IRightDelimiter,
      IBlockComment {

    public string Value
      => "*/";

    public OpenBlockComment Left
      => OpenBlockComment.Type;

    ILeftDelimiter IRightDelimiter.Left
      => Left;
  }
}
