namespace Indra.Astra.Tokens
{
  public interface IBlockComment
  : IComment,
    IDelimiter
  {
    public new IBlockComment Pair
      => (IBlockComment)(this as IDelimiter).Pair;
  }
}
