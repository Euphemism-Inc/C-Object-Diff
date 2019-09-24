// (c) Euphemism Inc. All right reserved.

using Coconut.Library.Common.Diff.Attributes;

namespace Coconut.Library.Common.Tests.Diff.TestModels
{
    internal class ModelE
    {
        [ExcludeFromDiff]
        public int Integer { get; set; }
    }
}
