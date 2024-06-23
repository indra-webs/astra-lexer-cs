namespace Indra.Astra.Tokens {
  public record DoubleStar
    : TokenType<DoubleStar>,
      IOperator {

    public string Value
      => "**";
  }
}
