namespace Indra.Astra.Tokens {

  public abstract partial class Pad {
    /// <summary>
    /// Represents the padding charachter immediately after a token.
    /// </summary>
    public class After(Token token)
      #pragma warning disable CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.
      : Pad(token) {
      #pragma warning restore CS9107 // Parameter is captured into the state of the enclosing type and its value is also passed to the base constructor. The value might be captured by the base class as well.

            /// <summary>
            /// The index of the character after this token.
            /// </summary>
            public override int Index
          => token.Index + token.Length;
    }
  }
}