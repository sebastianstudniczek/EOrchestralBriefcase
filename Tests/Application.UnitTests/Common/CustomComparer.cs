using System.Collections;
using System.Reflection;

namespace EOrchestralBriefcase.Application.UnitTests.Common
{
    internal static class CustomComparer<T> where T : class
    {
        internal static bool CheckIfPropertiesAreEqual(T expectedObject, T actualObject)
        {
            PropertyInfo[] properties = expectedObject.GetType().GetProperties();

            foreach (PropertyInfo property in properties)
            {
                var value1 = property.GetValue(expectedObject);
                var value2 = property.GetValue(actualObject);

                if (IsNonStringEnumerable(property) == false)
                {
                    if (Equals(value1, value2) == false)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private static bool IsNonStringEnumerable(PropertyInfo property)
        {
            var propertyType = property.PropertyType;
            if (typeof(string).IsAssignableFrom(propertyType))
            {
                return false;
            }
            bool isNonEnumerable = typeof(IEnumerable).IsAssignableFrom(propertyType);

            return isNonEnumerable;
        }
    }
}
