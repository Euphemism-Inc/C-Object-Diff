// (c) Euphemism Inc. All right reserved.

namespace Coconut.Library.Common.Diff.Models
{
    /// <summary>
    /// The type of change made to an object.
    /// </summary>
    public enum ChangeTypeEnum
    {
        /// <summary>
        /// Unknown Change type - not initialized
        /// </summary>
        Unknown = 0,

        /// <summary>
        /// No Change
        /// </summary>
        None,

        /// <summary>
        /// Created
        /// </summary>
        Create,

        /// <summary>
        /// Updated
        /// </summary>
        Update,

        /// <summary>
        /// Deleted
        /// </summary>
        Delete
    }
}
