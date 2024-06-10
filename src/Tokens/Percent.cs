namespace Indra.Astra.Tokens
{
  public record Percent
  : TokenType<Percent>,
    IOperator,
    IAllowedAsWordLink
  {

    public char Value
      => '%';
  }
}
