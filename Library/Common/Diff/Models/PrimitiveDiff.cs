// (c) Euphemism Inc. All right reserved.

namespace Coconut.Library.Common.Diff.Models
{
    /// <summary>
    /// A compare/diff of two primitive objects.
    /// </summary>
    public class PrimitiveDiff : ObjectDiff
    {
        /// <summary>
        /// Gets or sets the original value.
        /// </summary>
        public string OriginalValue { get; set; }

        /// <summary>
        /// Gets or sets the target value.
        /// </summary>
        public string TargetValue { get; set; }
    }
}
