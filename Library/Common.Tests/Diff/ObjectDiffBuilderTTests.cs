// (c) Euphemism Inc. All right reserved.

using Coconut.Library.Common.Diff;
using Coconut.Library.Common.Diff.Models;
using Coconut.Library.Common.Tests.Diff.TestModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;

namespace Coconut.Library.Common.Tests.Diff
{
    [TestClass]
    public class ObjectDiffBuilderTTests
    {
        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Same_Succeeds()
        {
            var originalModelA = new ModelA() { Integer = 123, Enum = ModelAEnum.One, EmptyModel = new EmptyModel(), String = "TestString" };
            var targetModelA = new ModelA() { Integer = 123, Enum = ModelAEnum.One, EmptyModel = new EmptyModel(), String = "TestString" };

            var builder = new ObjectDiffBuilder<ModelA>(originalModelA, targetModelA);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.None, diff.ChangeType);
            Assert.AreEqual(typeof(ModelA), diff.Type);
            Assert.AreEqual(3, diff.PrimitiveProperties.Count);
            Assert.AreEqual(1, diff.ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.Integer), diff.PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.PrimitiveProperties[0].Type);
            Assert.AreEqual($"{originalModelA.Integer}", diff.PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual($"{targetModelA.Integer}", diff.PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveProperties[1].ChangeType);
            Assert.AreEqual(nameof(ModelA.Enum), diff.PrimitiveProperties[1].PropertyName);
            Assert.AreEqual(typeof(ModelAEnum), diff.PrimitiveProperties[1].Type);
            Assert.AreEqual($"{originalModelA.Enum}", diff.PrimitiveProperties[1].OriginalValue);
            Assert.AreEqual($"{targetModelA.Enum}", diff.PrimitiveProperties[1].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveProperties[2].ChangeType);
            Assert.AreEqual(nameof(ModelA.String), diff.PrimitiveProperties[2].PropertyName);
            Assert.AreEqual(typeof(string), diff.PrimitiveProperties[2].Type);
            Assert.AreEqual(originalModelA.String, diff.PrimitiveProperties[2].OriginalValue);
            Assert.AreEqual(targetModelA.String, diff.PrimitiveProperties[2].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.EmptyModel), diff.ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexProperties[0].Type);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Different_Int_Succeeds()
        {
            var originalModelA = new ModelA() { Integer = 123 };
            var targetModelA = new ModelA() { Integer = 124 };

            var builder = new ObjectDiffBuilder<ModelA>(originalModelA, targetModelA);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.Update, diff.ChangeType);
            Assert.AreEqual(typeof(ModelA), diff.Type);
            Assert.AreEqual(3, diff.PrimitiveProperties.Count);
            Assert.AreEqual(1, diff.ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Update, diff.PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.Integer), diff.PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.PrimitiveProperties[0].Type);
            Assert.AreEqual($"{originalModelA.Integer}", diff.PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual($"{targetModelA.Integer}", diff.PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveProperties[1].ChangeType);
            Assert.AreEqual(nameof(ModelA.Enum), diff.PrimitiveProperties[1].PropertyName);
            Assert.AreEqual(typeof(ModelAEnum), diff.PrimitiveProperties[1].Type);
            Assert.AreEqual($"{originalModelA.Enum}", diff.PrimitiveProperties[1].OriginalValue);
            Assert.AreEqual($"{targetModelA.Enum}", diff.PrimitiveProperties[1].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveProperties[2].ChangeType);
            Assert.AreEqual(nameof(ModelA.String), diff.PrimitiveProperties[2].PropertyName);
            Assert.AreEqual(typeof(string), diff.PrimitiveProperties[2].Type);
            Assert.AreEqual(originalModelA.String, diff.PrimitiveProperties[2].OriginalValue);
            Assert.AreEqual(targetModelA.String, diff.PrimitiveProperties[2].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.EmptyModel), diff.ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexProperties[0].Type);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Different_Enum_Succeeds()
        {
            var originalModelA = new ModelA() { Enum = ModelAEnum.Unknown };
            var targetModelA = new ModelA() { Enum = ModelAEnum.One };

            var builder = new ObjectDiffBuilder<ModelA>(originalModelA, targetModelA);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.Update, diff.ChangeType);
            Assert.AreEqual(typeof(ModelA), diff.Type);
            Assert.AreEqual(3, diff.PrimitiveProperties.Count);
            Assert.AreEqual(1, diff.ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.Integer), diff.PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.PrimitiveProperties[0].Type);
            Assert.AreEqual($"{originalModelA.Integer}", diff.PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual($"{targetModelA.Integer}", diff.PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.Update, diff.PrimitiveProperties[1].ChangeType);
            Assert.AreEqual(nameof(ModelA.Enum), diff.PrimitiveProperties[1].PropertyName);
            Assert.AreEqual(typeof(ModelAEnum), diff.PrimitiveProperties[1].Type);
            Assert.AreEqual($"{originalModelA.Enum}", diff.PrimitiveProperties[1].OriginalValue);
            Assert.AreEqual($"{targetModelA.Enum}", diff.PrimitiveProperties[1].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveProperties[2].ChangeType);
            Assert.AreEqual(nameof(ModelA.String), diff.PrimitiveProperties[2].PropertyName);
            Assert.AreEqual(typeof(string), diff.PrimitiveProperties[2].Type);
            Assert.AreEqual(originalModelA.String, diff.PrimitiveProperties[2].OriginalValue);
            Assert.AreEqual(targetModelA.String, diff.PrimitiveProperties[2].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.EmptyModel), diff.ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexProperties[0].Type);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Different_String_Succeeds()
        {
            var originalModelA = new ModelA() { String = null };
            var targetModelA = new ModelA() { String = "String" };

            var builder = new ObjectDiffBuilder<ModelA>(originalModelA, targetModelA);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.Update, diff.ChangeType);
            Assert.AreEqual(typeof(ModelA), diff.Type);
            Assert.AreEqual(3, diff.PrimitiveProperties.Count);
            Assert.AreEqual(1, diff.ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.Integer), diff.PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.PrimitiveProperties[0].Type);
            Assert.AreEqual($"{originalModelA.Integer}", diff.PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual($"{targetModelA.Integer}", diff.PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveProperties[1].ChangeType);
            Assert.AreEqual(nameof(ModelA.Enum), diff.PrimitiveProperties[1].PropertyName);
            Assert.AreEqual(typeof(ModelAEnum), diff.PrimitiveProperties[1].Type);
            Assert.AreEqual($"{originalModelA.Enum}", diff.PrimitiveProperties[1].OriginalValue);
            Assert.AreEqual($"{targetModelA.Enum}", diff.PrimitiveProperties[1].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.Create, diff.PrimitiveProperties[2].ChangeType);
            Assert.AreEqual(nameof(ModelA.String), diff.PrimitiveProperties[2].PropertyName);
            Assert.AreEqual(typeof(string), diff.PrimitiveProperties[2].Type);
            Assert.AreEqual(originalModelA.String, diff.PrimitiveProperties[2].OriginalValue);
            Assert.AreEqual(targetModelA.String, diff.PrimitiveProperties[2].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.EmptyModel), diff.ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexProperties[0].Type);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Different_Class_Succeeds()
        {
            var originalModelA = new ModelA() { EmptyModel = new EmptyModel() };
            var targetModelA = new ModelA() { EmptyModel = null };

            var builder = new ObjectDiffBuilder<ModelA>(originalModelA, targetModelA);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.Update, diff.ChangeType);
            Assert.AreEqual(typeof(ModelA), diff.Type);
            Assert.AreEqual(3, diff.PrimitiveProperties.Count);
            Assert.AreEqual(1, diff.ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.Integer), diff.PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.PrimitiveProperties[0].Type);
            Assert.AreEqual($"{originalModelA.Integer}", diff.PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual($"{targetModelA.Integer}", diff.PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveProperties[1].ChangeType);
            Assert.AreEqual(nameof(ModelA.Enum), diff.PrimitiveProperties[1].PropertyName);
            Assert.AreEqual(typeof(ModelAEnum), diff.PrimitiveProperties[1].Type);
            Assert.AreEqual($"{originalModelA.Enum}", diff.PrimitiveProperties[1].OriginalValue);
            Assert.AreEqual($"{targetModelA.Enum}", diff.PrimitiveProperties[1].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveProperties[2].ChangeType);
            Assert.AreEqual(nameof(ModelA.String), diff.PrimitiveProperties[2].PropertyName);
            Assert.AreEqual(typeof(string), diff.PrimitiveProperties[2].Type);
            Assert.AreEqual(originalModelA.String, diff.PrimitiveProperties[2].OriginalValue);
            Assert.AreEqual(targetModelA.String, diff.PrimitiveProperties[2].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(originalModelA.EmptyModel), diff.ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexProperties[0].Type);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Different_Class_Null_Succeeds()
        {
            var originalModelD = new ModelD() { ModelA = new ModelA() { EmptyModel = new EmptyModel() } };
            var targetModelD = new ModelD() { ModelA = null };

            var builder = new ObjectDiffBuilder<ModelD>(originalModelD, targetModelD);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.Update, diff.ChangeType);
            Assert.AreEqual(typeof(ModelD), diff.Type);
            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(1, diff.ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties.Count);

            Assert.AreEqual(nameof(originalModelD.ModelA), diff.ComplexProperties[0].PropertyName);
            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexProperties[0].ChangeType);
            Assert.AreEqual(typeof(ModelA), diff.ComplexProperties[0].Type);
            Assert.AreEqual(3, diff.ComplexProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexProperties[0].PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.Integer), diff.ComplexProperties[0].PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.ComplexProperties[0].PrimitiveProperties[0].Type);
            Assert.AreEqual($"{originalModelD.ModelA.Integer}", diff.ComplexProperties[0].PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual(null, diff.ComplexProperties[0].PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexProperties[0].PrimitiveProperties[1].ChangeType);
            Assert.AreEqual(nameof(ModelA.Enum), diff.ComplexProperties[0].PrimitiveProperties[1].PropertyName);
            Assert.AreEqual(typeof(ModelAEnum), diff.ComplexProperties[0].PrimitiveProperties[1].Type);
            Assert.AreEqual($"{originalModelD.ModelA.Enum}", diff.ComplexProperties[0].PrimitiveProperties[1].OriginalValue);
            Assert.AreEqual(null, diff.ComplexProperties[0].PrimitiveProperties[1].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexProperties[0].PrimitiveProperties[2].ChangeType);
            Assert.AreEqual(nameof(ModelA.String), diff.ComplexProperties[0].PrimitiveProperties[2].PropertyName);
            Assert.AreEqual(typeof(string), diff.ComplexProperties[0].PrimitiveProperties[2].Type);
            Assert.AreEqual(originalModelD.ModelA.String, diff.ComplexProperties[0].PrimitiveProperties[2].OriginalValue);
            Assert.AreEqual(null, diff.ComplexProperties[0].PrimitiveProperties[2].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexProperties[0].ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.EmptyModel), diff.ComplexProperties[0].ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexProperties[0].ComplexProperties[0].Type);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Different_Class_Null_Primitive_Succeeds()
        {
            var originalModelD = new ModelD() { ModelA = new ModelA() { String = "Test String" } };
            var targetModelD = new ModelD() { ModelA = null };

            var builder = new ObjectDiffBuilder<ModelD>(originalModelD, targetModelD);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.Update, diff.ChangeType);
            Assert.AreEqual(typeof(ModelD), diff.Type);

            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(1, diff.ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties.Count);

            Assert.AreEqual(nameof(originalModelD.ModelA), diff.ComplexProperties[0].PropertyName);
            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexProperties[0].ChangeType);
            Assert.AreEqual(typeof(ModelA), diff.ComplexProperties[0].Type);
            Assert.AreEqual(3, diff.ComplexProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexProperties[0].PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(originalModelD.ModelA.Integer), diff.ComplexProperties[0].PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.ComplexProperties[0].PrimitiveProperties[0].Type);
            Assert.AreEqual($"{originalModelD.ModelA.Integer}", diff.ComplexProperties[0].PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual(null, diff.ComplexProperties[0].PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexProperties[0].PrimitiveProperties[1].ChangeType);
            Assert.AreEqual(nameof(originalModelD.ModelA.Enum), diff.ComplexProperties[0].PrimitiveProperties[1].PropertyName);
            Assert.AreEqual(typeof(ModelAEnum), diff.ComplexProperties[0].PrimitiveProperties[1].Type);
            Assert.AreEqual($"{originalModelD.ModelA.Enum}", diff.ComplexProperties[0].PrimitiveProperties[1].OriginalValue);
            Assert.AreEqual(null, diff.ComplexProperties[0].PrimitiveProperties[1].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexProperties[0].PrimitiveProperties[2].ChangeType);
            Assert.AreEqual(nameof(originalModelD.ModelA.String), diff.ComplexProperties[0].PrimitiveProperties[2].PropertyName);
            Assert.AreEqual(typeof(string), diff.ComplexProperties[0].PrimitiveProperties[2].Type);
            Assert.AreEqual($"{originalModelD.ModelA.String}", diff.ComplexProperties[0].PrimitiveProperties[2].OriginalValue);
            Assert.AreEqual(null, diff.ComplexProperties[0].PrimitiveProperties[2].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexProperties[0].ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.EmptyModel), diff.ComplexProperties[0].ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexProperties[0].ComplexProperties[0].Type);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Same_Model_Empty_Succeeds()
        {
            var originalModelB = new ModelB();
            var targetModelB = new ModelB();

            var builder = new ObjectDiffBuilder<ModelB>(originalModelB, targetModelB);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.None, diff.ChangeType);
            Assert.AreEqual(typeof(ModelB), diff.Type);

            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties.Count);
            Assert.AreEqual(1, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(originalModelB.ModelAs), diff.ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelA>), diff.ComplexListProperties[0].Type);

            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveListProperties[0].ChangeType);
            Assert.AreEqual(nameof(originalModelB.Integers), diff.PrimitiveListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<int>), diff.PrimitiveListProperties[0].Type);

            Assert.AreEqual(0, diff.PrimitiveListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].List.Count);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Same_ModelWithList_Filled_Succeeds()
        {
            var originalModelB = new ModelB();
            originalModelB.ModelAs.Add(new ModelA() {
                Integer = 1
            });
            originalModelB.Integers.Add(123);
            var targetModelB = new ModelB();
            targetModelB.ModelAs.Add(new ModelA() {
                Integer = 1
            });
            targetModelB.Integers.Add(123);

            var builder = new ObjectDiffBuilder<ModelB>(originalModelB, targetModelB);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.None, diff.ChangeType);
            Assert.AreEqual(typeof(ModelB), diff.Type);

            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties.Count);
            Assert.AreEqual(1, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelB.ModelAs), diff.ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelA>), diff.ComplexListProperties[0].Type);

            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(ModelA), diff.ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(3, diff.ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.Integer), diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].Type);
            Assert.AreEqual("1", diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual("1", diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].ChangeType);
            Assert.AreEqual(nameof(ModelA.Enum), diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].PropertyName);
            Assert.AreEqual(typeof(ModelAEnum), diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].Type);
            Assert.AreEqual("Unknown", diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].OriginalValue);
            Assert.AreEqual("Unknown", diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].ChangeType);
            Assert.AreEqual(nameof(ModelA.String), diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].PropertyName);
            Assert.AreEqual(typeof(string), diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].Type);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].OriginalValue);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.EmptyModel), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexListProperties[0].List[0].ComplexProperties[0].Type);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelB.Integers), diff.PrimitiveListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<int>), diff.PrimitiveListProperties[0].Type);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.PrimitiveListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.PrimitiveListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.PrimitiveListProperties[0].List[0].Type);
            Assert.AreEqual("123", diff.PrimitiveListProperties[0].List[0].OriginalValue);
            Assert.AreEqual("123", diff.PrimitiveListProperties[0].List[0].TargetValue);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Different_ModelWithList_Filled_Succeeds()
        {
            var originalModelB = new ModelB();
            originalModelB.ModelAs.Add(new ModelA() {
                Integer = 1
            });
            var targetModelB = new ModelB();
            targetModelB.ModelAs.Add(new ModelA() {
                Integer = 2
            });

            var builder = new ObjectDiffBuilder<ModelB>(originalModelB, targetModelB);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.Update, diff.ChangeType);
            Assert.AreEqual(typeof(ModelB), diff.Type);

            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties.Count);
            Assert.AreEqual(1, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Update, diff.ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelB.ModelAs), diff.ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelA>), diff.ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.Update, diff.ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(ModelA), diff.ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(3, diff.ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Update, diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.Integer), diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].Type);
            Assert.AreEqual("1", diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual("2", diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].ChangeType);
            Assert.AreEqual(nameof(ModelA.Enum), diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].PropertyName);
            Assert.AreEqual(typeof(ModelAEnum), diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].Type);
            Assert.AreEqual("Unknown", diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].OriginalValue);
            Assert.AreEqual("Unknown", diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].ChangeType);
            Assert.AreEqual(nameof(ModelA.String), diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].PropertyName);
            Assert.AreEqual(typeof(string), diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].Type);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].OriginalValue);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.EmptyModel), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexListProperties[0].List[0].ComplexProperties[0].Type);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelB.Integers), diff.PrimitiveListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<int>), diff.PrimitiveListProperties[0].Type);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].List.Count);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Same_ModelWithDictionary_Filled_Succeeds()
        {
            var originalModelC = new ModelC();
            originalModelC.Dictionary.Add(1, new ModelA() {
                Integer = 1
            });
            var targetModelC = new ModelC();
            targetModelC.Dictionary.Add(1, new ModelA() {
                Integer = 1
            });

            var builder = new ObjectDiffBuilder<ModelC>(originalModelC, targetModelC);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.None, diff.ChangeType);
            Assert.AreEqual(typeof(ModelC), diff.Type);
            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelC.Dictionary), diff.ComplexListProperties[0].PropertyName);
            Assert.AreEqual(nameof(ModelC.Dictionary), diff.ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(IDictionary<int, ModelA>), diff.ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(KeyValuePair<int, ModelA>), diff.ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(KeyValuePair<int, ModelA>.Key), diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].Type);
            Assert.AreEqual("1", diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual("1", diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(KeyValuePair<int, ModelA>.Value), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(ModelA), diff.ComplexListProperties[0].List[0].ComplexProperties[0].Type);
            Assert.AreEqual(3, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.Integer), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[0].Type);
            Assert.AreEqual("1", diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual("1", diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[1].ChangeType);
            Assert.AreEqual(nameof(ModelA.Enum), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[1].PropertyName);
            Assert.AreEqual(typeof(ModelAEnum), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[1].Type);
            Assert.AreEqual("Unknown", diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[1].OriginalValue);
            Assert.AreEqual("Unknown", diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[1].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[2].ChangeType);
            Assert.AreEqual(nameof(ModelA.String), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[2].PropertyName);
            Assert.AreEqual(typeof(string), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[2].Type);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[2].OriginalValue);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[2].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.EmptyModel), diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexProperties[0].Type);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Different_ModelWithDictionary_Filled_Succeeds()
        {
            var originalModelC = new ModelC();
            originalModelC.Dictionary.Add(1, new ModelA() {
                Integer = 1
            });
            var targetModelC = new ModelC();
            targetModelC.Dictionary.Add(1, new ModelA() {
                Integer = 2
            });

            var builder = new ObjectDiffBuilder<ModelC>(originalModelC, targetModelC);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.Update, diff.ChangeType);
            Assert.AreEqual(typeof(ModelC), diff.Type);
            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Update, diff.ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelC.Dictionary), diff.ComplexListProperties[0].PropertyName);
            Assert.AreEqual(nameof(ModelC.Dictionary), diff.ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(IDictionary<int, ModelA>), diff.ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.Update, diff.ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(KeyValuePair<int, ModelA>), diff.ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(KeyValuePair<int, ModelA>.Key), diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].Type);
            Assert.AreEqual("1", diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual("1", diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.Update, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(KeyValuePair<int, ModelA>.Value), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(ModelA), diff.ComplexListProperties[0].List[0].ComplexProperties[0].Type);
            Assert.AreEqual(3, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Update, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.Integer), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[0].Type);
            Assert.AreEqual("1", diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual("2", diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[1].ChangeType);
            Assert.AreEqual(nameof(ModelA.Enum), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[1].PropertyName);
            Assert.AreEqual(typeof(ModelAEnum), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[1].Type);
            Assert.AreEqual("Unknown", diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[1].OriginalValue);
            Assert.AreEqual("Unknown", diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[1].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[2].ChangeType);
            Assert.AreEqual(nameof(ModelA.String), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[2].PropertyName);
            Assert.AreEqual(typeof(string), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[2].Type);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[2].OriginalValue);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[2].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.EmptyModel), diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexProperties[0].Type);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Different_ModelWithDictionary_Null_Succeeds()
        {
            var originalModelC = new ModelC();
            originalModelC.Dictionary.Add(1, new ModelA() {
                Integer = 1
            });
            var targetModelC = new ModelC();

            var builder = new ObjectDiffBuilder<ModelC>(originalModelC, targetModelC);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.Update, diff.ChangeType);
            Assert.AreEqual(typeof(ModelC), diff.Type);
            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Update, diff.ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelC.Dictionary), diff.ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(IDictionary<int, ModelA>), diff.ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(KeyValuePair<int, ModelA>), diff.ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(KeyValuePair<int, ModelA>.Key), diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].Type);
            Assert.AreEqual("1", diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(KeyValuePair<int, ModelA>.Value), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(ModelA), diff.ComplexListProperties[0].List[0].ComplexProperties[0].Type);
            Assert.AreEqual(3, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.Integer), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[0].Type);
            Assert.AreEqual("1", diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[1].ChangeType);
            Assert.AreEqual(nameof(ModelA.Enum), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[1].PropertyName);
            Assert.AreEqual(typeof(ModelAEnum), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[1].Type);
            Assert.AreEqual("Unknown", diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[1].OriginalValue);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[1].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[2].ChangeType);
            Assert.AreEqual(nameof(ModelA.String), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[2].PropertyName);
            Assert.AreEqual(typeof(string), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[2].Type);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[2].OriginalValue);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties[2].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.EmptyModel), diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexProperties[0].Type);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Same_ListInList_Succeeds()
        {
            var originalModelF = new ModelF() {
                ModelFs = new List<ModelF>() {
                    new ModelF() {
                        ModelFs = new List<ModelF>() {
                            new ModelF() {
                                ModelFs = new List<ModelF>() {
                                    new ModelF()
                                }
                            }
                        }
                    }
                }
            };
            var targetModelF = new ModelF() {
                ModelFs = new List<ModelF>() {
                    new ModelF() {
                        ModelFs = new List<ModelF>() {
                            new ModelF() {
                                ModelFs = new List<ModelF>() {
                                    new ModelF()
                                }
                            }
                        }
                    }
                }
            };

            var builder = new ObjectDiffBuilder<ModelF>(originalModelF, targetModelF);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.None, diff.ChangeType);
            Assert.AreEqual(typeof(ModelF), diff.Type);
            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelF.ModelFs), diff.ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelF>), diff.ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(ModelF), diff.ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelF.ModelFs), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelF>), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(ModelF), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelF.ModelFs), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelF>), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(ModelF), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelF.ModelFs), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelF>), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List.Count);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Different_ListInList_Create_Succeeds()
        {
            ModelF originalModelF = null;
            var targetModelF = new ModelF() {
                ModelFs = new List<ModelF>() {
                    new ModelF() {
                        ModelFs = new List<ModelF>() {
                            new ModelF() {
                                ModelFs = new List<ModelF>() {
                                    new ModelF()
                                }
                            }
                        }
                    }
                }
            };

            var builder = new ObjectDiffBuilder<ModelF>(originalModelF, targetModelF);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.Create, diff.ChangeType);
            Assert.AreEqual(typeof(ModelF), diff.Type);
            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Create, diff.ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelF.ModelFs), diff.ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelF>), diff.ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.Create, diff.ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(ModelF), diff.ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Create, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelF.ModelFs), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelF>), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.Create, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(ModelF), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Create, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelF.ModelFs), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelF>), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.Create, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(ModelF), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Create, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelF.ModelFs), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelF>), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List.Count);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Different_ListInList_Delete_Succeeds()
        {
            var originalModelF = new ModelF() {
                ModelFs = new List<ModelF>() {
                    new ModelF() {
                        ModelFs = new List<ModelF>() {
                            new ModelF() {
                                ModelFs = new List<ModelF>() {
                                    new ModelF()
                                }
                            }
                        }
                    }
                }
            };
            ModelF targetModelF = null;

            var builder = new ObjectDiffBuilder<ModelF>(originalModelF, targetModelF);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ChangeType);
            Assert.AreEqual(typeof(ModelF), diff.Type);
            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelF.ModelFs), diff.ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelF>), diff.ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(ModelF), diff.ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelF.ModelFs), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelF>), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(ModelF), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelF.ModelFs), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelF>), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(ModelF), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelF.ModelFs), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelF>), diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List[0].ComplexListProperties[0].List.Count);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        [ExpectedException(typeof(System.ArgumentNullException))]
        public void Build_ObjectDiff_Null_Succeeds()
        {
            var builder = new ObjectDiffBuilder<ModelD>(null, null);
            ComplexDiff diff = builder.CreateDiff();

            // I expect an exception in the building process. If for some reason we change this later, the code below should suffice.

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.None, diff.ChangeType);
            Assert.AreEqual(typeof(ModelD), diff.Type);
            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(1, diff.ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelD.ModelA), diff.ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(ModelA), diff.ComplexProperties[0].Type);
            Assert.AreEqual(3, diff.ComplexProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(1, diff.ComplexProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexProperties[0].PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.Integer), diff.ComplexProperties[0].PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.ComplexProperties[0].PrimitiveProperties[0].Type);
            Assert.AreEqual(null, diff.ComplexProperties[0].PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual(null, diff.ComplexProperties[0].PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexProperties[0].PrimitiveProperties[1].ChangeType);
            Assert.AreEqual(nameof(ModelA.Enum), diff.ComplexProperties[0].PrimitiveProperties[1].PropertyName);
            Assert.AreEqual(typeof(ModelAEnum), diff.ComplexProperties[0].PrimitiveProperties[1].Type);
            Assert.AreEqual(null, diff.ComplexProperties[0].PrimitiveProperties[1].OriginalValue);
            Assert.AreEqual(null, diff.ComplexProperties[0].PrimitiveProperties[1].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexProperties[0].PrimitiveProperties[2].ChangeType);
            Assert.AreEqual(nameof(ModelA.String), diff.ComplexProperties[0].PrimitiveProperties[2].PropertyName);
            Assert.AreEqual(typeof(string), diff.ComplexProperties[0].PrimitiveProperties[2].Type);
            Assert.AreEqual(null, diff.ComplexProperties[0].PrimitiveProperties[2].OriginalValue);
            Assert.AreEqual(null, diff.ComplexProperties[0].PrimitiveProperties[2].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexProperties[0].ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.EmptyModel), diff.ComplexProperties[0].ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexProperties[0].ComplexProperties[0].Type);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Create_Succeeds()
        {
            ModelB originalModelB = null;
            var targetModelB = new ModelB() {
                ModelAs = new List<ModelA>()
                {
                    new ModelA()
                    {
                        EmptyModel = new EmptyModel()
                    }
                }
            };

            var builder = new ObjectDiffBuilder<ModelB>(originalModelB, targetModelB);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.Create, diff.ChangeType);
            Assert.AreEqual(typeof(ModelB), diff.Type);

            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties.Count);
            Assert.AreEqual(1, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Create, diff.ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelB.ModelAs), diff.ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelA>), diff.ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.Create, diff.ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(ModelA), diff.ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(3, diff.ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Create, diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.Integer), diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].Type);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.Create, diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].ChangeType);
            Assert.AreEqual(nameof(ModelA.Enum), diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].PropertyName);
            Assert.AreEqual(typeof(ModelAEnum), diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].Type);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].OriginalValue);
            Assert.AreEqual("Unknown", diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].ChangeType);
            Assert.AreEqual(nameof(ModelA.String), diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].PropertyName);
            Assert.AreEqual(typeof(string), diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].Type);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].OriginalValue);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.Create, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.EmptyModel), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexListProperties[0].List[0].ComplexProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Create, diff.PrimitiveListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelB.Integers), diff.PrimitiveListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<int>), diff.PrimitiveListProperties[0].Type);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].List.Count);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Delete_Succeeds()
        {
            var originalModelB = new ModelB() {
                ModelAs = new List<ModelA>()
                {
                    new ModelA()
                    {
                        EmptyModel = new EmptyModel()
                    }
                }
            };
            ModelB targetModelB = null;

            var builder = new ObjectDiffBuilder<ModelB>(originalModelB, targetModelB);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ChangeType);
            Assert.AreEqual(typeof(ModelB), diff.Type);

            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties.Count);
            Assert.AreEqual(1, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelB.ModelAs), diff.ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<ModelA>), diff.ComplexListProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(ModelA), diff.ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(3, diff.ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.Integer), diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(int), diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].Type);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].ChangeType);
            Assert.AreEqual(nameof(ModelA.Enum), diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].PropertyName);
            Assert.AreEqual(typeof(ModelAEnum), diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].Type);
            Assert.AreEqual("Unknown", diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].OriginalValue);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].PrimitiveProperties[1].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].ChangeType);
            Assert.AreEqual(nameof(ModelA.String), diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].PropertyName);
            Assert.AreEqual(typeof(string), diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].Type);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].OriginalValue);
            Assert.AreEqual(null, diff.ComplexListProperties[0].List[0].PrimitiveProperties[2].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelA.EmptyModel), diff.ComplexListProperties[0].List[0].ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexListProperties[0].List[0].ComplexProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.PrimitiveListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelB.Integers), diff.PrimitiveListProperties[0].PropertyName);
            Assert.AreEqual(typeof(List<int>), diff.PrimitiveListProperties[0].Type);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].List.Count);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Different_ExcludeFromDiff_Succeeds()
        {
            var originalModelE = new ModelE() { Integer = 123 };
            var targetModelE = new ModelE() { Integer = 124 };

            var builder = new ObjectDiffBuilder<ModelE>(originalModelE, targetModelE);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.None, diff.ChangeType);
            Assert.AreEqual(typeof(ModelE), diff.Type);
            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties.Count);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Build_ObjectDiff_Same_Collections_Succeeds()
        {
            var listItem = new ArrayModel()
            {
                new EmptyModel(),
                new EmptyModel(),
                new EmptyModel()
            };
            listItem.ArrayTitle = "SomeTitle";

            var originalModelE = new ModelG() { Collection = { "A" }, ArrayModel = listItem };
            var targetModelE = new ModelG() { Collection = { "A" }, ArrayModel = listItem };

            var builder = new ObjectDiffBuilder<ModelG>(originalModelE, targetModelE);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.None, diff.ChangeType);
            Assert.AreEqual(typeof(ModelG), diff.Type);
            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties.Count);
            Assert.AreEqual(1, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(1, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelG.Collection), diff.PrimitiveListProperties[0].PropertyName);
            Assert.AreEqual(typeof(ArrayList), diff.PrimitiveListProperties[0].Type);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(1, diff.PrimitiveListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.PrimitiveListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.PrimitiveListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(string), diff.PrimitiveListProperties[0].List[0].Type);
            Assert.AreEqual("A", diff.PrimitiveListProperties[0].List[0].OriginalValue);
            Assert.AreEqual("A", diff.PrimitiveListProperties[0].List[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].ChangeType);
            Assert.AreEqual(nameof(ArrayModel), diff.ComplexListProperties[0].PropertyName);
            Assert.AreEqual(typeof(ArrayModel), diff.ComplexListProperties[0].Type);
            Assert.AreEqual(1, diff.ComplexListProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].ComplexListProperties.Count);
            Assert.AreEqual(3, diff.ComplexListProperties[0].List.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(ArrayModel.ArrayTitle), diff.ComplexListProperties[0].PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(string), diff.ComplexListProperties[0].PrimitiveProperties[0].Type);
            Assert.AreEqual("SomeTitle", diff.ComplexListProperties[0].PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual("SomeTitle", diff.ComplexListProperties[0].PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[0].ChangeType);
            Assert.AreEqual("0", diff.ComplexListProperties[0].List[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexListProperties[0].List[0].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[1].ChangeType);
            Assert.AreEqual("1", diff.ComplexListProperties[0].List[1].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexListProperties[0].List[1].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[1].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[1].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[1].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[1].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexListProperties[0].List[2].ChangeType);
            Assert.AreEqual("2", diff.ComplexListProperties[0].List[2].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexListProperties[0].List[2].Type);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[2].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[2].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[2].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties[0].List[2].ComplexListProperties.Count);
        }

        [TestMethod]
        [TestCategory("UnitTest")]
        [TestCategory("Library")]
        [TestCategory("Common")]
        public void Bluid_ObjectDiff_Different_Interface_Succeeds()
        {
            var originalModelE = new ModelH() { Property = new PropertyA() { Integer = 123, String = "SomeString" } };
            var targetModelE = new ModelH() { Property = new PropertyB() { Class = new EmptyModel() } };

            var builder = new ObjectDiffBuilder<ModelH>(originalModelE, targetModelE);
            ComplexDiff diff = builder.CreateDiff();

            Assert.AreEqual("Root", diff.PropertyName);
            Assert.AreEqual(ChangeTypeEnum.Update, diff.ChangeType);
            Assert.AreEqual(typeof(ModelH), diff.Type);
            Assert.AreEqual(0, diff.PrimitiveProperties.Count);
            Assert.AreEqual(1, diff.ComplexProperties.Count);
            Assert.AreEqual(0, diff.PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Update, diff.ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(ModelH.Property), diff.ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(IProperty), diff.ComplexProperties[0].Type);
            Assert.AreEqual(2, diff.ComplexProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(2, diff.ComplexProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexProperties[0].PrimitiveProperties[0].ChangeType);
            Assert.AreEqual(nameof(PropertyA.String), diff.ComplexProperties[0].PrimitiveProperties[0].PropertyName);
            Assert.AreEqual(typeof(string), diff.ComplexProperties[0].PrimitiveProperties[0].Type);
            Assert.AreEqual("SomeString", diff.ComplexProperties[0].PrimitiveProperties[0].OriginalValue);
            Assert.AreEqual(null, diff.ComplexProperties[0].PrimitiveProperties[0].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.Delete, diff.ComplexProperties[0].PrimitiveProperties[1].ChangeType);
            Assert.AreEqual(nameof(PropertyA.Integer), diff.ComplexProperties[0].PrimitiveProperties[1].PropertyName);
            Assert.AreEqual(typeof(int), diff.ComplexProperties[0].PrimitiveProperties[1].Type);
            Assert.AreEqual("123", diff.ComplexProperties[0].PrimitiveProperties[1].OriginalValue);
            Assert.AreEqual(null, diff.ComplexProperties[0].PrimitiveProperties[1].TargetValue);

            Assert.AreEqual(ChangeTypeEnum.None, diff.ComplexProperties[0].ComplexProperties[0].ChangeType);
            Assert.AreEqual(nameof(PropertyA.SameProperty), diff.ComplexProperties[0].ComplexProperties[0].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexProperties[0].ComplexProperties[0].Type);
            Assert.AreEqual(0, diff.ComplexProperties[0].ComplexProperties[0].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties[0].ComplexProperties[0].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties[0].ComplexProperties[0].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties[0].ComplexProperties[0].ComplexListProperties.Count);

            Assert.AreEqual(ChangeTypeEnum.Create, diff.ComplexProperties[0].ComplexProperties[1].ChangeType);
            Assert.AreEqual(nameof(PropertyB.Class), diff.ComplexProperties[0].ComplexProperties[1].PropertyName);
            Assert.AreEqual(typeof(EmptyModel), diff.ComplexProperties[0].ComplexProperties[1].Type);
            Assert.AreEqual(0, diff.ComplexProperties[0].ComplexProperties[1].PrimitiveProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties[0].ComplexProperties[1].PrimitiveListProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties[0].ComplexProperties[1].ComplexProperties.Count);
            Assert.AreEqual(0, diff.ComplexProperties[0].ComplexProperties[1].ComplexListProperties.Count);
        }
    }
}
