namespace Indra.Astra.Tokens {
  public record TildeEqual
    : TokenType<TildeEqual>,
      IOperator {

    public string Value
      => "~=";
  }
}
