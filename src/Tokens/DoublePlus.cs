namespace Indra.Astra.Tokens {
  public record DoublePlus
    : TokenType<DoublePlus>,
      IOperator {

    public string Value
      => "++";
  }
}
