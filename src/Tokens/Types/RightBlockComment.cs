namespace Indra.Astra.Tokens {
  public record CloseBlockComment
    : TokenType<CloseBlockComment>,
      ICloseDelimiter,
      IBlockComment {

    public string Value
      => "*/";

    public OpenBlockComment Open
      => OpenBlockComment.Type;

    IOpenDelimiter ICloseDelimiter.Open
      => Open;
  }
}
