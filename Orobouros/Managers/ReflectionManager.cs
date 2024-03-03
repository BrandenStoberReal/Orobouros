using System.Reflection;

namespace Orobouros.Managers
{
    /// <summary>
    /// Manages any and all reflection tasks delegated by the framework. This is most used by the
    /// module manager.
    /// </summary>
    public static class ReflectionManager
    {
        /// <summary>
        /// Runs a method obtained by reflection.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="psuedoClass"></param>
        /// <param name="parameters"></param>
        /// <returns>An object representing whatever the method originally returns</returns>
        public static object? InvokeReflectedMethod(MethodInfo method, object psuedoClass, object[] parameters = null)
        {
            return method.Invoke(psuedoClass, parameters);
        }

        /// <summary>
        /// Instantiates a skeleton class based on a reflected type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object? CreateSkeletonClass(Type type)
        {
            return Activator.CreateInstance(type);
        }

        /// <summary>
        /// Determines whether a type has a specified attached attribute.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static bool TypeHasAttribute(Type type, Type attribute)
        {
            TypeInfo tInfo = type.GetTypeInfo();
            object[] attributes = tInfo.GetCustomAttributes(true);
            return attributes.Any(x => x.GetType().Name == attribute.Name);
        }

        /// <summary>
        /// Determines whether a property has a specified attached attribute.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static bool PropertyHasAttribute(PropertyInfo prop, Type attribute)
        {
            object[] attributes = prop.GetCustomAttributes(true);
            return attributes.Any(x => x.GetType().Name == attribute.Name);
        }

        /// <summary>
        /// Determines whether a method has a specified attached attribute.
        /// </summary>
        /// <param name="method"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static bool MethodHasAttribute(MethodInfo method, Type attribute)
        {
            object[] attributes = method.GetCustomAttributes(true);
            return attributes.Any(x => x.GetType().Name == attribute.Name);
        }

        /// <summary>
        /// Fetches an attribute's class from an attached type.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static object FetchAttributeFromType(Type type, Type attribute)
        {
            TypeInfo tInfo = type.GetTypeInfo();
            object[] attributes = tInfo.GetCustomAttributes(true);
            return attributes.FirstOrDefault(x => x.GetType().Name == attribute.Name);
        }

        /// <summary>
        /// Fetches an attribute's class from an attached property.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="attribute"></param>
        /// <returns></returns>
        public static object FetchAttributeFromProperty(PropertyInfo prop, Type attribute)
        {
            object[] attributes = prop.GetCustomAttributes(true);
            return attributes.FirstOrDefault(x => x.GetType().Name == attribute.Name);
        }

        /// <summary>
        /// Fetches the value of a class property.
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="psuedoclass"></param>
        /// <returns></returns>
        public static object? GetValueOfProperty(PropertyInfo prop, object psuedoclass)
        {
            return prop.GetValue(psuedoclass, null);
        }
    }
}