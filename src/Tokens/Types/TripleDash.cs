namespace Indra.Astra.Tokens {
  public record TripleDash
    : TokenType<TripleDash>,
      ISeparator {

    public string Value
      => "---";
  }
}
