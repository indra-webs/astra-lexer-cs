namespace Indra.Astra.Tokens {
  public record DoubleHash
    : TokenType<DoubleHash>,
      ILookup {

    public string Value
      => "##";
  }
}
