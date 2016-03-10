using System;
using System.Collections.Generic;

namespace Versionado.Infrastructure
{
    public static class TypeExtensions
    {
        public static Type GetEnumerableType(this Type type)
        {
            if (IsIEnumerable(type))
                return type.GetGenericArguments()[0];
            else
            {
                foreach (var i in type.GetInterfaces())
                {
                    if (IsIEnumerable(i))
                        return i.GetGenericArguments()[0];
                }
            }

            return null;
        }

        private static Boolean IsIEnumerable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }
    }
}