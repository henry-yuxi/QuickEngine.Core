namespace QuickEngine.Extensions
{
    using System;
    using System.Linq;
    using System.Reflection;

    public static class SystemReflectionExtensions
    {
        internal static bool HasAttribute<T>(this MemberInfo m) where T : Attribute
        {
#if UNITY_WSA && !UNITY_EDITOR
            return  m.CustomAttributes.Any(o => o.AttributeType.Equals(typeof (T)));
#else
            return Attribute.IsDefined(m, typeof(T));
#endif
        }

        internal static bool HasAttribute<T>(this Type m) where T : Attribute
        {
#if UNITY_WSA && !UNITY_EDITOR
            return m.GetTypeInfo().CustomAttributes.Any(o => o.AttributeType == typeof(T));
#else
            return Attribute.IsDefined(m, typeof(T));
#endif
        }

#if UNITY_WSA && !UNITY_EDITOR

        internal static bool HasAttribute<T>(this TypeInfo m) where T : Attribute
        {
            return m.CustomAttributes.Any(o => o.AttributeType == typeof(T));
        }

#endif

        internal static T GetAttribute<T>(this MemberInfo m) where T : Attribute
        {
            return m.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
        }

        internal static T GetAttribute<T>(this Type m) where T : Attribute
        {
#if UNITY_WSA && !UNITY_EDITOR
            return m.GetTypeInfo().GetCustomAttribute<T>();
#else
            return (T)Attribute.GetCustomAttribute(m, typeof(T));
#endif
        }

        internal static Type GetMemberType(this MemberInfo member)
        {
            if (member is MethodInfo)
                return ((MethodInfo)member).ReturnType;

            if (member is PropertyInfo)
                return ((PropertyInfo)member).PropertyType;

            return ((FieldInfo)member).FieldType;
        }

        internal static void SetMemberValue(this MemberInfo member, object instance, object value)
        {
            if (member is MethodInfo)
            {
                var method = ((MethodInfo)member);

                if (method.GetParameters().Any())
                {
                    method.Invoke(instance, new[] { value });
                }
                else
                {
                    method.Invoke(instance, null);
                }
            }
            else if (member is PropertyInfo)
            {
                ((PropertyInfo)member).SetValue(instance, value, null);
            }
            else
            {
                ((FieldInfo)member).SetValue(instance, value);
            }
        }
    }
}