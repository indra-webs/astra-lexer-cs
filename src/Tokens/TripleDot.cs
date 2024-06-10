namespace Indra.Astra.Tokens {
  public record TripleDot
    : TokenType<TripleDot>,
      ILookup,
      IOperator,
      IAmbiguous<TripleDot, IOperator, ILookup> {

    public string Value
      => "...";
  }
}
