namespace Indra.Astra.Tokens {
  public record RightAngle
  : TokenType<RightAngle>,
    IRightDelimiter {
    public string Value
      => ">";

    public LeftAngle Left
      => LeftAngle.Type;

    ILeftDelimiter IRightDelimiter.Left
      => Left;
  }
}
