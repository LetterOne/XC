using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
namespace XC.Library.NoSQL
{
    class RedisHelper
    {
        private IDistributedCache _cache;
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
          string ReturnStr = "";

            if (!string.IsNullOrEmpty(key))
            {
                if (Exists(key))
                {
                    ReturnStr = Encoding.UTF8.GetString(_cache.Get(key));
                }

            }
            return ReturnStr;

        }

        /// <summary>
        /// 使用异步获取缓存信息
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>

        public async Task<object> GetAsync(string key)

        {

            string ReturnString = null;

            var value = await _cache.GetAsync(key);

            if (value != null)

            {

                ReturnString = Encoding.UTF8.GetString(value);

            }

            return ReturnString;

        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>

        public bool Add(string key, object value)

        {

            byte[] val = null;

            if (value.ToString() != "")

            {

                val = Encoding.UTF8.GetBytes(value.ToString());

            }

            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();

            //设置绝对过期时间 两种写法

            options.AbsoluteExpiration = DateTime.Now.AddMinutes(30);

            // options.SetAbsoluteExpiration(DateTime.Now.AddMinutes(30));

            //设置滑动过期时间 两种写法

            options.SlidingExpiration = TimeSpan.FromSeconds(30);

            //options.SetSlidingExpiration(TimeSpan.FromSeconds(30));

            //添加缓存

            _cache.Set(key, val, options);

            //刷新缓存

            _cache.Refresh(key);

            return Exists(key);

        }


        /// <summary>
        /// 修改缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Modify(string key, object value)

        {

            bool ReturnBool = false;

            if (key != "" || key != null)

            {

                if (Remove(key))

                {

                    ReturnBool = Add(key, value.ToString());

                }



            }

            return ReturnBool;

        }


        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Remove(string key)

        {
            bool ReturnBool = false;
            if (key != "" || key != null)
            {
                _cache.Remove(key);
                if (Exists(key) == false)
                {
                    ReturnBool = true;
                }

            }
            return ReturnBool;
        }

        /// <summary>
        /// 验证是否存在
    
        /// </summary>

        /// <param name="key"></param>

        /// <returns></returns>

        public bool Exists(string key)

        {

            bool ReturnBool = true;

            byte[] val = _cache.Get(key);

            if (val == null || val.Length == 0)

            {

                ReturnBool = false;

            }

            return ReturnBool;

        }


    }
}
