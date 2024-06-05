using System.Diagnostics.CodeAnalysis;

using Meep.Tech.Data;
using Meep.Tech.Text;

namespace Indra.Astra {

    public partial class Lexer {

        public static readonly HashSet<char> VALID_KEY_LINK_SYMBOLS
            = ['+', '-', '~', '%', '\''];

        public static readonly HashSet<char> INVALID_PLAIN_KEY_SYMBOLS
            = ['.', '/', '?', '!', '&', '|', '=', '<', '>', ',', ':', ';', '"', '`', '#', '*', '(', ')', '[', ']', '{', '}', '<', '>'];

        public static readonly IReadOnlyDictionary<TokenType, TokenType> DelimiterPairs
            = new Dictionary<TokenType, TokenType> {
                { TokenType.LEFT_PARENTHESIS, TokenType.RIGHT_PARENTHESIS },
                { TokenType.RIGHT_PARENTHESIS, TokenType.LEFT_PARENTHESIS },
                { TokenType.LEFT_BRACKET, TokenType.RIGHT_BRACKET },
                { TokenType.RIGHT_BRACKET, TokenType.LEFT_BRACKET },
                { TokenType.LEFT_BRACE, TokenType.RIGHT_BRACE },
                { TokenType.RIGHT_BRACE, TokenType.LEFT_BRACE },
                { TokenType.LEFT_ANGLE, TokenType.RIGHT_ANGLE },
                { TokenType.RIGHT_ANGLE, TokenType.LEFT_ANGLE },
                { TokenType.SINGLE_QUOTE, TokenType.SINGLE_QUOTE },
                { TokenType.DOUBLE_QUOTE, TokenType.DOUBLE_QUOTE },
                { TokenType.BACKTICK, TokenType.BACKTICK },
                { TokenType.OPEN_BLOCK_COMMENT, TokenType.CLOSE_BLOCK_COMMENT},
                { TokenType.CLOSE_BLOCK_COMMENT, TokenType.OPEN_BLOCK_COMMENT }
            };

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
                        _lex_indents(cursor, tokens, state);
                    }

