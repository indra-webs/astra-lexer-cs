using System.Diagnostics.CodeAnalysis;

using Indra.Astra.Tokens;

using Meep.Tech.Data;
using Meep.Tech.Text;

namespace Indra.Astra {

    public partial class Lexer {

        /// <summary>
        /// Symbols that are allowed to appear in the middle of a word just once.
        ///     They cannot appear at the beginning or end of a word.
        ///     They cannot appear twice in a row.
        /// </summary>
        public static readonly HashSet<char> WORD_LINK_SYMBOLS
            = ['+', '-', '~', '%', '\''];

        /// <summary>
        /// Symbols that are not allowed to appear in a word at all.
        /// </summary>
        public static readonly HashSet<char> INVALID_WORD_SYMBOLS
            = [
                '.', '/', '?', '!', '&', '|',
                '=', '<', '>', ',', ':', ';',
                '"', '`', '#', '*', '(', ')',
                '[', ']', '{', '}', '<', '>'
            ];

        public Lexer() { }

        public Result Lex(IEnumerable<char> input) {
            TextCursor cursor = new(input);

            if(cursor.HasEmptySource) {
                return new Success(cursor.Text, []);
            } // Scan:
            else {
                List<Token> tokens = [];
                State state = new();

                do {
                    // skip & count the beginning whitespace as indentation
                    if(state.IsReadingIndent) {
                        lex_indents(cursor, tokens, state);
                    }

                    char current = cursor.Current;
                    switch(current) {
                        #region Whitespace
                        // check for newlines and carriage returns
                        case '\r': { // CR
                                appendToken_newLine(cursor.Next is '\n' ? 2 : 1);
                                continue;
                            }
                        case '\n': {// LF
                                appendToken_newLine(cursor.Next is '\r' ? 2 : 1);
                                continue;
                            }
                        // skip spaces and tabs and null chars within a line (after indentation & first token / between tokens / end of line)
                        case ' ' or '\t' or '\0': {
                                while(cursor.Read([' ', '\t', '\0'])) {
                                    continue;
                                }

                                continue;
                            }
                        #endregion
                        #region Symbols
                        #region Brackets
                        case '(': {
                                appendToken_length1(IToken.LEFT_PARENTHESIS);
                                break;
                            }
                        case ')': {
                                appendToken_length1(IToken.RIGHT_PARENTHESIS);
                                break;
                            }
                        case '[': {
                                appendToken_length1(IToken.LEFT_BRACKET);
                                break;
                            }
                        case ']': {
                                appendToken_length1(IToken.RIGHT_BRACKET);
                                break;
                            }
                        case '{': {
                                appendToken_length1(IToken.LEFT_BRACE);
                                break;
                            }
                        case '}': {
                                appendToken_length1(IToken.RIGHT_BRACE);
                                break;
                            }
                        case '<': {
                                switch(cursor.Next) {
                                    case '<': {
                                            switch(cursor.Peek(2)) {
                                                case '<': {
                                                        appendToken_length3(IToken.TRIPLE_LEFT_ANGLE);
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2(IToken.DOUBLE_LEFT_ANGLE);
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(IToken.LEFT_EQUALS_ARROW);
                                            break;
                                        }
                                    case '-': {
                                            appendToken_length2(IToken.LEFT_DASH_ARROW);
                                            break;
                                        }
                                    case '~': {
                                            appendToken_length2(IToken.LEFT_TILDE_ARROW);
                                            break;
                                        }
                                    case '+': {
                                            appendToken_length2(IToken.LEFT_PLUS_ARROW);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(IToken.LEFT_ANGLE);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '>': {
                                if(cursor.Next is '>') {
                                    switch(cursor.Peek(2)) {
                                        case '>':
                                            appendToken_lengthOf(3, IToken.TRIPLE_RIGHT_ANGLE, true);
                                            break;
                                        default:
                                            appendToken_length2(IToken.DOUBLE_RIGHT_ANGLE);
                                            break;
                                    }

                                    break;
                                }
                                else if(cursor.Next is '=') {
                                    appendToken_length2(IToken.GREATER_EQUALS);
                                    break;
                                }
                                else {
                                    appendToken_length1(IToken.RIGHT_ANGLE);
                                    break;
                                }
                            }
                        #endregion
                        #region Quotes
                        case '\'': {
                                appendToken_quote(IToken.SINGLE_QUOTE);
                                break;
                            }
                        case '"': {
                                appendToken_quote(IToken.DOUBLE_QUOTE);
                                break;
                            }
                        case '`': {
                                appendToken_quote(IToken.BACKTICK);
                                break;
                            }
                        #endregion
                        case ':': {
                                switch(cursor.Next) {
                                    case ':': {
                                            switch(cursor.Peek(2)) {
                                                case ':': {
                                                        appendToken_length3(IToken.TRIPLE_COLON);
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2(IToken.DOUBLE_COLON);
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(IToken.COLON_EQUALS);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(IToken.COLON);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '.': {
                                switch(cursor.Next) {
                                    case '.': {
                                            switch(cursor.Peek(2)) {
                                                case '.': {
                                                        appendToken_length3(IToken.TRIPLE_DOT);
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2(IToken.DOUBLE_DOT);
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(IToken.DOT_EQUALS);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(IToken.DOT);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '#': {
                                switch(cursor.Next) {
                                    case '#': {
                                            switch(cursor.Peek(2)) {
                                                case '=': {
                                                        appendToken_length3(IToken.DOUBLE_HASH_EQUALS);
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2(IToken.DOUBLE_HASH);
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(IToken.HASH_EQUALS);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(IToken.HASH);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '+': {
                                switch(cursor.Next) {
                                    case '+': {
                                            appendToken_length2(IToken.DOUBLE_PLUS);
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(IToken.PLUS_EQUALS);
                                            break;
                                        }
                                    case '>': {
                                            appendToken_length2(IToken.RIGHT_PLUS_ARROW);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(IToken.PLUS);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '-': {
                                switch(cursor.Next) {
                                    case '-': {
                                            appendToken_length2(IToken.DOUBLE_DASH);
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(IToken.MINUS_EQUALS);
                                            break;
                                        }
                                    case '>': {
                                            appendToken_length2(IToken.RIGHT_DASH_ARROW);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(IToken.DASH);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '*': {
                                switch(cursor.Next) {
                                    case '*': {
                                            appendToken_length2(IToken.DOUBLE_TIMES);
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(IToken.TIMES_EQUALS);
                                            break;
                                        }
                                    case '/' when state.InCommentBlock: {
                                            appendToken_length2(IToken.CLOSE_BLOCK_COMMENT);
                                            state.InCommentBlock = false;
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(IToken.STAR);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '/': {
                                switch(cursor.Next) {
                                    case '/': {
                                            appendToken_length2(IToken.DOUBLE_SLASH);
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(IToken.DIVISION_EQUALS);
                                            break;
                                        }
                                    case '*' when cursor.Previous.IsWhiteSpaceOrNull() && cursor.Peek(2).IsWhiteSpaceOrNull(): {
                                            appendToken_length2(IToken.OPEN_BLOCK_COMMENT);
                                            state.InCommentBlock = true;
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(IToken.SLASH);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '~': {
                                switch(cursor.Next) {
                                    case '~': {
                                            appendToken_length2(IToken.DOUBLE_TILDE);
                                            break;
                                        }
                                    case '>': {
                                            appendToken_length2(IToken.RIGHT_TILDE_ARROW);
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(IToken.TILDE_EQUALS);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(IToken.TILDE);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '%': {
                                switch(cursor.Next) {
                                    case '%': {
                                            appendToken_length2(IToken.DOUBLE_PERCENT);
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(IToken.PERCENT_EQUALS);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(IToken.PERCENT);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '=': {
                                switch(cursor.Next) {
                                    case '=': {
                                            appendToken_length2(IToken.DOUBLE_EQUALS);
                                            break;
                                        }
                                    case '>': {
                                            appendToken_length2(IToken.RIGHT_EQUALS_ARROW);
                                            break;
                                        }
                                    case '<': {
                                            appendToken_length2(IToken.EQUALS_LESS);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(IToken.EQUALS);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '&': {
                                switch(cursor.Next) {
                                    case '&': {
                                            appendToken_length2(IToken.DOUBLE_AND);
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(IToken.AND_EQUALS);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(IToken.AND);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '|': {
                                switch(cursor.Next) {
                                    case '|': {
                                            appendToken_length2(IToken.DOUBLE_PIPE);
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(IToken.PIPE_EQUALS);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(IToken.PIPE);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '?': {
                                switch(cursor.Next) {
                                    case '?': {
                                            switch(cursor.Peek(2)) {
                                                case '=': {
                                                        appendToken_length3(IToken.DOUBLE_QUESTION_EQUALS);
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2(IToken.DOUBLE_QUESTION);
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(IToken.QUESTION_EQUALS);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(IToken.QUESTION);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '!': {
                                switch(cursor.Next) {
                                    case '!': {
                                            switch(cursor.Peek(2)) {
                                                case '=': {
                                                        appendToken_length3(IToken.DOUBLE_BANG_EQUALS);
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2(IToken.DOUBLE_BANG);
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(IToken.BANG_EQUALS);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(IToken.BANG);
                                            break;
                                        }
                                }

                                break;
                            }
                        case ',': {
                                appendToken_length1(IToken.COMMA);
                                break;
                            }
                        case ';': {
                                switch(cursor.Next) {
                                    case ';': {
                                            appendToken_length2(IToken.DOUBLE_SEMICOLON);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(IToken.SEMICOLON);
                                            break;
                                        }
                                }

                                break;
                            }
                        #endregion
                        #region Variables
                        // escape sequences via backslash
                        case '\\': {
                                if(cursor.ReadNext("eof") || cursor.ReadNext("EOF")) {
                                    appendToken_lengthOf(4, IToken.ESCAPE, false);
                                }
                                else {
                                    if(!tryAppendToken_length2(IToken.ESCAPE)) {
                                        return fail_withError(
                                            ErrorCode.UNEXPECTED_EOF,
                                            IToken.ESCAPE,
                                            position: cursor.Position + cursor.Buffer
                                        );
                                    }
                                }

                                break;
                            }
                        // all other characters, including alphanumeric digits, letters, $, @, etc; indicate a Word, Number, or Hybrid token
                        default: {
                                tokens.Add(lex_alphanumeric(cursor, state));
                                break;
                            }
                            #endregion
                    }

                    state.IsStartOfLine = false;

                } while(cursor.Move(1));

                tokens.Add(new(IToken.EOF) {
                    Position = cursor.Position + 1,
                    Line = cursor.Line,
                    Column = cursor.Column + 1,
                    Length = 0
                });

                return new Success(cursor.Text, [.. tokens]);

                #region Local Helper Functions

                void appendToken_newLine(int length) {
                    appendToken_lengthOf(length, IToken.NEWLINE, true);
                    state._endLine();
                }

                void appendToken_length1(IToken type)
                    => appendToken_lengthOf(1, type, false);

                void appendToken_length2(IToken type)
                    => appendToken_lengthOf(2, type, true);

                void appendToken_length3(IToken type)
                    => appendToken_lengthOf(3, type, true);

                void appendToken_lengthOf(int length, IToken type, bool skip = true) {
                    tokens.Add(new_token(type, length));

                    if(skip) {
                        cursor.Skip(length - 1);
                    }
                }

                void appendToken_quote(IToken type) {
                    Token quote = new_token(type);
                    if(state.CurrentQuote is null) {
                        state.CurrentQuote = quote;
                    }
                    else if(state.CurrentQuote.Type == type) {
                        state.CurrentQuote = null;
                    }

                    tokens.Add(quote);
                }

                bool tryAppendToken_length2(IToken type)
                    => tryAppendToken(2, type);

                bool tryAppendToken(int length, IToken type) {
                    Token token = new_token(type, length);
                    if(cursor.Move(length - 1)) {
                        tokens.Add(token);
                        return true;
                    }
                    else {
                        tokens.Add(new Token.Incomplete(type) {
                            Position = cursor.Position,
                            Line = cursor.Line,
                            Column = cursor.Column,
                            Length = cursor.Buffer
                        });

                        return false;
                    }
                }

                Failure fail_withError(
                    ErrorCode code,
                    IToken? type = null,
                    object? data = null,
                    object? found = null,
                    object? expected = null,
                    string? summary = null,
                    int? length = null,
                    int? position = null
                ) => new(cursor.Text, [
                        new Error(
                            code,
                            position ?? cursor.Position,
                            cursor.Line,
                            cursor.Column,
                            type,
                            data,
                            summary,
                            found?.ToString(),
                            expected?.ToString(),
                            length ?? 1
                        )
                    ], tokens);

                #endregion
            }

            #region Local Helper Functions

            Token new_token(IToken type, int length = 1)
                => new(type) {
                    Position = cursor.Position,
                    Line = cursor.Line,
                    Column = cursor.Column,
                    Length = length
                };

            void lex_indents(TextCursor cursor, List<Token> tokens, State state) {
                while(cursor.Read([' ', '\t'], out char indent)) {
                    state._pushIndent(indent);
                }

                state._endIndents(cursor, tokens);
            }

            Token lex_alphanumeric(
                TextCursor cursor,
                State state
            ) {
                int start = cursor.Position;
                int line = cursor.Line;
                int column = cursor.Column;
                bool isNumeric = true;

                do {
                    // check for pure numbers
                    if(isNumeric && !char.IsDigit(cursor.Current)) {
                        isNumeric = false;
                    }

                    // check for end of word via whitespace, end of source, or invalid symbols
                    if(!isValid_wordChar(1, false)) {
                        break;
                    } // check for link symbols (only allowed in the middle of a word; once)
                    else if(isValid_linkChar(1)) {
                        if(isValid_afterLinkChar(2)) {
                            if(cursor.Next is '\'' && state.CurrentQuote?.Type == IToken.SINGLE_QUOTE) {
                                break;
                            }

                            // equations get special treatment and are split
                            if(isNumeric && char.IsDigit(cursor.Peek(2))) {
                                break;
                            }
                        }
                        else {
                            break;
                        }
                    }
                } while(cursor.Move(1));

                return new(
                    isNumeric
                        ? IToken.NUMBER
                        : IToken.WORD
                ) {
                    Position = start,
                    Line = line,
                    Column = column,
                    Length = cursor.Position - start + 1
                };

                #region Local Helper Functions

                bool isValid_wordChar(int offset = 0, bool checkForLinkChars = true)
                    => cursor.Peek(offset, out char peeked)
                       && !INVALID_WORD_SYMBOLS.Contains(peeked)
                       && !char.IsWhiteSpace(peeked)
                       && (!checkForLinkChars
                          || !isValid_linkChar(offset)
                          || isValid_afterLinkChar(offset + 1));

                bool isValid_afterLinkChar(int offset = 0)
                    => isValid_wordChar(offset, false)
                        && !isValid_linkChar(offset);

                bool isValid_linkChar(int offset = 0)
                    => WORD_LINK_SYMBOLS.Contains(cursor.Peek(offset));

                #endregion
            }

            #endregion
        }
    }
}
