using System.Collections.ObjectModel;
using System.Reflection;

namespace Indra.Astra.Tokens {
  public static class Types {
    private static Lazy<Dictionary<System.Type, TokenType>> _types
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
          )!)
      );

    private static ReadOnlyDictionary<Type, TokenType> _ro_types
      = null!;

    public static IReadOnlyDictionary<System.Type, TokenType> All
      => _ro_types ??= _types.Value.AsReadOnly();

    public static TTokenType Get<TTokenType>()
      where TTokenType : TokenType<TTokenType>
      => (TTokenType)_ro_types[typeof(TTokenType)];
  }
}
