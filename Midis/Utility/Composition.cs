// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.Utility
{
    using System;

    public static class CompositionExtensions
    {
        public static Func<T, bool> Not<T>(this Func<T, bool> op)
        {
            return x => !op(x);
        }

        public static Func<TRet> Curry<T, TRet>(this Func<T, TRet> f, T value)
        {
            return () => f(value);
        }

        public static Func<T2, TRet> Curry<T1, T2, TRet>(this Func<T1, T2, TRet> f, T1 v1)
        {
            return v2 => f(v1, v2);
        }

        public static Func<T2, T1, TRet> Swap<T1, T2, TRet>(this Func<T1, T2, TRet> f)
        {
            return (a, b) => f(b, a);
        }
    }
}