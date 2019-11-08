using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Reflection;
using System.Data.Common;

namespace XC.Library.Utils
{
    public class ConvertHelper<T> where T : new()
    {

        /// <summary>  
        /// 利用反射和泛型  
        /// </summary>  
        /// <param name="dt"></param>  
        /// <returns></returns>  
        public static List<T> ConvertToList(DataTable dt)
        {

            // 定义集合  
            List<T> ts = new List<T>();

            // 获得此模型的类型  
            Type type = typeof(T);
            //定义一个临时变量  
            string tempName = string.Empty;
            //遍历DataTable中所有的数据行  
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性  
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性  
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;//将属性名称赋值给临时变量  
                    //检查DataTable是否包含此列（列名==对象的属性名）    
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter  
                        if (!pi.CanWrite) continue;//该属性不可写，直接跳出  
                        //取值  
                        object value = dr[tempName];
                        //如果非空，则赋给对象的属性  
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                //对象添加到泛型集合中  
                ts.Add(t);
            }

            return ts;
        }


        /// <summary>   
        /// DataReader转换为obj list   
        /// </summary>   
        /// <typeparam name="T">泛型</typeparam>   
        /// <param name="rdr">datareader</param>   
        /// <returns>返回泛型类型</returns>   
        public static List<T> ConvertToEntityList<T>(DbDataReader rdr)
        {
            List<T> list = new List<T>();

            while (rdr.Read())
            {
                T t = System.Activator.CreateInstance<T>();
                Type obj = t.GetType();
                // 循环字段   
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    object tempValue = null;
                    PropertyInfo currentProperty = obj.GetProperty(rdr.GetName(i));

                    if (currentProperty != null)
                    {
                        if (rdr.IsDBNull(i))
                        {
                            string typeFullName = currentProperty.PropertyType.FullName;
                            tempValue = GetDBNullValue(typeFullName);
                        }
                        else
                        {
                            tempValue = rdr.GetValue(i);
                        }

                        currentProperty.SetValue(t, tempValue, null);

                    }
                }

                list.Add(t);

            }

            if (!rdr.IsClosed)
                rdr.Close();

            return list;
        }


        /// <summary>   
        /// DataReader转换为obj   
        /// </summary>   
        /// <typeparam name="T">泛型</typeparam>   
        /// <param name="rdr">datareader</param>   
        /// <returns>返回泛型类型</returns>   
        public static object ConvertToEntity<T>(DbDataReader rdr)
        {
            try
            {

                if (rdr.Read())
                {
                    T t = System.Activator.CreateInstance<T>();
                    Type obj = t.GetType();

                    // 循环字段   
                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        object tempValue = null;
                        PropertyInfo currentProperty = obj.GetProperty(rdr.GetName(i));

                        if (currentProperty != null)
                        {
                            if (rdr.IsDBNull(i))
                            {
                                string typeFullName = currentProperty.PropertyType.FullName;
                                tempValue = GetDBNullValue(typeFullName);
                            }
                            else
                            {
                                tempValue = rdr.GetValue(i);
                            }
                            currentProperty.SetValue(t, tempValue, null);
                        }
                    }
                    return t;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {

                if (!rdr.IsClosed)
                    rdr.Close();
            }

        }

        /// <summary>   
        /// 返回值为DBnull的默认值   
        /// </summary>   
        /// <param name="typeFullName">数据类型的全称，类如：system.int32</param>   
        /// <returns>返回的默认值</returns>   
        private static object GetDBNullValue(string typeFullName)
        {
            object ret = null;
            switch (typeFullName)
            {
                case "System.String":
                    ret = String.Empty;
                    break;
                case "System.Int32":
                    ret = 0;
                    break;
                case "System.DateTime":
                    ret = DateTime.MinValue;
                    break;
                case "System.Boolean":
                    ret = false;
                    break;
                case "System.Decimal":
                    ret = Convert.ToDecimal(0);
                    break;
                case "System.Double":
                    ret = Convert.ToDouble(0);
                    break;
                default:
                    ret = null;
                    break;
            }
            return ret;
        }

    }
}