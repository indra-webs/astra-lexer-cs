namespace Indra.Astra.Tokens {
  public record DoubleDot
    : TokenType<DoubleDot>,
      ILookup,
      IOperator,
      IAmbiguous<DoubleDot, IOperator, ILookup> {

    public string Value
      => "..";
  }
}
