using Indra.Astra.Tokens;

namespace Indra.Astra {

  public partial class Lexer {
    public abstract record Result(string Source) {
      public abstract bool IsSuccess { get; }
      public abstract Token[]? Tokens { get; }
      public abstract Error[]? Errors { get; }
    }
  }
}
