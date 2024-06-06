using Indra.Astra.Tokens;

namespace Indra.Astra {

  public partial class Lexer {
    public record Failure : Result {
      private Token[]? _tokens;
      private readonly IEnumerable<Token>? __tokens;

      public override bool IsSuccess
        => false;

      public override Token[]? Tokens
        => _tokens ??= __tokens?.ToArray();

      public override Error[] Errors { get; }

      public Failure(string source, Error[] errors, IEnumerable<Token>? tokens = null)
          : base(source) {
        Errors = errors;
        __tokens = tokens;
      }
    }
  }
}
