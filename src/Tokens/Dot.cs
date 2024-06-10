namespace Indra.Astra.Tokens {
  public record Dot
    : TokenType<Dot>,
      ILookup,
      INotAllowedInWord {

    public char Value
      => '.';
  }
}
