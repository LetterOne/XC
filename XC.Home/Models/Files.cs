using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace XC.Home.Models
{
    public class Files
    {
        public Files()
        {

        }
        [DisplayName(displayName: "文件名称")]
        public string ProductSerial { get; set; }

        [DisplayName(displayName: "文件地址")]
        public string ProductNo { get; set; }

    }
}
