using System.Collections;

namespace SearchengineResult.Extensions
{
    public static class Extensions
    {
        public static void AddIfNotNull<T>(this List<T> coll, T item)
        {
            if (item != null)
            {
                coll.Add(item);
            }
        }
    }
}
