using Meep.Tech.Text;

namespace Indra.Astra.Tokens {

  /// <summary>
  /// Abstract class representing padding around one side of a token.
  /// </summary>
  /// <param name="token"></param>
  public abstract partial class Pad(Token token) {

    /// <summary>
    /// The index of the padding character.
    /// </summary>
    public abstract int Index { get; }

    /// <summary>
    /// The character padding this token in the given direction.
    /// </summary>
    public char Char
        => token.Source.Text.TryGetCharAt(Index, out char? c)
            ? c.Value
            : '\0';

    /// <summary>
    /// Whether the padding charachter is a whitespace character.
    /// </summary>
    public bool IsAny
        => Char.IsWhiteSpaceOrNull();

    /// <summary>
    /// If there is no padding character touching this token.
    /// </summary>
    public bool IsNone
        => !IsAny;
  }
}