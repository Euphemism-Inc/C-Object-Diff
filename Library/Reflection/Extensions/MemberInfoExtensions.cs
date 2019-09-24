// (c) Euphemism Inc. All right reserved.

using System.Reflection;

namespace Coconut.Library.Reflection.Extensions
{
    /// <summary>
    /// Extension methods for MemberInfo derivatives.
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// Tries to get the value from a field from an oject.
        /// </summary>
        /// <typeparam name="TOutValue">The type of the out value.</typeparam>
        /// <param name="fieldInfo">The FieldInfo.</param>
        /// <param name="object">The object.</param>
        /// <param name="destValue">The destination value.</param>
        /// <returns>True if successfull, or else false.</returns>
        public static bool TryGetValue<TOutValue>(
            this FieldInfo fieldInfo,
            object @object,
            out TOutValue destValue
        ) {
            destValue = default(TOutValue);
            bool success = false;

            if (fieldInfo != null && @object != null)
            {
                object valueObj = fieldInfo.GetValue(@object);
                if (valueObj is TOutValue castValue)
                {
                    success = true;
                    destValue = castValue;
                }
            }

            return success;
        }

        /// <summary>
        /// Tries to get the value from a property from an oject.
        /// </summary>
        /// <typeparam name="TOutValue">The type of the out value.</typeparam>
        /// <param name="propertyInfo">The PropertyInfo.</param>
        /// <param name="object">The object.</param>
        /// <returns>True if successfull, or else false.</returns>
        /// <returns></returns>
        public static bool TryGetValue<TOutValue>(
            this PropertyInfo propertyInfo,
            object @object,
            out TOutValue destValue
        ) {
            destValue = default(TOutValue);
            bool success = false;

            if (propertyInfo != null && @object != null)
            {
                object valueObj = propertyInfo.GetValue(@object);
                if (valueObj is TOutValue castValue)
                {
                    success = true;
                    destValue = castValue;
                }
            }

            return success;
        }
    }
}
