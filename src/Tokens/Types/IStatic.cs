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

    string IToken.DefaultValue
      => Value;
  }

  /// <summary>
  /// A IStatic token that has a single character value.
  /// </summary>
  /// <remarks>
  ///  <term><b>See Also</b></term><related><list type="bullet">
  ///   <item>
  ///    <term><seealso cref="IStatic"/></term>
  ///   <description>Base interface.</description>
  /// </item>
  /// </list></related>
  /// </remarks>
  public interface ISingle
    : IStatic {
    public new char Value { get; }

    string IStatic.Value
      => Value.ToString();
  }
}
