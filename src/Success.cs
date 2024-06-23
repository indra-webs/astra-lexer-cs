using Indra.Astra.Tokens;

using Meep.Tech.Collections;

namespace Indra.Astra {

  public partial class Lexer {
    /// <summary>
    /// Represents a successful result of a <see cref="Lexer"/> operation.
    /// </summary>
    /// <remarks>
    ///  <term><b>See Also</b></term><related><list type="bullet"> 
    ///   <item>
    ///   <term><seealso href="Result">Result</seealso></term>
    ///   <description>Base Type</description>
    ///   </item>
    ///   <item>
    ///   <term><seealso href="Failure">Failure</seealso></term>
    ///   <description><inheritdoc cref="Failure" path="/summary"/></description>
    ///   </item>
    ///  </list></related>
    /// </remarks>
    public record Success : Result {
      /// <inheritdoc />
      public override Token[] Tokens { get; }

      /// <inheritdoc />
      public override bool IsSuccess
        => true;

      /// <inheritdoc />
      public override Error[] Errors
        => [];

      /// <summary>
      /// Creates a new successful result.
      /// </summary>
      public Success(
        string source,
        Token[] tokens,
        HashSet<TokenType> types
      ) : base(source)
        => (Tokens, Types)
          = (tokens, types.AsReadOnly());
    }
  }
}
