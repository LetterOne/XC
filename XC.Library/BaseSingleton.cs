using System;
using System.Collections.Generic;
using System.Text;

namespace XC.Library
{
    /// <summary>
    /// 双检锁单例模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
  public static class BaseSingleton<T> where T:class,new()
    {
        public static T _Instance;
        private static object _lock0bj = new object();

        public static T GetInstance()
        {
            if (_Instance != null)
                return _Instance;
            lock (_lock0bj)
            { 
            if (_Instance == null)
            {
                    var TempObj = Activator.CreateInstance<T>();
                    System.Threading.Interlocked.Exchange(ref _Instance, TempObj);
            }
        }
            return _Instance;
        }
    }
}


