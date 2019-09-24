// (c) Euphemism Inc. All right reserved.

namespace Coconut.Library.Common.Tests.Diff.TestModels
{
    internal class ModelA
    {
        public int Integer { get; set; }

        public ModelAEnum Enum { get; set; }

        public string String { get; set; }

        public EmptyModel EmptyModel { get; set; }
    }

    internal enum ModelAEnum
    {
        Unknown = 0,
        One = 1
    }
}
