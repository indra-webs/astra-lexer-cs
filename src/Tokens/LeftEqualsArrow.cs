namespace Indra.Astra.Tokens {
  public record LeftEqualArrow
    : TokenType<LeftEqualArrow>,
      IAssigner {

    public string Value
      => "<=";
  }
}