namespace Indra.Astra.Tokens {

  /// <summary>
  /// A token whose value is always the same and is always empty.
  /// </summary>
  public interface IEmpty
    : IStatic {
    public new string? Value
      => null;

    string IStatic.Value
      => "";
  }
}
