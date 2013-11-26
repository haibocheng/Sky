
using System;

namespace GreedyToolkit.Extension
{
    public static class TypeExtension
    {
        /// <summary>
        /// 通过默认构造函数创建指定类的实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object CreateInstance(this Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// 通过构造函数创建指定类的实例
        /// </summary>
        /// <param name="type"></param>
        /// <param name="constructorArgs">构造函数所需的参数,注意顺序</param>
        /// <returns></returns>
        public static object CreateInstance(this Type type, params object[] constructorArgs)
        {
            return Activator.CreateInstance(type, constructorArgs);
        }

        /// <summary>
        /// 通过构造函数创建指定类的实例,并强制转换成N泛型指定的类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="type">构造函数所需的参数,注意顺序</param>
        /// <param name="constructorArgs"></param>
        /// <returns></returns>
        public static T CreateInstance<T>(this Type type, params object[] constructorArgs)
        {
            var instance = Activator.CreateInstance(type, constructorArgs);
            return (T)instance;
        }

        /// <summary>
        /// 判断该类型是否为 Nullable 类型
        /// </summary>
        public static bool IsNullableType(this Type type)
        {
            return type != null && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// 获取Nullable类型中的泛型类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static Type GetNonNullableType(this Type type)
        {
            return type.IsNullableType() ? type.GetGenericArguments()[0] : type;
        }
    }
}
