using System;

namespace QuickServiceAdmin.Core.Helpers
{
    public static class IsType
    {
        public static bool Simple(Type type)
        {
            while (true)
            {
                if (!type.IsGenericType || type.GetGenericTypeDefinition() != typeof(Nullable<>))
                    return type.IsPrimitive || type.IsEnum || type == typeof(string) ||
                           type == typeof(decimal);
                // nullable type, check if the nested type is simple.
                type = type.GetGenericArguments()[0];
            }
        }
    }
}