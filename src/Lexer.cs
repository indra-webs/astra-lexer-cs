using Indra.Astra.Tokens;

using Meep.Tech.Data;
using Meep.Tech.Text;

namespace Indra.Astra {

    /// <summary>
    /// A lexer for the Astra family of languages.
    /// </summary>
    public partial class Lexer {

        /// <summary>
        /// Lexes the input text into a sequence of tokens.
        /// </summary>
        public Result Lex(IEnumerable<char> input) {
            TextCursor cursor = new(input);

            if(cursor.SourceIsEmpty) {
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
                                while(cursor.ReadNext(' ', '\t', '\0')) {
                                    continue;
                                }

                                break;
                            }
                        #endregion
                        #region Symbols
                        #region Brackets
                        case '(': {
                                appendToken_length1_ofType<LeftParenthesis>();
                                break;
                            }
                        case ')': {
                                appendToken_length1_ofType<RightParenthesis>();
                                break;
                            }
                        case '[': {
                                appendToken_length1_ofType<LeftBracket>();
                                break;
                            }
                        case ']': {
                                appendToken_length1_ofType<RightBracket>();
                                break;
                            }
                        case '{': {
                                appendToken_length1_ofType<LeftBrace>();
                                break;
                            }
                        case '}': {
                                appendToken_length1_ofType<RightBrace>();
                                break;
                            }
                        case '<': {
                                switch(cursor.Next) {
                                    case '<': {
                                            switch(cursor.Peek(2)) {
                                                case '<': {
                                                        appendToken_length3_ofType<TripleLeftAngle>();
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2_ofType<DoubleLeftAngle>();
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2_ofType<LeftEqualArrow>();
                                            break;
                                        }
                                    case '-': {
                                            appendToken_length2_ofType<LeftDashArrow>();
                                            break;
                                        }
                                    case '~': {
                                            appendToken_length2_ofType<LeftTildeArrow>();
                                            break;
                                        }
                                    case '+': {
                                            appendToken_length2_ofType<LeftPlusArrow>();
                                            break;
                                        }
                                    default: {
                                            appendToken_length1_ofType<LeftAngle>();
                                            break;
                                        }
                                }

                                break;
                            }
                        case '>': {
                                if(cursor.Next is '>') {
                                    switch(cursor.Peek(2)) {
                                        case '>':
                                            appendToken_length3_ofType<TripleRightAngle>();
                                            break;
                                        default:
                                            appendToken_length2_ofType<DoubleRightAngle>();
                                            break;
                                    }

                                    break;
                                }
                                else {
                                    appendToken_length1_ofType<RightAngle>();
                                    break;
                                }
                            }
                        #endregion
                        #region Quotes
                        case '\'': {
                                appendToken_quote<SingleQuote>();
                                break;
                            }
                        case '"': {
                                appendToken_quote<DoubleQuote>();
                                break;
                            }
                        case '`': {
                                appendToken_quote<Backtick>();
                                break;
                            }
                        #endregion
                        #region Other
                        case ':': {
                                switch(cursor.Next) {
                                    case ':': {
                                            switch(cursor.Peek(2)) {
                                                case ':': {
                                                        appendToken_length3_ofType<TripleColon>();
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2_ofType<DoubleColon>();
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2_ofType<ColonEqual>();
                                            break;
                                        }
                                    default: {
                                            appendToken_length1_ofType<Colon>();
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
                                                        appendToken_length3_ofType<TripleDot>();
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2_ofType<DoubleDot>();
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2_ofType<DotEqual>();
                                            break;
                                        }
                                    default: {
                                            appendToken_length1_ofType<Dot>();
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
                                                        appendToken_length3_ofType<DoubleHashEqual>();
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2_ofType<DoubleHash>();
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2_ofType<HashEqual>();
                                            break;
                                        }
                                    default: {
                                            appendToken_length1_ofType<Hash>();
                                            break;
                                        }
                                }

                                break;
                            }
                        case '+': {
                                switch(cursor.Next) {
                                    case '+': {
                                            appendToken_length2_ofType<DoublePlus>();
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2_ofType<PlusEqual>();
                                            break;
                                        }
                                    case '>': {
                                            appendToken_length2_ofType<RightPlusArrow>();
                                            break;
                                        }
                                    default: {
                                            appendToken_length1_ofType<Plus>();
                                            break;
                                        }
                                }

                                break;
                            }
                        case '-': {
                                switch(cursor.Next) {
                                    case '-': {
                                            switch(cursor.Peek(2)) {
                                                case '-' when cursor.IsStartOfLine: {
                                                        appendToken_length3_ofType<TripleDash>();
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2_ofType<DoubleDash>();
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2_ofType<DashEqual>();
                                            break;
                                        }
                                    case '>': {
                                            appendToken_length2_ofType<RightDashArrow>();
                                            break;
                                        }
                                    default: {
                                            appendToken_length1_ofType<Dash>();
                                            break;
                                        }
                                }

                                break;
                            }
                        case '*': {
                                switch(cursor.Next) {
                                    case '*': {
                                            appendToken_length2_ofType<DoubleStar>();
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2_ofType<StarEqual>();
                                            break;
                                        }
                                    case '/' when state.InCommentBlock: {
                                            appendToken_length2_ofType<CloseBlockComment>();
                                            state.InCommentBlock = false;
                                            break;
                                        }
                                    default: {
                                            appendToken_length1_ofType<Star>();
                                            break;
                                        }
                                }

                                break;
                            }
                        case '/': {
                                switch(cursor.Next) {
                                    case '/': {
                                            appendToken_length2_ofType<DoubleSlash>();
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2_ofType<SlashEqual>();
                                            break;
                                        }
                                    case '*' when cursor.Previous.IsWhiteSpaceOrNull() && cursor.Peek(2).IsWhiteSpaceOrNull(): {
                                            appendToken_length2_ofType<OpenBlockComment>();
                                            state.InCommentBlock = true;
                                            break;
                                        }
                                    default: {
                                            appendToken_length1_ofType<Slash>();
                                            break;
                                        }
                                }

                                break;
                            }
                        case '~': {
                                switch(cursor.Next) {
                                    case '~': {
                                            appendToken_length2_ofType<DoubleTilde>();
                                            break;
                                        }
                                    case '>': {
                                            appendToken_length2_ofType<RightTildeArrow>();
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2_ofType<TildeEqual>();
                                            break;
                                        }
                                    default: {
                                            appendToken_length1_ofType<Tilde>();
                                            break;
                                        }
                                }

                                break;
                            }
                        case '%': {
                                switch(cursor.Next) {
                                    case '%': {
                                            appendToken_length2_ofType<DoublePercent>();
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2_ofType<PercentEqual>();
                                            break;
                                        }
                                    default: {
                                            appendToken_length1_ofType<Percent>();
                                            break;
                                        }
                                }

                                break;
                            }
                        case '=': {
                                switch(cursor.Next) {
                                    case '=': {
                                            appendToken_length2_ofType<DoubleEqual>();
                                            break;
                                        }
                                    case '>': {
                                            appendToken_length2_ofType<RightEqualArrow>();
                                            break;
                                        }
                                    default: {
                                            appendToken_length1_ofType<Equal>();
                                            break;
                                        }
                                }

                                break;
                            }
                        case '&': {
                                switch(cursor.Next) {
                                    case '&': {
                                            appendToken_length2_ofType<DoubleAnd>();
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2_ofType<AndEqual>();
                                            break;
                                        }
                                    default: {
                                            appendToken_length1_ofType<And>();
                                            break;
                                        }
                                }

                                break;
                            }
                        case '|': {
                                switch(cursor.Next) {
                                    case '|': {
                                            appendToken_length2_ofType<DoublePipe>();
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2_ofType<PipeEqual>();
                                            break;
                                        }
                                    default: {
                                            appendToken_length1_ofType<Pipe>();
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
                                                        appendToken_length3_ofType<DoubleQuestionEqual>();
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2_ofType<DoubleQuestion>();
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2_ofType<QuestionEqual>();
                                            break;
                                        }
                                    default: {
                                            appendToken_length1_ofType<Question>();
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
                                                        appendToken_length3_ofType<DoubleBangEqual>();
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2_ofType<DoubleBang>();
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2_ofType<BangEqual>();
                                            break;
                                        }
                                    default: {
                                            appendToken_length1_ofType<Bang>();
                                            break;
                                        }
                                }

                                break;
                            }
                        case ',': {
                                appendToken_length1_ofType<Comma>();
                                break;
                            }
                        case ';': {
                                switch(cursor.Next) {
                                    case ';': {
                                            appendToken_length2_ofType<DoubleSemiColon>();
                                            break;
                                        }
                                    default: {
                                            appendToken_length1_ofType<SemiColon>();
                                            break;
                                        }
                                }

                                break;
                            }
                        #endregion
                        #endregion
                        #region Variable
                        // escape sequences via backslash
                        case '\\': {
                                if(cursor.ReadNext("eof") || cursor.ReadNext("EOF")) {
                                    appendToken_ofType<Escape>(2, false);
                                }
                                else {
                                    if(!tryAppend_token<Escape>(2)) {
                                        appendToken_ofType<Backslash>(1);
                                    }
                                    else {
                                        appendToken_ofType<Escape>(2);
                                    }
                                }

                                break;
                            }
                        // numbers and words
                        // all other characters, including alphanumeric digits, letters, $, @, etc; indicate a Word, Number, or Hybrid token
                        default: {
                                Token wordOrNumber = lex_alphanumeric(cursor, state);
                                tokens.Add(wordOrNumber);

                                break;
                            }
                            #endregion
                    }

                    state.IsStartOfLine = false;

                } while(cursor.Move(1));

                tokens.Add(new Token.OfType<EndOfFile> {
                    Index = cursor.Position + 1,
                    Line = cursor.Line,
                    Column = cursor.Column + 1,
                    Length = 0
                });

                return new Success(cursor.Text, [.. tokens]);

                #region Local Helper Functions

                void appendToken(TokenType type, int length, bool read = true) {
                    tokens.Add(new_token(type, length));

                    if(read) {
                        cursor.Skip(length - 1);
                    }
                }

                void appendToken_ofType<T>(int length, bool read = true)
                    where T : TokenType<T>
                    => appendToken(Types.Get<T>(), length, read);

                void appendToken_length1_ofType<T>()
                    where T : TokenType<T>
                    => appendToken_ofType<T>(1, false);

                void appendToken_length2_ofType<T>()
                    where T : TokenType<T>
                    => appendToken_ofType<T>(2, true);

                void appendToken_length3_ofType<T>()
                    where T : TokenType<T>
                    => appendToken_ofType<T>(3, true);

                void appendToken_newLine(int length) {
                    appendToken_ofType<NewLine>(length);
                    state._endLine();
                }

                void appendToken_quote<TQuote>()
                    where TQuote : TokenType<TQuote>, IQuote<TQuote> {
                    TQuote type = Types.Get<TQuote>();
                    Token quote = new_token(type);
                    if(state.CurrentQuote is null) {
                        state.CurrentQuote = quote;
                    }
                    else if(state.CurrentQuote.Type == type) {
                        state.CurrentQuote = null;
                    }

                    tokens.Add(quote);
                }

                bool tryAppend_token<T>(int length)
                    where T : TokenType<T> {
                    Token token = new_token_ofType<T>(length);
                    if(cursor.Move(length - 1)) {
                        tokens.Add(token);
                        return true;
                    }
                    else {
                        tokens.Add(new Token.Incomplete(Types.Get<T>()) {
                            Index = cursor.Position,
                            Line = cursor.Line,
                            Column = cursor.Column,
                            Length = cursor.Buffer
                        });

                        return false;
                    }
                }

#pragma warning disable CS8321 // Local function is declared but never used
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
#pragma warning restore CS8321 // Local function is declared but never used

                #endregion
            }

            #region Local Helper Functions

            Token new_token(TokenType type, int length = 1)
                => new(type) {
                    Index = cursor.Position,
                    Line = cursor.Line,
                    Column = cursor.Column,
                    Length = length
                };

            Token new_token_ofType<T>(int length = 1)
                where T : TokenType<T>
                => new Token.OfType<T> {
                    Index = cursor.Position,
                    Line = cursor.Line,
                    Column = cursor.Column,
                    Length = length
                };

            void lex_indents(TextCursor cursor, List<Token> tokens, State state) {
                while(cursor.Read(out char indent, [' ', '\t'])) {
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
                    } // check for link symbols (allowed once without repetition in the middle of the word)
                    else if(isValid_linkChar(1)) {
                        if(isValid_afterLinkChar(2)) {
                            if(cursor.Next is '\'' && state.CurrentQuote?.Type is SingleQuote) {
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
                        ? Number.Type
                        : Word.Type
                ) {
                    Index = start,
                    Line = line,
                    Column = column,
                    Length = cursor.Position - start + 1
                };

                #region Local Helper Functions

                bool isValid_wordChar(int offset = 0, bool checkForLinkChars = true)
                    => cursor.Peek(offset, out char peeked)
                       && !Types.InvalidWordChars.Contains(peeked)
                       && !char.IsWhiteSpace(peeked)
                       && (!checkForLinkChars
                          || !isValid_linkChar(offset)
                          || isValid_afterLinkChar(offset + 1));

                bool isValid_afterLinkChar(int offset = 0)
                    => isValid_wordChar(offset, false)
                        && !isValid_linkChar(offset);

                bool isValid_linkChar(int offset = 0)
                    => Types.WordLinkingChars.Contains(cursor.Peek(offset));

                #endregion
            }

            #endregion
        }
    }
}
