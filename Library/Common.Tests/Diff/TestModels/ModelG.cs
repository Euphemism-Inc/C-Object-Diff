// (c) Euphemism Inc. All right reserved.

using System.Collections;
using System.Collections.Generic;

namespace Coconut.Library.Common.Tests.Diff.TestModels
{
    internal class ModelG
    {
        public ArrayList Collection { get; set; } = new ArrayList();

        public ArrayModel ArrayModel { get; set; } = new ArrayModel();
    }

    internal class ArrayModel : List<EmptyModel>
    {
        public string ArrayTitle { get; set; }
    }
}