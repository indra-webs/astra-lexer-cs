namespace Indra.Astra.Tokens {
  public record TripleLeftAngle
    : TokenType<TripleLeftAngle>,
      IStatic {

    public string Value
      => "<<<";
  }
}
