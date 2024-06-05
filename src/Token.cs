using System.Diagnostics.CodeAnalysis;

namespace Indra.Astra {
    public partial class Lexer {
        public class Token(TokenType type) {
            public class Incomplete(TokenType type)
                : Token(type) {
                public override string Name
                    => $"!{base.Name}";

                override public bool IsValid
                    => false;

                override public string? GetExtraInfo()
                    => $"*INCOMPLETE*";
            }

            public TokenType Type { get; }
                = type;

            public required int Position { get; init; }

            public required int Line { get; init; }

            public required int Column { get; init; }

            public required int Length { get; init; }

            public virtual bool IsValid
                => true;

            public virtual string Name
                => Type.ToString();

            public int Start
                => Position;

            public int End
                => Position + Length;

            public sealed override string ToString()
                => ToString(default!);

            public virtual string ToString(
                string source,
                Func<
                    (string name, string info, string code, string extra),
                    (string name, string info, string code, string extra)
                > formatParts = null!
            ) {
                if(formatParts is null) {
                    return _joinStringParts(
                       Name,
                       GetLocationInfo(),
                       source is not null
                           ? GetSourceText(source)
                           : null,
                          GetExtraInfo()
                   );
                }
                else {
                    (string name, string info, string code, string extra)
                        = formatParts((Name, GetLocationInfo(), GetSourceText(source), GetExtraInfo() ?? ""));

                    return _joinStringParts(name, info, code, extra);
                }
            }

            public string GetLocationInfo()
                => $"({Line}, {Column}) [{Position}{(
                    Length > 0
                        ? $"..{Position + Length}]{"{"}{Length}{"}"}"
                        : $"]")}";

            public virtual string GetSourceText([NotNull] string source)
                => Type == TokenType.EOF
                    ? "\\EOF"
                    : Type == TokenType.NEWLINE
                        ? "\\n"
                        : Type == TokenType.INDENT
                            ? source[Position] == '\t'
                                ? "\\t"
                                : "\\s"
                            : Type == TokenType.DEDENT
                                ? "\\b"
                                : source[Position..(Position + Length)];

            public virtual string? GetExtraInfo()
                => null;

            private static string _joinStringParts(
                string name,
                string location,
                string? source,
                string? extra
            ) => $"{name,-(3 * 8)}| {location,-(3 * 8)}| {(!string.IsNullOrEmpty(source)
                ? $"\"{source}\""
                : ""),-(3 * 8)}| {(!string.IsNullOrEmpty(extra)
                    ? $"{extra}"
                    : "")}";

        }
    }
    public static class LexerTokenExtensions {

        #region Type Extensions

        public static bool IsOpen(this Lexer.TokenType type)
            => type switch {
                Lexer.TokenType.LEFT_PARENTHESIS => true,
                Lexer.TokenType.LEFT_BRACKET => true,
                Lexer.TokenType.LEFT_BRACE => true,
                Lexer.TokenType.LEFT_ANGLE => true,
                Lexer.TokenType.OPEN_BLOCK_COMMENT => true,
                _ => false
            };

        public static bool IsClose(this Lexer.TokenType type)
            => type switch {
                Lexer.TokenType.RIGHT_PARENTHESIS => true,
                Lexer.TokenType.RIGHT_BRACKET => true,
                Lexer.TokenType.RIGHT_BRACE => true,
                Lexer.TokenType.RIGHT_ANGLE => true,
                Lexer.TokenType.CLOSE_BLOCK_COMMENT => true,
                _ => false
            };

        public static bool IsSeparator(this Lexer.TokenType type)
            => type switch {
                Lexer.TokenType.COMMA => true,
                Lexer.TokenType.SEMICOLON => true,
                _ => false
            };

        public static bool IsQuote(this Lexer.TokenType type)
            => type switch {
                Lexer.TokenType.SINGLE_QUOTE => true,
                Lexer.TokenType.DOUBLE_QUOTE => true,
                Lexer.TokenType.BACKTICK => true,
                _ => false
            };

        /// <summary>
        /// Tokens that begin a sequence
        /// </summary>
        public static bool IsStart(this Lexer.TokenType type)
            => type.IsQuote()
            || type.IsOpen()
            || type.IsInitial();

        /// <summary>
        /// First in a line
        /// </summary>
        public static bool IsInitial(this Lexer.TokenType type)
            => type switch {
                Lexer.TokenType.DASH => true,
                Lexer.TokenType.STAR => true,
                Lexer.TokenType.PLUS => true,
                _ => false
            };

        /// <summary>
        /// Tokens that end a sequence
        /// </summary>
        public static bool IsEnd(this Lexer.TokenType type)
            => type.IsQuote()
            || type.IsClose();

        /// <summary>
        /// Tokens that are used to define sequences and their elements
        /// </summary>
        public static bool IsDelimiter(this Lexer.TokenType type)
            => type.IsStart()
            || type.IsEnd()
            || type.IsSeparator();

        public static bool IsVariable(this Lexer.TokenType type)
            => type switch {
                Lexer.TokenType.WORD => true,
                Lexer.TokenType.NUMBER => true,
                Lexer.TokenType.ESCAPE => true,
                _ => false
            };

        public static bool IsWhiteSpace(this Lexer.TokenType type)
            => type switch {
                Lexer.TokenType.NEWLINE
                    or Lexer.TokenType.INDENT
                    or Lexer.TokenType.DEDENT
                    or Lexer.TokenType.EOF => true,
                _ => false
            };

        #endregion
    }
}
