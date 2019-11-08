using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace XC.Library.Utils
{
    public class JsonUtil
    {
        public static string ObjToJson(Object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public static string ObjectToList(List<Object> list) 
        {
            return JsonConvert.SerializeObject(list, Formatting.Indented);
        }

        public static Object JsonToObj(string json, Type type) 
        {
            return JsonConvert.DeserializeObject(json, type);
        }
         

    }
}