using System.Diagnostics.CodeAnalysis;

using Meep.Tech.Data;

namespace Indra.Astra {

    public partial class Lexer {
        public class State {
            public class IndentStack {
                private readonly List<char[]> _stack = [];
                private readonly List<char> _currentLine = [];

                public IReadOnlyList<char> CurrentLine
                    => _currentLine;

                public IReadOnlyList<char> PreviousLine
                    => _stack.Count > 0
                        ? _stack[^1]
                        : [];

                public int CurrentLevel
                    => _currentLine.Count;

                public int PreviousLevel
                    => _stack.Count > 0
                        ? _stack[^1].Length
                        : 0;

                public IndentStack() { }

                internal void _push(char c)
                    => _currentLine.Add(c);

                internal void _endIndents(
                    TextCursor cursor,
                    out Token[] indentationTokens
                ) {
                    if(CurrentLevel > PreviousLevel) {
                        indentationTokens = new Token[CurrentLevel - PreviousLevel];
                        for(int i = indentationTokens.Length - 1; i >= 0; i--) {
                            indentationTokens[i] = new Token(TokenType.INDENT) {
                                Position = cursor.Position - i - 1,
                                Length = 1,
                                Line = cursor.Line,
                                Column = cursor.Column - i - 1
                            };
                        }
                    }
                    else if(PreviousLevel > CurrentLevel) {
                        indentationTokens = new Token[PreviousLevel - CurrentLevel];
                        for(int i = indentationTokens.Length - 1; i >= 0; i--) {
                            indentationTokens[i] = new Token(TokenType.DEDENT) {
                                Position = cursor.Position - i - 1,
                                Length = 1,
                                Line = cursor.Line,
                                Column = cursor.Column - i - 1
                            };
                        }
                    }
                    else {
                        indentationTokens = [];
                    }
                }

                internal void _endLine() {
                    if(PreviousLevel != CurrentLevel) {
                        _stack.Add([.. _currentLine]);
                    }

                    _currentLine.Clear();
                }
            }

            /// <summary>
            /// If the lexer is currently reading the indentation of a line.
            /// (This is set to false after the indentation tokens (if any) are read and added)
            /// </summary>
            public bool IsReadingIndent { get; private set; }
                = true;

            /// <summary>
            /// If there hasn't been a non-whitespace token since the last newline.
            /// (This is set to false after the first non-whitespace token is read and added)
            /// </summary>
            public bool IsStartOfLine { get; internal set; }
                = true;

            /// <summary>
            /// If the lexer is currently in a comment block.
            /// </summary>
            public bool InCommentBlock { get; internal set; }

            /// <summary>
            /// The type of the outermost quote token, if there is one.
            public Token? CurrentQuote { get; internal set; }

            public IndentStack Indents { get; private set; }
                = new();

            public State() { }

            internal void _pushIndent(char c)
                => Indents._push(c);

            internal void _endIndents(
                TextCursor cursor,
                List<Token> tokens
            ) {
                Indents._endIndents(cursor, out Token[]? indentTokens);
                tokens.AddRange(indentTokens);
                IsReadingIndent = false;
            }

            internal void _endLine() {
                IsReadingIndent = true;
                IsStartOfLine = true;
                Indents._endLine();
            }
        }
    }
}
