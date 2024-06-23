namespace Indra.Astra.Tokens {

  public record LeftAngle
    : TokenType<LeftAngle>,
      IAngle,
      ILeftDelimiter,
      IComparer,
      IAmbiguous<LeftAngle, IComparer, IAngle>,
      IAmbiguous<LeftAngle, IComparer, ILeftDelimiter>,
      INotAllowedInWord {

    public char Value
      => '<';

    public RightAngle Right
      => RightAngle.Type;

    IRightDelimiter ILeftDelimiter.Right
      => Right;
  }
}
