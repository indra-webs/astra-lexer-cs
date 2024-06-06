namespace Indra.Astra.Tokens {
  public record NewLine
    : TokenType<NewLine>,
    IWhitespace,
    ILimited {
    public IReadOnlySet<string> Values
      => new HashSet<string> { "\n", "\r\n", "\r", "\n\r" };
  }
}
