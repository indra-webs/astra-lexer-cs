namespace Indra.Astra.Tokens {
  public record TripleRightAngle
    : TokenType<TripleRightAngle>,
      IStatic {

    public string Value
      => ">>>";
  }
}
