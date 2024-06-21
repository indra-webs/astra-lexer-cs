namespace Indra.Astra.Tokens {
  /// <summary>
  /// Symbols that are allowed to appear in the middle of a word just once.
  ///     They cannot appear at the beginning or end of a word.
  ///     They cannot appear twice in a row.
  /// </summary>
  public interface IAllowedAsWordLink
    : ISingle;
}
