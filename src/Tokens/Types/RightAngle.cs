namespace Indra.Astra.Tokens {
  public record RightAngle
    : TokenType<RightAngle>,
      ICloseDelimiter,
      IAngle,
      IComparer,
      IAmbiguous<RightAngle, IComparer, IAngle>,
      IAmbiguous<RightAngle, IComparer, ICloseDelimiter>,
      INotAllowedInWord {

    public char Value
      => '>';

    public LeftAngle Left
      => LeftAngle.Type;

    IOpenDelimiter ICloseDelimiter.Open
      => Left;
  }
}
