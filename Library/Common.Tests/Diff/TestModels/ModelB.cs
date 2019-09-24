// (c) Euphemism Inc. All right reserved.

using System.Collections.Generic;

namespace Coconut.Library.Common.Tests.Diff.TestModels
{
    internal class ModelB
    {
        public List<ModelA> ModelAs { get; set; } = new List<ModelA>();

        public List<int> Integers { get; set; } = new List<int>();
    }
}
