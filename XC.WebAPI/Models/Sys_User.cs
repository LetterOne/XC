using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XC.WebAPI.Models
{
    /// <summary>
    /// 用户信息类 
    /// </summary>
    public class Sys_User:BaseFlowModel
    {
        public string Name { get; set; }
        public string PassWord { get; set; }
        public string DepartMent { get; set; }
       
    }
}
