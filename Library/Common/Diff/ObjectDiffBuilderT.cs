// (c) Euphemism Inc. All right reserved.

using Coconut.Library.Common.Diff.Attributes;
using Coconut.Library.Common.Diff.Models;
using Coconut.Library.Reflection.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace Coconut.Library.Common.Diff
{
    /// <summary>
    /// This class creates a diff between properties of objects, recursively. It cannot handle circular references.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ObjectDiffBuilder<T>
        where T : class
    {
        private static readonly IList<Type> _alternativePrimitiveTypes = new List<Type>();
        private static readonly IDictionary<Type, PropertyInfo[]> _excludePropertiesFromType;


        /// <summary>
        /// Gets the original object.
        /// </summary>
        /// <value>
        /// The original object.
        /// </value>
        internal T OriginalObject { get; }

        /// <summary>
        /// Gets the target/changed object.
        /// </summary>
        /// <value>
        /// The target/changed object.
        /// </value>
        internal T TargetObject { get; }

        /// <summary>
        /// Gets or sets the alternative primitive types. That is, types for whom a simple <see cref="Object.ToString"/> is enough.
        /// </summary>
        /// <value>
        /// The alternative primitive types.
        /// </value>
        public static IList<Type> AlternativePrimitiveTypes
        {
            get => _alternativePrimitiveTypes;
            set {
                _alternativePrimitiveTypes.Clear();
                if (value != null)
                {
                    foreach (Type val in value)
                    {
                        _alternativePrimitiveTypes.Add(val);
                    }
                }
            }
        }

        /// <summary>
        /// Static constructor.
        /// </summary>
        static ObjectDiffBuilder()
        {
            _excludePropertiesFromType = new Dictionary<Type, PropertyInfo[]>() {
                { typeof(ArrayList), typeof(ArrayList).GetProperties() },
                { typeof(ICollection), typeof(ICollection).GetProperties() },
                { typeof(ICollection<>), typeof(ICollection<>).GetProperties() },
                { typeof(Collection<>), typeof(Collection<>).GetProperties() },
                { typeof(IDictionary), typeof(IDictionary).GetProperties() },
                { typeof(IDictionary<,>), typeof(IDictionary<,>).GetProperties() },
                { typeof(Dictionary<,>), typeof(Dictionary<,>).GetProperties() },
                { typeof(IEnumerable), typeof(IEnumerable).GetProperties() },
                { typeof(IEnumerable<>), typeof(IEnumerable<>).GetProperties() },
                { typeof(IList), typeof(IList).GetProperties() },
                { typeof(IList<>), typeof(IList<>).GetProperties() },
                { typeof(List<>), typeof(List<>).GetProperties() },
                { typeof(IReadOnlyList<>), typeof(IReadOnlyList<>).GetProperties() },
                { typeof(IReadOnlyCollection<>), typeof(IReadOnlyCollection<>).GetProperties() },
                { typeof(ReadOnlyCollection<>), typeof(ReadOnlyCollection<>).GetProperties() },
                { typeof(SortedList), typeof(SortedList).GetProperties() },
                { typeof(SortedList<,>), typeof(SortedList<,>).GetProperties() },
                { typeof(CollectionBase), typeof(CollectionBase).GetProperties() },
            };
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectDiffBuilder{T}"/> class.
        /// </summary>
        /// <param name="originalObject">The left object.</param>
        /// <param name="changedObject">The right object.</param>
        public ObjectDiffBuilder(T originalObject, T changedObject)
        {
            if ((originalObject == null) && (changedObject == null))
                throw new ArgumentNullException(nameof(changedObject));

            OriginalObject = originalObject;
            TargetObject = changedObject;
        }


        /// <summary>
        /// Creates a comparison between the original and changed object.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public ComplexDiff CreateDiff()
        {
            return CreateComplexDiff("Root", OriginalObject, TargetObject, typeof(T));
        }

        /// <summary>
        /// Creates a comparison between the original and target/changed object.
        /// </summary>
        /// <param name="srcComplexDiff">The complex diff containing this property.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="originalObject">The original object.</param>
        /// <param name="targetObject">The target/changed object.</param>
        /// <param name="refType"></param>
        /// <returns></returns>
        private static void CreateDiff_Internal(ComplexDiff srcComplexDiff, string propertyName, object originalObject, object targetObject, Type refType)
        {
            if (IsIEnumerable(refType))
            {
                CreateListDiff_Internal(srcComplexDiff, propertyName, originalObject, targetObject, refType);
            }
            else if (IsPrimitive(refType))
            {
                PrimitiveDiff newPrimitiveDiff = CreatePrimitiveDiff(propertyName, originalObject, targetObject, refType);
                srcComplexDiff.PrimitiveProperties.Add(newPrimitiveDiff);
                UpdateChangeType(srcComplexDiff, newPrimitiveDiff.ChangeType);
            }
            else
            {
                ComplexDiff newComplexDiff = CreateComplexDiff(propertyName, originalObject, targetObject, refType);
                srcComplexDiff.ComplexProperties.Add(newComplexDiff);
                UpdateChangeType(srcComplexDiff, newComplexDiff.ChangeType);
            }
        }

        private static void CreateListDiff_Internal(ComplexDiff srcComplexDiff, string propertyName, object originalObject, object targetObject, Type refType)
        {
            var originalIEnumerable = (IEnumerable)originalObject;
            var targetIEnumerable = (IEnumerable)targetObject;

            List<DiffKvp> diffList = CreateDiffList(originalIEnumerable, targetIEnumerable);

            DiffKvp kvp = diffList.FirstOrDefault();

            //
            // This [if] statement can be simplified. The contents of both the [if] and [else] are the same, except for a type and an action, the listDiff and the property to add the list to.
            //

            // refType.IsConstructedGenericType is for .NET 1.0 non-generic IEnumerables. Contains objects (the base class of every class) only. Objects don't have properties.
            if (refType.IsConstructedGenericType ? IsPrimitiveList(refType) : IsPrimitiveList(originalObject, targetObject))
            {
                var listDiff = new ListDiff<PrimitiveDiff>() {
                    PropertyName = propertyName,
                    Type = refType,
                    ChangeType = srcComplexDiff.ChangeType == ChangeTypeEnum.Create || srcComplexDiff.ChangeType == ChangeTypeEnum.Delete ? srcComplexDiff.ChangeType : ChangeTypeEnum.None
                };

                void addAction(ListDiff<PrimitiveDiff> srcListDiff, string listPropertyName, object originalListObject, object targetListObject, Type subRefType)
                {
                    PrimitiveDiff newPrimitiveDiff = CreatePrimitiveDiff(listPropertyName, originalListObject, targetListObject, subRefType);
                    srcListDiff.List.Add(newPrimitiveDiff);
                    UpdateChangeType(srcListDiff, newPrimitiveDiff.ChangeType);
                }

                int index = 0;
                foreach (DiffKvp objectKvp in diffList)
                {
                    object currentOriginalObject = objectKvp.OriginalObject;
                    object currentTargetObject = objectKvp.TargetObject;
                    CreateListDiff_Internal(listDiff, $"{index++}", currentOriginalObject, currentTargetObject, addAction);
                }

                FillComplexDiff(listDiff, originalObject, targetObject, refType);

                UpdateChangeType(srcComplexDiff, listDiff.ChangeType);
                srcComplexDiff.PrimitiveListProperties.Add(listDiff);
            }
            else
            {
                var listDiff = new ListDiff<ComplexDiff>() {
                    PropertyName = propertyName,
                    Type = refType,
                    ChangeType = srcComplexDiff.ChangeType == ChangeTypeEnum.Create || srcComplexDiff.ChangeType == ChangeTypeEnum.Delete ? srcComplexDiff.ChangeType : ChangeTypeEnum.None
                };

                void addAction(ListDiff<ComplexDiff> srcListDiff, string listPropertyName, object originalListObject, object targetListObject, Type subRefType)
                {
                    ComplexDiff newComplexDiff = CreateComplexDiff(listPropertyName, originalListObject, targetListObject, subRefType);
                    srcListDiff.List.Add(newComplexDiff);
                    UpdateChangeType(srcListDiff, newComplexDiff.ChangeType);
                }

                int index = 0;
                foreach (DiffKvp objectKvp in diffList)
                {
                    object currentOriginalObject = objectKvp.OriginalObject;
                    object currentTargetObject = objectKvp.TargetObject;
                    CreateListDiff_Internal(listDiff, $"{index++}", currentOriginalObject, currentTargetObject, addAction);
                }

                FillComplexDiff(listDiff, originalObject, targetObject, refType);

                UpdateChangeType(srcComplexDiff, listDiff.ChangeType);
                srcComplexDiff.ComplexListProperties.Add(listDiff);
            }
        }

        private static void CreateListDiff_Internal<TDiff>(
            ListDiff<TDiff> srcComplexDiff,
            string propertyName,
            object originalObject,
            object targetObject,
            Action<ListDiff<TDiff>, string, object, object, Type> addAction
        )
            where TDiff : ObjectDiff
        {
            Type refType = GetObjectType(originalObject, targetObject);

            if (IsIEnumerable(refType))
            {
                CreateListDiff_Internal(srcComplexDiff, propertyName, originalObject, targetObject, refType);
            }
            else
            {
                addAction(srcComplexDiff, propertyName, originalObject, targetObject, refType);
            }
        }


        /// <summary>
        /// Creates the primitive difference.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="originalObject">The original object.</param>
        /// <param name="targetObject">The target object.</param>
        /// <param name="refType"></param>
        /// <returns></returns>
        /// <remarks>
        /// A primitive type is <see cref="Int32"/>, <see cref="Double"/>, <see cref="Single"/>, etc.
        /// </remarks>
        private static PrimitiveDiff CreatePrimitiveDiff(string propertyName, object originalObject, object targetObject, Type refType)
        {
            return new PrimitiveDiff() {
                PropertyName = propertyName,
                Type = refType,
                OriginalValue = originalObject?.ToString(),
                TargetValue = targetObject?.ToString(),
                ChangeType = GetChangeType(originalObject, targetObject)
            };
        }

        /// <summary>
        /// Creates a complex difference object that compares two non-primitive types.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="originalObject">The original object.</param>
        /// <param name="targetObject">The target object.</param>
        /// <param name="refType"></param>
        /// <returns></returns>
        /// <remarks>
        /// <see cref="CreatePrimitiveDiff"/>. Complex types are non-primitive types.
        /// Examples are instances of user defined classes and structs. Thus <see cref="DateTime"/>, <see cref="List{T}"/> and <see cref="ObjectDiffBuilder{T}"/>, but not <see cref="Int32"/>.
        /// </remarks>
        private static ComplexDiff CreateComplexDiff(string propertyName, object originalObject, object targetObject, Type refType)
        {
            var dstComplexDiff = new ComplexDiff() {
                PropertyName = propertyName,
                Type = refType,
                ChangeType = GetChangeType(originalObject, targetObject)
            };

            FillComplexDiff(dstComplexDiff, originalObject, targetObject, refType);

            return dstComplexDiff;
        }

        /// <summary>
        /// Fills the complex diff
        /// </summary>
        /// <param name="dstComplexDiff">The destination complex diff.</param>
        /// <param name="originalObject">The original object.</param>
        /// <param name="targetObject">The target object.</param>
        /// <param name="refType">Type of the reference.</param>
        private static void FillComplexDiff(ComplexDiff dstComplexDiff, object originalObject, object targetObject, Type refType)
        {
            foreach (PropertyInfoTuple currentProperty in GetPropertyInfos(refType, originalObject, targetObject))
            {
                string currentPropertyName = (currentProperty.Left ?? currentProperty.Right).Name;
                Type currentPropertyType = (currentProperty.Left ?? currentProperty.Right).PropertyType;

                currentProperty.Left.TryGetValue(originalObject, out object currentOriginalPropertyObject);
                currentProperty.Right.TryGetValue(targetObject, out object currentTargetPropertyObject);

                if (!(originalObject == null && targetObject == null && currentOriginalPropertyObject == null && currentTargetPropertyObject == null))
                {
                    CreateDiff_Internal(dstComplexDiff, currentPropertyName, currentOriginalPropertyObject, currentTargetPropertyObject, currentPropertyType);
                }
            }
        }

        // - - - - Static Helpers

        private static IEnumerable<PropertyInfoTuple> GetPropertyInfos(Type refType, object originalObject, object targetObject)
        {
//#error TODO: make caching.

            Type originalType = originalObject?.GetType();
            Type targetType = targetObject?.GetType();

            var dstProperties = new List<PropertyInfoTuple>();
            var tmpProperties = new List<PropertyInfoTuple>();

            foreach (PropertyInfo property in refType.GetProperties())
            {
                if (!MustIgnore(property))
                {
                    dstProperties.Add(new PropertyInfoTuple(property, property));
                }
            }

            if (originalType != null)
            {
                foreach (PropertyInfo property in originalType.GetProperties())
                {
                    bool notInRefType = !dstProperties.Any(x => String.Equals(x.Left.Name, property.Name, StringComparison.InvariantCulture));
                    if (notInRefType && !MustIgnore(property))
                    {
                        tmpProperties.Add(new PropertyInfoTuple(property, null));
                    }
                }
            }

            if (targetType != null)
            {
                foreach (PropertyInfo property in targetType.GetProperties())
                {
                    bool notInRefType = !dstProperties.Any(x => String.Equals(x.Left.Name, property.Name, StringComparison.InvariantCulture));
                    PropertyInfoTuple tmpProperty = tmpProperties.SingleOrDefault(x => x.Left != null && String.Equals(x.Left.Name, property.Name, StringComparison.InvariantCulture));

                    if (notInRefType && !MustIgnore(property))
                    {
                        if (tmpProperty != null)
                        {
                            tmpProperty.Right = property;
                        }
                        else
                        {
                            tmpProperties.Add(new PropertyInfoTuple(null, property));
                        }
                    }
                }
            }

            dstProperties.AddRange(tmpProperties);

            return dstProperties;
        }

        /// <summary>
        /// Determine the type of the change.
        /// </summary>
        /// <param name="originalObject">The original object.</param>
        /// <param name="targetObject">The target object.</param>
        /// <returns></returns>
        private static ChangeTypeEnum GetChangeType(object originalObject, object targetObject)
        {
            bool originalIsNull = originalObject == null;
            bool targetIsNull = targetObject == null;

            if (originalIsNull && targetIsNull)
            {
                return ChangeTypeEnum.None;
            }
            else if (originalIsNull)
            {
                return ChangeTypeEnum.Create;
            }
            else if (targetIsNull)
            {
                return ChangeTypeEnum.Delete;
            }
            else
            {
                string strOriginalObject = originalObject.ToString();
                string strOTargetObject = targetObject.ToString();
                return String.Equals(strOriginalObject, strOTargetObject) ? ChangeTypeEnum.None : ChangeTypeEnum.Update;
            }
        }

        /// <summary>
        /// Updates the type of the change on the <see cref="ComplexDiff"/>, if necessary.
        /// </summary>
        /// <param name="srcComplexDiff">The source complex difference.</param>
        /// <param name="newChangeType">New type of the change.</param>
        private static void UpdateChangeType(ComplexDiff srcComplexDiff, ChangeTypeEnum newChangeType)
        {
            ChangeTypeEnum currentChangeType = srcComplexDiff.ChangeType;

            if (currentChangeType == ChangeTypeEnum.Unknown)
                throw new InvalidOperationException($"{nameof(currentChangeType)} must have a changetype already");
            if (srcComplexDiff.ChangeType == ChangeTypeEnum.Unknown)
                throw new InvalidOperationException($"{nameof(srcComplexDiff.ChangeType)} must have a changetype already");

            if (currentChangeType == ChangeTypeEnum.None && newChangeType != ChangeTypeEnum.None)
            {
                srcComplexDiff.ChangeType = ChangeTypeEnum.Update;
            }
        }

        /// <summary>
        /// Determines whether the specified type has a generic type that is primitive (For lists).
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsPrimitiveList(Type type)
        {
            Type[] genericTypeArguments = type.GetGenericArguments();

            return IsIEnumerable(type)
                && genericTypeArguments.Length == 1
                && genericTypeArguments.All(IsPrimitive);
        }

        /// <summary>
        /// Determines whether the specified type has a generic type that is primitive (For lists).
        /// </summary>
        /// <param name="originalObject">The original object.</param>
        /// <param name="targetObject">The target object.</param>
        private static bool IsPrimitiveList(object originalObject, object targetObject)
        {
            object originalType = originalObject ?? targetObject;
            bool isPrimitive = false;

            if (originalType is IEnumerable enumerable)
            {
                foreach (object item in enumerable)
                {
                    Type firstItemType = item.GetType();
                    isPrimitive = IsPrimitive(firstItemType);

                    break;
                }
            }

            return isPrimitive;
        }

        /// <summary>
        /// Determines whether the specified type is primitive.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///   <c>true</c> if the specified type is primitive; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsPrimitive(Type type)
        {
            return type.IsPrimitive
                || type.IsEnum
                || type == typeof(string)
                || type == typeof(DateTime)
                || AlternativePrimitiveTypes.Any(x => x == type);
        }

        /// <summary>
        /// Determines whether the specified type is an <see cref="IEnumerable"/>.
        /// </summary>
        /// <param name="refType">The <see cref="Type"/>.</param>
        /// <returns>
        ///   <c>true</c> if the the specified <see cref="Type"/> is an <see cref="IEnumerable"/> otherwise, <c>false</c>.
        /// </returns>
        private static bool IsIEnumerable(Type refType)
        {
            return refType.GetInterfaces().Contains(typeof(IEnumerable))
                && !refType.IsAssignableFrom(typeof(string));
        }

        /// <summary>
        /// Check wheter of not to ignore this property.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private static bool MustIgnore(PropertyInfo property)
        {
            return property.GetCustomAttributes(typeof(ExcludeFromDiffAttribute), false).Length > 0 || MustExclude(property);
        }

        private static bool MustExclude(PropertyInfo property)
        {
            Type type = property.DeclaringType;
            Type[] interfaces = type.GetInterfaces();

            IEnumerable<PropertyInfo> excludeProperties = _excludePropertiesFromType
                .Where(x => IsBaseTypeSame(x.Key, type, interfaces)
                         || IsInstanceOfGenericType(x.Key, type, interfaces))
                .SelectMany(x => x.Value);

            return excludeProperties.Any(x => String.Equals(x.Name, property.Name, StringComparison.InvariantCulture));
        }

        /// <summary>
        /// Gets the type of the object.
        /// </summary>
        /// <param name="originalObject">The original object.</param>
        /// <param name="targetObject">The target object.</param>
        /// <returns></returns>
        private static Type GetObjectType(object originalObject, object targetObject)
        {
            Type originalType = originalObject?.GetType();
            Type targetType = targetObject?.GetType();

            return originalType ?? targetType;
        }

        /// <summary>
        /// Determines whether type is instance of generic type base type.
        /// </summary>
        /// <param name="baseType">Type of the base.</param>
        /// <param name="type">The type.</param>
        /// <param name="typeInterfaces">The type interfaces.</param>
        /// <returns>
        ///   <c>true</c> if the specified base type is derrived or instanciated from basetype; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsInstanceOfGenericType(Type baseType, Type type, Type[] typeInterfaces)
        {
            bool isInstanceOfGenericType = false;
            if (type.IsConstructedGenericType && type.GenericTypeArguments.Length == baseType.GetGenericArguments().Length)
            {
                Type constructedType = baseType.MakeGenericType(type.GenericTypeArguments);
                isInstanceOfGenericType = IsSame(constructedType, type, typeInterfaces);
            }
            bool baseTypeIsInstanceOfGenericType = isInstanceOfGenericType || type.BaseType != null && type.BaseType != typeof(object) && IsInstanceOfGenericType(baseType, type.BaseType, typeInterfaces);

            return isInstanceOfGenericType || baseTypeIsInstanceOfGenericType;
        }

        /// <summary>
        /// Determines whether the specified type is derrived or instanciated from basetype.
        /// </summary>
        /// <param name="baseType">Type of the base.</param>
        /// <param name="type">The type.</param>
        /// <param name="typeInterfaces">The type interfaces.</param>
        /// <returns>
        ///   <c>true</c> if the specified base type is derrived or instanciated from basetype; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsSame(Type baseType, Type type, Type[] typeInterfaces)
        {
            return type == baseType
                || type.IsInstanceOfType(baseType)
                || typeInterfaces.Any(x => x == baseType);
        }

        /// <summary>
        /// Determines whether [is base type same] [the specified base type].
        /// </summary>
        /// <param name="baseType">Type of the base.</param>
        /// <param name="type">The type.</param>
        /// <param name="typeInterfaces">The type interfaces.</param>
        /// <returns>
        ///   <c>true</c> if [is base type same] [the specified base type]; otherwise, <c>false</c>.
        /// </returns>
        private static bool IsBaseTypeSame(Type baseType, Type type, Type[] typeInterfaces)
        {
            bool isSame = false;
            Type currentType = type;
            while (currentType != null && currentType != typeof(object) && !isSame)
            {
                isSame = IsSame(baseType, currentType, typeInterfaces);
                currentType = currentType.BaseType;
            }
            return isSame;
        }

        /// <summary>
        /// Creates the list of differences from two <see cref="IEnumerable"/>'s.
        /// </summary>
        /// <param name="originalIEnumerable">The original i enumerable.</param>
        /// <param name="targetIEnumerable">The target i enumerable.</param>
        /// <returns></returns>\
        /// <remarks>Basically this is an <see cref="IDictionary"/> that can contain nulls as keys.</remarks>
        private static List<DiffKvp> CreateDiffList(IEnumerable originalIEnumerable, IEnumerable targetIEnumerable)
        {
            var dstDiffList = new List<DiffKvp>();

            IEnumerator origEnumerator = originalIEnumerable?.GetEnumerator();
            IEnumerator targEnumerator = targetIEnumerable?.GetEnumerator();

            bool oHasNext = origEnumerator?.MoveNext() ?? false;
            bool tHasNext = targEnumerator?.MoveNext() ?? false;
            while (oHasNext || tHasNext)
            {
                var kvp = new DiffKvp() {
                    OriginalObject = oHasNext ? origEnumerator.Current : null,
                    TargetObject = tHasNext ? targEnumerator.Current : null
                };

                dstDiffList.Add(kvp);

                oHasNext = oHasNext && origEnumerator.MoveNext();
                tHasNext = tHasNext && targEnumerator.MoveNext();
            }

            return dstDiffList;
        }

        /// <summary>
        /// The key-value pair for the  difference list.
        /// </summary>
        private class DiffKvp
        {
            internal object OriginalObject { get; set; }

            internal object TargetObject { get; set; }
        }
        private class PropertyInfoTuple
        {
            public PropertyInfo Left { get; set; }
            public PropertyInfo Right { get; set; }

            public PropertyInfoTuple(PropertyInfo left, PropertyInfo right)
            {
                // Deze 'tuple' kan een null waarde hebben, of twee
                Left = left;
                Right = right;
            }
        }
    }
}