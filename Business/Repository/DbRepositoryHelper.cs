using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Business.Repository
{
    public static class DbRepositoryHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntry"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsNewEntry<TEntry>(TEntry entity) where TEntry : class
        {
            var identityProp = GetIdentityProperty(entity);
            if (identityProp == null) throw new NullReferenceException();

            var propType = identityProp.PropertyType;
            var identityPropValue = Convert.ChangeType(identityProp.GetValue(entity), propType);
            var defaultValueOfType = Activator.CreateInstance(propType);

            return identityPropValue.Equals(defaultValueOfType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntry"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static object GetEntityId<TEntry>(TEntry entity) where TEntry : class
        {
            var identityProp = GetIdentityProperty(entity);
            if (identityProp == null) throw new NullReferenceException();

            return identityProp.GetValue(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntry"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        private static PropertyInfo GetIdentityProperty<TEntry>(TEntry item) where TEntry : class
        {
            var type = item.GetType();
            return type.GetProperties().FirstOrDefault(prop => prop.Name == "Id" || prop.Name == type.Name + "Id");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntry"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<PropertyInfo> GetNonSystemTypesProperties<TEntry>(TEntry type)
        {
            return type.GetType().GetProperties().Where(t => t.PropertyType.Namespace != "System");
        }
    }
}