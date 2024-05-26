namespace Indra.Astra {
  public partial class Lexer {
    public enum TokenType {
      // Brackets and Delimiters
      OPEN_PARENTHESIS,
      CLOSE_PARENTHESIS,
      OPEN_BRACKET,
      CLOSE_BRACKET,
      OPEN_BRACE,
      CLOSE_BRACE,
      OPEN_ANGLE,
      CLOSE_ANGLE,
      SINGLE_QUOTE,
      DOUBLE_QUOTE,
      BACKTICK,

      // Comments
      EOL_SLASH_COMMENT,
      EOL_HASH_COMMENT,
      DOC_HASH_COMMENT,
      OPEN_BLOCK_COMMENT,
      CLOSE_BLOCK_COMMENT,

      // Single character symbols
      COMMA,
      COLON_CALLER,
      COLON_ASSIGNER,
      SEMICOLON,
      UNDERSCORE,
      DOT,
      HASH,
      TILDE,
      PERCENT,
      DASH,
      MINUS,
      PLUS,
      CROSS,
      TIMES,
      STAR,
      DIVISION,
      SLASH,
      QUESTION,
      BANG,
      AMPERSAND,
      PIPE,
      GREATER_THAN,
      LESS_THAN,
      EQUALS,
      /// <summary>
      /// (&lt;)
      /// </summary>
      LEFT_CHEVRON,
      /// <summary>
      /// (&gt;)
      /// </summary>
      RIGHT_CHEVRON,

      // Double character symbols
      DOUBLE_COLON_ASSIGNER,
      DOUBLE_COLON_PREFIX,
      DOUBLE_DOT,
      DOUBLE_SEMICOLON,
      DOUBLE_UNDERSCORE,
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
      GREATER_OR_EQUALS,
      EQUALS_OR_LESS,
      BANG_EQUALS,
      QUESTION_EQUALS,
      HASH_EQUALS,
      HASH_COLON,
      PLUS_EQUALS,
      MINUS_EQUALS,
      TIMES_EQUALS,
      TILDE_EQUALS,
      EQUALS_TILDE,
      DIVISION_EQUALS,
      PERCENT_EQUALS,
      QUESTION_DOT,
      DOT_QUESTION,
      COLON_RIGHT_ANGLE,
      BANG_DOT,
      DOT_BANG,
      COLON_EQUALS,
      EQUALS_COLON,
      DOT_HASH,
      DOT_EQUALS,
      HASH_BANG,
      BANG_HASH,
      QUESTION_HASH,

      // Triple character symbols
      TRIPLE_UNDERSCORE,
      TRIPLE_DOT,
      TRIPLE_COLON_ASSIGNER,
      TRIPLE_RIGHT_ANGLE, // TODO
      TRIPLE_LEFT_ANGLE, // TODO

      // Three character symbols
      DOUBLE_QUESTION_EQUALS,
      DOUBLE_BANG_EQUALS,
      DOUBLE_HASH_EQUALS,
      DOUBLE_HASH_COLON,
      DOUBLE_COLON_EQUALS,
      DOUBLE_COLON_RIGHT_ANGLE,
      DOUBLE_DOT_QUESTION,
      DOUBLE_DOT_BANG,
      DOUBLE_DOT_HASH,
      COLON_DOUBLE_RIGHT_ANGLE,
      QUESTION_DOT_HASH,
      BANG_DOT_HASH,
      DOT_QUESTION_HASH,
      DOT_BANG_HASH,

      // Four character symbols
      DOUBLE_HASH_DOUBLE_COLON,
      DOUBLE_COLON_DOUBLE_RIGHT_ANGLE,

      // Words
      WORD,
      NUMBER,
      HYBRID,
      ESCAPE,

      // Whitespace
      INDENT,
      DEDENT,
      NEWLINE,
      EOF
    }
  }
}
