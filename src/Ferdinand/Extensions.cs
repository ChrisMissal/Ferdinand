namespace Ferdinand
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerStepThrough]
    internal static class Extensions
    {
        public static IEnumerable<T> Do<T>(this IEnumerable<T> self, Action<T> action)
        {
            foreach (var item in self)
            {
                action(item);
                yield return item;
            }
        }
    }
}
