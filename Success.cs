namespace Indra.Astra {

  public partial class Lexer {
    public record Success : Result {
      public override bool IsSuccess => true;
      public override Token[] Tokens { get; }
      public override Error[] Errors => [];
      public Success(string source, Token[] tokens)
          : base(source)
          => Tokens = tokens;
    }
  }
}
