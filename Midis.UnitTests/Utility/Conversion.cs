// Copyright (c) 2011 Tall Ambitions, LLC
// See included LICENSE for details.
namespace Midis.UnitTests.Utility
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class Conversion
    {
        public static IEnumerable<T> ConvertTo<T>(this string values, Func<string, T> conversion)
        {
            return values.Split(new []{',', ' '}, StringSplitOptions.RemoveEmptyEntries).Select(conversion);
        }
    }
}