namespace Indra.Astra.Tokens {

  public interface IToken {
    public abstract static IToken Data { get; }
    public abstract string Name { get; }
  }

  public interface IToken<TSelf>
    : IToken
    where TSelf : IToken<TSelf> {
    public new virtual static TSelf Data
      => Activator.CreateInstance<TSelf>()!;
    static IToken IToken.Data
      => TSelf.Data;
  }

  public interface IStatic
    : IToken {
    public string Value { get; }
  }

  public interface IStatic<TSelf>
    : IStatic,
      IToken<TSelf>
    where TSelf : IStatic<TSelf>;

  public interface IDynamic
    : IToken;

  public interface IDynamic<TSelf>
    : IDynamic,
      IToken<TSelf>
    where TSelf : IDynamic<TSelf>;

  // public enum TokenType {
  //   // Separators
  //   COMMA,
  //   COLON,

  //   // Brackets and Delimiters
  //   LEFT_PARENTHESIS,
  //   RIGHT_PARENTHESIS,
  //   LEFT_BRACKET,
  //   RIGHT_BRACKET,
  //   LEFT_BRACE,
  //   RIGHT_BRACE,
  //   LEFT_ANGLE,
  //   RIGHT_ANGLE,
  //   SINGLE_QUOTE,
  //   DOUBLE_QUOTE,
  //   BACKTICK,

  //   // Comments
  //   OPEN_BLOCK_COMMENT,
  //   CLOSE_BLOCK_COMMENT,

  //   // Single character symbols
  //   SEMICOLON,
  //   DOT,
  //   HASH,
  //   TILDE,
  //   PERCENT,
  //   DASH,
  //   PLUS,
  //   STAR,
  //   SLASH,
  //   QUESTION,
  //   BANG,
  //   AND,
  //   PIPE,
  //   EQUALS,

  //   // Double character symbols
  //   DOUBLE_COLON,
  //   DOUBLE_DOT,
  //   DOUBLE_SEMICOLON,
  //   DOUBLE_HASH,
  //   DOUBLE_PERCENT,
  //   DOUBLE_TILDE,
  //   DOUBLE_EQUALS,
  //   DOUBLE_DASH,
  //   DOUBLE_PLUS,
  //   DOUBLE_TIMES,
  //   DOUBLE_SLASH,
  //   DOUBLE_QUESTION,
  //   DOUBLE_BANG,
  //   DOUBLE_AND,
  //   DOUBLE_PIPE,
  //   DOUBLE_RIGHT_ANGLE,
  //   DOUBLE_LEFT_ANGLE,

  //   // Two character symbols
  //   RIGHT_EQUALS_ARROW,
  //   LEFT_EQUALS_ARROW,
  //   RIGHT_DASH_ARROW,
  //   LEFT_DASH_ARROW,
  //   RIGHT_PLUS_ARROW,
  //   LEFT_PLUS_ARROW,
  //   RIGHT_TILDE_ARROW,
  //   LEFT_TILDE_ARROW,
  //   GREATER_EQUALS,
  //   EQUALS_LESS,
  //   BANG_EQUALS,
  //   QUESTION_EQUALS,
  //   HASH_EQUALS,
  //   PLUS_EQUALS,
  //   MINUS_EQUALS,
  //   TIMES_EQUALS,
  //   TILDE_EQUALS,
  //   DIVISION_EQUALS,
  //   PERCENT_EQUALS,
  //   AND_EQUALS,
  //   PIPE_EQUALS,
  //   COLON_EQUALS,
  //   DOT_EQUALS,

  //   // Triple character symbols
  //   TRIPLE_DOT,
  //   TRIPLE_COLON,
  //   TRIPLE_RIGHT_ANGLE,
  //   TRIPLE_LEFT_ANGLE,

  //   // Three character symbols
  //   DOUBLE_QUESTION_EQUALS,
  //   DOUBLE_BANG_EQUALS,
  //   DOUBLE_HASH_EQUALS,

  //   // Words
  //   WORD,
  //   NUMBER,
  //   ESCAPE,

  //   // Whitespace
  //   INDENT,
  //   DEDENT,
  //   NEWLINE,
  //   EOF
  // }
}
