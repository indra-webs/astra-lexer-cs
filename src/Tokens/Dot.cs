namespace Indra.Astra.Tokens {
  public record Dot
    : TokenType<Dot>,
      ILookup {

    public string Value
      => ".";
  }
}
