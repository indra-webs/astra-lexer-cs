namespace Indra.Astra {
    public static class LexerCharExtensions {
        public static bool IsWhiteSpaceOrNull(this char c)
            => c is '\0' || char.IsWhiteSpace(c);
    }
}
