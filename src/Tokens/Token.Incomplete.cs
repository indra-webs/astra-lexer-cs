namespace Indra.Astra.Tokens
{
  public partial class Token
  {
    public class Incomplete(Tokens.IToken type)
        : Token(type)
    {
      public override string Name
          => $"!{base.Name}";

      override public bool IsValid
          => false;

      override public string? GetExtraInfo()
          => $"*INCOMPLETE*";
    }

  }
}