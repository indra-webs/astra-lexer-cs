namespace Indra.Astra.Tokens {
  /// <summary>
  /// A token type whose lexeme can be any string limited by a specific pattern or set of rules.
  /// </summary>
  /// <remarks>
  ///   <term><b>See Also</b></term><related><list type="bullet">
  ///    <item>
  ///      <term><seealso cref="IDynamic"/></term>
  ///       <description>Base Type</description>
  ///     </item>
  ///     <item>
  ///       <term><seealso cref="IStatic"/></term>
  ///       <description><inheritdoc cref="IStatic" path="/summary"/></description>
  ///     </item>
  ///     <item>
  ///       <term><seealso cref="ILimited"/></term>
  ///       <description><inheritdoc cref="ILimited" path="/summary"/></description>
  ///     </item>
  ///   </list></related>
  /// </remarks>
  public interface IVariable
    : IDynamic;
}
