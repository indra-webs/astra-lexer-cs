namespace Indra.Astra.Tokens {
  public record DoubleLeftAngle
    : TokenType<DoubleLeftAngle>,
      IAssigner {

    public string Value
      => "<<";
  }
}