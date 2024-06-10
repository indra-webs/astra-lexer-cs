using System.Collections.ObjectModel;
using System.Reflection;

namespace Indra.Astra.Tokens {
  public static class Types {
    private static readonly Lazy<ReadOnlyDictionary<System.Type, TokenType>> _types
      = new (() => typeof(Types).Assembly.GetTypes()
          .Where(t => t.Namespace == "Indra.Astra.Tokens"
            && t.IsAssignableTo(typeof(TokenType))
            && !t.IsInterface
            && !t.IsAbstract)
          .ToDictionary(t => t,
            t => (TokenType)Activator.CreateInstance(
              t, BindingFlags.CreateInstance
                | BindingFlags.NonPublic
                | BindingFlags.Instance
          )!).AsReadOnly()
      );

    private static readonly Lazy<IReadOnlySet<char>> _wordLinkingChars
      = new (() => (IReadOnlySet<char>)All.Values
          .Where(t => t is IAllowedAsWordLink)
          .Select(t => ((IStatic)t).DefaultValue)
          .ToHashSet()
      );

    private static readonly Lazy<IReadOnlySet<char>> _invalidWordChars
      = new (() => (IReadOnlySet<char>)All.Values
          .Where(t => t is INotAllowedInWord)
          .Select(t => ((IStatic)t).DefaultValue)
          .ToHashSet()
      );

    public static IReadOnlyDictionary<System.Type, TokenType> All
      => _types.Value;

    public static IReadOnlySet<char> WordLinkingChars
      => _wordLinkingChars.Value;

    public static IReadOnlySet<char> InvalidWordChars
      => _invalidWordChars.Value;

    public static TTokenType Get<TTokenType>()
      where TTokenType : TokenType<TTokenType>
      => (TTokenType)All[typeof(TTokenType)];
  }
}
