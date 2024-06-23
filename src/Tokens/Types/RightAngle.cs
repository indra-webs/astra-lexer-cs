namespace Indra.Astra.Tokens {
  public record RightAngle
    : TokenType<RightAngle>,
      IRightDelimiter,
      IAngle,
      IComparer,
      IAmbiguous<RightAngle, IComparer, IAngle>,
      IAmbiguous<RightAngle, IComparer, IRightDelimiter>,
      INotAllowedInWord {

    public char Value
      => '>';

    public LeftAngle Left
      => LeftAngle.Type;

    ILeftDelimiter IRightDelimiter.Left
      => Left;
  }
}
