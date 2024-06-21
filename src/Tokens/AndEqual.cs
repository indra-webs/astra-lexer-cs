namespace Indra.Astra.Tokens {
  public record AndEqual
    : TokenType<AndEqual>,
      IOperator {

    public string Value
      => "&=";
  }
}
