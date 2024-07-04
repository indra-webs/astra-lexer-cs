namespace Indra.Astra.Tokens {

    /// <summary>
    /// Represents padding around a token; providing information 
    ///   about the characters immediately before and after it.
    /// </summary>
    public class Padding(Token token) {

        /// <summary>
        /// The padding before the token.
        /// </summary>
        public Pad.Before Before { get; }
            = new(token);

        /// <summary>
        /// The padding after the token.
        /// </summary>
        public Pad.After After { get; }
            = new(token);

        /// <summary>
        /// Whether there is any padding around this token on either side.
        /// </summary>
        public bool HasAny
            => Before.IsAny || After.IsAny;

        /// <summary>
        /// Whether there is no padding around this token on either side.
        /// </summary>
        public bool HasNone
            => !HasAny;

        /// <summary>
        /// Whether there is padding on both sides of this token.
        /// </summary>
        public bool IsSpaced
            => Before.IsAny && After.IsAny;
    }
}