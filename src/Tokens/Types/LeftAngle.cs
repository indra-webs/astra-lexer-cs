namespace Indra.Astra.Tokens {

  public record LeftAngle
    : TokenType<LeftAngle>,
      IAngle,
      IOpenDelimiter,
      IComparer,
      IAmbiguous<LeftAngle, IComparer, IAngle>,
      IAmbiguous<LeftAngle, IComparer, IOpenDelimiter>,
      INotAllowedInWord {

    public char Value
      => '<';

    public RightAngle Right
      => RightAngle.Type;

    ICloseDelimiter IOpenDelimiter.Close
      => Right;
  }
}
