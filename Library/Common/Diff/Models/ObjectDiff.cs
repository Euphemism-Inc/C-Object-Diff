// (c) Euphemism Inc. All right reserved.

using System;

namespace Coconut.Library.Common.Diff.Models
{
    /// <summary>
    /// A compare/diff of two objects.
    /// </summary>
    public abstract class ObjectDiff
    {
        /// <summary>
        /// Gets or sets the name of the property.
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// Gets or sets the type of the original valuje.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets or sets the type of the change.
        /// </summary>
        public ChangeTypeEnum ChangeType { get; set; }
    }
}
