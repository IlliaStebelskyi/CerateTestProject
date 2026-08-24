public interface Are
{
    bool AreEqual<T>(T a, T b) where T : IEquatable<T>;
}


public class AreeEqual : Are
{
    public bool AreEqual<T>(T a, T b) where T : IEquatable<T>
    {
        return a.Equals(b);

    }
}