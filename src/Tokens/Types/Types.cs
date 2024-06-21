using System.Collections.ObjectModel;
using System.Reflection;

using Meep.Tech.Collections;

namespace Indra.Astra.Tokens {

  /// <summary>
  /// Provides access to all token types.
  /// </summary>
  public static class Types {

    #region Private Fields

    private static readonly Lazy<ReadOnlyDictionary<System.Type, TokenType>> _types
      = new (() => typeof(Types).Assembly.GetTypes()
          .Where(t => t.Namespace == "Indra.Astra.Tokens"
            && t.IsAssignableTo(typeof(TokenType))
            && !t.IsInterface
            && !t.IsAbstract)
          .ToDictionary(t => t,
            t => (TokenType)t.Assembly.CreateInstance(
              t.FullName!,
              false,
              BindingFlags.CreateInstance
                | BindingFlags.Instance
                | BindingFlags.Public
                | BindingFlags.NonPublic,
              null, null, null, null
            )!).AsReadOnly()
      );

    private static readonly Lazy<ReadOnlySet<IStatic>> _static
      = new (() => All.Values
          .Where(t => t is IStatic)
          .Cast<IStatic>()
          .ToHashSet()
          .AsReadOnly()
      );

    private static readonly Lazy<ReadOnlySet<char>> _wordLinkingChars
      = new (() => All.Values
          .Where(t => t is IAllowedAsWordLink)
          .Select(t => ((ISingle)t).Value)
          .ToHashSet()
          .AsReadOnly()
      );

    private static readonly Lazy<ReadOnlySet<char>> _invalidWordChars
      = new (() => All.Values
          .Where(t => t is INotAllowedInWord)
          .Select(t => ((ISingle)t).Value)
          .ToHashSet()
          .AsReadOnly()
      );

    #endregion

    /// <summary>
    /// All token type singleton by their System.Type.
    /// </summary>
    public static IReadOnlyDictionary<System.Type, TokenType> All
      => _types.Value;

    /// <summary>
    /// All <see cref="IAllowedAsWordLink"/> token types.
    /// </summary>
    public static IReadOnlySet<char> WordLinkingChars
      => _wordLinkingChars.Value;

    /// <summary>
    /// All <see cref="INotAllowedInWord"/> token types.
    /// </summary>
    public static IReadOnlySet<char> InvalidWordChars
      => _invalidWordChars.Value;

    /// <summary>
    /// All <see cref="IStatic"/> token types.
    /// </summary>
    public static IReadOnlySet<IStatic> Static
      => _static.Value;

    /// <summary>
    /// Get a token type by its System.Type.
    /// </summary>
    public static TTokenType Get<TTokenType>()
      where TTokenType : TokenType<TTokenType>
      => (TTokenType)All[typeof(TTokenType)];
  }
}
