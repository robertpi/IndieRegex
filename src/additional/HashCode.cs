#if NETFRAMEWORK || NETSTANDARD


namespace System
{
    internal static class HashCode
    {
        public static int Combine(params object[] objects) 
        {
            int result = 0;
            for (int i = 0; i < objects.Length; i++)
            {
                result ^= (31 *  objects[i]?.GetHashCode() ?? 0);
            }
            return result;
        }
    }
}
#endif