using System.Diagnostics.CodeAnalysis;

using Meep.Tech.Data;

namespace Indra.Astra {

    public partial class Lexer {
        public class State {

            public class ClosureStack {
                private readonly List<Token.Open> _stack = [];
                public IReadOnlyList<Token.Open> Stack
                    => _stack;

                public int Count
                    => _stack.Count;

                public Token.Open? Current
                    => _stack.Count == 0
                        ? null
                        : _stack[^1];

                public Token.Open? OuterQuote { get; private set; }

                public Token? OuterComment { get; internal set; }

                internal void _push(Token.Open start) {
                    _stack.Add(start);
                    if(OuterQuote is null) {
                        if(start.Type.IsQuote()) {
                            OuterQuote = start;
                        }
                        else {
                            _tryStartComment(start);
                        }
                    }
                }

                internal bool _tryStartComment(Token.Open start) {
                    if(start.Type.IsComment() && OuterComment is null) {
                        OuterComment = start;
                        return true;
                    }
                    else {
                        return false;
                    }
                }

                internal bool _tryEndComment([NotNullWhen(true)] out Token? start, TokenType? close = null) {
                    if(OuterComment is not null) {
                        if(OuterComment.Type.IsOpen()) {
                            if(close is null) {
                                start = OuterComment;
                                return false;
                            } // mismatched comment delimiters
                            else if(DelimiterPairs[OuterComment.Type] != close) {
                                start = OuterComment;
                                return false;
                            } // close the comment
                            else {
                                do {
                                    _stack.RemoveAt(_stack.Count - 1);
                                } while(_stack.Count > 0 && _stack[^1] != OuterComment);
                                start = OuterComment;
                                OuterComment = null;

                                return true;
                            }
                        } // mismatched comment delimiters
                        else if(close is not null) {
                            start = OuterComment;
                            return false;
                        } // ...unreachable
                        else {
                            start = OuterComment;
                            OuterComment = null;

                            return true;
                        }
                    }

                    start = null;
                    return false;
                }

                internal bool _tryPop(TokenType close, [NotNullWhen(true)] out Token.Open? start) {
                    if(_stack.Count == 0) {
                        start = null;
                        return false;
                    }

                    start = _stack[^1];
                    // match
                    if(DelimiterPairs[start.Type] == close) {
                        _stack.RemoveAt(_stack.Count - 1);
                        if(start == OuterQuote) {
                            OuterQuote = null;
                        }
                        else if(start == OuterComment) {
                            OuterComment = null;
                        }

                        return true;
                    } // mismatch with no outer quote
                    else if(OuterQuote is null) {
                        if(OuterComment is null) {
                            return false;
                        }
                        else if(_tryEndComment(out Token? open, close)) {
                            start = (Token.Open)open;

                            return true;
                        }
                        else {
                            return false;
                        }
                    } // If it's in a quote, we don't care about other mismatched delimiters within it
                    else if(DelimiterPairs[OuterQuote.Type] == close) {
                        do {
                            _stack.RemoveAt(_stack.Count - 1);
                        } while(_stack.Count > 0 && _stack[^1] != OuterQuote);
                        start = OuterQuote;
                        OuterQuote = null;

                        return true;
                    } // ...unreachable
                    else {
                        return false;
                    }
                }
            }

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

            public IndentStack Indents { get; private set; }
                = new();

            public ClosureStack Closures { get; private set; }
                = new();

            public State() { }

            internal void _pushIndent(char c)
                => Indents._push(c);

            internal void _pushClosure(Token.Open start)
                => Closures._push(start);

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
