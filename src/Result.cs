using Indra.Astra.Tokens;

using Meep.Tech.Collections;

namespace Indra.Astra {

  public partial class Lexer {

    /// <summary>
    /// The result of a lexer operation.
    /// </summary>
    /// <param name="Text">The entirety of the source text that was processed by the lexer.</param>
    /// <remarks>
    ///  <term><b>See Also</b></term><related><list type="bullet"> 
    ///   <item>
    ///   <term><seealso href="Success">Success</seealso></term>
    ///   <description><inheritdoc cref="Success" path="/summary"/></description>
    ///   </item>
    ///   <item>
    ///   <term><seealso href="Failure">Failure</seealso></term>
    ///   <description><inheritdoc cref="Failure" path="/summary"/></description>
    ///   </item>
    ///  </list></related>
    /// </remarks>
    public abstract record Result(string Text) {

      /// <summary>
      /// Whether the lexer operation was successful.
      /// </summary>
      public abstract bool IsSuccess { get; }

      /// <summary>
      /// The tokens that were produced by the lexer (if any).
      /// </summary> 
      public abstract Token[]? Tokens { get; }

      /// <summary>
      /// The errors that were produced by the lexer (if any).
      /// </summary> 
      public abstract Error[]? Errors { get; }

      /// <summary>
      /// The types of tokens that were produced by the lexer (if any).
      /// </summary>
      public ReadOnlySet<TokenType> Types {
        get;
        init;
      } = [];
    }
  }
}
