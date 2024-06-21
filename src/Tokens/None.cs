namespace Indra.Astra.Tokens
{
  /// <summary>
  /// Special; used to represent no token being present.
  /// </summary>
  public record None
  : TokenType<None>,
    IEmpty;
}
