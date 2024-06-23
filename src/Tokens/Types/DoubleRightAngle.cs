namespace Indra.Astra.Tokens {
  public record DoubleRightAngle
    : TokenType<DoubleRightAngle>,
      IAssigner {

    public string Value
      => ">>";
  }
}
