using System.Diagnostics.CodeAnalysis;

namespace Indra.Astra.Tokens {
    public class Token(IToken type) {

        public interface IType {
            public string Name { get; }
        }

        public class Incomplete(Tokens.IToken type)
            : Token(type) {
            public override string Name
                => $"!{base.Name}";

            override public bool IsValid
                => false;

            override public string? GetExtraInfo()
                => $"*INCOMPLETE*";
        }

        public IToken Type { get; }
            = type;

        public required int Position { get; init; }

        public required int Line { get; init; }

        public required int Column { get; init; }

        public required int Length { get; init; }

        public virtual bool IsValid
            => true;

        public virtual string Name
            => Type.Name;

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
            => Type == Tokens.IToken.EOF
                ? "\\EOF"
                : Type == Tokens.IToken.NEWLINE
                    ? "\\n"
                    : Type == Tokens.IToken.INDENT
                        ? source[Position] == '\t'
                            ? "\\t"
                            : "\\s"
                        : Type == Tokens.IToken.DEDENT
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