namespace Indra.Astra {
  public partial class Lexer {
    public enum TokenType {
      // Brackets and Delimiters
      LEFT_PARENTHESIS,
      RIGHT_PARENTHESIS,
      LEFT_BRACKET,
      RIGHT_BRACKET,
      LEFT_BRACE,
      RIGHT_BRACE,
      LEFT_ANGLE,
      RIGHT_ANGLE,
      SINGLE_QUOTE,
      DOUBLE_QUOTE,
      BACKTICK,

      // Comments
      OPEN_BLOCK_COMMENT,
      CLOSE_BLOCK_COMMENT,

      // Single character symbols
      COMMA,
      COLON,
      SEMICOLON,
      DOT,
      HASH,
      TILDE,
      PERCENT,
      DASH,
      CROSS,
      STAR,
      SLASH,
      QUESTION,
      BANG,
      AND,
      PIPE,
      EQUALS,

      // Double character symbols
      DOUBLE_COLON,
      DOUBLE_DOT,
      DOUBLE_SEMICOLON,
      DOUBLE_HASH,
      DOUBLE_PERCENT,
      DOUBLE_TILDE,
      DOUBLE_EQUALS,
      DOUBLE_DASH,
      DOUBLE_PLUS,
      DOUBLE_TIMES,
      DOUBLE_DIVISION,
      DOUBLE_QUESTION,
      DOUBLE_BANG,
      DOUBLE_AMPERSAND,
      DOUBLE_PIPE,
      DOUBLE_RIGHT_ANGLE,
      DOUBLE_LEFT_ANGLE,

      // Two character symbols
      RIGHT_EQUALS_ARROW,
      LEFT_EQUALS_ARROW,
      RIGHT_DASH_ARROW,
      LEFT_DASH_ARROW,
      RIGHT_PLUS_ARROW,
      LEFT_PLUS_ARROW,
      RIGHT_TILDE_ARROW,
      LEFT_TILDE_ARROW,
      GREATER_EQUALS,
      EQUALS_LESS,
      BANG_EQUALS,
      QUESTION_EQUALS,
      HASH_EQUALS,
      PLUS_EQUALS,
      MINUS_EQUALS,
      TIMES_EQUALS,
      TILDE_EQUALS,
      DIVISION_EQUALS,
      PERCENT_EQUALS,
      DOT_EQUALS,

      // Triple character symbols
      TRIPLE_DOT,
      TRIPLE_COLON,
      TRIPLE_RIGHT_ANGLE,
      TRIPLE_LEFT_ANGLE,

      // Three character symbols
      DOUBLE_QUESTION_EQUALS,
      DOUBLE_BANG_EQUALS,
      DOUBLE_HASH_EQUALS,

      // Words
      WORD,
      NUMBER,
      ESCAPE,

      // Whitespace
      INDENT,
      DEDENT,
      NEWLINE,
      EOF
    }
  }
}
