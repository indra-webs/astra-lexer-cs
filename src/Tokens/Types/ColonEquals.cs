namespace Indra.Astra.Tokens {
  public record ColonEqual
  : TokenType<ColonEqual>,
    IAssigner {

    public string Value
      => ":=";
  }
}
