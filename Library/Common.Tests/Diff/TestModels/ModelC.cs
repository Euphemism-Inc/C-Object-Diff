// (c) Euphemism Inc. All right reserved.

using System.Collections.Generic;

namespace Coconut.Library.Common.Tests.Diff.TestModels
{
    internal class ModelC
    {
        public IDictionary<int, ModelA> Dictionary { get; set; } = new Dictionary<int, ModelA>();
    }
}
