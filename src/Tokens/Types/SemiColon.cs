namespace Indra.Astra.Tokens {
  public record SemiColon
    : TokenType<SemiColon>,
      ISeparator,
      INotAllowedInWord {

    public char Value
      => ';';
  }
}
