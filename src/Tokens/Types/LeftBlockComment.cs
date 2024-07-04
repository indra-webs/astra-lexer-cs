namespace Indra.Astra.Tokens {
  public record OpenBlockComment
    : TokenType<OpenBlockComment>,
      IOpenDelimiter,
      IBlockComment {

    public string Value
      => "/*";

    public CloseBlockComment Close
      => CloseBlockComment.Type;

    ICloseDelimiter IOpenDelimiter.Close
      => Close;

  }
}
