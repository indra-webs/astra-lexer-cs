namespace Indra.Astra.Tokens {

  /// <summary>
  /// A token whose value is always the same.
  /// </summary>
  /// <remarks>
  ///   <term><b>See Also</b></term><related><list type="bullet">
  ///     <item>
  ///       <term><seealso cref="IDynamic"/></term>
  ///       <description>Opposite of <seealso cref="IStatic"/>.</description>
  ///     </item>
  ///     <item>
  ///       <term><seealso cref="ILimited"/></term>
  ///       <description><inheritdoc cref="ILimited" path="/summary"/></description>
  ///     </item>
  ///     <item>
  ///       <term><seealso cref="IVariable"/></term>
  ///       <description><inheritdoc cref="IVariable" path="/summary"/></description>
  ///     </item>
  ///     <item>
  ///       <term><seealso cref="IEmpty"/></term>
  ///       <description><inheritdoc cref="IEmpty" path="/summary"/></description>
  ///     </item>
  ///   </list></related>
  /// </remarks>
  public interface IStatic
    : IToken {
    public string Value { get; }
  }
}
