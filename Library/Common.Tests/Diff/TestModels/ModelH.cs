// (c) Euphemism Inc. All right reserved.

namespace Coconut.Library.Common.Tests.Diff.TestModels
{
    interface IModelH
    {
        IProperty Property { get; set; }
    }

    internal class ModelH : IModelH
    {
        public IProperty Property { get; set; }
    }

    interface IProperty
    {
        EmptyModel SameProperty { get; set; }
    }

    internal class PropertyA : IProperty
    {
        public EmptyModel SameProperty { get; set; }
        public string String { get; set; }

        public int Integer { get; set; }
    }

    internal class PropertyB : IProperty
    {
        public EmptyModel SameProperty { get; set; }
        public EmptyModel Class { get; set; }
    }
}