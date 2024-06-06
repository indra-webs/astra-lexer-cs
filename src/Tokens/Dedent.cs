namespace Indra.Astra.Tokens {
  public record Dedent
  : TokenType<Dedent>,
    IWhitespace,
    IEmpty;
}
