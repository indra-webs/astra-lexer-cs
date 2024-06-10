namespace Indra.Astra.Tokens {
  public record Backslash
    : TokenType<Backslash>,
      IReserved,
      INotAllowedInWord {

    public char Value
      => '\\';
  }
}
