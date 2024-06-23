namespace Indra.Astra.Tokens {
  public record Indent
    : TokenType<Indent>,
      IWhitespace,
      ILimited {

    public IReadOnlySet<string> Values
      => new HashSet<string> { "\t", " " };
  }
}
