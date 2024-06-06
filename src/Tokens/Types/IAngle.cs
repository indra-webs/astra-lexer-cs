namespace Indra.Astra.Tokens {
  public interface IAngle
    : IDelimiter {
    public new IAngle Pair
      => (IAngle)(this as IDelimiter).Pair;
  }
}
