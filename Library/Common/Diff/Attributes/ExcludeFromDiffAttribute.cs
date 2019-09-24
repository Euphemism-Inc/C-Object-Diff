// (c) Euphemism Inc. All right reserved.

using System;

namespace Coconut.Library.Common.Diff.Attributes
{
    /// <summary>
    /// Attribute to use to exclude an property from a object comparison/diff.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public sealed class ExcludeFromDiffAttribute : Attribute
    {
    }
}
