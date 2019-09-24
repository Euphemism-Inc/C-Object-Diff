// (c) Euphemism Inc. All right reserved.

using System.Collections.Generic;

namespace Coconut.Library.Common.Diff.Models
{
    /// <summary>
    /// A compare/diff of two complex objects with a list of objects in it.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ListDiff<T> : ComplexDiff
         where T : ObjectDiff
    {
        /// <summary>
        /// A list of objects that are compared/diffed.
        /// </summary>
        public List<T> List { get; } = new List<T>();
    }
}
