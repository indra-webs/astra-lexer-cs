namespace Indra.Astra.Tokens {

  /// <summary>
  ///   The base interface for all Token Type singletons and Token Type interfaces. Represents any/all type(s) of Token.
  /// </summary>
  /// <remarks>
  ///   <term><b>See Also</b></term><related><list type="bullet">
  ///     <item>
  ///       <term><seealso href="Token">Token</seealso></term>
  ///       <description><inheritdoc cref="Token" path="/summary"/></description>
  ///     </item>
  ///     <item>
  ///       <term><seealso href="TokenType">TokenType</seealso></term>
  ///       <description><inheritdoc cref="TokenType" path="/summary"/></description>
  ///     </item>
  ///     <item>
  ///       <term><seealso href="IToken{TSelf}">IToken&lt;&gt;</seealso></term>
  ///       <description><inheritdoc cref="IToken{TSelf}" path="/summary"/></description>
  ///     </item>
  ///     <item>
  ///       <term><seealso href="TokenType{TSelf}">TokenType&lt;&gt;</seealso></term>
  ///       <description><inheritdoc cref="TokenType{TSelf}" path="/summary"/></description>
  ///     </item>
  ///   </list></related>
  ///   <term><b>Notes</b></term><notes><list type="number">
  ///     <item><note>Use the generic version (<seealso href="IToken{TSelf}">IToken&lt;&gt;</seealso>) to define new token types instead of this.</note></item>
  ///   </list></notes>
  /// </remarks>
  public interface IToken {

    /// <summary>
    /// The name of this token type.
    /// </summary>
    public string Name
      => GetType().Name;

    /// <summary>
    /// The instance of this type of token.
    /// </summary>
    public virtual static IToken Type
      => throw new NotImplementedException();
  }

  /// <summary>
  ///   The base generic interface for all Token Type singletons and Token Type interfaces.
  /// </summary>
  /// <remarks>
  ///   <term><b>See Also</b></term><related><list type="bullet">
  ///     <item>
  ///       <term><seealso href="Token">Token</seealso></term>
  ///       <description><inheritdoc cref="Token" path="/summary"/></description>
  ///     </item>
  ///     <item>
  ///       <term><seealso href="IToken">IToken</seealso></term>
  ///       <description><inheritdoc cref="IToken" path="/summary"/></description>
  ///     </item>
  ///     <item>
  ///       <term><seealso href="TokenType">TokenType</seealso></term>
  ///       <description><inheritdoc cref="TokenType" path="/summary"/></description>
  ///     </item>
  ///     <item>
  ///       <term><seealso href="TokenType{TSelf}">TokenType&lt;&gt;</seealso></term>
  ///       <description><inheritdoc cref="TokenType{TSelf}" path="/summary"/></description>
  ///     </item>
  ///   </list></related>
  ///   <term><b>Notes</b></term><notes><list type="number">
  ///     <item><note>Use this to define new token types instead of the non-generic version (<seealso href="IToken">IToken</seealso>).</note></item>
  ///   </list></notes>
  /// </remarks>
  public interface IToken<TSelf>
    : IToken
    where TSelf : IToken<TSelf> {

    /// <inheritdoc cref="IToken.Type"/>
    public new abstract static TSelf Type { get; }
    static IToken IToken.Type
      => TSelf.Type;
  }

  /// <summary>
  ///   The base record class for all token type singletons.
  /// </summary>
  /// <remarks>
  ///   <term><b>See Also</b></term><related><list type="bullet">
  ///     <item>
  ///       <term><seealso href="Token">Token</seealso></term>
  ///       <description><inheritdoc cref="Token" path="/summary"/></description>
  ///     </item>
  ///     <item>
  ///       <term><seealso href="IToken">IToken</seealso></term>
  ///       <description><inheritdoc cref="IToken" path="/summary"/></description>
  ///     </item>
  ///     <item>
  ///       <term><seealso href="IToken{TSelf}">IToken&lt;&gt;</seealso></term>
  ///       <description><inheritdoc cref="IToken{TSelf}" path="/summary"/></description>
  ///     </item>
  ///     <item>
  ///       <term><seealso href="TokenType{TSelf}">TokenType&lt;&gt;</seealso></term>
  ///       <description><inheritdoc cref="TokenType{TSelf}" path="/summary"/></description>
  ///     </item>
  ///   </list></related>
  ///   <term><b>Notes</b></term><notes><list type="number">
  ///     <item><note>Use the generic version (<seealso href="TokenType{TSelf}">TokenType&lt;&gt;</seealso>) to define new token types instead of this.</note></item>
  ///   </list></notes>
  /// </remarks>
  public abstract record TokenType
    : IToken {

    /// <inheritdoc cref="IToken.Name"/>
    public string Name
      => ((IToken)this).Name;

    /// <inheritdoc cref="IToken.Type"/>
    public static IToken Type
      => throw new NotImplementedException();
  }

  /// <summary>
  /// The base generic record class for all token type singletons.
  /// </summary>
  /// <remarks>
  /// <term><b>See Also</b></term>
  /// <list type="bullet">
  ///   <item>
  ///     <term><seealso href="Token">Token</seealso></term>
  ///     <description><inheritdoc cref="Token" path="/summary"/></description>
  ///   </item>
  ///   <item>
  ///     <term><seealso href="IToken">IToken</seealso></term>
  ///     <description><inheritdoc cref="IToken" path="/summary"/></description>
  ///   </item>
  ///   <item>
  ///     <term><seealso href="TokenType">TokenType</seealso></term>
  ///     <description><inheritdoc cref="TokenType" path="/summary"/></description>
  ///   </item>
  ///   <item>
  ///     <term><seealso href="IToken{TSelf}">IToken&lt;&gt;</seealso></term>
  ///     <description><inheritdoc cref="IToken{TSelf}" path="/summary"/></description>
  ///   </item>
  /// </list>
  /// <term><b>Notes</b></term>
  /// <notes><list type="number">
  ///   <item><note>Use this to define new token types instead of the non-generic version (<seealso href="TokenType">TokenType</seealso>).</note></item>
  /// </list></notes>
  /// </remarks>
  public abstract record TokenType<TSelf>
    : TokenType,
      IToken<TSelf>
    where TSelf : TokenType<TSelf> {

    /// <inheritdoc cref="IToken.Type"/>
    public new static TSelf Type
      => Types.Get<TSelf>();

    static IToken IToken.Type
      => Type;
  }
}
