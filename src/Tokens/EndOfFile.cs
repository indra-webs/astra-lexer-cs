namespace Indra.Astra.Tokens {
  public record EndOfFile
    : TokenType<EndOfFile>,
      IWhitespace,
      IEmpty;
}
