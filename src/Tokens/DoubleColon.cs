namespace Indra.Astra.Tokens {
  public record DoubleColon
    : TokenType<DoubleColon>,
      IAssigner {

    public string Value
      => "::";
  }
}
