using System.Diagnostics.CodeAnalysis;

using Meep.Tech.Text;

namespace Indra.Astra.Tokens {

    /// <summary>
    ///   An individual processed token from the lexer with location and Token Type data attached.
    /// </summary>
    /// <remarks>
    ///   <term><b>See Also</b></term><related><list type="bullet">
    ///     <item>
    ///       <term><seealso href="IToken">IToken</seealso></term>
    ///       <description><inheritdoc cref="IToken" path="/summary"/></description>
    ///     </item>
    ///     <item>
    ///       <term><seealso href="TokenType">TokenType</seealso></term>
    ///       <description><inheritdoc cref="TokenType" path="/summary"/></description>
    ///     </item>
    ///     <item>
    ///       <term><seealso href="IToken{TSelf}">IToken&lt;&gt;</seealso></term>
    ///       <description><inheritdoc cref="IToken{TSelf}" path="/summary"/></description>
    ///     </item>
    ///     <item>
    ///       <term><seealso href="TokenType{TSelf}">TokenType&lt;&gt;</seealso></term>
    ///       <description><inheritdoc cref="TokenType{TSelf}" path="/summary"/></description>
    ///     </item>
    ///   </list></related>
    /// </remarks>
    /// <param name="type">The <see cref="TokenType"/> of the token.</param>
    public partial class Token(TokenType type) {

        /// <summary>
        /// A <see cref="Token"/> with a specific <see cref="TokenType"/>.
        /// </summary>
        public class OfType<T> : Token
            where T : TokenType<T> {

            /// <summary>
            /// Helper constructor to create a token of a specific type.
            /// </summary>
            public OfType()
                : base(Types.Get<T>()) { }
        }

        /// <summary>
        /// What kind of token this is.
        /// </summary>
        public TokenType Type { get; }
            = type;

        /// <summary>
        /// The result of the lexer operation that produced this token.
        /// </summary>
        public Lexer.Result Source { get; internal set; }
            = null!;

        /// <summary>
        /// The index of the start of this token in the source text. (inclusive if length > 0)
        /// </summary>
        public required int Index { get; init; }

        /// <summary>
        /// The line number of this token in the source text.
        /// </summary>
        public required int Line { get; init; }

        /// <summary>
        /// The column number of the start of this token in the source text.
        /// </summary>
        public required int Column { get; init; }

        /// <summary>
        /// The length of this token in the source text.
        /// </summary>
        public required int Length { get; init; }

        /// <summary>
        /// Whether this token is valid.
        /// </summary>
        public virtual bool IsValid
            => true;

        /// <summary>
        /// The name of the token type.
        /// </summary>
        public virtual string Name
            => Type.Name
                .ToSnakeCase()
                .ToUpperInvariant();

        /// <inheritdoc cref="Index"/>
        public int Start
            => Index;

        /// <summary>
        /// The index of the end of this token in the source text. (always exclusive)
        /// </summary>
        public int End
            => Index + Length;

        /// <summary>
        /// The range of this token in the source text.
        /// </summary>
        /// <returns></returns>
        public Range Range
            => Index..(Index + Length);

        /// <summary>
        /// The text of this token as it appears in the source.
        /// </summary>
        public string Text
            => GetSourceText();

        /// <summary>
        /// Whether this token is of a specific type.
        /// </summary>
        public bool Is<T>(T? type = null)
            where T : TokenType<T>
            => Type is T;

        /// <summary>
        /// Get a string containing all the information about this token.
        /// </summary> 
        public virtual string GetDebugText(
            Func<
                (string name, string info, string code, string extra),
                (string name, string info, string code, string extra)
            > formatParts = null!
        ) {
            if(formatParts is null) {
                return _formatDebugText(
                   Name,
                   GetLocationText(),
                   GetSourceText(),
                   GetExtraText()
               );
            }
            else {
                (string name, string info, string code, string extra)
                    = formatParts((Name, GetLocationText(), GetSourceText(), GetExtraText() ?? ""));

                return _formatDebugText(name, info, code, extra);
            }
        }

        /// <summary>
        /// Get the location information of this token.
        /// </summary> 
        public string GetLocationText()
            => $"({Line}, {Column}) [{Index}{(
                Length > 0
                    ? $"..{Index + Length}]{"{"}{Length}{"}"}"
                    : $"]")}";

        /// <summary>
        /// Get the text of this token from the full source text.
        /// </summary> 
        public virtual string GetSourceText()
            => Type is EndOfFile
                ? "\\EOF"
                : Type is NewLine
                    ? "\\n"
                    : Type is Indent
                        ? Source.Text[Index] is '\t'
                            ? "\\t"
                            : "\\s"
                        : Type is Dedent
                            ? "\\b"
                            : Source.Text[Range];

        /// <summary>
        /// Get any extra information about this token.
        /// </summary>
        public virtual string? GetExtraText()
            => null;

        /// <summary>
        /// Get the type of this token.
        /// </summary>
        public static implicit operator TokenType(Token token)
            => token.Type;

        private static string _formatDebugText(
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