namespace Indra.Astra.Tokens {

  public record LeftAngle
    : TokenType<LeftAngle>,
      ILeftDelimiter {
    public string Value
      => "<";
    public RightAngle Right
      => RightAngle.Type;

    IRightDelimiter ILeftDelimiter.Right
      => Right;
  }
}
