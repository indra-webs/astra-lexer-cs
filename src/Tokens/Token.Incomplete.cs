namespace Indra.Astra.Tokens {
  public partial class Token {

    /// <summary>
    /// Represents a <see cref="Token"/> that could not be propertly 
    ///   read by the lexer and was therefore left in an
    ///   incomplete state.
    /// </summary>
    public class Incomplete(TokenType type)
        : Token(type) {

      /// <inheritdoc />
      public override string Name
          => $"!{base.Name}";

      /// <inheritdoc />
      override public bool IsValid
          => false;

      /// <inheritdoc />
      override public string? GetExtraText()
          => $"*INCOMPLETE*";
    }
  }
}