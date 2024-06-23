namespace Indra.Astra.Tokens {
  public record Hash
    : TokenType<Hash>,
      ILookup,
      INotAllowedInWord {

    public char Value
      => '#';
  }
}
