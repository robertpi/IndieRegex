#if !NET5_0_OR_GREATER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace IndieSystem.Text.RegularExpressions
{
    internal static class InterlockedExtensions
    {
        public static uint Or(ref uint location1, uint value)
        {
            uint current = location1;
            while (true)
            {
                uint newValue = current | value;
                uint oldValue = (uint)Interlocked.CompareExchange(ref Unsafe.As<uint, int>(ref location1), (int)newValue, (int)current);
                if (oldValue == current)
                {
                    return oldValue;
                }
                current = oldValue;
            }
        }
    }
}
#endif