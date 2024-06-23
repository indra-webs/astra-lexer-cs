namespace Indra.Astra.Tokens {

  /// <summary>
  /// A token who's lexeme is not always the same. (Opposite of <see cref="IStatic"/>)
  /// </summary>
  /// <remarks>
  ///   <term><b>See Also</b></term><related><list type="bullet">
  ///     <item>
  ///       <term><seealso cref="IStatic"/></term>
  ///       <description>Opposite of <see cref="IDynamic"/></description>
  ///     </item>
  ///     <item>
  ///       <term><seealso cref="ILimited"/></term>
  ///       <description>For dynamic tokens with a limited number of possible lexeme values.</description>
  ///     </item>
  ///     <item>
  ///       <term><seealso cref="IVariable"/></term> 
  ///       <description>For dynamic tokens with lexemes limited by a specific pattern or set of rules.</description>
  ///    </item>
  ///   </list></related>
  /// </remarks>
  public interface IDynamic
    : IToken;
}
