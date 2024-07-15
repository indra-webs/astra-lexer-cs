using Indra.Astra.Tokens;

using Meep.Tech.Collections;

namespace Indra.Astra {

  public partial class Lexer {

    /// <summary>
    /// Represents a failed result of a <see cref="Lexer"/> operation.
    /// </summary>
    /// <remarks>
    ///  <term><b>See Also</b></term><related><list type="bullet"> 
    ///   <item>
    ///   <term><seealso href="Result">Result</seealso></term>
    ///   <description>Base Type</description>
    ///   </item>
    ///   <item>
    ///   <term><seealso href="Success">Success</seealso></term>
    ///   <description><inheritdoc cref="Success" path="/summary"/></description>
    ///   </item>
    ///  </list></related>
    /// </remarks>
    public record Failure : Result {
      private Token[]? _tokens;
      private readonly IEnumerable<Token>? __tokens;

      /// <inheritdoc />
      public override bool IsSuccess
        => false;

      /// <inheritdoc />
      public override Token[]? Tokens
        => _tokens ??= __tokens?.ToArray();

      /// <inheritdoc />
      public override Error[] Errors { get; }

      /// <summary>
      /// Creates a new failed result.
      /// </summary>
      internal Failure(
        string source,
        Error[] errors,
        IEnumerable<Token>? tokens = null,
        HashSet<TokenType>? types = null
      ) : base(source) {
        Errors = errors;
        Types = types?.AsReadOnly() ?? Types;
        __tokens = tokens?.ForEach(t => t.Source = this);
      }
    }
  }
}
