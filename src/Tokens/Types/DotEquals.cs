namespace Indra.Astra.Tokens {
  public record DotEqual
  : TokenType<DotEqual>,
    IAssigner {

    public string Value
      => ".=";
  }
}
