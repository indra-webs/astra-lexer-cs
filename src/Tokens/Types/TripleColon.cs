namespace Indra.Astra.Tokens {
  public record TripleColon
    : TokenType<TripleColon>,
      IAssigner {

    public string Value
      => ":::";
  }
}
