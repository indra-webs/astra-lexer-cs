namespace Indra.Astra.Tokens {
  public record Equal
    : TokenType<Equal>,
      IAssigner,
      INotAllowedInWord {

    public char Value
      => '=';
  }
}
