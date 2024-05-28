using System.Text;

namespace Indra.Astra {

    public partial class Lexer {

        public enum ErrorCode {
            UNKNOWN,
            UNMATCHED_DELIMITER,
            UNEXPECTED_EOF
        }

        public readonly record struct Error(
            ErrorCode Code,
            int Position,
            int Line,
            int Column,
            TokenType? Token = null,
            object? Data = null,
            string? Summary = null,
            string? Found = null,
            string? Expected = null,
            int Length = 1
        ) {
            public static readonly string[] Messages
                = [
                    "An unknown error occurred",
                    "An unmatched delimiter was encountered",
                    "An unexpected end of file was encountered"
                ];

            override public string ToString()
                => $"ERROR({(int)Code}): {Code} @ [I:{Position}](L:{Line}, C:{Column})\n\t\t :: {Message}";

            public string Message {
                get {
                    StringBuilder message = new(Summary ?? Messages[(int)Code]);

                    if(Token is not null) {
                        message.Append($" in token of type: {Token}.");
                    }

                    if(Found is not null) {
                        message.Append($"\n\t\t + {"Found",-(3 * 8)}: {Found}");
                    }

                    if(Expected is not null) {
                        message.Append($"\n\t\t + {"Expected",-(3 * 8)}: {Expected}");
                    }

                    if(Data is not null) {
                        foreach(System.Reflection.PropertyInfo prop in Data.GetType().GetProperties(
                            System.Reflection.BindingFlags.Instance
                            | System.Reflection.BindingFlags.Public
                        )) {
                            message.Append($"\n\t\t + {prop.Name,-(3 * 8)}: {prop.GetValue(Data)}");
                        }
                    }

                    return message.ToString();
                }
            }
        }
    }
}
