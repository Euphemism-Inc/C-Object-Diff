// (c) Euphemism Inc. All right reserved.

using System.Collections.Generic;

namespace Coconut.Library.Common.Diff.Models
{
    /// <summary>
    /// A compare/diff of two complex objects.
    /// </summary>
    public class ComplexDiff : ObjectDiff
    {
        /// <summary>
        /// Gets the primitive properties.
        /// </summary>
        public List<PrimitiveDiff> PrimitiveProperties { get; } = new List<PrimitiveDiff>();

        /// <summary>
        /// Gets the complex properties.
        /// </summary>
        public List<ComplexDiff> ComplexProperties { get; } = new List<ComplexDiff>();

        /// <summary>
        /// Gets the properties of lists with primitive in it.
        /// </summary>
        public List<ListDiff<PrimitiveDiff>> PrimitiveListProperties { get; } = new List<ListDiff<PrimitiveDiff>>();

        /// <summary>
        /// Gets the properties of lists with primitive in it.
        /// </summary>
        public List<ListDiff<ComplexDiff>> ComplexListProperties { get; } = new List<ListDiff<ComplexDiff>>();
    }
}