                    char current = cursor.Current;
                    switch(current) {
                        #region Whitespace
                        // check for newlines and carriage returns
                        case '\r': { // CR
                                appendToken_newLine(cursor.Next is '\n' ? 2 : 1);
                                state._endLine();
                                continue;
                            }
                        case '\n': {// LF
                                appendToken_newLine(cursor.Next is '\r' ? 2 : 1);
                                state._endLine();
                                continue;
                            }
                        // skip spaces and tabs and null chars within a line (after indentation & first token / between tokens / end of line)
                        case ' ' or '\t' or '\0': {
                                continue;
                            }
                        #endregion
                        #region Symbols
                        #region Brackets
                        case '(': {
                                appendToken_startDelimiter(TokenType.LEFT_PARENTHESIS);
                                break;
                            }
                        case ')': {
                                if(tryAppendToken_endDelimiter(TokenType.RIGHT_PARENTHESIS, out Failure? failure)) {
                                    break;
                                }
                                else {
                                    appendToken_length1(TokenType.RIGHT_PARENTHESIS);
                                    return failure;
                                }
                            }
                        case '[': {
                                appendToken_startDelimiter(TokenType.LEFT_BRACKET);
                                break;
                            }
                        case ']': {
                                if(tryAppendToken_endDelimiter(TokenType.RIGHT_BRACKET, out Failure? failure)) {
                                    break;
                                }
                                else {
                                    appendToken_length1(TokenType.RIGHT_BRACKET);
                                    return failure;
                                }
                            }
                        case '{': {
                                appendToken_startDelimiter(TokenType.LEFT_BRACE);
                                break;
                            }
                        case '}': {
                                if(tryAppendToken_endDelimiter(TokenType.RIGHT_BRACE, out Failure? failure)) {
                                    break;
                                }
                                else {
                                    appendToken_length1(TokenType.RIGHT_BRACE);
                                    return failure;
                                }
                            }
                        case '<': {
                                switch(cursor.Next) {
                                    case '<': {
                                            switch(cursor.Peek(2)) {
                                                case '<': {
                                                        appendToken_length3(TokenType.TRIPLE_LEFT_ANGLE);
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2(TokenType.DOUBLE_LEFT_ANGLE);
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(TokenType.LEFT_EQUALS_ARROW);
                                            break;
                                        }
                                    case '-': {
                                            appendToken_length2(TokenType.LEFT_DASH_ARROW);
                                            break;
                                        }
                                    case '~': {
                                            appendToken_length2(TokenType.LEFT_TILDE_ARROW);
                                            break;
                                        }
                                    case '+': {
                                            appendToken_length2(TokenType.LEFT_PLUS_ARROW);
                                            break;
                                        }
                                    default: {
                                            if(cursor.Previous.IsWhiteSpaceOrNull()) {
                                                if(cursor.Next.IsWhiteSpace()) {
                                                    if(state.IsStartOfLine) {
                                                        appendToken_length1(TokenType.LEFT_CHEVRON);
                                                    }
                                                    else {
                                                        appendToken_length1(TokenType.LESS_THAN);
                                                    }
                                                }
                                                else {
                                                    appendToken_length1(TokenType.LEFT_CHEVRON);
                                                }
                                            }
                                            else if(cursor.Next is '+') {
                                                appendToken_length2(TokenType.LEFT_PLUS_ARROW);
                                            }
                                            else {
                                                appendToken_startDelimiter(TokenType.LEFT_ANGLE);
                                            }

                                            break;
                                        }
                                }

                                break;
                            }
                        case '>': {
                                if(tryAppendToken_closeDelimiter(TokenType.RIGHT_ANGLE)) {
                                    break;
                                }
                                else if(cursor.Next is '>') {
                                    switch(cursor.Peek(2)) {
                                        case '>':
                                            appendToken_lengthOf(3, TokenType.TRIPLE_RIGHT_ANGLE, true);
                                            break;
                                        default:
                                            appendToken_length2(TokenType.DOUBLE_RIGHT_ANGLE);
                                            break;
                                    }

                                    break;
                                }
                                else if(cursor.Next is '=') {
                                    appendToken_length2(TokenType.GREATER_EQUALS);
                                    break;
                                }
                                else if(cursor.Previous.IsWhiteSpaceOrNull()) {
                                    if(cursor.Next.IsWhiteSpace()) {
                                        if(state.IsStartOfLine) {
                                            appendToken_length1(TokenType.RIGHT_CHEVRON);
                                        }
                                        else {
                                            appendToken_length1(TokenType.GREATER_THAN);
                                        }
                                    }
                                    else {
                                        appendToken_length1(TokenType.RIGHT_CHEVRON);
                                    }

                                    break;
                                }
                                else if(tokens.LastOrDefault()?.Type.IsDelimiter() ?? true) {
                                    appendToken_length1(TokenType.RIGHT_CHEVRON);
                                    break;
                                }
                                else if(tryAppendToken_orphanDelimiter_inQuote(TokenType.RIGHT_ANGLE)) {
                                    break;
                                }
                                else {
                                    appendToken_length1(TokenType.RIGHT_ANGLE);
                                    return fail_withError(
                                        ErrorCode.UNMATCHED_DELIMITER,
                                        TokenType.RIGHT_ANGLE
                                    );
                                }
                            }
                        #endregion
                        #region Quotes
                        case '\'': {
                                if(tryAppendToken_endQuote(TokenType.SINGLE_QUOTE)) {
                                    break;
                                }
                                else {
                                    appendToken_startDelimiter(TokenType.SINGLE_QUOTE);
                                    break;
                                }
                            }
                        case '"': {
                                if(tryAppendToken_endQuote(TokenType.DOUBLE_QUOTE)) {
                                    break;
                                }
                                else {
                                    appendToken_startDelimiter(TokenType.DOUBLE_QUOTE);
                                    break;
                                }
                            }
                        case '`': {
                                if(tryAppendToken_endQuote(TokenType.BACKTICK)) {
                                    break;
                                }
                                else {
                                    appendToken_startDelimiter(TokenType.BACKTICK);
                                    break;
                                }
                            }
                        #endregion
                        case ':': {
                                if(cursor.Next.IsWhiteSpaceOrNull()) {
                                    appendToken_length1(TokenType.COLON_ASSIGNER);
                                }
                                else if(cursor.Next is ':') {
                                    switch(cursor.Peek(2)) {
                                        case ':': {
                                                appendToken_length3(TokenType.TRIPLE_COLON);
                                                break;
                                            }
                                        case '=': {
                                                appendToken_length3(TokenType.DOUBLE_COLON_EQUALS);
                                                break;
                                            }
                                        case '>': {
                                                if(cursor.Peek(3) is '>') {
                                                    appendToken_lengthOf(4, TokenType.DOUBLE_COLON_DOUBLE_RIGHT_ANGLE, true);
                                                }
                                                else {
                                                    appendToken_length3(TokenType.DOUBLE_COLON_RIGHT_ANGLE);
                                                }

                                                break;
                                            }
                                        default: {
                                                if(cursor.Peek(2).IsWhiteSpace()) {
                                                    appendToken_length2(TokenType.DOUBLE_COLON);
                                                }
                                                else {
                                                    appendToken_length2(TokenType.DOUBLE_COLON_PREFIX);
                                                }

                                                break;
                                            }
                                    }
                                }
                                else if(cursor.Next is '=') {
                                    appendToken_length2(TokenType.COLON_EQUALS);
                                }
                                else if(cursor.Next is '>') {
                                    switch(cursor.Peek(2)) {
                                        case '>':
                                            appendToken_length3(TokenType.COLON_DOUBLE_RIGHT_ANGLE);
                                            break;
                                        default:
                                            appendToken_length2(TokenType.COLON_RIGHT_ANGLE);
                                            break;
                                    }
                                }
                                else {
                                    appendToken_length1(TokenType.COLON);
                                }

                                break;
                            }
                        case '.': {
                                switch(cursor.Next) {
                                    case '.': {
                                            switch(cursor.Peek(2)) {
                                                case '.': {
                                                        appendToken_length3(TokenType.TRIPLE_DOT);
                                                        break;
                                                    }
                                                case '#': {
                                                        appendToken_length3(TokenType.DOUBLE_DOT_HASH);
                                                        break;
                                                    }
                                                case '?': {
                                                        appendToken_length3(TokenType.DOUBLE_DOT_QUESTION);
                                                        break;
                                                    }
                                                case '!': {
                                                        appendToken_length3(TokenType.DOUBLE_DOT_BANG);
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2(TokenType.DOUBLE_DOT);
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '?': {
                                            if(cursor.Peek(2) is '#') {
                                                appendToken_length3(TokenType.DOT_QUESTION_HASH);
                                            }
                                            else {
                                                appendToken_length2(TokenType.DOT_QUESTION);
                                            }

                                            break;
                                        }
                                    case '!': {
                                            if(cursor.Peek(2) is '#') {
                                                appendToken_length3(TokenType.DOT_BANG_HASH);
                                            }
                                            else {
                                                appendToken_length2(TokenType.DOT_BANG);
                                            }

                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(TokenType.DOT_EQUALS);
                                            break;
                                        }
                                    case '#': {
                                            appendToken_length2(TokenType.DOT_HASH);
                                            break;
                                        }
                                    default: {
                                            if(cursor.Next.IsDigit()) {
                                                appendToken_length1(TokenType.DOT);
                                            }
                                            else {
                                                appendToken_length1(TokenType.DOT);
                                            }

                                            break;
                                        }
                                }

                                break;
                            }
                        case '#': {
                                switch(cursor.Next) {
                                    case '#': {
                                            if(cursor.Peek(2) is '=') {
                                                appendToken_length3(TokenType.DOUBLE_HASH_EQUALS);
                                            }
                                            else if(cursor.Peek(2) is ':') {
                                                switch(cursor.Peek(3)) {
                                                    case ':':
                                                        appendToken_lengthOf(4, TokenType.DOUBLE_HASH_DOUBLE_COLON);
                                                        break;
                                                    default:
                                                        appendToken_length3(TokenType.DOUBLE_HASH_COLON);
                                                        break;
                                                }
                                            }
                                            else if(state.IsStartOfLine && cursor.Peek(2).IsWhiteSpace()) {
                                                appendToken_length2(TokenType.DOC_HASH_COMMENT);
                                            }
                                            else {
                                                appendToken_length2(TokenType.DOUBLE_HASH);
                                            }

                                            break;
                                        }
                                    case ':': {
                                            appendToken_length2(TokenType.HASH_COLON);
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(TokenType.HASH_EQUALS);
                                            break;
                                        }
                                    case '!': {
                                            appendToken_length2(TokenType.HASH_BANG);
                                            break;
                                        }
                                    default: {
                                            if(cursor.Previous.IsWhiteSpace() && cursor.Next.IsWhiteSpace()) {
                                                appendToken_length1(TokenType.EOL_HASH_COMMENT);
                                            }
                                            else {
                                                appendToken_length1(TokenType.HASH);
                                            }

                                            break;
                                        }
                                }

                                break;
                            }
                        case '+': {
                                switch(cursor.Next) {
                                    case '+': {
                                            appendToken_length2(TokenType.DOUBLE_PLUS);
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(TokenType.PLUS_EQUALS);
                                            break;
                                        }
                                    case '>': {
                                            appendToken_length2(TokenType.RIGHT_PLUS_ARROW);
                                            break;
                                        }
                                    default: {
                                            if((!state.IsStartOfLine && isValid_mathSpacing())
                                                || isValid_mathPrefix()
                                            ) {
                                                appendToken_length1(TokenType.PLUS);
                                            }
                                            else {
                                                appendToken_length1(TokenType.CROSS);
                                            }

                                            break;
                                        }
                                }

                                break;
                            }
                        case '-': {
                                switch(cursor.Next) {
                                    case '-': {
                                            appendToken_length2(TokenType.DOUBLE_DASH);
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(TokenType.MINUS_EQUALS);
                                            break;
                                        }
                                    case '>': {
                                            appendToken_length2(TokenType.RIGHT_DASH_ARROW);
                                            break;
                                        }
                                    default: {
                                            if((!state.IsStartOfLine && isValid_mathSpacing())
                                                || isValid_mathPrefix()
                                            ) {
                                                appendToken_length1(TokenType.MINUS);
                                            }
                                            else {
                                                appendToken_length1(TokenType.DASH);
                                            }

                                            break;
                                        }
                                }

                                break;
                            }
                        case '*': {
                                switch(cursor.Next) {
                                    case '*':
                                        appendToken_length2(TokenType.DOUBLE_TIMES);
                                        break;
                                    case '=':
                                        appendToken_length2(TokenType.TIMES_EQUALS);
                                        break;
                                    case '/':
                                        if(tryAppendToken_endDelimiter(TokenType.CLOSE_BLOCK_COMMENT, out Failure? failure, 2)) {
                                            break;
                                        }
                                        else {
                                            appendToken_length1(TokenType.STAR);
                                            break;
                                        }
                                    default:
                                        if(!state.IsStartOfLine && isValid_mathSpacing()) {
                                            appendToken_length1(TokenType.TIMES);
                                        }
                                        else {
                                            appendToken_length1(TokenType.STAR);
                                        }

                                        break;
                                }

                                break;
                            }
                        case '/': {
                                switch(cursor.Next) {
                                    case '/':
                                        if(cursor.Peek(2).IsWhiteSpace() && cursor.Previous.IsWhiteSpaceOrNull()) {
                                            appendToken_length2(TokenType.EOL_SLASH_COMMENT);
                                        }
                                        else {
                                            appendToken_length2(TokenType.DOUBLE_DIVISION);
                                        }

                                        break;
                                    case '=':
                                        appendToken_length2(TokenType.DIVISION_EQUALS);
                                        break;
                                    case '*' when cursor.Peek(2).IsWhiteSpace():
                                        appendToken_startDelimiter(TokenType.OPEN_BLOCK_COMMENT, 2);
                                        break;
                                    default:
                                        if(isValid_mathSpacing()) {
                                            appendToken_length1(TokenType.DIVISION);
                                        }
                                        else {
                                            appendToken_length1(TokenType.SLASH);
                                        }

                                        break;
                                }

                                break;
                            }
                        case '~': {
                                switch(cursor.Next) {
                                    case '~': {
                                            appendToken_length2(TokenType.DOUBLE_TILDE);
                                            break;
                                        }
                                    case '>': {
                                            appendToken_length2(TokenType.RIGHT_TILDE_ARROW);
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(TokenType.TILDE_EQUALS);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(TokenType.TILDE);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '%': {
                                switch(cursor.Next) {
                                    case '%': {
                                            appendToken_length2(TokenType.DOUBLE_PERCENT);
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(TokenType.PERCENT_EQUALS);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(TokenType.PERCENT);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '=': {
                                switch(cursor.Next) {
                                    case '=': {
                                            appendToken_length2(TokenType.DOUBLE_EQUALS);
                                            break;
                                        }
                                    case '>': {
                                            appendToken_length2(TokenType.RIGHT_EQUALS_ARROW);
                                            break;
                                        }
                                    case '<': {
                                            appendToken_length2(TokenType.EQUALS_LESS);
                                            break;
                                        }
                                    case ':': {
                                            appendToken_length2(TokenType.EQUALS_COLON);
                                            break;
                                        }
                                    case '~': {
                                            appendToken_length2(TokenType.EQUALS_TILDE);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(TokenType.EQUALS);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '&': {
                                switch(cursor.Next) {
                                    case '&': {
                                            appendToken_length2(TokenType.DOUBLE_AMPERSAND);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(TokenType.AND);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '|': {
                                switch(cursor.Next) {
                                    case '|': {
                                            appendToken_length2(TokenType.DOUBLE_PIPE);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(TokenType.PIPE);
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
                                                        appendToken_length3(TokenType.DOUBLE_QUESTION_EQUALS);
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2(TokenType.DOUBLE_QUESTION);
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '.': {
                                            if(cursor.Peek(2) is '#') {
                                                appendToken_length3(TokenType.QUESTION_DOT_HASH);
                                            }
                                            else {
                                                appendToken_length2(TokenType.QUESTION_DOT);
                                            }

                                            break;
                                        }
                                    case '#': {
                                            appendToken_length2(TokenType.QUESTION_HASH);
                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(TokenType.QUESTION_EQUALS);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(TokenType.QUESTION);
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
                                                        appendToken_length3(TokenType.DOUBLE_BANG_EQUALS);
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2(TokenType.DOUBLE_BANG);
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '=': {
                                            appendToken_length2(TokenType.BANG_EQUALS);
                                            break;
                                        }
                                    case '.': {
                                            switch(cursor.Peek(2)) {
                                                case '#': {
                                                        appendToken_length3(TokenType.BANG_DOT_HASH);
                                                        break;
                                                    }
                                                default: {
                                                        appendToken_length2(TokenType.BANG_DOT);
                                                        break;
                                                    }
                                            }

                                            break;
                                        }
                                    case '#': {
                                            appendToken_length2(TokenType.BANG_HASH);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(TokenType.BANG);
                                            break;
                                        }
                                }

                                break;
                            }
                        case '_': {
                                switch(cursor.Next) {
                                    case '_': {
                                            if(cursor.Peek(2) is '_') {
                                                appendToken_length3(TokenType.TRIPLE_UNDERSCORE);
                                            }
                                            else {
                                                appendToken_length2(TokenType.DOUBLE_UNDERSCORE);
                                            }

                                            break;
                                        }
                                    default: {
                                            appendToken_length1(TokenType.UNDERSCORE);
                                            break;
                                        }
                                }

                                break;
                            }
                        case ',': {
                                appendToken_length1(TokenType.COMMA);
                                break;
                            }
                        case ';': {
                                switch(cursor.Next) {
                                    case ';': {
                                            appendToken_length2(TokenType.DOUBLE_SEMICOLON);
                                            break;
                                        }
                                    default: {
                                            appendToken_length1(TokenType.SEMICOLON);
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
                                    appendToken_lengthOf(4, TokenType.ESCAPE, false);
                                }
                                else {
                                    if(!tryAppendToken_length2(TokenType.ESCAPE)) {
                                        return fail_withError(
                                            ErrorCode.UNEXPECTED_EOF,
                                            TokenType.ESCAPE,
                                            position: cursor.Position + cursor.Buffer
                                        );
                                    }
                                }

                                break;
                            }
                        // all other characters, including alphanumeric digits, letters, $, @, etc; indicate a Word, Number, or Hybrid token
                        default: {
                                tokens.Add(_lex_alphanumeric(cursor, state));
                                break;
                            }
                            #endregion
                    }

                    state.IsStartOfLine = false;

                } while(cursor.Move(1));

                if(state.Closures.Count > 0) {
                    return fail_withError(
                        ErrorCode.UNEXPECTED_EOF,
                        type: DelimiterPairs[state.Closures.Current!.Type]
                    );
                }

                tokens.Add(new(TokenType.EOF) {
                    Position = cursor.Position + 1,
                    Line = cursor.Line,
                    Column = cursor.Column + 1,
                    Length = 0
                });

                return new Success(cursor.Text, [.. tokens]);

                #region Local Helper Functions

                void appendToken_newLine(int length)
                    => appendToken_lengthOf(length, TokenType.NEWLINE, true);

                void appendToken_length1(TokenType type)
                    => appendToken_lengthOf(1, type, false);

                void appendToken_length2(TokenType type)
                    => appendToken_lengthOf(2, type, true);

                void appendToken_length3(TokenType type)
                    => appendToken_lengthOf(3, type, true);

                void appendToken_lengthOf(int length, TokenType type, bool skip = true) {
                    tokens.Add(new(type) {
                        Position = cursor.Position,
                        Line = cursor.Line,
                        Column = cursor.Column,
                        Length = length
                    });

                    if(skip) {
                        cursor.Skip(length - 1);
                    }
                }

                bool tryAppendToken_length2(TokenType type)
                    => tryAppendToken(2, type);

                bool tryAppendToken(int length, TokenType type) {
                    Token token = new(type)
                    {
                        Position = cursor.Position,
                        Line = cursor.Line,
                        Column = cursor.Column,
                        Length = length
                    };

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

                void appendToken_startDelimiter(TokenType type, int length = 1) {
                    Token.Open start = new(type) {
                        Position = cursor.Position,
                        Line = cursor.Line,
                        Column = cursor.Column,
                        Length = length
                    };

                    tokens.Add(start);
                    state._pushClosure(start);
                }

                bool tryAppendToken_closeDelimiter(TokenType type, int length = 1) {
                    if(state.Closures._tryPop(type, out Token.Open? start)) {
                        Token.Close end = new(type) {
                            Position = cursor.Position,
                            Line = cursor.Line,
                            Column = cursor.Column,
                            Length = length
                        };

                        end._link(start);
                        tokens.Add(end);

                        return true;
                    }
                    else {
                        return false;
                    }
                }

                bool tryAppendToken_orphanDelimiter_inQuote(TokenType type, int length = 1) {
                    if(state.Closures.OuterQuote is not null) {
                        tokens.Add(new(type) {
                            Position = cursor.Position,
                            Line = cursor.Line,
                            Column = cursor.Column,
                            Length = length
                        });

                        return true;
                    }
                    else {
                        return false;
                    }
                }

                bool tryAppendToken_endQuote(TokenType type)
                    => tryAppendToken_closeDelimiter(type)
                    || tryAppendToken_orphanDelimiter_inQuote(type);

                bool tryAppendToken_endDelimiter(
                    TokenType type,
                    [NotNullWhen(false)] out Failure? failure,
                    int length = 1
                ) {
                    if(tryAppendToken_closeDelimiter(type, length)) {
                        failure = null;
                        return true;
                    }
                    else if(tryAppendToken_orphanDelimiter_inQuote(type, length)) {
                        failure = null;
                        return true;
                    }
                    else {
                        failure = fail_withError(
                            ErrorCode.UNMATCHED_DELIMITER,
                            found: type
                        );

                        return false;
                    }
                }

                bool isValid_mathSpacing()
                    => (cursor.Previous.IsWhiteSpace() && cursor.Next.IsWhiteSpace())
                        || (cursor.Next.IsDigit() && lastToken_isType(TokenType.NUMBER));

                bool isValid_mathPrefix()
                    => cursor.Next.IsDigit()
                        && cursor.Previous.IsWhiteSpaceOrNull();

                bool lastToken_isType(TokenType type)
                    => tokens.LastOrDefault()?.Type == type;

                Failure fail_withError(
                    ErrorCode code,
                    TokenType? type = null,
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
        }

        #region Internal Helper Functions

        private void _lex_indents(TextCursor cursor, List<Token> tokens, State state) {
            while(cursor.Read([' ', '\t'], out char indent)) {
                state._pushIndent(indent);
            }

            state._endIndents(cursor, tokens);
        }

        private Token _lex_alphanumeric(
            TextCursor cursor,
            State state
        ) {
            int start = cursor.Position;
            int line = cursor.Line;
            int column = cursor.Column;

            bool hasSymbols = false;
            bool isNumeric = true;

            do {
                // check for pure numbers
                if(isNumeric
                    && cursor.Current is not '_'
                    && !char.IsDigit(cursor.Current)
                ) {
                    isNumeric = false;
                }

                // check for end of word via whitespace, end of source, or invalid symbols
                if(!isValid_wordChar(1, false)) {
                    break;
                } // check for tailing underscores (up to 3)
                else if(cursor.Next is '_') {
                    if(cursor.Peek(2) is '_') {
                        if(cursor.Peek(3) is '_') {
                            if(!isValid_wordChar(4)) {
                                break;
                            }
                            else {
                                cursor.Skip(3);
                            }
                        }
                        else if(!isValid_wordChar(3)) {
                            break;
                        }
                        else {
                            cursor.Skip(2);
                        }
                    }
                    else if(!isValid_wordChar(2)) {
                        break;
                    }
                    else {
                        cursor.Skip(1);
                    }
                } // check for link symbols (only allowed in the middle of a word; once)
                else if(isValid_linkChar(1)) {
                    if(isValid_afterLinkChar(2)) {
                        if(cursor.Next is '\'' && state.Closures.Current?.Type == TokenType.SINGLE_QUOTE) {
                            break;
                        }

                        // equations get special treatment and are split
                        if(isNumeric && char.IsDigit(cursor.Peek(2))) {
                            break;
                        }
                        else {
                            hasSymbols = true;
                            continue;
                        }
                    }
                    else {
                        break;
                    }
                }
            } while(cursor.Move(1));

            return new(
                hasSymbols
                    ? TokenType.HYBRID
                    : isNumeric
                        ? TokenType.NUMBER
                        : TokenType.WORD
            ) {
                Position = start,
                Line = line,
                Column = column,
                Length = cursor.Position - start + 1
            };

            bool isValid_wordChar(int offset = 0, bool checkForLinkChars = true)
                => cursor.Peek(offset, out char peeked)
                   && !INVALID_PLAIN_KEY_SYMBOLS.Contains(peeked)
                   && !char.IsWhiteSpace(peeked)
                   && (!checkForLinkChars
                      || !isValid_linkChar(offset)
                      || isValid_afterLinkChar(offset + 1));

            bool isValid_afterLinkChar(int offset = 0)
                => isValid_wordChar(offset, false)
                    && !isValid_linkChar(offset);

            bool isValid_linkChar(int offset = 0)
                => VALID_KEY_LINK_SYMBOLS.Contains(cursor.Peek(offset));
        }

        #endregion
    }
}
