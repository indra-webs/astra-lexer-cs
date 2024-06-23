namespace Indra.Astra.Tokens {
  public record SlashEqual
    : TokenType<SlashEqual>,
      IOperator {

    public string Value
      => "/=";
  }
}
