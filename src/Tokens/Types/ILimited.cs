namespace Indra.Astra.Tokens {
  /// <summary>
  /// A token type with a limited number of possible lexeme values.
  /// </summary>
  /// <remarks>
  ///   <term><b>See Also</b></term><related><list type="bullet">
  ///     <item>
  ///       <term><seealso cref="IDynamic"/></term>
  ///       <description>Base Type</description>
  ///     </item>
  ///     <item>
  ///       <term><seealso cref="IVariable"/></term>
  ///       <description><inheritdoc cref="IVariable" path="/summary"/></description>
  ///     </item>
  ///     <item>
  ///       <term><seealso cref="IStatic"/></term>
  ///       <description><inheritdoc cref="IStatic" path="/summary"/></description>
  ///     </item>
  ///   </list></related>
  /// </remarks>
  public interface ILimited
    : IDynamic {
    public IReadOnlySet<string> Values { get; }
  }
}
