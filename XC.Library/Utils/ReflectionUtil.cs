using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XC.Library.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public class ReflectionUtil
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="memberName"></param>
        /// <returns></returns>
        public static object GetValue(object instance, string memberName)
        {
            var propertyInfo = instance.GetType().GetProperty(memberName);
            if (propertyInfo != null)
            {
                return propertyInfo.GetValue(instance, null);
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="memberName"></param>
        /// <param name="newValue"></param>
        public static void SetValue(object instance, string memberName, object newValue)
        {
            var propertyInfo = instance.GetType().GetProperty(memberName);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(instance, newValue, null);
            }
        }
    }
}